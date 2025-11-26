using MongoDB.Driver;
using OruMongoDB.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

/*
 Summary
 -------
 Defines the repository contract and implementation for podcast episodes (PoddAvsnitt) backed by MongoDB Atlas.
 - Interface exposes async CRUD helpers and feed-scoped operations.
 - Implementation uses the official MongoDB .NET driver and supports ACID transactions via session-aware overloads.
 - Keeps data access isolated behind interfaces to align with layering and testability.
*/

namespace OruMongoDB.Infrastructure
{
    public interface IPoddAvsnittRepository : IRepository<PoddAvsnitt>
    {
        // Bulk insert within an active transaction session.
        Task AddRangeAsync(IClientSessionHandle session, IEnumerable<PoddAvsnitt> entities);

        // Query all episodes for a given feed (non-transactional read).
        Task<List<PoddAvsnitt>> GetByFeedIdAsync(string feedId);

        // Delete all episodes for a given feed within a transaction session.
        Task DeleteByFeedIdAsync(IClientSessionHandle session, string feedId);
    }

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