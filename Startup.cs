using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using telegram_bot.Base;
using Telegram.Bot;

namespace telegram_bot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        TelegramBotClient InitBot()
        {
            var bot = new TelegramBotClient(Configuration["telegram_key"]);
            bot.SetWebhookAsync(Configuration["telegram_webhook"]).Wait();
            var me = bot.GetMeAsync().Result;
            Console.WriteLine($"Hello! My name is {me.FirstName}");
            return bot;
        }

	    CommandsContainer InitCommands()
	    {
		    var dict = Assembly.GetExecutingAssembly().GetTypes().Where(r => r.IsClass 
		                                                              && typeof(MessageBase).IsAssignableFrom(r)
		                                                              && r != typeof(MessageBase))
			    .ToDictionary(pair => pair.Name.ToLower());
			return new CommandsContainer(dict);
			
	    }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICommandsContainer>(InitCommands());
            services.AddSingleton(InitBot());
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
