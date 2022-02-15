﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace TrpgDiceBot
{
	static class Coc6CommandProcess
	{
		public static async Task Execute(SocketUserMessage msg, string command)
		{
			command.TrimStart();
			Console.WriteLine(command);

			// ランダムなステータスを表示
			if(string.IsNullOrEmpty(command))
			{
				await Coc6CharacterCreate.Create(msg);
				return;
			}
			string[] cmd_unit = command.Split(',');
			string cmd_top = cmd_unit[0].ToLower().Trim();
			// ランダムなステータスを表示
			if(cmd_unit[0].ToLower().Trim() == "randomstatus")
			{
				await Coc6CharacterCreate.Create(msg);
				return;
			}
			// キャラクリ
			else if(cmd_top == "chcreate" || cmd_top == "characreate")
			{
				if (!UserManager.Users.ContainsKey(msg.Author.Id))
				{
					UserManager.Users.Add(msg.Author.Id, new UserData(1));
				}
				else
				{
					UserManager.Users[msg.Author.Id].IncrementCount();
				}
				CocCharacterSheetManager.Create(msg.Author.Id, cmd_unit[1].Trim());
				return;
			}
			// キャラターデータをファイルに保存
			else if(cmd_top == "export")
			{
				CocCharacterSheetManager.Export(msg.Author.Id, cmd_unit[1].Trim());

				return;
			}
		}
	}
}
