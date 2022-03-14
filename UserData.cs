using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace TrpgDiceBot
{
	[Serializable()]
	class UserData
	{
		public string UserName { get; set; }
		public int CharaCount
		{
			get
			{
				return _charaCount;
			}
		}
		private int _charaCount;
		public int CurrentSettingCharaId
		{
			get
			{
				return _currentSettingCharaId;
			}
			set
			{
				_currentSettingCharaId = Math.Max(value, _charaCount) - 1;
			}
		}

		private int _currentSettingCharaId = -1;

		public UserData(int charaCount)
		{
			_charaCount = charaCount;
			UserName = "";
		}

		public void IncrementCount()
		{
			_charaCount++;
		}
	}
}