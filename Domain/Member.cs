using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

/*
 Summary
 -------
 Domain model representing an application member stored in MongoDB Atlas.
 Properties:
 - Id: BSON ObjectId string primary key (generated client-side for early reference).
 - Namn: Member display name.
 - Epost: Email address (plain string; validation performed in higher layers).
 - Losenord: Password placeholder (should store a salted hash, not plain text; handled upstream).
 Design:
 - Uses BSON attributes for explicit Id representation and forward-compatible schema mapping.
 - No business logic here; pure data container consumed by services/repositories.
*/

namespace OruMongoDB.Domain
{
 public class Member
 {
 /// <summary>
 /// MongoDB ObjectId as string primary key. Generated immediately for referencing before persistence.
 /// </summary>
 [BsonId]
 [BsonRepresentation(BsonType.ObjectId)]
 public string? Id { get; set; } = ObjectId.GenerateNewId().ToString();

 /// <summary>Member display name.</summary>
 public string Namn { get; set; } = string.Empty;

 /// <summary>Member email address (validate format in service/UI layer).</summary>
 public string Epost { get; set; } = string.Empty;

 /// <summary>Password (expected to be a hashed value upstream; never store raw secrets).</summary>
 public string Losenord { get; set; } = string.Empty;
 }
}