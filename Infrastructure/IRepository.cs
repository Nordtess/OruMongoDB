using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

/*
 Summary
 -------
 Generic repository contract for MongoDB Atlas–backed entities.
 - Provides async CRUD operations using the MongoDB .NET driver.
 - Includes session-aware overloads to participate in ACID transactions
 (insert/update/delete) when a client session is provided.
 - Keeps data access behind an interface to support layering and testability.
*/

namespace OruMongoDB.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // Reads
        Task<TEntity> GetByIdAsync(string id);
        Task<IEnumerable<TEntity>> GetAllAsync();

        // Creates
        Task AddAsync(TEntity entity);
        Task AddAsync(IClientSessionHandle session, TEntity entity);

        // Updates
        Task UpdateAsync(string id, TEntity entity);
        Task UpdateAsync(IClientSessionHandle session, string id, TEntity entity);

        // Deletes
        Task DeleteAsync(string id);
        Task DeleteAsync(IClientSessionHandle session, string id);
    }
}