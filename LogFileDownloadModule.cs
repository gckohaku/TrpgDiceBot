using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Text;
using System.Text.RegularExpressions;
using Discord;
using Discord.Commands;
using System.Linq;
using System.Net;
using System.IO;

namespace TrpgDiceBot
{
	[Group("botlog")]
	public class LogFileDownloadModule : ModuleBase<SocketCommandContext>
	{
		private string _lastChoiceDay = "20200101";

		[Command("view")]
		public async Task ViewAsync(string day)
		{
			if(!Directory.Exists(MyLogger._logDirectory + day))
			{
				return;
			}

			IEnumerable<string> log_files = Directory.EnumerateFiles(MyLogger._logDirectory + day);

			string send_msg = "```\n";
			int idx = 1;
			foreach(string file in log_files)
			{
				send_msg += idx + ". " + file.Split("\\")[^1] + "\n";
				idx++;
			}

			await Context.Channel.SendMessageAsync(send_msg + "```");
		}

		[Command("download")]
		public async Task DownloadAsync(string day, int idx)
		{
			if (!Directory.Exists(MyLogger._logDirectory + day))
			{
				return;
			}

			await Context.Channel.SendFileAsync(Directory.EnumerateFiles(MyLogger._logDirectory + day).ToList<string>()[idx - 1]);
		}
	}
}
