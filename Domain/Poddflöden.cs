using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OruMongoDB.Domain
{
    
    public class Poddflöden
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] 
        public string? Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public string rssUrl { get; set; } = string.Empty;
        public string displayName { get; set; } = string.Empty;
        public string categoryId { get; set; } = string.Empty;

        public bool IsSaved { get; set; } = false;
        public DateTime? SavedAt { get; set; }
    }
}
