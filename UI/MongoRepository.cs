using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OruMongoDB.Infrastructure
{
    
    public abstract class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IMongoCollection<TEntity> _collection;

       
        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<TEntity>(collectionName);
        }

       
        public async Task<TEntity> GetByIdAsync(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public Task AddAsync(TEntity entity)
        {
            return _collection.InsertOneAsync(entity);
        }

        public Task AddAsync(IClientSessionHandle session, TEntity entity)
        {
            return _collection.InsertOneAsync(session, entity);
        }

       
        public Task UpdateAsync(string id, TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));
            return _collection.ReplaceOneAsync(filter, entity);
        }

        
        public Task UpdateAsync(IClientSessionHandle session, string id, TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));
            return _collection.ReplaceOneAsync(session, filter, entity);
        }

        
        public Task DeleteAsync(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));
            return _collection.DeleteOneAsync(filter);
        }

        
        public Task DeleteAsync(IClientSessionHandle session, string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));
            return _collection.DeleteOneAsync(session, filter);
        }
    }
}