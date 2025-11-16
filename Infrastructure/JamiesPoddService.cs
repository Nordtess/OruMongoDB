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
            return _flodeCollection.Find(Builders<Poddflöden>.Filter.Empty).ToList();
        }
    }
}
