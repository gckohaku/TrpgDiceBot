using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

using TrpgDiceBot.DoNotUpToGit;

namespace TrpgDiceBot
{
	static class UserManager
	{
		public static Dictionary<ulong, UserData> Users { get; } = new Dictionary<ulong, UserData>();

		public static void IncrementCount(ulong userId)
		{
			if (!Users.ContainsKey(userId))
			{
				Users.Add(userId, new UserData(1));
			}
			else
			{
				Users[userId].IncrementCount();
			}
		}

		public static void Export()
		{
			Console.WriteLine("userdata export now");

			string dir_path = HiddingStrings.MemoryDataDirectryString + "userdata/";
			FileStream fs = new FileStream(dir_path + ".userdata", FileMode.Create, FileAccess.Write);
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(fs, Users);
			fs.Close();

			Console.WriteLine("finish");
		}
	}
}
