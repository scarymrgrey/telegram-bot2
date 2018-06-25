using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Car : MongoEntity
{
    public string Make { get; set; }
    public string Model { get; set; }
}