using MongoDB.Driver;
using OruMongoDB.Domain;
using System.Threading.Tasks;

namespace OruMongoDB.Infrastructure
{
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
