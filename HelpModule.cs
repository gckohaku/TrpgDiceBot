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
	[Group("help")]
	public class HelpModule : ModuleBase<SocketCommandContext>
	{
		[Command("")]
		public async Task HelpAsync()
		{
			await Context.Channel.SendMessageAsync(
				"この Bot に用意されているコマンドは以下の通りです。\n" +
				"`%help`\n" +
				"この文章を呼び出すコマンド。\n" +
				"`%version` `%ver`\n" +
				"バージョンの情報を表示します。\n" +
				"`%git`\n" +
				"github でソースコードが見れるページに行けます。\n" +
				"\n" +
				"また、ダイスに関しては以下のコマンドがあります。\n" +
				"`&1d10` など\n" +
				"ダイスを振ります。\n" +
				"`&p`\n" +
				"1D100 を振ることができます。\n" +
				"`&10DX@8` など\n" +
				"DX3 でのダイスを振ります。\n" +
				"`&2EM5` など\n" +
				"エモクロアでのダイスを振ります。\n" +
				"`&coc`\n" +
				"CoC でのキャラクターのステータス決めを自動で行います。\n" +
				"\n" +
				"それぞれのコマンドの詳しい説明は `%help 1d10` などで確認してください。(※現在全コマンドの help 未実装)"
				);
		}
	}
}
