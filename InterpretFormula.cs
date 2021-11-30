using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace TrpgDiceBot
{
	static class InterpretFormula
	{
		// 0: エラーなし
		// 1: "(" の直後に演算子がある
		// 2: "(" の直後に ")" がある
		// 3: 数値の直後に "(" がある ("*" を省略してはいけない)
		// 4: ありえないエラー 数値の直後に数値がある
		// 5: 演算子が連続している
		// 6: 演算子の直後に ")" がある
		// 7: ")" の直後に "(" がある ("*" を省略してはいけない)
		// 8: ")" の直後に数値がある ("*" を省略してはいけない)
		private static List<List<int>> _errCheck = new List<List<int>> {
			new List<int> {0, 0, 1, 1, 1, 2},
			new List<int> {3, 4, 0, 0, 0, 0},
			new List<int> {0, 0, 5, 5, 5, 6},
			new List<int> {0, 0, 5, 5, 5, 6},
			new List<int> {0, 0, 5, 5, 5, 6},
			new List<int> {7, 8, 0, 0, 0, 0},
		};

		private static int _errNo = 0;
		private static bool _isBeforeNum = false;
		public static int Calc(string formula)
		{
			_isBeforeNum = false;

			MatchCollection mc = Regex.Matches(formula, @"(?<front>\-)(?<val>\d+)(?<bottom>\^{1}\(*\-?\d+\)*)");

			foreach (Match m in mc)
			{
				Console.WriteLine(m.Value);
				formula = formula.Replace(m.Value, m.Groups["front"].Value + "(" + m.Groups["val"].Value + m.Groups["bottom"].Value + ")");
			}

			mc = Regex.Matches(formula, @"\^\((?<val>\d+)\)");

			foreach (Match m in mc)
			{
				Console.WriteLine(m.Value);
				formula = formula.Replace(m.Value, "^" + m.Groups["val"].Value);
			}

			// 数字の直後に括弧は * を挿入する
			mc = Regex.Matches(formula, @"(?<val>\d+)\(");

			foreach (Match m in mc)
			{
				Console.WriteLine(m.Value);
				formula = formula.Replace(m.Value, m.Groups["val"].Value + "*(");
			}

			formula = formula.Replace(")(", ")*(");
			formula = formula.Replace("(+", "(");
			formula = formula.Replace("-(", "-1*(");
			formula = formula.Replace("++", "+");
			formula = formula.Replace("-+", "-");

			Console.WriteLine(formula);

			List<string> reverse_poland = MakeReversePoland(formula);
			Stack<int> stk = new Stack<int>();
			stk.Push(int.Parse(reverse_poland[0]));
			int i = 1;

			while (i < reverse_poland.Count)
			{
				if (char.IsNumber(reverse_poland[i][0]) || (reverse_poland[i][0] == '-' && reverse_poland[i].Length > 1 && char.IsNumber(reverse_poland[i][1])))
				{
					stk.Push(int.Parse(reverse_poland[i]));
					i++;
					continue;
				}

				int b = stk.Pop();
				int a = stk.Pop();

				stk.Push(CalcSwitchByOperator.Calc[reverse_poland[i]](a, b));
				i++;
			}

			return stk.Pop();
		}

		private static List<string> MakeReversePoland(string formula)
		{
			List<Tuple<string, int>> div_formula = SepartateFormula(formula);

			Stack<Tuple<string, int>> stk = new Stack<Tuple<string, int>>();
			List<string> res = new List<string>();
			int i = 0;

			stk.Push(new Tuple<string, int>("x", 0));

			while(i < div_formula.Count)
			{
				int element_priority = div_formula[i].Item2;

				while(stk.Peek().Item2 >= element_priority && stk.Peek().Item1 != "(")
				{
					res.Add(stk.Pop().Item1);
				}

				if(div_formula[i].Item1 == ")")
				{
					stk.Pop();
				}
				else
				{
					stk.Push(div_formula[i]);
				}

				i++;
			}

			while (stk.Peek().Item1 != "x")
			{
				res.Add(stk.Pop().Item1);
			}

			for (int loop = 0; loop < res.Count; loop++)
			{
				Console.Write(res[loop] + " ");
			}

			Console.Write("\n");

			return res;
		}

		private static List<Tuple<string, int>> SepartateFormula(string formula)
		{
			List<Tuple<string, int>> ret = new List<Tuple<string, int>>();

			int i = 0;

			while (i < formula.Length)
			{
				if (char.IsNumber(formula[i]) || (formula[i] == '-' && !_isBeforeNum))
				{
					int len = 1;

					while (formula.Length > i + len && char.IsNumber(formula[i + len]))
					{
						len++;
					}

					string unit = formula.Substring(i, len);
					ret.Add(new Tuple<string, int>(unit, GetPriority(unit)));
					i += len;

					continue;
				}

				ret.Add(new Tuple<string, int>(formula[i].ToString(), GetPriority(formula[i].ToString())));
				i++;
			}

			for(int loop = 0; loop < ret.Count; loop++)
			{
				Console.WriteLine(ret[loop]);
			}

			return ret;
		}

		private static int GetPriority(string str)
		{
			if (str == ")")
			{
				_isBeforeNum = true;
				return 1;
			}
			else if (str == "+" || (str == "-" && str.Length == 1))
			{
				_isBeforeNum = false;
				return 2;
			}
			else if (str == "*" || str == "/" || str == "%")
			{
				_isBeforeNum = false;
				return 3;
			}
			else if (str == "^")
			{
				_isBeforeNum = false;
				return 4;
			}
			else if (char.IsNumber(str[0]) || (str[0] == '-' && str.Length > 1 && char.IsNumber(str[1])))
			{
				_isBeforeNum = true;
				return 5;
			}
			else if(str == "(")
			{
				_isBeforeNum = false;
				return 6;
			}

			_isBeforeNum = false;
			return 0;
		}

		private static bool IsErr(in int before, in int current)
		{
			if(_errCheck[before][current] != 0)
			{
				_errNo = _errCheck[before][current];
				return true;
			}

			return false;
		}
	}
}
