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
			else if(cmd_top == "setstatus")
			{
				ulong user_id = msg.Author.Id;
				UserData user = UserManager.Users[user_id];
				string ret_str = msg.Author.Mention + "\n";
				Coc6CharacterSheet sheet = CocCharacterSheetManager.Sheets[user_id][user.CurrentSettingCharaId];
				Coc6CharacterStatus status = sheet.Statuses;

				for(int i = 1; i < cmd_unit.Length; i++)
				{
					string[] cmd_pair = cmd_unit[i].Split(":");
					ret_str += sheet.Statuses.SetStatus(cmd_pair[0].Trim().ToUpper(), cmd_pair[1].Trim());
				}

				await msg.Channel.SendMessageAsync(ret_str + "ステータスの変更が完了しました");

				CocCharacterSheetManager.ExportAll(user_id);

				return;
			}
			// Discord 側で選択中のキャラクターのステータスの表示
			else if(cmd_top == "viewstatus")
			{
				ulong id = msg.Author.Id;

				await msg.Channel.SendMessageAsync(msg.Author.Mention + "\n" + CocCharacterSheetManager.Sheets[id][UserManager.Users[id].CurrentSettingCharaId].CharacterName + "\n```\n" + CocCharacterSheetManager.StatusesToString(id, UserManager.Users[id].CurrentSettingCharaId) + "\n```");

				return;
			}
			// 選択するキャラを変更
			else if (cmd_top == "cred" || cmd_top == "currentedit")
			{
				UserData user = UserManager.Users[msg.Author.Id];
				List<Coc6CharacterSheet> sheet = CocCharacterSheetManager.Sheets[msg.Author.Id];

				int data;
				if (int.TryParse(cmd_unit[1], out data)){
					user.CurrentSettingCharaId = data - 1;
				}
				else
				{
					user.CurrentSettingCharaId = sheet.Find(s => s.CharacterName == cmd_unit[1]).CharacterIndex;
				}
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
