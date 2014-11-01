using System;
using System.IO;
using NUnit.Framework.Constraints;

namespace CleanCode
{
	public static class RefactorMethod
	{
	    public static void SaveToFile(string filename, byte[] data)
	    {
	        using (var stream = new FileStream(filename, FileMode.OpenOrCreate))
	        {
	            stream.Write(data, 0, data.Length);
	        }
	    }

		private static void SaveData(string s, byte[] d)
		{
		    SaveToFile(s, d);
		    SaveToFile(Path.ChangeExtension(s, "bkp"), d);
            SaveToFile(s + ".time", BitConverter.GetBytes((DateTime.Now.Ticks)));
		}
	}
}