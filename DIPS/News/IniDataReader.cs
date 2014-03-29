using System;
using System.IO;
using System.Text.RegularExpressions;

namespace DIPS.News
{
	/// <summary>
	/// Summary description for IniDataReader.
	/// </summary>
	public class IniDataReader
	{
        //public enum ValueType
        //{
        //    String,
        //    Int,
        //    DateTime
        //}

		private string iniFile;

		public IniDataReader(string iniFileName)
		{
			iniFile = iniFileName;
		}

        public int GetIntValue(string keyName)
        {
            return Int32.Parse(GetValue(keyName));
        }

        public string GetStringValue(string keyName)
        {
            return GetValue(keyName);
        }

		private string GetValue(string keyName)
		{
			string valReturn = String.Empty;

			if (File.Exists(iniFile))
			{
				StreamReader sr = new StreamReader(iniFile);
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					string search = Regex.Match(line,keyName + ".*", RegexOptions.Singleline).Value;
					if (!String.IsNullOrEmpty(search))
					{
						valReturn = search.Replace(keyName + "=","");
						break;
					}
				}
				sr.Close();
			}
            return valReturn;
		}
	}
}
