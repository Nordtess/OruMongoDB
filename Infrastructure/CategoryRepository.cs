using MongoDB.Driver;
using OruMongoDB.Domain;

namespace OruMongoDB.Infrastructure
{
    public class CategoryRepository : MongoRepository<Kategori>
    {
        public CategoryRepository(IMongoDatabase database)
            : base(database, "Kategorier")
        {
        }

        
        public Task InsertAsync(Kategori category)
        {
            return _collection.InsertOneAsync(category);
        }

        public async Task UpdateCategoryNameAsync(string categoryId, string newName)
        {
            var filter = Builders<Kategori>.Filter.Eq(c => c.Id, categoryId);
            var update = Builders<Kategori>.Update.Set(c => c.Namn, newName);

            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteCategoryAsync(string categoryId)
        {
            var filter = Builders<Kategori>.Filter.Eq(c => c.Id, categoryId);
            await _collection.DeleteOneAsync(filter);
        }
    }
}
