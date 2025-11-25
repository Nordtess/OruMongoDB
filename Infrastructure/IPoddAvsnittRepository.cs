using MongoDB.Driver;
using OruMongoDB.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OruMongoDB.Infrastructure
{
    
    // Interface utökad med metoder för att hämta och ta bort avsnitt baserat på feedId
    public interface IPoddAvsnittRepository : IRepository<PoddAvsnitt>
    {
        Task AddRangeAsync(IClientSessionHandle session, IEnumerable<PoddAvsnitt> entities);
        Task<List<PoddAvsnitt>> GetByFeedIdAsync(string feedId);
        Task DeleteByFeedIdAsync(IClientSessionHandle session, string feedId);
    }

    
    public class PoddAvsnittRepository : MongoRepository<PoddAvsnitt>, IPoddAvsnittRepository
    {
        public PoddAvsnittRepository(IMongoDatabase database)
            : base(database, "PoddAvsnitt")
        {
        }

        public async Task AddRangeAsync(IClientSessionHandle session, IEnumerable<PoddAvsnitt> entities)
        {
            await _collection.InsertManyAsync(session, entities);
        }

        public async Task<List<PoddAvsnitt>> GetByFeedIdAsync(string feedId)
        {
            var filter = Builders<PoddAvsnitt>.Filter.Eq(a => a.feedId, feedId);
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task DeleteByFeedIdAsync(IClientSessionHandle session, string feedId)
        {
            var filter = Builders<PoddAvsnitt>.Filter.Eq(a => a.feedId, feedId);
            await _collection.DeleteManyAsync(session, filter);
        }
    }
}