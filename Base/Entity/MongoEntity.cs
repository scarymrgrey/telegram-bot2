using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class MongoEntity
{
    [BsonId]
    public ObjectId Id { get; set; }
}
