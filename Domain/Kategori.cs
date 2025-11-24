using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OruMongoDB.Domain
{
    [BsonIgnoreExtraElements] 
    public class Kategori
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        
        [BsonElement("Namn")]
        public string Namn { get; set; } = string.Empty;
    }
}
