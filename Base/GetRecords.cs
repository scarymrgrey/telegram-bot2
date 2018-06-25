using System.Linq;
using MongoDB.Driver;

namespace telegram_bot.Base
{
	public class GetRecords : MessageBase
	{
		public class Car
		{
			public string Make { get; set; }
			public string Model { get; set; }
		}

		public override string Execute(string[] args)
		{
			string connectionString = "mongodb://scarymrgrey:Incoding,1234@mongodb:27017";
			MongoClient client = new MongoClient(connectionString);
			IMongoDatabase database = client.GetDatabase("cars");
			var collection = database.GetCollection<Car>("cars");
			
			return collection.FindSync(r => true).ToList().Count.ToString();
		}
	}
}