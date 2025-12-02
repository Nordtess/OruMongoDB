using System;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

/*
 Summary
 -------
 Generic MongoDB Atlas–backed repository base for entities.
 - Provides async CRUD operations using the official MongoDB .NET driver.
 - Enforces transactional writes only via session overloads.
 - Centralizes common patterns like ObjectId parsing for _id lookups.
*/

namespace OruMongoDB.Infrastructure
{
    public abstract class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IMongoCollection<TEntity> _collection;


        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            if (database is null) throw new ArgumentNullException(nameof(database));
            if (string.IsNullOrWhiteSpace(collectionName)) throw new ArgumentException("Collection name is required.", nameof(collectionName));
            _collection = database.GetCollection<TEntity>(collectionName);
        }

        // Reads
        public async Task<TEntity> GetByIdAsync(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            // Find all documents; ToListAsync returns List<TEntity> which is IEnumerable<TEntity>.
            return await _collection.Find(Builders<TEntity>.Filter.Empty).ToListAsync();
        }

        // Writes (transactional only)
        public Task AddAsync(IClientSessionHandle session, TEntity entity)
        {
            return _collection.InsertOneAsync(session, entity);
        }

        public Task UpdateAsync(IClientSessionHandle session, string id, TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));
            return _collection.ReplaceOneAsync(session, filter, entity);
        }

        public Task DeleteAsync(IClientSessionHandle session, string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));
            return _collection.DeleteOneAsync(session, filter);
        }
    }
}