using MongoDB.Driver;
using OruMongoDB.Domain;
using OruMongoDB.Infrastructure;

namespace OruMongoDB.Core
{
    public class AlexKrav : PoddServiceBase
    {
        private readonly IMongoCollection<Poddflöden> _flodeCollection;
        private readonly IMongoCollection<PoddAvsnitt> _avsnittCollection;
        private readonly IMongoCollection<Kategori> _kategoriCollection;
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _db;

        public AlexKrav()
        {
            var connector = MongoConnector.Instance;

            _client = connector.Client;     
            _db = connector.Database;       

            _flodeCollection = connector.GetCollection<Poddflöden>("Poddflöden");
            _avsnittCollection = connector.GetCollection<PoddAvsnitt>("PoddAvsnitt");
            _kategoriCollection = connector.GetCollection<Kategori>("Kategorier");
        }

       
        public override async Task<(Poddflöden Flode, List<PoddAvsnitt> Avsnitt)>
            HamtaPoddflodeFranUrlAsync(string rssUrl)
        {
            try
            {
                var flode = await _flodeCollection
                    .Find(f => f.rssUrl == rssUrl)
                    .FirstOrDefaultAsync();

                if (flode == null)
                    throw new InvalidOperationException($"Inget poddflöde med URL: {rssUrl}");

                var avsnitt = await _avsnittCollection
                    .Find(a => a.feedId == flode.Id)
                    .ToListAsync();

                return (flode, avsnitt);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Fel vid hämtning av poddflöde.", ex);
            }
        }

        
        public override async Task SparaPoddflodeAsync(Poddflöden flode)
        {
            using var session = await _client.StartSessionAsync();

            try
            {
                session.StartTransaction();

                var filter = Builders<Poddflöden>.Filter.Eq(f => f.rssUrl, flode.rssUrl);

                await _flodeCollection.ReplaceOneAsync(
                    session,
                    filter,
                    flode,
                    new ReplaceOptions { IsUpsert = true });

                await session.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await session.AbortTransactionAsync();
                throw new ApplicationException("Fel vid sparande av poddflöde.", ex);
            }
        }

        
        public override async Task SparaAvsnittAsync(List<PoddAvsnitt> avsnitt)
        {
            if (avsnitt == null || avsnitt.Count == 0)
                return;

            using var session = await _client.StartSessionAsync();

            try
            {
                session.StartTransaction();

                await _avsnittCollection.InsertManyAsync(session, avsnitt);

                await session.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await session.AbortTransactionAsync();
                throw new ApplicationException("Fel vid sparande av avsnitt.", ex);
            }
        }

       
        public async Task AndraNamnPaPoddflodeAsync(string id, string nyttNamn)
        {
            using var session = await _client.StartSessionAsync();

            try
            {
                session.StartTransaction();

                var filter = Builders<Poddflöden>.Filter.Eq(f => f.Id, id);
                var update = Builders<Poddflöden>.Update.Set(f => f.displayName, nyttNamn);

                var result = await _flodeCollection.UpdateOneAsync(session, filter, update);

                if (result.MatchedCount == 0)
                    throw new InvalidOperationException($"Kunde inte hitta poddflöde med id {id}");

                await session.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await session.AbortTransactionAsync();
                throw new ApplicationException("Fel vid uppdatering av namn.", ex);
            }
        }

        
        public async Task<Poddflöden> HamtaFlodeViaIdAsync(string id)
        {
            try
            {
                return await _flodeCollection
                    .Find(f => f.Id == id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Fel vid hämtning av poddflöde via ID.", ex);
            }
        }

        public async Task<Poddflöden> SkapaNyttPoddflodeAsync(string rssUrl, string egetNamn)
        {
            using var session = await _client.StartSessionAsync();

            try
            {
                session.StartTransaction();

                var befintligt = await _flodeCollection
                    .Find(f => f.rssUrl == rssUrl)
                    .FirstOrDefaultAsync();

                if (befintligt != null)
                    throw new InvalidOperationException("Detta poddflöde finns redan.");

                var nyttFlode = new Poddflöden
                {
                    rssUrl = rssUrl,
                    displayName = egetNamn,
                    categoryId = "6915bfa1d1eb7bd02636fd87"
                };

                await _flodeCollection.InsertOneAsync(session, nyttFlode);

                await session.CommitTransactionAsync();

                return nyttFlode;
            }
            catch (Exception ex)
            {
                await session.AbortTransactionAsync();
                throw new ApplicationException("Fel vid skapande av nytt poddflöde.", ex);
            }
        }

        
        public async Task RaderaKategoriAsync(string kategoriId)
        {
            using var session = await _client.StartSessionAsync();

            try
            {
                session.StartTransaction();

                // kontrollera att kategorin finns
                var finns = await _kategoriCollection
                    .Find(c => c.Id == kategoriId)
                    .FirstOrDefaultAsync();

                if (finns == null)
                    throw new InvalidOperationException("Kategorin hittades inte.");

                // radera kategorin
                await _kategoriCollection.DeleteOneAsync(session, c => c.Id == kategoriId);

                // ta bort categoryId från poddflöden
                var filter = Builders<Poddflöden>.Filter.Eq(f => f.categoryId, kategoriId);
                var update = Builders<Poddflöden>.Update.Set(f => f.categoryId, null);

                await _flodeCollection.UpdateManyAsync(session, filter, update);

                await session.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await session.AbortTransactionAsync();
                throw new ApplicationException("Kategorin hittades inte.", ex);
            }
        }
    }
}
