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
		public Coc6CharacterStatus Statuses
		{
			get
			{
				return _status;
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

		// キャラシートごとの TRPG のルール ID (一番下のコメントにルール ID の説明あり)
		public int Rule
		{
			get
			{
				return _rule;
			}
			set
			{
				_rule = value;
			}
		}
		private int _rule;

		public Coc6CharacterSheet(string characterName, int characterIndex, int rule = 0)
		{
			_characterName = characterName;
			_characterIndex = characterIndex;
			_rule = rule;
		}
	}
}

/*
 *	TRPG のルール ID
 *		0	: Call of Cthulhu 第6版
 *		1	: Call of Cthulhu 第7版
 *		
 *		10	: ソードワールド2.0
 *		11	: ソードワールド2.5
 *		
 *		20	: ダブルクロス The 2nd Edition
 *		21	: ダブルクロス The 3rd Edition
 *		
 *		30	: エモクロア
 *		
 *		40	: パラノイア
 */