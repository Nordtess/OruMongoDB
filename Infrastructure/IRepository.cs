using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

/*
 Summary
 -------
 Generic repository contract for MongoDB Atlas–backed entities.
 - Provides async CRUD operations using the MongoDB .NET driver.
 - Requires session-aware overloads for ACID transactions on writes (insert/update/delete).
 - Keeps data access behind an interface to support layering and testability.
*/

namespace OruMongoDB.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // Reads (non-transactional)
        Task<TEntity> GetByIdAsync(string id);
        Task<IEnumerable<TEntity>> GetAllAsync();

        // Writes (transactional only)
        Task AddAsync(IClientSessionHandle session, TEntity entity);
        Task UpdateAsync(IClientSessionHandle session, string id, TEntity entity);
        Task DeleteAsync(IClientSessionHandle session, string id);
    }
}