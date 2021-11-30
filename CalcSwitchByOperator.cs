using System;
using System.Collections.Generic;
using System.Text;

namespace TrpgDiceBot
{
	static class CalcSwitchByOperator
	{
		public delegate int CalcDelegate(in int a, in int b);
		public static Dictionary<string, CalcDelegate> Calc = new Dictionary<string, CalcDelegate> {
			{ "+", CalcAdd },
			{ "-", CalcSub },
			{ "*", CalcMul },
			{ "/", CalcDiv },
			{ "%", CalcRem },
			{ "^", CalcPow }
		};

		private static int CalcAdd(in int a, in int b)
		{
			return a + b;
		}

		private static int CalcSub(in int a, in int b)
		{
			return a - b;
		}

		private static int CalcMul(in int a, in int b)
		{
			return a * b;
		}

		private static int CalcDiv(in int a, in int b)
		{
			return a / b;
		}

		private static int CalcRem(in int a, in int b)
		{
			return a % b;
		}

		private static int CalcPow(in int a, in int b)
		{
			int ret = 1;
			int bs = a;
			int ex = b;

			while(ex > 0)
			{
				if((ex & 1) == 1)
				{
					ret *= bs;
				}
				bs *= bs;
				ex >>= 1;
			}

			return ret;
		}
	}
}
