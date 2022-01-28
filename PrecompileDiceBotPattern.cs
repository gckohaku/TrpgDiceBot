using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;

namespace TrpgDiceBot
{
	static class PrecompileDiceBotPattern
	{
		static public void CompileRegex()
		{
			string namespace_name = "TrpgDiceRegex";

			// コンパイルする正規表現に関する情報を作成
			// nDs
			var dice = new RegexCompilationInfo(@"(?i)(?<sign>(\+|\-|))(?<value>\d+)(?<type>(d|r))(?<sides>\d+)((\[|@)(?<critical>\d+)(\]|))?", RegexOptions.None, "DiceRegex", namespace_name, true);
			// fix
			var fix = new RegexCompilationInfo(@"(?i)(?<fix>(\+|\-)\d+)(?=(\+|\-|$))", RegexOptions.None, "FixRegex", namespace_name, true);
			// nDX@c
			var dx = new RegexCompilationInfo(@"(?i)(?<sign>(\+|\-|))(?<value>\d+)dx((\[|@)(?<critical>\d+)(\]|))?", RegexOptions.None, "DXRegex", namespace_name, true);
			// nDX@c
			var emo = new RegexCompilationInfo(@"(?i)(?<value>\d+)dx((\[|@)(?<critical>\d+)(\]|))?", RegexOptions.None, "EmokloreRegex", namespace_name, true);

			// アセンブリの名前空間を指定
			var assm_name = new AssemblyName(namespace_name);

			// コンパイルしてアセンブリを生成
			Regex.CompileToAssembly(new[] {dice, fix, dx, emo}, assm_name);
		}
	}
}
