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
            var hasOne = collection
            .Find(r => r.Make == args[0] && r.Model == args[1])
            .Any();
            if (hasOne)
                return "Already exist!";

            var resp = HttpGet($"https://www.avito.ru/moskovskaya_oblast/avtomobili/{args[0]}/{args[1]}");
            if (string.IsNullOrWhiteSpace(resp) || !resp.Contains("item-description-title-link"))
                return "No such car";

            collection.InsertOne(new Car()
            {
                Make = args[0],
                Model = args[1],
                Page = 0,
                Complete = false
            });
            return "Success";
        }
    }
}