using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TrpgDiceBot
{
	static class DirectoryUtils
	{
		public static DirectoryInfo SafeCreateDirectory(string path)
		{
			if (Directory.Exists(path))
			{
				return null;
			}
			return Directory.CreateDirectory(path);
		}
	}
}
