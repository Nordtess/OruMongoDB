using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

/*
 Summary
 -------
 Generic MongoDB Atlas–backed repository base for entities.
 - Provides async CRUD operations using the official MongoDB .NET driver.
 - Supports both standalone and transaction-aware operations (via session overloads in derived repos).
 - Centralizes common patterns like ObjectId parsing for _id lookups.
*/

namespace OruMongoDB.Infrastructure
{
    public abstract class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IMongoCollection<TEntity> _collection;

        /// <summary>
        /// Initialize repository with a specific collection.
        /// </summary>
        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<TEntity>(collectionName);
        }

        // Reads
        public Task<TEntity> GetByIdAsync(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));
            return _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            // Find all documents; ToListAsync returns List<TEntity> which is IEnumerable<TEntity>.
            return await _collection.Find(_ => true).ToListAsync();
        }

        // Creates
        public Task AddAsync(TEntity entity)
        {
            return _collection.InsertOneAsync(entity);
        }

        public Task AddAsync(IClientSessionHandle session, TEntity entity)
        {
            return _collection.InsertOneAsync(session, entity);
        }

        // Updates
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

        // Deletes
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