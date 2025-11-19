using MongoDB.Driver;
using OruMongoDB.Domain;
using OruMongoDB.Infrastructure;

namespace OruMongoDB.Core
{
    public class JamiesPoddService : PoddServiceBase
    {
        private readonly IMongoCollection<Poddflöden> _flodeCollection;
        private readonly IMongoCollection<PoddAvsnitt> _avsnittCollection;

        public JamiesPoddService()
        {
            var connector = MongoConnector.Instance;
            _flodeCollection = connector.GetCollection<Poddflöden>("Poddflöden");
            _avsnittCollection = connector.GetCollection<PoddAvsnitt>("PoddAvsnitt");
        }

        // Get podflow
        public override async Task<(Poddflöden Flode, List<PoddAvsnitt> Avsnitt)>
            HamtaPoddflodeFranUrlAsync(string rssUrl)
        {
            var flode = await _flodeCollection
                .Find(f => f.rssUrl == rssUrl)
                .FirstOrDefaultAsync();

            if (flode == null)
            {
                throw new InvalidOperationException(
                    $"Hittade inget poddflöde i databasen med rssUrl = {rssUrl}");
            }

            var avsnitt = await _avsnittCollection
                .Find(a => a.feedId == flode.Id)
                .ToListAsync();

            return (flode, avsnitt);
        }

        // Save new podflow
        public override async Task SparaPoddflodeAsync(Poddflöden flode)
        {
            
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

        // 🔹 Save episode
        public override async Task SparaAvsnittAsync(List<PoddAvsnitt> avsnitt)
        {
            if (avsnitt == null || avsnitt.Count == 0)
                return;

            await _avsnittCollection.InsertManyAsync(avsnitt);
        }

        // Get all flows from the DB
        public IReadOnlyList<Poddflöden> HamtaAllaFloden()
        {
            var filter = Builders<Poddflöden>.Filter.Eq(f => f.IsSaved, true);
            return _flodeCollection.Find(filter).ToList();
        }

        public async Task TaBortSparatFlodeAsync(string rssUrl)
        {
            var filter = Builders<Poddflöden>.Filter.Eq(f => f.rssUrl, rssUrl);

            var update = Builders<Poddflöden>.Update
                .Set(f => f.IsSaved, false)
                .Set(f => f.SavedAt, null);

            await _flodeCollection.UpdateOneAsync(filter, update);
        }
    }
}
