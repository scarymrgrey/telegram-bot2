using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace telegram_bot.Controllers
{
	[Route("api/message")]
	[ApiController]
	public class MessageController : ControllerBase
	{
		private TelegramBotClient _bot;
		public MessageController(TelegramBotClient bot)
		{
			_bot = bot;
		}

		string Get(string uri)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
			request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			using (Stream stream = response.GetResponseStream())
			using (StreamReader reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}

		[HttpPost("update")]
		public async void Post([FromBody]Update update)
		{
			var chatId = update.Message.Chat.Id;
			var messageId = update.Message.MessageId;
			var message = JsonConvert.SerializeObject(update.Message);
			var isCommand = update.Message?.Entities.Any(r => r.Type == MessageEntityType.BotCommand) ?? false;
			if (isCommand)
			{
				switch (update.Message.Text.ToLower())
				{
					case "//currency":
						var resp = Get(
							"https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency=USD&to_currency=RUB&apikey=Y23JNQ72WZZMP9WT");
						var json = JsonConvert.DeserializeObject<dynamic>(resp);
						var rate = json["Realtime Currency Exchange Rate"]["5. Exchange Rate"];
						await _bot.SendTextMessageAsync(chatId, $"Доллар стоит {rate} российских рубчика.");
						break;
					default:
						await _bot.SendTextMessageAsync(chatId, message);
						break;
				}


			}
			else
				await _bot.SendTextMessageAsync(chatId, message);

		}

		[HttpGet]
		public string Get()
		{
			return "Ok!";
		}
	}
}
