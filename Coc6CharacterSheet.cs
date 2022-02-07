using System;
using System.Collections.Generic;
using System.Text;

namespace TrpgDiceBot
{
	class Coc6CharacterSheet
	{
		public Dictionary<string, dynamic> Statuses {
			get {
				return _status.Statuses;
			}
		}
		private Coc6CharacterStatus _status { get; set; } = new Coc6CharacterStatus();
		public Coc6CharacterSkill _skill { get; set; } = new Coc6CharacterSkill();

		public Coc6CharacterSheet()
		{
			
		}
	}
}
