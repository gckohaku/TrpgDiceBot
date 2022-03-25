using System;
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
			if(cmd_top == "randomstatus")
			{
				await Coc6CharacterCreate.Create(msg);
				return;
			}
			// キャラクリ
			else if(cmd_top == "chcreate" || cmd_top == "characreate")
			{
				UserManager.IncrementCount(msg.Author.Id);
				CocCharacterSheetManager.Create(msg.Author.Id, cmd_unit[1].Trim());
				return;
			}
			// 現在作成しているキャラクターを表示
			else if(cmd_top == "viewch" || cmd_top == "viewchara" || cmd_top == "viewcharas" || cmd_top == "viewcharacters")
			{
				List<Coc6CharacterSheet> sheets = CocCharacterSheetManager.Sheets[msg.Author.Id];
				string ret_str = msg.Author.Mention + "\n";
				if(sheets.Count == 0)
				{
					await msg.Channel.SendMessageAsync(ret_str + "作成されたキャラクターはまだありません");
					return;
				}
				ret_str += "```\n";
				for (int i = 0; i < sheets.Count; i++)
				{
					ret_str += (i + 1) + " : " + sheets[i].CharacterName + "\n";
				}
				await msg.Channel.SendMessageAsync(ret_str + "```");
			}
			// ステータスの設定

			// Discord 側で選択中のキャラクターのステータスの表示
			else if(cmd_top == "viewstatus")
			{
				ulong id = msg.Author.Id;

				await msg.Channel.SendMessageAsync(msg.Author.Mention + "\n" + CocCharacterSheetManager.Sheets[id][UserManager.Users[id].CurrentSettingCharaId].CharacterName + "\n```" + CocCharacterSheetManager.StatusesToString(id, UserManager.Users[id].CurrentSettingCharaId) + "```");
			}
			// キャラターデータをファイルに保存
			else if(cmd_top == "export")
			{
				CocCharacterSheetManager.Export(msg.Author.Id, cmd_unit[1].Trim());

				return;
			}
			// テスト用　ユーザーデータの出力
			else if (cmd_top == "udexp")
			{
				UserManager.Export();

				return;
			}
		}
	}
}
