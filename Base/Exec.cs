using System;
using System.Diagnostics;

namespace telegram_bot.Base
{
	public class Exec : MessageBase
	{
		public override string Execute(string[] args)
		{
			ProcessStartInfo startInfo = new ProcessStartInfo() { FileName = "/bin/bash", Arguments = String.Join(" ",args) };
			Process proc = new Process() { StartInfo = startInfo, };
			proc.Start();
			var strOutput = proc.StandardOutput.ReadToEnd();
			//Wait for process to finish
			proc.WaitForExit(30000);
			return strOutput;
		}
	}
}