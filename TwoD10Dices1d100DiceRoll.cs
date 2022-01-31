using System;
using System.Collections.Generic;
using System.Text;

namespace TrpgDiceBot
{
	static class TwoD10Dices1d100DiceRoll
	{
		/// <summary>
		/// ２つの D10 ダイスを使って 1D100 を振った結果を文字列で返す
		/// </summary>
		/// <returns>"1" ～ "100" の数字の文字列を返す</returns>
		public static string Roll()
		{
			string ret_str = RandomManager.Rand(10).ToString() + RandomManager.Rand(10).ToString();

			if(ret_str[0] == '0')
			{
				if(ret_str[1] == '1')
				{
					return "100";
				}
				else
				{
					return ret_str[1].ToString();
				}
			}

			return ret_str;
		}

		/// <summary>
		/// ２つの D10 ダイスを使って 1D100 を振った結果を整数で返す
		/// </summary>
		/// <returns> 1 ～ 100 の数字を int 型で返す</returns>
		public static int RollInt()
		{
			int ret = RandomManager.Rand(10) * 10 + RandomManager.Rand(10);

			if(ret == 0)
			{
				return 100;
			}

			return ret;
		}
	}
}
