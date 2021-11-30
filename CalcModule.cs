using System;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace TrpgDiceBot
{
	[Group("calc")]
	public class CalcModule : ModuleBase<SocketCommandContext>
	{
		[Command("")]
		public async Task CalcAsync([Remainder] string calc)
		{
			string p_calc = ConvertAsciiAndNoSpace.Convert(calc);

			int res = InterpretFormula.Calc(p_calc);

			await Context.Channel.SendMessageAsync(Context.User.Mention + "\n" + res);
		}
	}
}
