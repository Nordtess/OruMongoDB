using MongoDB.Driver;
using OruMongoDB.BusinessLayer.Exceptions;
using OruMongoDB.BusinessLayer.Rss;
using OruMongoDB.Domain;
using OruMongoDB.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Xml;

/*
 Summary
 -------
 PoddService is the high-level application service for podcast feed operations. It:
 - Fetches and parses feeds from the Internet (via IRssParser) without persisting them.
 - Loads saved feeds + episodes from MongoDB Atlas.
 - Saves a feed with all its episodes atomically (ACID transaction).
 - Renames feeds and assigns/removes categories using transactions.
 - Deletes feeds and their episodes in a single transaction.
*/

namespace OruMongoDB.BusinessLayer
{
    public interface IPoddService
    {
        Task<(Poddflöden poddflode, List<PoddAvsnitt> avsnitt)> FetchPoddFeedAsync(string rssUrl);
        Task<(Poddflöden poddflode, List<PoddAvsnitt> avsnitt)> FetchFromDatabaseAsync(string rssUrl);
        Task SavePoddSubscriptionAsync(Poddflöden poddflode, List<PoddAvsnitt> avsnittList);
        Task<List<Poddflöden>> GetAllSavedFeedsAsync();
        Task DeleteFeedAndEpisodesAsync(string rssUrl);
        Task RenameFeedAsync(string id, string newName);
        Task AssignCategoryAsync(string poddId, string categoryId);
        Task RemoveCategoryAsync(string poddId);
    }

    public class PoddService : IPoddService
    {
        private readonly IPoddflodeRepository _poddRepo;
        private readonly IPoddAvsnittRepository _avsnittRepo;
        private readonly IRssParser _rssParser;
        private readonly MongoConnector _connector;

        public PoddService(
            IPoddflodeRepository poddRepo,
            IPoddAvsnittRepository avsnittRepo,
            IRssParser rssParser,
            MongoConnector connector)
        {
            _poddRepo = poddRepo ?? throw new ArgumentNullException(nameof(poddRepo));
            _avsnittRepo = avsnittRepo ?? throw new ArgumentNullException(nameof(avsnittRepo));
            _rssParser = rssParser ?? throw new ArgumentNullException(nameof(rssParser));
            _connector = connector ?? throw new ArgumentNullException(nameof(connector));
        }

