using System;
using System.Linq;
using MongoDB.Driver;

namespace telegram_bot.Base
{
    public class CarDTO
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public bool Loaded { get; set; }
    }
    public class GetRate : MessageBase
    {
        public override string Execute(string[] args)
        {
            var collection = database.GetCollection<Car>("cars")
            .Find(_ => true)
            .ToList()
            .Select(r => r.Make).Distinct();

            var res = collection.Select(make =>
            {
                var all = database.GetCollection<CarDTO>(make).Count(_ => true);
                var loaded = database.GetCollection<CarDTO>(make).Count(r => r.Loaded == true);
                return new Tuple<Int64, Int64>(loaded, all);
            }).Aggregate((acc, curr) => new Tuple<Int64, Int64>(acc.Item1 + curr.Item1, acc.Item2 + curr.Item2));

            return $"Current rate is: {res.Item1}/{res.Item2}";
        }
    }
}