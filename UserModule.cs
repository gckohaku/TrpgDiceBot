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

namespace TrpgDiceBot
{
	[Group("user")]
	public class UserModule : ModuleBase<SocketCommandContext>
	{
		[Command("setusername")]
		[Alias("setname")]
		public async Task SetUserNameAsync([Remainder] string userName)
		{
			ulong userId = Context.Message.Author.Id;

			if (!UserManager.Users.ContainsKey(userId))
			{
				UserManager.Users.Add(userId, new UserData(0));
			}

			if (userName.Length > 100)
			{
				await Context.Channel.SendMessageAsync(Context.Message.Author.Mention + "\nさすがに長すぎるかな．．．");
			}
			else {
				UserManager.Users[userId].UserName = userName;
				await Context.Channel.SendMessageAsync(Context.Message.Author.Mention + "\n名前を設定したよ！");
			}

			UserManager.Export();
		}

		[Command("viewusername")]
		[Alias("viewname")]
		public async Task ViewUserNameAsync()
		{
			ulong userId = Context.Message.Author.Id;

			if (!UserManager.Users.ContainsKey(userId))
			{
				UserManager.Users.Add(userId, new UserData(0));
			}

			string userName = UserManager.Users[userId].UserName;

			if (userName.Length == 0)
			{
				await Context.Channel.SendMessageAsync(Context.Message.Author.Mention + "のユーザーネームはまだありません。");
				return;
			}
			await Context.Channel.SendMessageAsync(Context.Message.Author.Mention + "のユーザーネームは「" + userName + "」です。");
		}
	}
}
