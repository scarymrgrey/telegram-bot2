using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

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
