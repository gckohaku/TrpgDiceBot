using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TrpgDiceBot
{
	static class MyLogger
	{
		private static bool _isConsole = false;
		private static DateTime _currentFileTime = DateTime.Now;

		public static void Create()
		{
			if (!_isConsole)
			{
				using (FileStream fs = new FileStream("./log/" + _currentFileTime.ToString("yyyyMMddHHmmss") + ".log", FileMode.Create)) { }
				Console.WriteLine("log file create success");
			}
		}

		public static void Write(string log)
		{
			if (_isConsole)
			{
				Console.Write(log);
			}
			else
			{
				using (FileStream fs = new FileStream("./log/" + _currentFileTime.ToString("yyyyMMddHHmmss") + ".log", FileMode.Append, FileAccess.Write))
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.Write(log);
				}
			}
		}

		public static void WriteLine(string log)
		{
			if (_isConsole)
			{
				Console.WriteLine(log);
			}
			else
			{
				using (FileStream fs = new FileStream("./log/" + _currentFileTime.ToString("yyyyMMddHHmmss") + ".log", FileMode.Append, FileAccess.Write))
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
				using (FileStream fs = new FileStream("./log/" + _currentFileTime.ToString("yyyyMMddHHmmss") + ".log", FileMode.Append, FileAccess.Write))
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
				using (FileStream fs = new FileStream("./log/" + _currentFileTime.ToString("yyyyMMddHHmmss") + ".log", FileMode.Append, FileAccess.Write))
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
				using (FileStream fs = new FileStream("./log/" + _currentFileTime.ToString("yyyyMMddHHmmss") + ".log", FileMode.Append, FileAccess.Write))
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
				using (FileStream fs = new FileStream("./log/" + _currentFileTime.ToString("yyyyMMddHHmmss") + ".log", FileMode.Append, FileAccess.Write))
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.WriteLine(format, arg0);
				}
			}
		}
	}
}
