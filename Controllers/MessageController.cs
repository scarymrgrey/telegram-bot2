﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
	        var message = JsonConvert.SerializeObject(update.Message);
	        if (update.Message.From.Username != "scarymrgrey")
		        await _bot.SendTextMessageAsync(chatId, "Hello, Shkurochka! Who are you?", replyToMessageId: messageId);
	        else
		        await _bot.SendTextMessageAsync(chatId, $"Hello, Scary!", replyToMessageId: messageId);
        }
        
        [HttpGet]
        public string Get()
        {
            return "Ok!";
        }
    }
}