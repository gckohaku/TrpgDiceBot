using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

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

		}
	}
}
