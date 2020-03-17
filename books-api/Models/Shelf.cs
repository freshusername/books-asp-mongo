using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace books_api.Models
{
    public class Shelf
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string ShelfName { get; set; }

    }
}
