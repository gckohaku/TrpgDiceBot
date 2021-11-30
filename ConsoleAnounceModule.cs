using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Text.RegularExpressions;
using Discord;
using Discord.Commands;
using System.Linq;

namespace TrpgDiceBot
{
	public class ConsoleAnounceModule : ModuleBase<SocketCommandContext>
	{
		[Command("consoleanounce")]
		[Alias("c_a")]
		public async Task ConsoleAnounceAsync(string sender, [Remainder] string str)
		{
			Console.WriteLine("\n" + sender + ">\n" + str + "\n");
		}
	}
}
