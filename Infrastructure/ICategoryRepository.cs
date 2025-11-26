using MongoDB.Driver;
using OruMongoDB.Domain;
using System.Threading.Tasks;

/*
 Summary
 -------
 Contract for category persistence operations backed by MongoDB Atlas.
 - Async CRUD methods using the official MongoDB .NET driver.
 - Transaction-aware overloads that accept IClientSessionHandle to participate in ACID
 transactions for insert/update/delete.
 - Keeps data-access concerns behind an interface to support testability and clear layering.
*/

namespace OruMongoDB.Infrastructure
{
    public interface ICategoryRepository : IRepository<Kategori>
    {
        // Non-transactional operations
        Task InsertAsync(Kategori category);
        Task UpdateCategoryNameAsync(string categoryId, string newName);
        Task DeleteCategoryAsync(string categoryId);

        // Transaction-aware overloads (to be used within an active session)
        Task InsertAsync(IClientSessionHandle session, Kategori category);
        Task UpdateCategoryNameAsync(IClientSessionHandle session, string categoryId, string newName);
        Task DeleteCategoryAsync(IClientSessionHandle session, string categoryId);
    }
}