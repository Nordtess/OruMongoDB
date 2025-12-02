using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

/*
 Summary
 -------
 Domain model representing a podcast category stored in MongoDB Atlas.
 - Id: BSON ObjectId string, generated client-side for convenience.
 - Namn: Category display name (persisted as field "Namn").
 BsonIgnoreExtraElements allows forward-compatible schema changes without deserialization errors.
*/

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