        // URL validation centralizes format checks before DB or network operations.
        private static void ValidateRssUrlInternal(string rssUrl)
        {
            if (string.IsNullOrWhiteSpace(rssUrl))
                throw new ValidationException("RSS URL must not be empty.");

            if (!Uri.TryCreate(rssUrl, UriKind.Absolute, out var uri) ||
                (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
            {
                throw new ValidationException("RSS URL is not valid (must start with http or https).");
            }
        }

        private static void ValidateFeedNameInternal(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException("Feed name cannot be empty.");
            if (name.Length > 200)
                throw new ValidationException("Feed name is too long (max200 characters).");
        }

        public async Task<(Poddflöden poddflode, List<PoddAvsnitt> avsnitt)> FetchPoddFeedAsync(string rssUrl)
        {
            ValidateRssUrlInternal(rssUrl);
            try
            {
                return await _rssParser.FetchAndParseAsync(rssUrl);
            }
            catch (HttpRequestException httpEx) when (httpEx.StatusCode == HttpStatusCode.NotFound)
            {
                // Friendly, non-crashing message for nonexistent feeds
                throw new ValidationException($"No podcast feed was found at URL: {rssUrl}");
            }
            catch (HttpRequestException httpEx)
            {
                var status = httpEx.StatusCode.HasValue
                    ? $"{(int)httpEx.StatusCode.Value} {httpEx.StatusCode.Value}"
                    : "network error";
                throw new ValidationException($"Could not access the URL ({status}). Please check the link and try again.");
            }
            catch (XmlException)
            {
                // Content fetched but not a valid RSS/Atom XML
                throw new ValidationException($"The content at '{rssUrl}' is not a valid RSS/Atom feed.");
            }
            catch (Exception ex)
            {
                // Unknown technical failures still surface as ServiceException
                throw new ServiceException(
                    $"Could not fetch or parse the podcast feed from URL '{rssUrl}'.",
                    ex);
            }
        }

        public async Task<(Poddflöden poddflode, List<PoddAvsnitt> avsnitt)> FetchFromDatabaseAsync(string rssUrl)
        {
            ValidateRssUrlInternal(rssUrl);

            var existing = await _poddRepo.GetByUrlAsync(rssUrl);
            if (existing == null)
                throw new ValidationException($"No podcast feed found in database with RSS URL: {rssUrl}");

            var episodes = await _avsnittRepo.GetByFeedIdAsync(existing.Id!);
            return (existing, episodes);
        }

        public async Task SavePoddSubscriptionAsync(Poddflöden poddflode, List<PoddAvsnitt> avsnittList)
        {
            if (poddflode == null) throw new ServiceException("Feed object may not be null.");
            if (avsnittList == null) throw new ServiceException("Episode list may not be null.");

            ValidateRssUrlInternal(poddflode.rssUrl);
            ValidateFeedNameInternal(poddflode.displayName);
            if (avsnittList.Count == 0) throw new ValidationException("There are no episodes to operate on.");

            // Prevent duplicate subscription by RSS URL.
            var existingPodd = await _poddRepo.GetByUrlAsync(poddflode.rssUrl);
            if (existingPodd != null)
                throw new ServiceException(
                    $"The feed with URL '{poddflode.rssUrl}' is already saved in the database.");

            try
            {
                await _connector.RunTransactionAsync(async session =>
                {
                    // Mark as saved BEFORE insert so persisted document contains correct values.
                    poddflode.IsSaved = true;
                    poddflode.SavedAt = DateTime.UtcNow;

                    //1) Insert the feed.
                    await _poddRepo.AddAsync(session, poddflode);

                    //2) Ensure all episodes reference the new feed id.
                    var newFeedId = poddflode.Id;
                    foreach (var ep in avsnittList)
                        ep.feedId = newFeedId!;

                    //3) Insert all episodes (already validated count >0).
                    await _avsnittRepo.AddRangeAsync(session, avsnittList);
                });
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    "Could not save the podcast feed and its episodes in a single transaction. All changes were rolled back.",
                    ex);
            }
        }

        public async Task<List<Poddflöden>> GetAllSavedFeedsAsync()
        {
            // Filter only feeds flagged as saved.
            var all = await _poddRepo.GetAllAsync();
            return new List<Poddflöden>(all.Where(f => f.IsSaved));
        }

        public async Task DeleteFeedAndEpisodesAsync(string rssUrl)
        {
            ValidateRssUrlInternal(rssUrl);

            try
            {
                await _connector.RunTransactionAsync(async session =>
                {
                    var feed = await _poddRepo.GetByUrlAsync(session, rssUrl);
                    if (feed == null)
                        throw new ValidationException($"No podcast feed found with RSS URL: {rssUrl}");

                    await _avsnittRepo.DeleteByFeedIdAsync(session, feed.Id!);
                    await _poddRepo.DeleteByIdAsync(session, feed.Id!);
                });
            }
            catch (Exception ex)
            {
                if (ex is ValidationException) throw;
                throw new ServiceException(
                    "Could not delete feed and episodes in a transaction.",
                    ex);
            }
        }

        public async Task RenameFeedAsync(string id, string newName)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ValidationException("Feed ID cannot be empty.");

            ValidateFeedNameInternal(newName);

            var existing = await _poddRepo.GetByIdAsync(id);
            if (existing == null)
                throw new ValidationException($"Could not find feed with id '{id}'.");

            if (string.Equals(existing.displayName, newName, StringComparison.Ordinal))
                throw new ValidationException("The new feed name is the same as the current name.");

            try
            {
                await _connector.RunTransactionAsync(async session =>
                {
                    existing.displayName = newName.Trim();
                    await _poddRepo.UpdateAsync(session, id, existing);
                });
            }
            catch (Exception ex)
            {
                if (ex is ValidationException) throw;
                throw new ServiceException(
                    "Could not rename feed in a transaction.",
                    ex);
            }
        }

        public async Task AssignCategoryAsync(string poddId, string categoryId)
        {
            if (string.IsNullOrWhiteSpace(poddId))
                throw new ServiceException("Feed ID cannot be empty.");
            if (string.IsNullOrWhiteSpace(categoryId))
                throw new ServiceException("Category ID cannot be empty.");

            var existing = await _poddRepo.GetByIdAsync(poddId);
            if (existing == null || !existing.IsSaved)
                throw new ValidationException("Feed must be saved before assigning category.");

            try
            {
                await _connector.RunTransactionAsync(async session =>
                {
                    await _poddRepo.UpdateCategoryAsync(session, poddId, categoryId);
                });
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    "Could not assign category to the selected feed.",
                    ex);
            }
        }

        public async Task RemoveCategoryAsync(string poddId)
        {
            if (string.IsNullOrWhiteSpace(poddId))
                throw new ServiceException("Feed ID cannot be empty.");

            var existing = await _poddRepo.GetByIdAsync(poddId);
            if (existing == null || !existing.IsSaved)
                throw new ValidationException("Feed must be saved before removing category.");

            try
            {
                await _connector.RunTransactionAsync(async session =>
                {
                    // Empty string signals "no category".
                    await _poddRepo.UpdateCategoryAsync(session, poddId, string.Empty);
                });
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
