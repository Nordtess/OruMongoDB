using MongoDB.Driver;
using OruMongoDB.BusinessLayer.Exceptions;
using OruMongoDB.BusinessLayer.Rss;
using OruMongoDB.Domain;
using OruMongoDB.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OruMongoDB.BusinessLayer
{
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
        /// Saves the given feed and its episodes as a subscription in MongoDB
        /// using a single ACID transaction.
        /// </summary>
        Task SavePoddSubscriptionAsync(Poddflöden poddflode, List<PoddAvsnitt> avsnittList);

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
            try
            {
                if (string.IsNullOrWhiteSpace(rssUrl))
                {
                    throw new ServiceException("RSS URL cannot be empty.");
                }

                if (!Uri.TryCreate(rssUrl, UriKind.Absolute, out var uri) ||
                    (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
                {
                    throw new ServiceException($"The given RSS URL '{rssUrl}' is not valid (must be http or https).");
                }

                // Let the RSS parser do the actual HTTP + XML work
                return await _rssParser.FetchAndParseAsync(rssUrl);
            }
            catch (ServiceException)
            {
                // Already a domain-specific error – just bubble it up
                throw;
            }
            catch (Exception ex)
            {
                // Wrap any unexpected error into a ServiceException so the UI
                // can show a friendly message without crashing.
                throw new ServiceException(
                    $"Could not fetch or parse the podcast feed from URL '{rssUrl}'.",
                    ex);
            }
        }

        /// <inheritdoc />
        public async Task SavePoddSubscriptionAsync(Poddflöden poddflode, List<PoddAvsnitt> avsnittList)
        {
            if (poddflode == null)
                throw new ServiceException("Feed object may not be null.");

            if (avsnittList == null)
                throw new ServiceException("Episode list may not be null.");

            // Check for duplicate subscription by RSS URL
            var existingPodd = await _poddRepo.GetByUrlAsync(poddflode.rssUrl);
            if (existingPodd != null)
            {
                throw new ServiceException(
                    $"The feed with URL '{poddflode.rssUrl}' is already saved in the database.");
            }

            using var session = await _client.StartSessionAsync();
            session.StartTransaction();

            try
            {
                // 1) Insert the feed
                await _poddRepo.AddAsync(session, poddflode);

                // 2) Ensure all episodes reference the new feed id
                var newFeedId = poddflode.Id;
                foreach (var ep in avsnittList)
                {
                    ep.feedId = newFeedId;
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

            try
            {
                await _poddRepo.UpdateCategoryAsync(poddId, categoryId);
            }
            catch (Exception ex)
            {
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

            try
            {
                // Convention: empty string means "no category"
                await _poddRepo.UpdateCategoryAsync(poddId, string.Empty);
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    "Could not remove category from the selected feed.",
                    ex);
            }
        }
    }
}
