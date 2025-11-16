using MongoDB.Driver;
using OruMongoDB.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OruMongoDB.Infrastructure
{
    
    public interface IPoddAvsnittRepository : IRepository<PoddAvsnitt>
    {
        Task AddRangeAsync(IClientSessionHandle session, IEnumerable<PoddAvsnitt> entities);
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
    }
}