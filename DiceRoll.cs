/*
 *	説明:
 *	ダイスロールの根幹部分
 *	先頭に "&" の文字があるメッセージに対して反応する
 *	
 *	問題点:
 *	このクラスのメインメソッドである "Execute" メソッドの内容が非常に長くなっていしまっている
 *		→メソッド分けすることを検討
 *			具体的にどのように？ :
 *				ダイスコマンドの解析部分、ルールが多くなればなるほどメソッド分けが有効。別のクラスに分けてしまうのも有効。
 *					→正規表現をコンパイルするクラスを作成することにした。
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
	static class DiceRoll
	{
		public static async Task Execute(SocketUserMessage msg)
		{
			string dice_area = msg.Content;

			if(dice_area == "")
			{
				return;
			}

			Console.WriteLine(msg.Content + "\n");

			// 全角文字を半角文字に
			dice_area = ConvertAsciiAndNoSpace.ConvertAscii(dice_area);

			// CoC Character Sheet Memory
#if DEBUG
			// memory test
			Match memory_match = Regex.Match(dice_area, @"(?i)^#mem\s(?<memory>.+)$");

			if (memory_match.Success)
			{
				MemoryTest.Memory(msg.Author.Id.ToString(), memory_match.Groups["memory"].Value);
				return;
			}

			Match coc_command_top_match = Regex.Match(dice_area, @"(?i)^#coc6");
#else
			Match coc_command_top_match = Regex.Match(dice_area, @"(?i)^&coc6");
#endif
			if (coc_command_top_match.Success)
			{

			}

			// スペースの除去
			dice_area = ConvertAsciiAndNoSpace.ConvertNoSpace(dice_area);

			if (dice_area == "33-4")
			{
				await msg.Channel.SendMessageAsync(msg.Author.Mention + "\n29");

				return;
			}

#if DEBUG
			dice_area = dice_area.Replace("＃", "#");
			if (dice_area[0] != '#')
#else
			dice_area = dice_area.Replace("＆", "&");
			if (dice_area[0] != '&')
#endif
			{
				return;
			}

			if (Regex.IsMatch(msg.ToString(), @"(?i)^&help$"))
			{
				await msg.Channel.SendMessageAsync(
					"実は本当のプレフィックスは `%` です。\n" +
					"コマンドは現在、`%help` `%version(ver)` `%git` `%calc` があります。"
					);

				return;
			}

			ProcessTaarget["+"] = ProcessTaargetAdd;

			ProcessTaarget["-"] = ProcessTaargetSub;

			ProcessTaarget["*"] = ProcessTaargetMul;

			ProcessTaarget["/"] = ProcessTaargetDiv;

			string send_msg = msg.Author.Mention + '\n';

			Match per_check = Regex.Match(dice_area, @"(?i)^&\s*(percent|per|p)");
			Match tar = Regex.Match(dice_area, @"(?i)(?<=^&\s*(percent|per|p)\s*)(?<minus>\-*)(?<val>\d+)");

			int finaly_tar = 0;


			if (per_check.Length != 0) {
				MatchCollection tar_fixs = Regex.Matches(dice_area, @"(?i)(?<=\d+\s*)(?<ope>(\+|\-|\*|/))(?<val>\d+)");
				dice_area = "1d100 tar=";
				if (tar.Length != 0)
				{
					finaly_tar = int.Parse(tar.Groups["val"].Value);

					if(tar.Groups["minus"].Value == "-")
					{
						finaly_tar = -finaly_tar;
					}

					foreach (Match m in tar_fixs)
					{
						ProcessTaarget[m.Groups["ope"].Value](ref finaly_tar, int.Parse(m.Groups["val"].Value));
					}

					dice_area += finaly_tar.ToString();
				}
			}

			ISocketMessageChannel channel = msg.Channel;

			RandomManager.ClearHistory();

			// キャラクリの位置
#if DEBUG
			Match create_coc_match = Regex.Match(dice_area, @"(?i)^#coc$");
#else
			Match create_coc_match = Regex.Match(dice_area, @"(?i)^&coc$");
#endif

			// コマンドが一致したらキャラクリをするよ
			if (create_coc_match.Success)
			{
				await Coc6CharacterCreate.Create(msg);
				return;
			}

			// ダイスコマンドの解析
			MatchCollection dices = Regex.Matches(dice_area, @"(?i)(?<sign>(\+|\-|))(?<value>\d+)(?<type>(d|r))(?<sides>\d+)((\[|@)(?<critical>\d+)(\]|))?");
			MatchCollection fixes = Regex.Matches(dice_area, @"(?i)(?<fix>(\+|\-)\d+)(?=(\+|\-|$))");
			MatchCollection dxs = Regex.Matches(dice_area, @"(?i)(?<sign>(\+|\-|))(?<value>\d+)dx((\[|@)(?<critical>\d+)(\]|))?");
			Match emo = Regex.Match(dice_area, @"(?i)(?<value>\d+)?(em|dm|emo){1}(<=|\s)?(?<success>\d+)");

			if (emo.Success)
			{
				await channel.SendMessageAsync(msg.Author.Mention + "\n" + EmokloreDiceRoll.Roll(emo));
				return;
			}

			Match target_match = Regex.Match(dice_area, @"(?i)(target|tar|trg|tgt)=(?<minus>\-*)(?<target>\d+)");

			if (target_match.Success)
			{
				int tar_val = int.Parse(target_match.Groups["minus"].Value + target_match.Groups["target"].Value);
				send_msg += "Target -> " + tar_val + "\n";

				if (tar_val <= 0)
				{
					await CocJudgeImageOutput.AutoFailAsync(msg);
					return;
				}else if(tar_val >= 100)
				{
					await CocJudgeImageOutput.AutoSuccessAsync(msg);
					return;
				}
			}

			// 数値がおかしくないかの確認
			foreach (Match m in dices)
			{
				if(Int64.Parse(m.Groups["value"].Value) > 10_000)
				{
					await channel.SendMessageAsync(msg.Author.Mention + "\nパソコンを破壊する気．．．？");
					return;
				}
				if(Int64.Parse(m.Groups["sides"].Value) > 1_000_000_000)
				{
					await channel.SendMessageAsync(msg.Author.Mention + "\nんん．．．流石に面の数が多すぎるかな．．．");
					return;
				}
			}

			foreach (Match m in dxs)
			{
				if (Int64.Parse(m.Groups["value"].Value) > 10_000)
				{
					await channel.SendMessageAsync(msg.Author.Mention + "\nパソコンを破壊する気．．．？");
					return;
				}
			}

			int d, r;
			d = r = 0;

			foreach (Match m in dices)
			{

				string res = "";

				if (m.Groups["type"].Value.ToLower() == "d")
				{
					res += NDSRoll(channel, int.Parse(m.Groups["value"].Value), int.Parse(m.Groups["sides"].Value), m.Groups["sign"].ToString() == "-");
					d++;
				}
				else if (m.Groups["type"].Value.ToLower() == "r")
				{
					if (m.Groups["critical"].Value == "")
					{
						res += NRSRoll(channel, int.Parse(m.Groups["value"].Value), int.Parse(m.Groups["sides"].Value));
					}
					else
					{
						res += NRSRoll(channel, int.Parse(m.Groups["value"].Value), int.Parse(m.Groups["sides"].Value), int.Parse(m.Groups["critical"].Value));
					}
					r++;
				}

				if (res.EndsWith("failed"))
				{
					return;
				}
				else
				{
					send_msg += res + '\n';
				}
			}

			foreach (Match m in dxs)
			{
				string res = "";

				if (m.Groups["critical"].Value == "")
				{
					res += NRSRoll(channel, int.Parse(m.Groups["value"].Value), 10);
				}
				else
				{
					res += NRSRoll(channel, int.Parse(m.Groups["value"].Value), 10, int.Parse(m.Groups["critical"].Value));
				}

				if (res.EndsWith("failed"))
				{
					return;
				}
				else
				{
					send_msg += res + '\n';
				}
				r++;
			}

			if(d + r == 0)
			{
				return;
			}

			int fix_sum = 0;

			foreach (Match m in fixes)
			{
				fix_sum += int.Parse(m.Groups["fix"].Value);
			}

			send_msg += "\nResult: " + RandomManager.DiceResultHistory.Sum(val => (Int64)val);

			if (fixes.Count != 0)
			{
				send_msg += ' ' + fix_sum.ToString("+ #;- #") + " -> " + (RandomManager.DiceResultHistory.Sum() + fix_sum);
			}

			if(target_match.Success)
			{
				string target = target_match.Groups["target"].Value;

				//if (5 >= (RandomManager.DiceResultHistory.Sum() + fix_sum) && int.Parse(target) >= (RandomManager.DiceResultHistory.Sum() + fix_sum))
				//{
				//	send_msg += "    _**__Critical!!!__**_";
				//}
				//else if(int.Parse(target) >= (RandomManager.DiceResultHistory.Sum() + fix_sum))
				//{
				//	send_msg += "    **Success**";
				//}
				//else if(95 >= (RandomManager.DiceResultHistory.Sum() + fix_sum))
				//{
				//	send_msg += "    **Fail**";
				//}
				//else
				//{
				//	send_msg += "    _**__Famble...__**_";
				//}

				await CocJudgeImageOutput.PutCocPDiceImageAsync(msg, RandomManager.DiceResultHistory.Sum() + fix_sum, int.Parse(target));

				RandomManager.ClearHistory();

				return;
			}

			if (send_msg.Length > 2000)
			{
				await TooLongStringAsync(msg, send_msg);
				return;
			}

			await msg.Channel.SendMessageAsync(send_msg);
		}

		// クトゥルフなど、とても一般的なダイスロール
		private static string NDSRoll(ISocketMessageChannel channel, int value, int sides, bool Negative = false)
		{
			// 戻り文字列初期化
			string ret_str = "";

			// 戻り文字列生成
			List<int> res = new List<int>();
			for (int i = 0; i < value; i++)
			{
				res.Add(RandomManager.Rand(sides, true, Negative));
			}

			ret_str += value + "D" + sides + " -> ";

			ret_str += GenerateDiceRollString(res, res.Count);

			ret_str += " -> " + res.Sum(val => (Int64)val);

			return ret_str;
		}

		// DX3 の特徴的なダイスロール
		private static string NRSRoll(ISocketMessageChannel channel, int value, int sides, int critical = 10, bool Negative = false)
		{
			critical = Math.Max(Math.Min(critical, sides), 2);

			string ret_str = "";

			// 一回目
			List<int> res = new List<int>();
			for (int i = 0; i < value; i++)
			{
				res.Add(RandomManager.Rand(sides, false));
			}

			int critical_value = res.Count(n => n >= critical);
			int ctimes = 0;
			int last_max = 0;
			res.Sort();

			ret_str += value + "DX" + (critical != 10 ? ("@" + critical) : "") + " -> ";

			// 2回目以降
			while (res.Count > 0)
			{
				if (critical_value > 0)
				{
					ctimes++;
				}

				ret_str += GenerateDiceRollString(res, res.Count, isCritical: true, critical: critical);

				last_max = res.Max();

				res.Clear();

				for (int i = 0; i < critical_value; i++)
				{
					res.Add(RandomManager.Rand(sides, false));
				}

				critical_value = res.Count(n => n >= critical);
				res.Sort();

				ret_str += " -> ";
			}

			int dice_res = ctimes * sides + last_max;

			ret_str += dice_res;

			RandomManager.DiceResultHistory.Add(dice_res);

			return ret_str;
		}

		private static string GenerateDiceRollString(List<int> diceRes, int count, bool isCritical = false, int critical = 0)
		{
			string ret_str = "[";

			for (int i = 0; i < count; i++)
			{
				if (isCritical && diceRes[i] >= critical)
				{
					ret_str += "**" + diceRes[i] + "**";
				}
				else
				{
					ret_str += diceRes[i];
				}

				if (i < count - 1)
				{
					ret_str += ", ";
				}
			}

			ret_str += "]";

			return ret_str;
		}

		private delegate void ProcessTargetDelegate(ref int tar, int val);

		private static Dictionary<string, ProcessTargetDelegate> ProcessTaarget = new Dictionary<string, ProcessTargetDelegate>();

		private static void ProcessTaargetAdd(ref int tar, int val)
		{
			tar += val;
		}

		private static void ProcessTaargetSub(ref int tar, int val)
		{
			tar -= val;
		}

		private static void ProcessTaargetMul(ref int tar, int val)
		{
			tar *= val;
		}

		private static void ProcessTaargetDiv(ref int tar, int val)
		{
			tar /= val;
		}

		public static async Task TooLongStringAsync(SocketUserMessage msg, string text)
		{
			string send_msg = "";

			if (text.Length > 5000)
			{
				send_msg += msg.Author.Mention + '\n';
				send_msg += "流石に文字数が多すぎるから、結果だけ伝えるね。\n";

				send_msg += "\nResult: " + RandomManager.DiceResultHistory.Sum(val => (Int64)val);

				await msg.Channel.SendMessageAsync(send_msg);
				return;
			}

			int cnt = 0;
			int unit_len = 1900;

			while(cnt < text.Length)
			{
				string unit = "";
				if(cnt != 0)
				{
					unit = "...";
				}

				if (cnt + unit_len >= text.Length)
				{
					unit += text.Substring(cnt, text.Length - cnt);
				}
				else
				{
					unit += text.Substring(cnt, unit_len);
					unit += "...";
				}

				await msg.Channel.SendMessageAsync(unit);

				cnt += unit_len;
			}
		}

		private static async Task AutomaticFailAsync(ISocketMessageChannel channel, string send_msg)
		{
			await channel.SendMessageAsync(send_msg + "**Fail** (自動失敗)");
		}

		private static async Task AutomaticSuccessAsync(ISocketMessageChannel channel, string send_msg)
		{
			await channel.SendMessageAsync(send_msg + "**Success** (自動成功)");
		}
	}
}
