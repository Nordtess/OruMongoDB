using System;
using System.Threading.Tasks;
using MongoDB.Driver;

/*
 Summary
 -------
 MongoConnector centralizes MongoDB Atlas connectivity for the application.
 - Lazy, thread-safe singleton exposing IMongoClient and IMongoDatabase.
 - Convenience accessors for client, database, and typed collections.
 - Helper to execute ACID transactions and propagate errors for higher-layer handling.
 - Supports environment variable overrides for URI/DB without breaking current defaults.
*/

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

        private MongoConnector()
        {
            // Allow configuration via environment variables when available.
            var connectionString =
                Environment.GetEnvironmentVariable("MONGODB_URI") ??
                "mongodb+srv://Jamie:h2IMNdTUGKAgewSC@orumongodb.theeg6u.mongodb.net/?appName=OruMongoDb";

            var databaseName =
                Environment.GetEnvironmentVariable("MONGODB_DB") ??
                "opponering";

            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName);
        }

        public IMongoDatabase GetDatabase() => _database;
        public IMongoClient GetClient() => _client;

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }

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

