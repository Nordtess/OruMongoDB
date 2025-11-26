using MongoDB.Driver;
using OruMongoDB.Domain;
using System.Threading.Tasks;

/*
 Summary
 -------
 Repository contract and implementation for podcast feeds (Poddflöden) backed by MongoDB Atlas.
 - Async queries/updates via the official MongoDB .NET driver.
 - Session-aware overloads to participate in ACID transactions where required.
 - Keeps data access behind interfaces to support layering and testability.
*/

namespace OruMongoDB.Infrastructure
{
    public interface IPoddflodeRepository : IRepository<Poddflöden>
    {
        // Non-transactional operations
        Task<Poddflöden> GetByUrlAsync(string rssUrl);
        Task UpdateCategoryAsync(string poddId, string categoryId);

        // Transaction-aware overloads (use with an active session)
        Task UpdateCategoryAsync(IClientSessionHandle session, string poddId, string categoryId);
        Task<Poddflöden> GetByUrlAsync(IClientSessionHandle session, string rssUrl);
        Task DeleteByIdAsync(IClientSessionHandle session, string id);
    }

    public class PoddflodeRepository : MongoRepository<Poddflöden>, IPoddflodeRepository
    {
        public PoddflodeRepository(IMongoDatabase database)
            : base(database, "Poddflöden") { }

        public Task<Poddflöden> GetByUrlAsync(string rssUrl)
        {
            var filter = Builders<Poddflöden>.Filter.Eq(p => p.rssUrl, rssUrl);
            return _collection.Find(filter).FirstOrDefaultAsync();
        }

        public Task<Poddflöden> GetByUrlAsync(IClientSessionHandle session, string rssUrl)
        {
            var filter = Builders<Poddflöden>.Filter.Eq(p => p.rssUrl, rssUrl);
            return _collection.Find(session, filter).FirstOrDefaultAsync();
        }

        public Task UpdateCategoryAsync(string poddId, string categoryId)
        {
            var filter = Builders<Poddflöden>.Filter.Eq(p => p.Id, poddId);
            var update = Builders<Poddflöden>.Update.Set(p => p.categoryId, categoryId);
            return _collection.UpdateOneAsync(filter, update);
        }

        public Task UpdateCategoryAsync(IClientSessionHandle session, string poddId, string categoryId)
        {
            var filter = Builders<Poddflöden>.Filter.Eq(p => p.Id, poddId);
            var update = Builders<Poddflöden>.Update.Set(p => p.categoryId, categoryId);
            return _collection.UpdateOneAsync(session, filter, update);
        }

        public Task DeleteByIdAsync(IClientSessionHandle session, string id)
        {
            var filter = Builders<Poddflöden>.Filter.Eq(p => p.Id, id);
            return _collection.DeleteOneAsync(session, filter);
        }
    }
}
