using MongoDB.Driver;
using OruMongoDB.Domain;

/*
 Summary
 -------
 Repository for `Kategori` (categories) backed by MongoDB Atlas.
 - Provides async CRUD operations using the official MongoDB .NET driver.
 - Exposes overloads that accept `IClientSessionHandle` to participate in ACID transactions
 (insert/update/delete).
 - Keeps pure data access concerns isolated from business logic via interfaces.
*/

namespace OruMongoDB.Infrastructure
{
    public class CategoryRepository : MongoRepository<Kategori>, ICategoryRepository
    {
        public CategoryRepository(IMongoDatabase database) : base(database, "Kategorier") { }

        // Transaction-aware writes (session required)
        public Task InsertAsync(IClientSessionHandle session, Kategori category) =>
            _collection.InsertOneAsync(session, category);

        public Task UpdateCategoryNameAsync(IClientSessionHandle session, string categoryId, string newName)
        {
            var filter = Builders<Kategori>.Filter.Eq(c => c.Id, categoryId);
            var update = Builders<Kategori>.Update.Set(c => c.Namn, newName);
            return _collection.UpdateOneAsync(session, filter, update);
        }

        public Task DeleteCategoryAsync(IClientSessionHandle session, string categoryId)
        {
            var filter = Builders<Kategori>.Filter.Eq(c => c.Id, categoryId);
            return _collection.DeleteOneAsync(session, filter);
        }
    }
}
