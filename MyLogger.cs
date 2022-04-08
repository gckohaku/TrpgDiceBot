using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TrpgDiceBot
{
	static class MyLogger
	{
		private static bool _isConsole = false;
		private static DateTime _currentFileTime;
		private static readonly int _recreateFileMinutes = 10;
		private static string _logDirectory;

		public static void Create()
		{
			if (!_isConsole)
			{
				_currentFileTime = DateTime.Now;
				FileStream fs = new FileStream(_logDirectory + _currentFileTime.ToString("yyyyMMddHHmmss") + ".log", FileMode.Create);
				Console.WriteLine("log file create success");
			}
		}

		public static void Write(string log)
		{
			RegenerateFileCheck();

			if (_isConsole)
			{
				Console.Write(log);
			}
			else
			{
				using (FileStream fs = new FileStream(_logDirectory + _currentFileTime.ToString("yyyyMMddHHmmss") + ".log", FileMode.Append, FileAccess.Write))
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.Write(log);
				}
			}
		}

		public static void WriteLine(string log)
		{
			RegenerateFileCheck();

			if (_isConsole)
			{
				Console.WriteLine(log);
			}
			else
			{
				using (FileStream fs = new FileStream(_logDirectory + _currentFileTime.ToString("yyyyMMddHHmmss") + ".log", FileMode.Append, FileAccess.Write))
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.WriteLine(log);
				}
			}
		}

		public static void Write(object obj)
		{
			RegenerateFileCheck();

			if (_isConsole)
			{
				Console.Write(obj);
			}
			else
			{
				using (FileStream fs = new FileStream(_logDirectory + _currentFileTime.ToString("yyyyMMddHHmmss") + ".log", FileMode.Append, FileAccess.Write))
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.Write(obj);
				}
			}
		}

		public static void WriteLine(object obj)
		{
			RegenerateFileCheck();

			if (_isConsole)
			{
				Console.WriteLine(obj);
			}
			else
			{
				using (FileStream fs = new FileStream(_logDirectory + _currentFileTime.ToString("yyyyMMddHHmmss") + ".log", FileMode.Append, FileAccess.Write))
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.WriteLine(obj);
				}
			}
		}

		public static void Write(string format, object arg0)
		{
			RegenerateFileCheck();

			if (_isConsole)
			{
				Console.Write(format, arg0);
			}
			else
			{
				using (FileStream fs = new FileStream(_logDirectory + _currentFileTime.ToString("yyyyMMddHHmmss") + ".log", FileMode.Append, FileAccess.Write))
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.Write(format, arg0);
				}
			}
		}

		public static void WriteLine(string format, object arg0)
		{
			RegenerateFileCheck();

			if (_isConsole)
			{
				Console.WriteLine(format, arg0);
			}
			else
			{
				using (FileStream fs = new FileStream(_logDirectory + _currentFileTime.ToString("yyyyMMddHHmmss") + ".log", FileMode.Append, FileAccess.Write))
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.WriteLine(format, arg0);
				}
			}
		}

		private static void RegenerateFileCheck()
		{
			if (_recreateFileMinutes < (DateTime.Now - _currentFileTime).Minutes)
			{
				_currentFileTime = DateTime.Now;
				Create();
			}
		}

		public static void ChoiceLogDirectory()
		{
			if(Environment.OSVersion.Platform == PlatformID.Win32NT)
			{
				_logDirectory = "./log/";
				return;
			}
			_logDirectory = "../../../home/gckohaku/trpgdicebot/log/";
		}
	}
}
