using MongoDB.Driver;
using OruMongoDB.Domain;

namespace OruMongoDB.Infrastructure
{
   
    public class MemberRepository : MongoRepository<Member>, IMemberRepository
    {
        public MemberRepository(IMongoDatabase database)
            : base(database, "Members")
        {
        }
    }
}