using System;
using System.Collections.Generic;

namespace telegram_bot.Base
{
	public interface ICommandsContainer
	{
		Dictionary<string, Type> GetCommands();
	}
}