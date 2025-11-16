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
    }
}
