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

        public AlexKrav()
        {
            var connector = MongoConnector.Instance;
            _flodeCollection = connector.GetCollection<Poddflöden>("Poddflöden");
            _avsnittCollection = connector.GetCollection<PoddAvsnitt>("PoddAvsnitt");
            _kategoriCollection = connector.GetCollection<Kategori>("Kategorier");
        }

        public override async Task<(Poddflöden Flode, List<PoddAvsnitt> Avsnitt)>
            HamtaPoddflodeFranUrlAsync(string rssUrl)
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

        public override async Task SparaPoddflodeAsync(Poddflöden flode)
        {
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

        public async Task AndraNamnPaPoddflodeAsync(string id, string nyttNamn)
        {
            var filter = Builders<Poddflöden>.Filter.Eq(f => f.Id, id);

            var update = Builders<Poddflöden>.Update.Set(f => f.displayName, nyttNamn);

            var result = await _flodeCollection.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 0)
                throw new InvalidOperationException(
                    $"Kunde inte hitta poddflöde med id {id}");
        }

        public async Task<Poddflöden> HamtaFlodeViaIdAsync(string id)
        {
            return await _flodeCollection
                .Find(f => f.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Poddflöden> SkapaNyttPoddflodeAsync(string rssUrl, string egetNamn)
        {
            // kontrollera om det redan finns
            var finns = await _flodeCollection.Find(f => f.rssUrl == rssUrl).FirstOrDefaultAsync();
            if (finns != null)
                throw new InvalidOperationException("Detta poddflöde finns redan i databasen.");

            // skapa nytt flöde
            var nyttFlode = new Poddflöden
            {
                rssUrl = rssUrl,
                displayName = egetNamn,
                categoryId = "6915bfa1d1eb7bd02636fd87"
            };

            await _flodeCollection.InsertOneAsync(nyttFlode);

            return nyttFlode;
        }


        public async Task RaderaKategoriAsync(string kategoriId)
        {
        

                // 1. kontrollera om kategorin finns
                var finns = await _kategoriCollection
                    .Find(c => c.Id == kategoriId)
                    .FirstOrDefaultAsync();

                if (finns == null)
        
                throw new InvalidOperationException("Kategorin hittades inte.");
            
                // 2. radera kategorin
                var result = await _kategoriCollection.DeleteOneAsync(c => c.Id == kategoriId);
                
                // 3. Sätta categoryId = null på flöden som använder kategorin
                var filter = Builders<Poddflöden>.Filter.Eq(f => f.categoryId, kategoriId);
                var update = Builders<Poddflöden>.Update.Set(f => f.categoryId, null);

                await _flodeCollection.UpdateManyAsync(filter, update);

              
            }
        
            }
        }

    



