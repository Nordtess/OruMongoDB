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
        /// <summary>MongoDB ObjectId primary key (string form).</summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = ObjectId.GenerateNewId().ToString();

        /// <summary>Original RSS feed URL.</summary>
        public string rssUrl { get; set; } = string.Empty;

        /// <summary>Display name (can be customized by user).</summary>
        public string displayName { get; set; } = string.Empty;

        /// <summary>Category reference (empty string indicates no category assigned).</summary>
        public string categoryId { get; set; } = string.Empty;

        /// <summary>True once the feed and episodes have been stored in MongoDB.</summary>
        public bool IsSaved { get; set; } = false;

        /// <summary>UTC timestamp when feed was saved. Null until persisted.</summary>
        public DateTime? SavedAt { get; set; }
    }
}
