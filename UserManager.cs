using System;
using System.Collections.Generic;
using System.Text;

namespace TrpgDiceBot
{
	static class UserManager
	{
		public static Dictionary<ulong, UserData> Users { get; } = new Dictionary<ulong, UserData>();
	}
}
