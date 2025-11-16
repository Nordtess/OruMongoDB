using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OruMongoDB.Infrastructure
{
    
    public interface IRepository<TEntity> where TEntity : class
    {
        
        Task<TEntity> GetByIdAsync(string id);
        Task<IEnumerable<TEntity>> GetAllAsync();

        
        Task AddAsync(TEntity entity);
        Task AddAsync(IClientSessionHandle session, TEntity entity);

        Task UpdateAsync(string id, TEntity entity);
        Task UpdateAsync(IClientSessionHandle session, string id, TEntity entity);

        Task DeleteAsync(string id);
        Task DeleteAsync(IClientSessionHandle session, string id);
    }
}