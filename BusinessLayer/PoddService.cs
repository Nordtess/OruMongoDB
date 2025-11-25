using MongoDB.Driver;
using OruMongoDB.BusinessLayer.Exceptions;
using OruMongoDB.BusinessLayer.Rss;
using OruMongoDB.Domain;
using OruMongoDB.Infrastructure;
using OruMongoDB.Core.Validation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OruMongoDB.BusinessLayer
{
    // NOTE: Kontrollera att samtliga collection-namn är konsekventa ("Poddfloden" vs "Poddflöden"). Fel namn gör att feeds inte hittas.
    /// <summary>
    /// High-level service for working with podcast feeds:
    /// - Fetch feed + episodes from an RSS URL
    /// - Save a subscription (feed + episodes) to MongoDB using a transaction
    /// - Assign / remove a category on a feed
    /// </summary>
    public interface IPoddService
    {
        /// <summary>
        /// Fetches and parses a podcast feed from the given RSS URL.
        /// Does NOT save anything to the database.
        /// </summary>
        Task<(Poddflöden poddflode, List<PoddAvsnitt> avsnitt)> FetchPoddFeedAsync(string rssUrl);

        /// <summary>
        /// Fetches a podcast feed and its episodes from the database by RSS URL.
        /// </summary>
        Task<(Poddflöden poddflode, List<PoddAvsnitt> avsnitt)> FetchFromDatabaseAsync(string rssUrl);

        /// <summary>
        /// Saves the given feed and its episodes as a subscription in MongoDB
        /// using a single ACID transaction.
        /// </summary>
        Task SavePoddSubscriptionAsync(Poddflöden poddflode, List<PoddAvsnitt> avsnittList);

        /// <summary>
        /// Returns all saved podcast feeds from the database.
        /// </summary>
        Task<List<Poddflöden>> GetAllSavedFeedsAsync();

        /// <summary>
        /// Deletes a podcast feed and its episodes from the database.
        /// </summary>
        Task DeleteFeedAndEpisodesAsync(string rssUrl);

        /// <summary>
        /// Renames an existing podcast feed.
        /// </summary>
        Task RenameFeedAsync(string id, string newName);

        /// <summary>
        /// Assigns a category to an existing feed.
        /// </summary>
        Task AssignCategoryAsync(string poddId, string categoryId);

        /// <summary>
        /// Removes the category from an existing feed (sets categoryId to empty).
        /// </summary>
        Task RemoveCategoryAsync(string poddId);
    }

    public class PoddService : IPoddService
    {
        private readonly IPoddflodeRepository _poddRepo;
        private readonly IPoddAvsnittRepository _avsnittRepo;
        private readonly IRssParser _rssParser;
        private readonly IMongoClient _client;

        public PoddService(
            IPoddflodeRepository poddRepo,
            IPoddAvsnittRepository avsnittRepo,
            IRssParser rssParser,
            MongoConnector connector)
        {
            _poddRepo = poddRepo ?? throw new ArgumentNullException(nameof(poddRepo));
            _avsnittRepo = avsnittRepo ?? throw new ArgumentNullException(nameof(avsnittRepo));
            _rssParser = rssParser ?? throw new ArgumentNullException(nameof(rssParser));

            if (connector == null)
                throw new ArgumentNullException(nameof(connector));

            _client = connector.GetClient()
                      ?? throw new ServiceException("Could not obtain MongoDB client instance.");
        }

        /// <inheritdoc />
        public async Task<(Poddflöden poddflode, List<PoddAvsnitt> avsnitt)> FetchPoddFeedAsync(string rssUrl)
        {
            PoddValidator.ValidateRssUrl(rssUrl);

            try
            {
                return await _rssParser.FetchAndParseAsync(rssUrl);
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    $"Could not fetch or parse the podcast feed from URL '{rssUrl}'.",
                    ex);
            }
        }

        /// <inheritdoc />
        public async Task<(Poddflöden poddflode, List<PoddAvsnitt> avsnitt)> FetchFromDatabaseAsync(string rssUrl)
        {
            PoddValidator.ValidateRssUrl(rssUrl);

            var existing = await _poddRepo.GetByUrlAsync(rssUrl);
            if (existing == null)
                throw new ValidationException($"No podcast feed found in database with RSS URL: {rssUrl}");

            var episodes = await _avsnittRepo.GetByFeedIdAsync(existing.Id!);
            return (existing, episodes);
        }

        /// <inheritdoc />
        public async Task SavePoddSubscriptionAsync(Poddflöden poddflode, List<PoddAvsnitt> avsnittList)
        {
            if (poddflode == null) throw new ServiceException("Feed object may not be null.");
            if (avsnittList == null) throw new ServiceException("Episode list may not be null.");

            PoddValidator.ValidateRssUrl(poddflode.rssUrl);
            PoddValidator.ValidateFeedName(poddflode.displayName);
            PoddValidator.EnsureEpisodesExist(avsnittList);

            // Check for duplicate subscription by RSS URL
            var existingPodd = await _poddRepo.GetByUrlAsync(poddflode.rssUrl);
            if (existingPodd != null)
                throw new ServiceException(
                    $"The feed with URL '{poddflode.rssUrl}' is already saved in the database.");

            using var session = await _client.StartSessionAsync();
            session.StartTransaction();

            try
            {
                // mark as saved BEFORE insert so persisted document contains correct values
                poddflode.IsSaved = true;
                poddflode.SavedAt = DateTime.UtcNow;

                // 1) Insert the feed
                await _poddRepo.AddAsync(session, poddflode);

                // 2) Ensure all episodes reference the new feed id
                var newFeedId = poddflode.Id;
                foreach (var ep in avsnittList)
                {
                    ep.feedId = newFeedId!;
                }

                // 3) Insert all episodes (if any) in the same transaction
                if (avsnittList.Count > 0)
                {
                    await _avsnittRepo.AddRangeAsync(session, avsnittList);
                }

                // 4) Commit if everything succeeded
                await session.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                // Roll back the transaction so we don't end up with a half-saved state
                await session.AbortTransactionAsync();

                throw new ServiceException(
                    "Could not save the podcast feed and its episodes in a single transaction. All changes were rolled back.",
                    ex);
            }
        }

        /// <inheritdoc />
        public async Task<List<Poddflöden>> GetAllSavedFeedsAsync()
        {
            var all = await _poddRepo.GetAllAsync();
            var list = new List<Poddflöden>();
            foreach (var f in all)
            {
                if (f.IsSaved) list.Add(f);
            }

            return list;
        }

        /// <inheritdoc />
        public async Task DeleteFeedAndEpisodesAsync(string rssUrl)
        {
            PoddValidator.ValidateRssUrl(rssUrl);

            using var session = await _client.StartSessionAsync();
            session.StartTransaction();

            try
            {
                var feed = await _poddRepo.GetByUrlAsync(session, rssUrl);
                if (feed == null)
                    throw new ValidationException($"No podcast feed found with RSS URL: {rssUrl}");

                await _avsnittRepo.DeleteByFeedIdAsync(session, feed.Id!);
                await _poddRepo.DeleteByIdAsync(session, feed.Id!);

                await session.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await session.AbortTransactionAsync();

                if (ex is ValidationException) throw;

                throw new ServiceException(
                    "Could not delete feed and episodes in a transaction.",
                    ex);
            }
        }

        /// <inheritdoc />
        public async Task RenameFeedAsync(string id, string newName)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ValidationException("Feed ID cannot be empty.");

            var existing = await _poddRepo.GetByIdAsync(id);
            if (existing == null)
                throw new ValidationException($"Could not find feed with id '{id}'.");

            PoddValidator.EnsureFeedRenameValid(existing, newName);

            using var session = await _client.StartSessionAsync();
            session.StartTransaction();

            try
            {
                existing.displayName = newName.Trim();

                await _poddRepo.UpdateAsync(session, id, existing);

                await session.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await session.AbortTransactionAsync();

                if (ex is ValidationException) throw;

                throw new ServiceException(
                    "Could not rename feed in a transaction.",
                    ex);
            }
        }

        /// <inheritdoc />
        public async Task AssignCategoryAsync(string poddId, string categoryId)
        {
            if (string.IsNullOrWhiteSpace(poddId))
            {
                throw new ServiceException("Feed ID cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(categoryId))
            {
                throw new ServiceException("Category ID cannot be empty.");
            }

            // säkerställ att feeden är sparad
            var existing = await _poddRepo.GetByIdAsync(poddId);
            if (existing == null || !existing.IsSaved)
                throw new ValidationException("Feed must be saved before assigning category.");

            using var session = await _client.StartSessionAsync();
            session.StartTransaction();

            try
            {
                await _poddRepo.UpdateCategoryAsync(session, poddId, categoryId);

                await session.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await session.AbortTransactionAsync();

                throw new ServiceException(
                    "Could not assign category to the selected feed.",
                    ex);
            }
        }

        /// <inheritdoc />
        public async Task RemoveCategoryAsync(string poddId)
        {
            if (string.IsNullOrWhiteSpace(poddId))
            {
                throw new ServiceException("Feed ID cannot be empty.");
            }

            var existing = await _poddRepo.GetByIdAsync(poddId);
            if (existing == null || !existing.IsSaved)
                throw new ValidationException("Feed must be saved before removing category.");

            using var session = await _client.StartSessionAsync();
            session.StartTransaction();

            try
            {
                // Convention: empty string means "no category"
                await _poddRepo.UpdateCategoryAsync(session, poddId, string.Empty);

                await session.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await session.AbortTransactionAsync();

                throw new ServiceException(
                    "Could not remove category from the selected feed.",
                    ex);
            }
        }
    }
}
