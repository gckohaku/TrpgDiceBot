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
		public static Dictionary<ulong, UserData> Users
		{
			get
			{
				return _users;
			}
		}
		private static Dictionary<ulong, UserData> _users = new Dictionary<ulong, UserData>();

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
			Console.WriteLine("_e\tuserdata export now");

			string dir_path = HiddingStrings.MemoryDataDirectryString + "userdata/";
			using (FileStream fs = new FileStream(dir_path + ".userdata", FileMode.Create, FileAccess.Write))
			{
				BinaryFormatter bf = new BinaryFormatter();
				bf.Serialize(fs, Users);
			}

			Console.WriteLine("_e\tfinish");
		}

		public static void Import()
		{
			Console.WriteLine("_e\tuserdata import now");

			string dir_path = HiddingStrings.MemoryDataDirectryString + "userdata/";

			using (FileStream fs = new FileStream(dir_path + ".userdata", FileMode.Open, FileAccess.Read))
			{
				BinaryFormatter bf = new BinaryFormatter();
				_users = (Dictionary<ulong, UserData>)bf.Deserialize(fs);
			}

			Console.WriteLine("_e\tfinish");
		}
	}
}