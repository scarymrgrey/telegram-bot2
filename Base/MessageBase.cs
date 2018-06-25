using MongoDB.Driver;

namespace telegram_bot.Base
{
	public abstract class MessageBase
	{
		protected IMongoDatabase database{get;set;}
		public MessageBase(){
			string connectionString = "mongodb://mongodb:27017";
			MongoClient client = new MongoClient(connectionString);
			database = client.GetDatabase("cars");
		}
		public abstract string Execute(string[] args);
		
	}
}