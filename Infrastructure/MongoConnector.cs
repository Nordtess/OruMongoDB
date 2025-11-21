using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace OruMongoDB.Infrastructure
{
    public class MongoConnector
    {
        // Singleton-instans (vi använder alltid MongoConnector.Instance)
        private static readonly Lazy<MongoConnector> _instance =
            new Lazy<MongoConnector>(() => new MongoConnector());

        public static MongoConnector Instance => _instance.Value;

        // Fält
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        // Publika properties om du vill komma åt dem direkt
        public IMongoClient Client => _client;
        public IMongoDatabase Database => _database;

        // Privat konstruktor eftersom vi använder singleton
        private MongoConnector()
        {
            var connectionString =
                "mongodb+srv://Jamie:h2IMNdTUGKAgewSC@orumongodb.theeg6u.mongodb.net/?appName=OruMongoDb";

            var databaseName = "G20";

            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName);
        }

        // Hämta databasen
        public IMongoDatabase GetDatabase() => _database;

        // Hämta klienten (behövs för transaktioner om du vill jobba mer low-level)
        public IMongoClient GetClient() => _client;

        // Hämta en collection
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }

        // 🔥 Enkel helper för ACID-transaktioner
        // Använd: await MongoConnector.Instance.RunTransactionAsync(async session => { ... });
        public async Task RunTransactionAsync(Func<IClientSessionHandle, Task> operations)
        {
            using var session = await _client.StartSessionAsync();
            session.StartTransaction();

            try
            {
                await operations(session);
                await session.CommitTransactionAsync();
            }
            catch
            {
                await session.AbortTransactionAsync();
                throw;
            }
        }
    }
}

