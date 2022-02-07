﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TrpgDiceBot
{
	static class Coc6DamageBonus
	{
		public static string Calc(int val)
		{
			if (val >= 2 && val <= 12)
			{
				return "-1D6";
			}
			if (val >= 13 && val <= 16)
			{
				return "-1D4";
			}
			if (val >= 17 && val <= 24)
			{
				return "+0";
			}
			if (val >= 25 && val <= 32)
			{
				return "+1D4";
			}
			else
			{
				return "+1d6";
			}
		}
	}
}
