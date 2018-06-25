using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Car : MongoEntity
{
    public string Make { get; set; }
    public string Model { get; set; }
     public int Page { get; set; }
     public bool Complete { get; set; }
}