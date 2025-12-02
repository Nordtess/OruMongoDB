using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

/*
 Summary
 -------
 Domain model representing a podcast feed (Poddflöden) stored in MongoDB Atlas.
 Fields:
 - Id: BSON ObjectId (string) primary key, generated client-side.
 - rssUrl: Source RSS URL used to fetch episodes.
 - displayName: User-assigned or parsed name of the feed.
 - categoryId: Optional category reference (empty string = no category).
 - IsSaved: Flag indicating persistence state (true once stored in DB).
 - SavedAt: UTC timestamp when persisted (null until saved).
*/

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
