using MongoDB.Driver;

namespace OruMongoDB.Infrastructure
{
    public class MongoConnector
    {
        private static readonly Lazy<MongoConnector> _instance =
            new Lazy<MongoConnector>(() => new MongoConnector());

        public static MongoConnector Instance => _instance.Value;

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public IMongoClient Client => _client;   
        public IMongoDatabase Database => _database;
        public MongoConnector()
        {
            var connectionString =
                "mongodb+srv://Jamie:h2IMNdTUGKAgewSC@orumongodb.theeg6u.mongodb.net/?appName=OruMongoDb";

            var databaseName = "G20";

             _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName);
        }

        public IMongoDatabase GetDatabase() => _database;

        public IMongoClient GetClient() => _database.Client;

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}
