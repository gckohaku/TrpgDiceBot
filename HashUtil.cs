using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Linq;

namespace TrpgDiceBot
{
	internal class HashUtil
	{
		private static readonly SHA256CryptoServiceProvider _hashProvider = new SHA256CryptoServiceProvider();

		public static string GetSHA256Hash(object obj)
		{
			return string.Join("", _hashProvider.ComputeHash(Encoding.UTF8.GetBytes(obj.ToString())).Select(x => $"{x:x2}"));
		}
	}
}
