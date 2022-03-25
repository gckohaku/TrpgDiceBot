using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TrpgDiceBot
{
	[Serializable()]
	class Coc6CharacterStatus
	{
		public Dictionary<string, dynamic> Statuses { get; } = new Dictionary<string, dynamic>();
		private readonly List<string> _keyList = new List<string> { "STR", "CON", "POW", "DEX", "APP", "SIZ", "INT", "EDU" };
		private readonly List<string> _desideDoNotReculcKey = new List<string> { "DEX", "APP" };
		public Coc6CharacterStatus()
		{
			Statuses.Add("STR", 0);
			Statuses.Add("CON", 0);
			Statuses.Add("POW", 0);
			Statuses.Add("DEX", 0);
			Statuses.Add("APP", 0);
			Statuses.Add("SIZ", 0);
			Statuses.Add("INT", 0);
			Statuses.Add("EDU", 0);
			ReculcStatus();

			Statuses["ダメボ"] = Statuses["DB"];
			Statuses["ダメージボーナス"] = Statuses["DB"];
			Statuses["アイデア"] = Statuses["IDEA"];
			Statuses["知識"] = Statuses["KNOW"];
			Statuses["幸運"] = Statuses["LUCK"];
		}

		public string SetStatus(in string name, in string value)
		{
			if (!_keyList.Contains(name))
			{
				return name + " does not exsist key\n";
			}

			int data;
			if (int.TryParse(value, out data))
			{
				Statuses[name] = data;
			}
			else
			{
				return "this data isn't integer.\n";
			}
			if (!_desideDoNotReculcKey.Contains(name))
			{
				ReculcStatus();
			}
			return "";
		}

		private void ReculcStatus()
		{
			Statuses["HP"] = (Statuses["CON"] + Statuses["SIZ"]) / 2;
			Statuses["MP"] = Statuses["POW"];
			Statuses["SAN"] = Statuses["POW"] * 5;
			Statuses["DB"] = Coc6DamageBonus.Calc(Statuses["STR"] + Statuses["SIZ"]);
			Statuses["IDEA"] = Statuses["INT"] * 5;
			Statuses["KNOW"] = Statuses["EDU"] * 5;
			Statuses["LUCK"] = Statuses["POW"] * 5;
		}
	}
}
