using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace telegram_bot
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args)
		{
			var config = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json",  false)
				.Build();

			return WebHost
			   .CreateDefaultBuilder(args)
			   .UseUrls(config["Host"])
			   .UseStartup<Startup>();
		}

	}
}
