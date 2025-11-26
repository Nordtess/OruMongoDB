using System;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

/*
 Summary:
 Generic MongoDB repository base for entities of type TEntity.
 Uses MongoDB .NET Driver against a MongoDB Atlas database for persistent storage.
 Provides async CRUD operations and overloads that accept IClientSessionHandle so callers
 can execute inserts/updates/deletes inside multi-operation transactions when needed.
 Exceptions are expected to be handled at higher layers to keep the application resilient.
*/

namespace OruMongoDB.Infrastructure
{
    public abstract class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        // Backing collection for the concrete entity
        protected readonly IMongoCollection<TEntity> _collection;

        // The collection is resolved from the provided database and collection name
        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            if (database is null) throw new ArgumentNullException(nameof(database));
            if (string.IsNullOrWhiteSpace(collectionName)) throw new ArgumentException("Collection name is required.", nameof(collectionName));
            _collection = database.GetCollection<TEntity>(collectionName);
        }

        // Read a single document by its MongoDB ObjectId string
        public async Task<TEntity> GetByIdAsync(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id)); // throws if id is not a valid ObjectId
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        // Read all documents in the collection
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _collection.Find(Builders<TEntity>.Filter.Empty).ToListAsync();
        }

        // Insert a single document (no explicit transaction)
        public Task AddAsync(TEntity entity)
        {
            return _collection.InsertOneAsync(entity);
        }

        // Insert within a caller-provided session (to allow transactions)
        public Task AddAsync(IClientSessionHandle session, TEntity entity)
        {
            return _collection.InsertOneAsync(session, entity);
        }

        // Replace an existing document by id (no explicit transaction)
        public Task UpdateAsync(string id, TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));
            return _collection.ReplaceOneAsync(filter, entity);
        }

        // Replace within a caller-provided session (to allow transactions)
        public Task UpdateAsync(IClientSessionHandle session, string id, TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));
            return _collection.ReplaceOneAsync(session, filter, entity);
        }

        // Delete a single document by id (no explicit transaction)
        public Task DeleteAsync(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));
            return _collection.DeleteOneAsync(filter);
        }

        // Delete within a caller-provided session (to allow transactions)
        public Task DeleteAsync(IClientSessionHandle session, string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", ObjectId.Parse(id));
            return _collection.DeleteOneAsync(session, filter);
        }
    }
}