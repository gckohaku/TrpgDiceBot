using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using TrpgDiceBot.DoNotUpToGit;

namespace TrpgDiceBot
{
	static class MemoryTest
	{
		public static void Memory(in string id, in string memoryString)
		{
			MyLogger.WriteLine(id + "\n" + memoryString);
			using (StreamWriter sw = new StreamWriter(HiddingStrings.MemoryDataDirectryString + "test/" + id + ".txt"))
			{
				sw.Write(memoryString);
			}
		}
	}
}
