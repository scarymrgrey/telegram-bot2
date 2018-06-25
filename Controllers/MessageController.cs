using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

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

        [HttpPost("update")]
        public async void Post([FromBody]Update update)
        {
            var chatId = update.Message.Chat.Id;
            var messageId = update.Message.MessageId;
            await _bot.SendTextMessageAsync(chatId, "Hello!", replyToMessageId: messageId);
        }
        
        [HttpGet]
        public string Get()
        {
            return "Ok!";
        }
    }
}
