using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TrpgDiceBot
{
	public class DbCharacterSheet
	{
		int CharacterId;
		int RuleId;
		int SheetId;
		[MaxLength(1000)]
		string Memo;
	}
}
