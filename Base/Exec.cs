using System;
using System.Diagnostics;

namespace telegram_bot.Base
{
	public class Exec : MessageBase
	{
		static string ExecuteBashCommand(string command)
		{
			// according to: https://stackoverflow.com/a/15262019/637142
			// thans to this we will pass everything as one command
			command = command.Replace("\"", "\"\"");

			var proc = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "/bin/bash",
					Arguments = "-c \"" + command + "\"",
					UseShellExecute = false,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				}
			};

			proc.Start();
			proc.WaitForExit(5000);

			return proc.StandardOutput.ReadToEnd();
		}

		public override string Execute(string[] args)
		{
			return ExecuteBashCommand(String.Join(" ", args));
		}
	}
}