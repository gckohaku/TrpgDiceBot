using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TrpgDiceBot
{
	static class TrpgDiceEmoklore
	{
		public static string DiceRoll(in Match m)
		{
			int val;
			string s_val = m.Groups["value"].Value;
			if (s_val == "")
			{
				val = 1;
			}
			else
			{
				val = int.Parse(s_val);
			}

			if (val > 10000)
			{
				return "パソコンを破壊する気．．．？";
			}

			int success_line = int.Parse(m.Groups["success"].Value);

			if (success_line < 1)
			{
				success_line = 1;
			}
			else if (success_line > 10)
			{
				success_line = 10;
			}

			string ret_str = val + "EM<=" + success_line + " -> [";
			int success_value = 0;

			for (int i = 0; i < val; i++)
			{
				int r = RandomManager.Rand(10, false);

				if (r <= success_line)
				{
					ret_str += "**";
					success_value++;
					if (r == 1)
					{
						ret_str += "_" + r + "_";
						success_value++;
					}
					else
					{
						ret_str += r;
					}
					ret_str += "**";
				}
				else
				{
					if (r == 10)
					{
						ret_str += "__" + r + "__";
						success_value--;
					}
					else
					{
						ret_str += r;
					}
				}

				ret_str += ", ";
			}

			ret_str = ret_str[0..^2] + "]\n\nResult: " + success_value + "    " + GetResultString(success_value);

			return ret_str;
		}

		private static string GetResultString(in int sv)
		{
			if (sv < 0)
			{
				return "𝓕𝓾𝓶𝓫𝓵𝓮";
			}
			else if (sv == 0)
			{
				return "𝙴𝚛𝚛𝚘𝚛";
			}
			else if (sv == 1)
			{
				return "Single";
			}
			else if (sv == 2)
			{
				return "**Double**";
			}
			else if (sv == 3)
			{
				return "_**Triple**_";
			}
			else if (sv >= 4 && sv <= 9)
			{
				return "_**__MIRACLE__**_";
			}
			else
			{
				return "𝕮𝕬𝕿𝕬𝕾𝕿𝕽𝕺𝕻𝕳𝕰";
			}
		}
	}
}
