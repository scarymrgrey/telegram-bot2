using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace telegram_bot.Base
{
    public class GetList : MessageBase
    {
        public override string Execute(string[] args)
        {
            var collection = database.GetCollection<Car>("cars");

            return string.Join("\r\n", collection
            .FindSync(r => true)
            .ToList()
            .Take(50)
            .Select(r => $"{r.Make}/{r.Model}"));
        }
    }
}