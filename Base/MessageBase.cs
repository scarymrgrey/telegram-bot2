﻿using System.IO;
using System.Net;
using MongoDB.Driver;

namespace telegram_bot.Base
{
	public abstract class MessageBase
	{
		internal string HttpGet(string uri)
		{
			var request = (HttpWebRequest)WebRequest.Create(uri);
			request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

			using (var response = (HttpWebResponse)request.GetResponse())
			using (var stream = response.GetResponseStream())
			using (var reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}
		protected IMongoDatabase database{get;set;}
		public MessageBase(){
			string connectionString = "mongodb://mongodb:27017";
			MongoClient client = new MongoClient(connectionString);
			database = client.GetDatabase("cars");
		}
		public abstract string Execute(string[] args);
		
	}
}