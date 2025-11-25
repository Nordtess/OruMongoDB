using MongoDB.Driver;
using OruMongoDB.Domain;
using System.Threading.Tasks;

namespace OruMongoDB.Infrastructure
{
    public interface IPoddflodeRepository : IRepository<Poddflöden>
    {
        Task<Poddflöden> GetByUrlAsync(string rssUrl);
        Task UpdateCategoryAsync(string poddId, string categoryId);
        Task UpdateCategoryAsync(IClientSessionHandle session, string poddId, string categoryId);
    }

    public class PoddflodeRepository : MongoRepository<Poddflöden>, IPoddflodeRepository
    {
        public PoddflodeRepository(IMongoDatabase database)
            : base(database, "Poddfloden") { }

        public async Task<Poddflöden> GetByUrlAsync(string rssUrl)
        {
            var filter = Builders<Poddflöden>.Filter.Eq(p => p.rssUrl, rssUrl);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateCategoryAsync(string poddId, string categoryId)
        {
            var filter = Builders<Poddflöden>.Filter.Eq(p => p.Id, poddId);
            var update = Builders<Poddflöden>.Update.Set(p => p.categoryId, categoryId);
            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateCategoryAsync(IClientSessionHandle session, string poddId, string categoryId)
        {
            var filter = Builders<Poddflöden>.Filter.Eq(p => p.Id, poddId);
            var update = Builders<Poddflöden>.Update.Set(p => p.categoryId, categoryId);
            await _collection.UpdateOneAsync(session, filter, update);
        }
    }
}
