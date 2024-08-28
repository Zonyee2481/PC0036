using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Machine
{
    public class IniFile
    {
        public string iniFilePath;
        private const int bufferSize = 2048;
        private const int MAX_BUFFER_SIZE = 32767;

        private readonly string[] arrTrueStrings = new string[]
        {
            "YES",
            "TRUE",
            "1",
            "PASS"
        };

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
          string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileStringA(string section,
          int key, int val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
          string key, string def, StringBuilder retVal,
          int size, string filePath);

        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileSection(string lpAppName, string lpString, string lpFileName);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileSection(string lpAppName,
           byte[] lpszReturnBuffer, int nSize, string lpFileName);

        [DllImport("kernel32")]
        static extern int DeletePrivateProfileSection(String Section, int NoKey, int NoSetting, String FileName);

        public IniFile(string INIPath)
        {
            iniFilePath = INIPath;
        }

        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.iniFilePath);
        }

        public void IniWriteSection(string section, IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            var writeString = ToSectionValueString(keyValuePairs);
            var stringInBytes = writeString.Length * sizeof(char);
            if (stringInBytes > 65535)
                throw new Exception("Write String Has Exceed maximum allowable single write size!");

            WritePrivateProfileSection(section, writeString, iniFilePath);
        }

        private string ToSectionValueString(IEnumerable<KeyValuePair<string, string>> values)
        {
            if (values == null)
                return null;

            if (!values.FirstOrDefault(x => x.Key.Trim() == string.Empty).Equals(default(KeyValuePair<string, string>)))
            {
                throw new Exception("Dictionary Contains Empty String Key");
            }

            var valueStrings = values.Select(kvp => kvp.Key + "=" + kvp.Value);
            var writeString = string.Empty;
            foreach (var valueString in valueStrings)
            {
                writeString += valueString + "\0";
            }
            writeString += "\0";

            return writeString;
            //return String.Join("\0", values.Select(kvp => kvp.Key + "=" + kvp.Value));
        }

        /// <summary>
        /// 31Oct18 Jannex Retrieve all keys in INI Section
        /// </summary>
        /// <param name="Section">Section Name</param>
        /// <returns></returns>
        public string[] ReadSection(string Section, bool maxBuffer = false)
        {
            /******************************************************************
            The maximum profile section size is 32,767 characters.
            Based on //docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-getprofilesectiona
            Some INI file section is not datatable based. So it will be very large in a section (Motor Position)
            Open up max buffer to read the section
            32767 should be able to cater 1024 data
            Motor Position need 3 data per position (341 In Total) (Now around 170++ 18/6/2021)
            ***********************************************************************/

            int bufferSize = maxBuffer ? MAX_BUFFER_SIZE : 8184;
            byte[] buffer = new byte[bufferSize]; //8184 to cater for 256 data

            GetPrivateProfileSection(Section, buffer, bufferSize, iniFilePath);
            string[] tmp = Encoding.ASCII.GetString(buffer).Trim('\0').Split('\0');

            return tmp;
        }

        private string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(bufferSize);
            int i = GetPrivateProfileString(Section, Key, "", temp, bufferSize, this.iniFilePath);
            return temp.ToString();
        }

        /// <summary>
        /// read  string from ini file
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public string GetString(string Section, string Key, string defaultValue, bool createIfFail = false)
        {
            string str = IniReadValue(Section, Key);
            if (str == "")
            {
                if (createIfFail)
                {
                    // create the key
                    IniWriteValue(Section, Key, defaultValue);
                }

                str = defaultValue;
            }
            return str;
        }

        /// <summary>
        /// Read data from ini file as double
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public double GetDouble(string Section, string Key, double defaultValue, bool createIfFail = false)
        {
            string str = IniReadValue(Section, Key);
            if (str == "")
            {
                if (createIfFail)
                {
                    // create the key
                    IniWriteValue(Section, Key, defaultValue.ToString());
                }

                return defaultValue;
            }
            str = Regex.Replace(str, "[^0-9-.eE+].+", ""); // remove any non-Number but include Exponential (E)

            double number;
            if (Double.TryParse(str, out number))
            {
                return number;
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Read data from ini file as long
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public int GetInteger(string Section, string Key, int defaultValue, bool createIfFail = false)
        {
            string str = IniReadValue(Section, Key);
            if (str == "")
            {
                if (createIfFail)
                {
                    //create the key
                    IniWriteValue(Section, Key, defaultValue.ToString());
                }

                return defaultValue;
            }
            str = Regex.Replace(str, "[^0-9-+].+", ""); // remove any non-Number - decimal point are not allow
            int number;
            if (Int32.TryParse(str, out number))
            {
                return number;
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Read data from ini file as bool
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="defaultValue">the literal form of current parameter. i.e. YES/Yes/True/TRUE etc.</param>
        /// <returns></returns>
        public bool GetBool(string Section, string Key, string defaultValue, bool createIfFail = false)
        {
            string str = IniReadValue(Section, Key);
            if (str == "") // not found
            {
                if (createIfFail)
                {
                    // create the key in ini file
                    IniWriteValue(Section, Key, defaultValue);
                }
                // use default value for comparison
                str = defaultValue;
            }

            str = Regex.Replace(str, "[/;#]" + ".+", String.Empty).Trim(); // Remove Comments End of Line

            string strTmp = str.ToUpper();

            bool state = (Array.IndexOf(arrTrueStrings, strTmp) >= 0);

            return state;

        }
        /// <summary>
        /// DeletePrivateProfileSection
        /// </summary>
        /// <param name="section"></param>
        public void DeletePrivateProfileSection(string section)
        {
            WritePrivateProfileStringA(section, 0, 0, this.iniFilePath);
        }
    }
}
