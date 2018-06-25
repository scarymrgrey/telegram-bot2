using System.Linq;

namespace telegram_bot.Base
{
	public class GetArgs : MessageBase
	{
		public override string Execute(string[] args)
		{
			return args.Aggregate((a,b)=> $"{a} {b}");
		}
	}
}