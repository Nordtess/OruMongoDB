using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OruMongoDB.Domain
{
    
    public class Member
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public string Namn { get; set; } = string.Empty;
        public string Epost { get; set; } = string.Empty;
        public string Losenord { get; set; } = string.Empty;
    }
}