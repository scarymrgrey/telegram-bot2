using System;
using System.Collections.Generic;

namespace telegram_bot.Base
{
	public class CommandsContainer : ICommandsContainer
	{
		public Dictionary<string, Type> _commands;

		public CommandsContainer(Dictionary<string, Type> commands)
		{
			_commands = commands;
		}

		public Dictionary<string, Type> GetCommands()
		{
			return _commands;
		}
	}
}