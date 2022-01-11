/*
 *	説明:
 *	振ろうとしているダイスに問題がある場合に呼び出される
 *	その問題の内容を Discord のチャット欄に表示する
 */

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
	static class DiceException
	{
		static public async Task TooMuchDiceValue(ISocketMessageChannel channel, SocketUserMessage msg)
		{
			await channel.SendMessageAsync(msg.Author.Mention + "\nパソコンを破壊する気．．．？");
			return;
		}

		static public async Task TooMuchDiceSide(ISocketMessageChannel channel, SocketUserMessage msg)
		{
			await channel.SendMessageAsync(msg.Author.Mention + "\nんん．．．流石に面の数が多すぎるかな．．．");
			return;
		}
	}
}
