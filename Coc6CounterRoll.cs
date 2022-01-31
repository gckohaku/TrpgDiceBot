using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TrpgDiceBot
{
	static class Coc6CounterRoll
	{
		public static string Roll(in Match m)
		{
			int this_side = int.Parse(m.Groups["this"].Value);
			int opponent_side = int.Parse(m.Groups["opponent"].Value);

			return GenerateResultString(this_side, opponent_side);
		}

		public static string Roll(in int thisSide, in int opponentSide)
		{
			return GenerateResultString(thisSide, opponentSide);
		}

		private static string GenerateResultString(in int thisSide, in int opponentSide)
		{
			int success_line = (50 + (thisSide - opponentSide) * 5);
			string ret_str = "Counter<=" + success_line + " -> ";
			int res = TwoD10Dices1d100DiceRoll.RollInt();

			ret_str += res.ToString() + "\n\n";

			if (res <= success_line)
			{
				ret_str += "SUCCESS!!!";
			}
			else
			{
				ret_str += "Failed...";
			}

			return ret_str;
		}
	}
}
