using MongoDB.Driver;
using OruMongoDB.Domain;
using System.Threading.Tasks;

/*
 Summary
 -------
 Repository contract for podcast feeds (Poddflöden) backed by MongoDB Atlas.
 - Async queries/updates via the official MongoDB .NET driver.
 - Enforces ACID transactions for writes (session required).
 - Keeps data access behind interfaces to support layering and testability.
*/

namespace OruMongoDB.Infrastructure
{
    public interface IPoddflodeRepository : IRepository<Poddflöden>
    {
        // Non-transactional reads
        Task<Poddflöden> GetByUrlAsync(string rssUrl);
        Task<Poddflöden> GetByUrlAsync(IClientSessionHandle session, string rssUrl);

        // Transactional writes (session required)
        Task UpdateCategoryAsync(IClientSessionHandle session, string poddId, string categoryId);
        Task DeleteByIdAsync(IClientSessionHandle session, string id);
    }
}
