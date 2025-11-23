using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OruMongoDB.Domain
{

    public class Kategori
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public string Namn { get; set; } = string.Empty;

        [BsonElement("name")]
        public string LegacyName
        {
            get => Namn;     
            set => Namn = value;
    }
}
}