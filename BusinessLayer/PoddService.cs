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
    
    public interface IPoddService
    {
        Task<(Poddflöden poddflode, List<PoddAvsnitt> avsnitt)> FetchPoddFeedAsync(string rssUrl);
        Task SavePoddSubscriptionAsync(Poddflöden poddflode, List<PoddAvsnitt> avsnittList);
        
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
            
            _client = connector.GetClient() ?? throw new ServiceException("Kunde inte ansluta till MongoDB-klienten.");
        }

        
        public async Task<(Poddflöden poddflode, List<PoddAvsnitt> avsnitt)> FetchPoddFeedAsync(string rssUrl)
        {
            try
            {
                
                if (!Uri.IsWellFormedUriString(rssUrl, UriKind.Absolute))
                {
                    throw new ServiceException($"Den angivna URL:en '{rssUrl}' är inte giltig.");
                }

                return await _rssParser.FetchAndParseAsync(rssUrl);
            }
            catch (ServiceException)
            {
                
                throw;
            }
            catch (Exception ex)
            {
                
                throw new ServiceException($"Kunde inte hämta eller tolka poddflödet från URL:en: {ex.Message}", ex);
            }
        }

        
        public async Task SavePoddSubscriptionAsync(Poddflöden poddflode, List<PoddAvsnitt> avsnittList)
        {
            
            var existingPodd = await _poddRepo.GetByUrlAsync(poddflode.rssUrl);
            if (existingPodd != null)
            {
                throw new ServiceException($"Poddflödet med URL '{poddflode.rssUrl}' är redan prenumererat på.");
            }

            
            using var session = await _client.StartSessionAsync();
            session.StartTransaction();

            try
            {
                
                await _poddRepo.AddAsync(session, poddflode);

                
                string newFeedId = poddflode.Id;
                avsnittList.ForEach(a => a.feedId = newFeedId);

                
                if (avsnittList.Count > 0)
                {
                    await _avsnittRepo.AddRangeAsync(session, avsnittList);
                }

                
                await session.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                
                await session.AbortTransactionAsync();

                throw new ServiceException("Kunde inte spara poddflödet och dess avsnitt i en transaktion. All data har rullats tillbaka.", ex);
            }
        }

        public async Task AssignCategoryAsync(string poddId, string categoryId)
        {
            if (string.IsNullOrWhiteSpace(poddId))
            {
                throw new ServiceException("Poddd ID får inte vara tomt.");
            }
            if (string.IsNullOrWhiteSpace(categoryId))
            {
                throw new ServiceException("Kategori ID får inte vara tomt.");
            }
            await _poddRepo.UpdateCategoryAsync(poddId, categoryId);
        }
    }
}