using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TrpgDiceBot
{
	static class MyLogger
	{
		private static readonly bool _isConsole = false;
		private static DateTime _currentFileTime;
		private static readonly int _recreateFileMinutes = 60;
		internal static readonly string _logDirectory = "./log/";
		private static string _currentWritingFile;

		public static void Create()
		{
			_currentFileTime = DateTime.Now;

			string today_directory = _logDirectory + _currentFileTime.ToString("yyyyMMdd") + "/";
			DirectoryUtils.SafeCreateDirectory(today_directory);

			_currentWritingFile = today_directory + _currentFileTime.ToString("HHmmss") + ".log";
			FileStream fs = new FileStream(_currentWritingFile, FileMode.Create);
			fs.Close();
			WriteLine("log file create success");
		}

		public static void Write(string log)
		{
			if (_isConsole)
			{
				Console.Write(log);
			}
			else
			{
				RegenerateFileCheck();

				using (FileStream fs = new FileStream(_currentWritingFile, FileMode.Append, FileAccess.Write))
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.Write(log);
				}
			}
		}

		public static void WriteLine(string log)
		{if (_isConsole)
			{
				Console.WriteLine(log);
			}
			else
			{
				RegenerateFileCheck();

				using (FileStream fs = new FileStream(_currentWritingFile, FileMode.Append, FileAccess.Write))
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.WriteLine(log);
				}
			}
		}

		public static void Write(object obj)
		{
			if (_isConsole)
			{
				Console.Write(obj);
			}
			else
			{
				RegenerateFileCheck();

				using (FileStream fs = new FileStream(_currentWritingFile, FileMode.Append, FileAccess.Write))
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.Write(obj);
				}
			}
		}

		public static void WriteLine(object obj)
		{
			if (_isConsole)
			{
				Console.WriteLine(obj);
			}
			else
			{
				RegenerateFileCheck();

				using (FileStream fs = new FileStream(_currentWritingFile, FileMode.Append, FileAccess.Write))
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.WriteLine(obj);
				}
			}
		}

		public static void Write(string format, object arg0)
		{
			if (_isConsole)
			{
				Console.Write(format, arg0);
			}
			else
			{
				RegenerateFileCheck();

				using (FileStream fs = new FileStream(_currentWritingFile, FileMode.Append, FileAccess.Write))
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.Write(format, arg0);
				}
			}
		}

		public static void WriteLine(string format, object arg0)
		{
			if (_isConsole)
			{
				Console.WriteLine(format, arg0);
			}
			else
			{
				RegenerateFileCheck();

				using (FileStream fs = new FileStream(_currentWritingFile, FileMode.Append, FileAccess.Write))
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.WriteLine(format, arg0);
				}
			}
		}

		private static void RegenerateFileCheck()
		{


			if (_recreateFileMinutes <= (DateTime.Now - _currentFileTime).TotalMinutes)
			{
				Create();
			}
		}
	}
}
