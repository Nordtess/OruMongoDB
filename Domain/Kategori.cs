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
 /// <summary>
 /// MongoDB ObjectId as string primary key. Generated immediately to allow referencing before persistence.
 /// </summary>
 [BsonId]
 [BsonRepresentation(BsonType.ObjectId)]
 public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

 /// <summary>
 /// Category name (localized property name kept for existing schema). Stored as BSON element "Namn".
 /// </summary>
 [BsonElement("Namn")]
 public string Namn { get; set; } = string.Empty;
 }
}
