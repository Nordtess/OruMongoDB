using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OruMongoDB.Domain
{
   
    public class PoddAvsnitt
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public string feedId { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string publishDate { get; set; } = string.Empty;
        public string link { get; set; } = string.Empty;
    }
}