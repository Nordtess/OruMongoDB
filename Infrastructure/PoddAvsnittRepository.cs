using MongoDB.Driver;
using OruMongoDB.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OruMongoDB.Infrastructure
{
    public class PoddAvsnittRepository : MongoRepository<PoddAvsnitt>, IPoddAvsnittRepository
    {
        public PoddAvsnittRepository(IMongoDatabase database)
        : base(database, "PoddAvsnitt")
        {
        }

        public Task AddRangeAsync(IClientSessionHandle session, IEnumerable<PoddAvsnitt> entities) =>
        _collection.InsertManyAsync(session, entities);

        public Task<List<PoddAvsnitt>> GetByFeedIdAsync(string feedId)
        {
            var filter = Builders<PoddAvsnitt>.Filter.Eq(a => a.feedId, feedId);
            return _collection.Find(filter).ToListAsync();
        }

        public Task DeleteByFeedIdAsync(IClientSessionHandle session, string feedId)
        {
            var filter = Builders<PoddAvsnitt>.Filter.Eq(a => a.feedId, feedId);
            return _collection.DeleteManyAsync(session, filter);
        }
    }
}
