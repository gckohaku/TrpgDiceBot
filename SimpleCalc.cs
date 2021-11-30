using System;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace TrpgDiceBot
{
	static class SimpleCalc
	{
		private delegate void ProcessTargetDelegate(ref int tar, int val);

		private static Dictionary<string, ProcessTargetDelegate> ProcessTaarget = new Dictionary<string, ProcessTargetDelegate>();

		public static int ExecuteInt(string str)
		{
			ProcessTaarget["+"] = ProcessTaargetAdd;
			ProcessTaarget["-"] = ProcessTaargetSub;
			ProcessTaarget["*"] = ProcessTaargetMul;
			ProcessTaarget["/"] = ProcessTaargetDiv;

			int res = int.Parse(Regex.Match(str, @"(?i)^\d+").Value);

			MatchCollection fixs = Regex.Matches(str, @"(?i)(?<=\d+\s*)(?<ope>(\+|\-|\*|/))\p{IsCJKUnifiedIdeographs}*(?<val>\d+)");

			foreach (Match m in fixs)
			{
				ProcessTaarget[m.Groups["ope"].Value](ref res, int.Parse(m.Groups["val"].Value));
			}

			return res;
		}

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
	}
}
