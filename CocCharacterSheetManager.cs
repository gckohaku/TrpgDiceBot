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
			MyLogger.WriteLine("_e\tcreate charasheet");
			if (!Sheets.ContainsKey(userId))
			{
				Sheets.Add(userId, new List<Coc6CharacterSheet>());
			}
			Sheets[userId].Add(new Coc6CharacterSheet(characterName, UserManager.Users[userId].CharaCount));
			MyLogger.WriteLine("_e\tok");

			Export(userId, characterName);
			UserManager.Export();
		}

		public static void AddStatus(string statusName, int value)
		{

		}

		public static string StatusesToString(ulong userId, int sheetIndex)
		{
			string ret = "";

			Dictionary<string, dynamic> statuses = Sheets[userId][sheetIndex].Statuses.Statuses;
			List<string> keyList = new List<string> { "STR", "CON", "POW", "DEX", "APP", "SIZ", "INT", "EDU", "HP", "MP", "SAN", "DB", "IDEA", "KNOW", "LUCK" };
			List<string> fixSpace = new List<string> { "  ", "  ", "  ", "  ", "  ", "  ", "  ", "  ", "   ", "   ", "  ", "   ", " ", " ", " " };

			for (int i = 0; i < 8; i++)
			{
				ret += keyList[i] + fixSpace[i] + statuses[keyList[i]] + "\n";
			}

			ret += "\n";

			for (int i = 8; i < 15; i++)
			{
				ret += keyList[i] + fixSpace[i] + statuses[keyList[i]] + "\n";
			}

			return ret;
		}

		public static string SkillsToString(ulong userId, int sheetIndex)
		{
			string ret = "dummy";



			return ret;
		}

		public static string ToString(ulong userId, int sheetIndex)
		{
			return StatusesToString(userId, sheetIndex) + "\n\n" + SkillsToString(userId, sheetIndex);
		}

		public static void Import(ulong userId)
		{
			MyLogger.WriteLine("_e\tcharactersheet " + userId + " (" + userId.ToString("x") + ")" + " import now");

			string dir_path = HiddingStrings.MemoryDataDirectryString + "chobjtest/" + userId.ToString("x") + "/";

			List<Coc6CharacterSheet> user_sheets = new List<Coc6CharacterSheet>();
			Sheets.Add(userId, new List<Coc6CharacterSheet>());

			for (int i = 0; i < UserManager.Users[userId].CharaCount; i++)
			{
				MyLogger.WriteLine("_e\t" + i);
				using (FileStream fs = new FileStream(dir_path + (i + 1) + ".chdata", FileMode.Open, FileAccess.Read))
				{
					BinaryFormatter bf = new BinaryFormatter();
					Sheets[userId].Add((Coc6CharacterSheet)bf.Deserialize(fs));
				}
			}

			MyLogger.WriteLine("_e\tfinish");
		}

		public static void Export(ulong userId, string characterName)
		{
			MyLogger.WriteLine("_e\tcharasheet export now");

			Coc6CharacterSheet sheet = Sheets[userId].Find(s => s.CharacterName == characterName);
			string dir_path = HiddingStrings.MemoryDataDirectryString + "chobjtest/" + userId.ToString("x");
			DirectoryUtils.SafeCreateDirectory(dir_path);
			FileStream fs = new FileStream(dir_path + "/" + sheet.CharacterIndex + ".chdata", FileMode.Create, FileAccess.Write);
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(fs, sheet);
			fs.Close();

			MyLogger.WriteLine("_e\tfinish");
		}

		public static void Export(ulong userId, int index)
		{
			MyLogger.WriteLine("_e\tcharasheet export now");

			Coc6CharacterSheet sheet = Sheets[userId][index];
			string dir_path = HiddingStrings.MemoryDataDirectryString + "chobjtest/" + userId.ToString("x");
			DirectoryUtils.SafeCreateDirectory(dir_path);
			FileStream fs = new FileStream(dir_path + "/" + sheet.CharacterIndex + ".chdata", FileMode.Create, FileAccess.Write);
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(fs, sheet);
			fs.Close();

			MyLogger.WriteLine("_e\tfinish");
		}

		public static void ExportAll(ulong userId)
		{
			MyLogger.WriteLine("_e\tcharasheet export now");

			List<Coc6CharacterSheet> sheets = Sheets[userId];

			foreach (var sheet in sheets)
			{
				string dir_path = HiddingStrings.MemoryDataDirectryString + "chobjtest/" + userId.ToString("x");
				DirectoryUtils.SafeCreateDirectory(dir_path);
				FileStream fs = new FileStream(dir_path + "/" + sheet.CharacterIndex + ".chdata", FileMode.Create, FileAccess.Write);
				BinaryFormatter bf = new BinaryFormatter();
				bf.Serialize(fs, sheet);
				fs.Close();
			}

			MyLogger.WriteLine("_e\tfinish");
		}
	}
}
