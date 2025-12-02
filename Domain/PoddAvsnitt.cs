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
*/

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