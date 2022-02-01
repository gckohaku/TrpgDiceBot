using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TrpgDiceBot
{
	static class ConvertAsciiAndNoSpace
	{
		static public string Convert(string str)
		{
			str = str.Replace(" ", "").Replace("　", "");
			str = Regex.Replace(str, "[０-９]", p => ((char)(p.Value[0] - '０' + '0')).ToString());
			str = Regex.Replace(str, "[ａ-ｚ]", p => ((char)(p.Value[0] - 'ａ' + 'a')).ToString());
			str = Regex.Replace(str, "[Ａ-Ｚ]", p => ((char)(p.Value[0] - 'Ａ' + 'A')).ToString());
			str = Regex.Replace(str, "＋", "+");
			str = Regex.Replace(str, "－", "-");
			str = Regex.Replace(str, "✕", "*");   // こっちは全角っぽく見えるもの
			str = Regex.Replace(str, "×", "*");   // こっちが半角っぽく見えるもの　どちらも環境による
			str = Regex.Replace(str, "＊", "*");
			str = Regex.Replace(str, "／", "/");
			str = Regex.Replace(str, "÷", "/");

			return str;
		}

		static public string ConvertAscii(string str)
		{
			str = Regex.Replace(str, "[０-９]", p => ((char)(p.Value[0] - '０' + '0')).ToString());
			str = Regex.Replace(str, "[ａ-ｚ]", p => ((char)(p.Value[0] - 'ａ' + 'a')).ToString());
			str = Regex.Replace(str, "[Ａ-Ｚ]", p => ((char)(p.Value[0] - 'Ａ' + 'A')).ToString());
			str = Regex.Replace(str, "＋", "+");
			str = Regex.Replace(str, "－", "-");
			str = Regex.Replace(str, "✕", "*");   // こっちは全角っぽく見えるもの
			str = Regex.Replace(str, "×", "*");   // こっちが半角っぽく見えるもの　どちらも環境による
			str = Regex.Replace(str, "＊", "*");
			str = Regex.Replace(str, "／", "/");
			str = Regex.Replace(str, "÷", "/");

			return str;
		}

		static public string ConvertNoSpace(string str)
		{
			str = str.Replace(" ", "").Replace("　", "");

			return str;
		}
	}
}
