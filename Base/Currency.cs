using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace telegram_bot.Base
{
	public class Currency : MessageBase
	{
		public override string Execute(string[] args)
		{
			var resp = HttpGet("https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency=USD&to_currency=RUB&apikey=Y23JNQ72WZZMP9WT");
			var json = JsonConvert.DeserializeObject<dynamic>(resp);
			var rate = json["Realtime Currency Exchange Rate"]["5. Exchange Rate"];
			return $"Доллар стоит {rate} российских рубчика.";
		}
	}
}