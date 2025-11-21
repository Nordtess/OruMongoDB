using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MongoDB.Driver;
using OruMongoDB.Core.Validation;
using OruMongoDB.Domain;
using OruMongoDB.Infrastructure;

namespace OruMongoDB.Core
{
    public class JamiesPoddService : PoddServiceBase
    {
        private readonly MongoConnector _connector;
        private readonly IMongoCollection<Poddflöden> _flodeCollection;
        private readonly IMongoCollection<PoddAvsnitt> _avsnittCollection;

        public JamiesPoddService()
        {
            _connector = MongoConnector.Instance;
            _flodeCollection = _connector.GetCollection<Poddflöden>("Poddflöden");
            _avsnittCollection = _connector.GetCollection<PoddAvsnitt>("PoddAvsnitt");
        }

        
        public override async Task<(Poddflöden Flode, List<PoddAvsnitt> Avsnitt)>
            HamtaPoddflodeFranUrlAsync(string rssUrl)
        {
            
            PoddValidator.ValidateRssUrl(rssUrl);

            
            var flode = await _flodeCollection
                .Find(f => f.rssUrl == rssUrl)
                .FirstOrDefaultAsync();

            if (flode == null)
            {
                throw new ValidationException(
                    $"Hittade inget poddflöde i databasen med RSS-URL: {rssUrl}");
            }

            
            var avsnitt = await _avsnittCollection
                .Find(a => a.feedId == flode.Id)
                .ToListAsync();

            return (flode, avsnitt);
        }

        
        public override async Task SparaPoddflodeAsync(Poddflöden flode)
        {
            if (flode == null)
                throw new ArgumentNullException(nameof(flode));

            PoddValidator.ValidateRssUrl(flode.rssUrl);
            PoddValidator.ValidatePoddNamn(flode.displayName);

            flode.IsSaved = true;

            if (flode.SavedAt == null)
            {
                flode.SavedAt = DateTime.UtcNow;
            }

            var filter = Builders<Poddflöden>.Filter.Eq(f => f.rssUrl, flode.rssUrl);

            await _flodeCollection.ReplaceOneAsync(
                filter,
                flode,
                new ReplaceOptions { IsUpsert = true });
        }

        
        public override async Task SparaAvsnittAsync(List<PoddAvsnitt> avsnitt)
        {
            if (avsnitt == null || avsnitt.Count == 0)
                return;

            await _avsnittCollection.InsertManyAsync(avsnitt);
        }

        
        public async Task SparaPoddflodeOchAvsnittAsync(
            Poddflöden flode,
            List<PoddAvsnitt> avsnitt)
        {
            if (flode == null)
                throw new ArgumentNullException(nameof(flode));

            PoddValidator.ValidateRssUrl(flode.rssUrl);
            PoddValidator.ValidatePoddNamn(flode.displayName);

            await _connector.RunTransactionAsync(async session =>
            {
                flode.IsSaved = true;
                if (flode.SavedAt == null)
                {
                    flode.SavedAt = DateTime.UtcNow;
                }

                var filter = Builders<Poddflöden>.Filter.Eq(f => f.rssUrl, flode.rssUrl);

                
                await _flodeCollection.ReplaceOneAsync(
                    session,
                    filter,
                    flode,
                    new ReplaceOptions { IsUpsert = true });

                if (avsnitt != null && avsnitt.Count > 0)
                {
                    
                    var avsnittFilter = Builders<PoddAvsnitt>.Filter.Eq(a => a.feedId, flode.Id);
                    await _avsnittCollection.DeleteManyAsync(session, avsnittFilter);

                    await _avsnittCollection.InsertManyAsync(session, avsnitt);
                }
            });
        }

        
        public IReadOnlyList<Poddflöden> HamtaAllaFloden()
        {
            var filter = Builders<Poddflöden>.Filter.Eq(f => f.IsSaved, true);
            return _flodeCollection.Find(filter).ToList();
        }

        
        public async Task TaBortSparatFlodeAsync(string rssUrl)
        {
            PoddValidator.ValidateRssUrl(rssUrl);

            var filter = Builders<Poddflöden>.Filter.Eq(f => f.rssUrl, rssUrl);

            var update = Builders<Poddflöden>.Update
                .Set(f => f.IsSaved, false)
                .Set(f => f.SavedAt, (DateTime?)null);

            var result = await _flodeCollection.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 0)
            {
                throw new ValidationException($"Hittade inget poddflöde med RSS-URL: {rssUrl}");
            }
        }
    }
}
