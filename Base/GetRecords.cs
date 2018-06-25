using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace telegram_bot.Base
{
    public class GetRecords : MessageBase
    {
        public class Car
        {
            [BsonId]
            public ObjectId Id { get; set; }
            public string Make { get; set; }
            public string Model { get; set; }
        }

        public override string Execute(string[] args)
        {
            string connectionString = "mongodb://mongodb:27017";
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase("cars");
            var collection = database.GetCollection<Car>("cars");

            return collection.FindSync(r => true).ToList().Count.ToString();
        }
    }
}