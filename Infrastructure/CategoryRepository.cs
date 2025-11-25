using MongoDB.Driver;
using OruMongoDB.Domain;

namespace OruMongoDB.Infrastructure
{
    public class CategoryRepository : MongoRepository<Kategori>, ICategoryRepository
    {
        public CategoryRepository(IMongoDatabase database) : base(database, "Kategorier") { }

        public Task InsertAsync(Kategori category) =>
            _collection.InsertOneAsync(category);

        public Task UpdateCategoryNameAsync(string categoryId, string newName)
        {
            var filter = Builders<Kategori>.Filter.Eq(c => c.Id, categoryId);
            var update = Builders<Kategori>.Update.Set(c => c.Namn, newName);
            return _collection.UpdateOneAsync(filter, update);
        }

        public Task DeleteCategoryAsync(string categoryId)
        {
            var filter = Builders<Kategori>.Filter.Eq(c => c.Id, categoryId);
            return _collection.DeleteOneAsync(filter);
        }

        // Transaction-aware overloads
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
