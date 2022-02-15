using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

using TrpgDiceBot.DoNotUpToGit;

namespace TrpgDiceBot
{
	static class CocCharacterSheetManager
	{
		public static Dictionary<ulong, List<Coc6CharacterSheet>> Sheets { get; set; } = new Dictionary<ulong, List<Coc6CharacterSheet>>();

		public static void Create(ulong userId, string characterName)
		{
			Console.WriteLine("hi");
			if (!Sheets.ContainsKey(userId))
			{
				Sheets.Add(userId, new List<Coc6CharacterSheet>());
			}
			Sheets[userId].Add(new Coc6CharacterSheet(characterName, UserManager.Users[userId].CharaCount));
			Console.WriteLine("ok");
		}

		public static void AddStatus(string statusName, int value)
		{

		}

		public static void Export(ulong userId, string characterName)
		{
			Console.WriteLine("export now");

			Coc6CharacterSheet sheet = Sheets[userId].Find(s => s.CharacterName == characterName);
			string dir_path = HiddingStrings.MemoryDataDirectryString + "chobjtest/" + userId;
			DirectoryUtils.SafeCreateDirectory(dir_path);
			FileStream fs = new FileStream(dir_path + "/" + sheet.CharacterIndex + ".chdata", FileMode.Create, FileAccess.Write);
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(fs, sheet);

			Console.WriteLine("finish");
		}
	}
}
