using System;
using System.Collections.Generic;
using System.Text;

namespace TrpgDiceBot
{
	class TrpgCharacterSheetBase
	{
		public string CharacterName
		{
			get
			{
				return _characterName;
			}
		}
		protected string _characterName;

		public int CharacterIndex
		{
			get
			{
				return _characterIndex;
			}
		}
		protected int _characterIndex;

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
		protected int _rule;

		public TrpgCharacterSheetBase()
		{
		}

		public TrpgCharacterSheetBase(string characterName, int characterIndex, int rule = 0)
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