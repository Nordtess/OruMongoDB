using MongoDB.Driver;
using OruMongoDB.Domain;
using System.Threading.Tasks;

/*
 Summary
 -------
 Contract for category persistence operations backed by MongoDB Atlas.
 - Non-transactional reads are allowed.
 - Transaction-aware overloads that accept IClientSessionHandle are required for ACID writes.
 - Keeps data-access concerns behind an interface to support testability and clear layering.
*/

namespace OruMongoDB.Infrastructure
{
    public interface ICategoryRepository : IRepository<Kategori>
    {
        // Non-transactional reads come from IRepository (GetAllAsync/GetByIdAsync)

        // Transaction-aware writes (to be used within an active session)
        Task InsertAsync(IClientSessionHandle session, Kategori category);
        Task UpdateCategoryNameAsync(IClientSessionHandle session, string categoryId, string newName);
        Task DeleteCategoryAsync(IClientSessionHandle session, string categoryId);
    }
}