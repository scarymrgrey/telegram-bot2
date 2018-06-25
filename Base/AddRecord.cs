using System.Linq;
using MongoDB.Driver;

namespace telegram_bot.Base
{
	public class AddRecord : MessageBase
	{
		public override string Execute(string[] args)
		{
			if (args == null || args.Length != 2)
				return "Failed: required valid args";

			var collection = database.GetCollection<Car>("cars");
			collection.InsertOne(new Car(){Make = args[0],Model = args[1]});
			return "Success";
		}
	}
}