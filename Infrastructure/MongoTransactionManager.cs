using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace OruMongoDB.Infrastructure
{
    public class MongoTransactionManager
    {
        private readonly IMongoClient _client;

        public MongoTransactionManager(IMongoClient client)
        {
            _client = client;
        }

        public async Task RunTransactionAsync(Func<IClientSessionHandle, Task> operations)
        {
            using (var session = await _client.StartSessionAsync())
            {
                session.StartTransaction();

                try
                {
                    await operations(session);
                    await session.CommitTransactionAsync();
                }
                catch (Exception)
                {
                    await session.AbortTransactionAsync();
                    throw;
                }
            }
        }
    }
}
