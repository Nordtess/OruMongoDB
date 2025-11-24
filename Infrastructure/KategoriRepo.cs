using MongoDB.Driver;
using OruMongoDB.Infrastructure;
using OruMongoDB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OruMongoDB.Infrastructure
{
    public class CategoryRepository : MongoRepository<Kategori>
    {
        public CategoryRepository(IMongoDatabase database)
            : base(database, "Kategorier")
        {
        }

        public async Task UpdateCategoryNameAsync(string categoryId, string newName)
        {
            var filter = Builders<Kategori>.Filter.Eq(c => c.Id, categoryId);
            var update = Builders<Kategori>.Update
                .Set(c => c.Namn, newName)
                .Set("name", newName);

            await _collection.UpdateOneAsync(filter, update);
        }

    }
}