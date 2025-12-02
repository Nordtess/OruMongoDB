using MongoDB.Driver;
using OruMongoDB.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

/*
 Summary
 -------
 Defines the repository contract for podcast episodes (PoddAvsnitt) backed by MongoDB Atlas.
 - Interface exposes async CRUD helpers and feed-scoped operations.
 - Implementation uses the official MongoDB .NET driver and participates in ACID transactions via session-aware writes.
 - Keeps data access isolated behind interfaces to align with layering and testability.
*/

namespace OruMongoDB.Infrastructure
{
    public interface IPoddAvsnittRepository : IRepository<PoddAvsnitt>
    {
        // Bulk insert within an active transaction session.
        Task AddRangeAsync(IClientSessionHandle session, IEnumerable<PoddAvsnitt> entities);

        // Query all episodes for a given feed (non-transactional read).
        Task<List<PoddAvsnitt>> GetByFeedIdAsync(string feedId);

        // Delete all episodes for a given feed within a transaction session.
        Task DeleteByFeedIdAsync(IClientSessionHandle session, string feedId);
    }
}