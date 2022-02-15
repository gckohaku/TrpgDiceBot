using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using TrpgDiceBot.DoNotUpToGit;

namespace TrpgDiceBot
{
	[Serializable()]
	class Coc6CharacterSheet
	{
		public Dictionary<string, dynamic> Statuses
		{
			get
			{
				return _status.Statuses;
			}
		}
		private Coc6CharacterStatus _status { get; set; } = new Coc6CharacterStatus();
		private Coc6CharacterSkill _skill { get; set; } = new Coc6CharacterSkill();
		public string CharacterName
		{
			get
			{
				return _characterName;
			}
		}
		private string _characterName;
		public int CharacterIndex
		{
			get
			{
				return _characterIndex;
			}
		}
		private int _characterIndex;

		public Coc6CharacterSheet(string characterName, int characterIndex)
		{
			_characterName = characterName;
			_characterIndex = characterIndex;
		}
	}
}
