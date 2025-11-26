using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

/*
 Summary
 -------
 Domain model representing a single podcast episode (PoddAvsnitt) stored in MongoDB Atlas.
 Properties:
 - Id: BSON ObjectId (string form) primary key, generated client-side for early reference.
 - feedId: Foreign key linking to the parent Poddflöden document.
 - title: Episode title (plain text, cleaned upstream).
 - description: Episode description (HTML stripped upstream by HtmlCleaner before persistence).
 - publishDate: Date published (stored as string for existing schema compatibility; consider ISO8601 for future).
 - link: External URL to the episode.
 No behavior included; this is a simple data container consumed by repositories/services.
*/

namespace OruMongoDB.Domain
{
    public class PoddAvsnitt
    {
        /// <summary>
        /// MongoDB ObjectId as string primary key. Generated immediately so episodes can be referenced before insert.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = ObjectId.GenerateNewId().ToString();

        /// <summary>Parent feed identifier (ObjectId string) linking episode to its feed.</summary>
        public string feedId { get; set; } = string.Empty;

        /// <summary>Episode title.</summary>
        public string title { get; set; } = string.Empty;

        /// <summary>Plain-text episode description (HTML removed upstream).</summary>
        public string description { get; set; } = string.Empty;

        /// <summary>Publish date as string (schema legacy). Prefer ISO date or DateTime in future migrations.</summary>
        public string publishDate { get; set; } = string.Empty;

        /// <summary>External URL for the episode audio/web page.</summary>
        public string link { get; set; } = string.Empty;
    }
}