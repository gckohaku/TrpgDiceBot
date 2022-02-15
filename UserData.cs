using System;
using System.Collections.Generic;
using System.Text;

namespace TrpgDiceBot
{

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

		public UserData(int charaCount)
		{
			_charaCount = charaCount;
		}

		public void IncrementCount()
		{
			_charaCount++;
		}
	}
}
