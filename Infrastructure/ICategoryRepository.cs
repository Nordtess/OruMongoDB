using MongoDB.Driver;
using OruMongoDB.Domain;
using System.Threading.Tasks;

namespace OruMongoDB.Infrastructure
{
    public interface ICategoryRepository : IRepository<Kategori>
    {
        Task InsertAsync(Kategori category);
        Task UpdateCategoryNameAsync(string categoryId, string newName);
        Task DeleteCategoryAsync(string categoryId);

        // Transaction-aware overloads
        Task InsertAsync(IClientSessionHandle session, Kategori category);
        Task UpdateCategoryNameAsync(IClientSessionHandle session, string categoryId, string newName);
        Task DeleteCategoryAsync(IClientSessionHandle session, string categoryId);
    }
}