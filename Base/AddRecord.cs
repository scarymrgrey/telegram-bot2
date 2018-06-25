using System.Linq;
using MongoDB.Driver;

namespace telegram_bot.Base
{
	public class AddRecord : MessageBase
	{
		public class Car
		{
			public string Make { get; set; }
			public string Model { get; set; }
		}

		public override string Execute(string[] args)
		{
			if (args == null || args.Length != 2)
				return "Failed: required valid args";

			string connectionString = "mongodb://mongodb:27017";
			MongoClient client = new MongoClient(connectionString);
			IMongoDatabase database = client.GetDatabase("cars");
			var collection = database.GetCollection<Car>("cars");
			collection.InsertOne(new Car(){Make = args[0],Model = args[1]});
			return "Success";
		}
	}
}