using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using telegram_bot.Base;
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
        private Dictionary<string, Type> commandsDict;
        public MessageController(TelegramBotClient bot, ICommandsContainer container)
        {
            _bot = bot;
            commandsDict = container.GetCommands();
        }


        [HttpPost("update")]
        public async void Post([FromBody]Update update)
        {
            try
            {
                var chatId = update.Message.Chat.Id;
                var messageId = update.Message.MessageId;
                var message = JsonConvert.SerializeObject(update.Message);
                var isCommand = update.Message?.Entities?.Any(r => r.Type == MessageEntityType.BotCommand) ?? false;
                if (isCommand)
                {
                    var all = update.Message.Text.ToLower().Split(' ');
                    var commandName = all.First().Replace("/", "");

                    if (!commandsDict.ContainsKey(commandName))
                        await _bot.SendTextMessageAsync(chatId, commandsDict.Keys.Aggregate((acc, res) => acc + $"\r\n/{res}"));

                    var command = Activator.CreateInstance(commandsDict[commandName]) as MessageBase;
                    await _bot.SendTextMessageAsync(chatId, command.Execute(all.Skip(1).ToArray()));
                }
                else
                    await _bot.SendTextMessageAsync(chatId, message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [HttpGet]
        public string Get()
        {
            return "Ok!";
        }
    }
}
