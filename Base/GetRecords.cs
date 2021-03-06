﻿using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace telegram_bot.Base
{
    public class GetRecords : MessageBase
    {
        public override string Execute(string[] args)
        {
            var collection = database.GetCollection<Car>("cars");

            return collection.FindSync(r => true).ToList().Count.ToString();
        }
    }
}