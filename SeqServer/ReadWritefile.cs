using Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SeqServer
{
    public class ReadWritefile
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeofprofile"></param = Class of the object>
        /// <param name="section"></param - Example: [Nest]>
        /// <param name="path"></param - Example: @"..\..\..\Nest\">
        /// <param name="filename"></param - Example : Nest.txt>
        /// <returns></returns>
        public dynamic ReadDynamicClassAndWritevariable(dynamic typeofprofile, string section, string path, string filename)
        {
            try
            {
                section = section.Trim().Replace(".txt", "");

                path = path + filename;
                string[] alltext = File.ReadAllLines(path);
                ParseIniFile(alltext);

                Type profiletype = typeofprofile.GetType();

                foreach (var velocityvariable in profiletype.GetFields())
                {
                    //Examples:
                    // 1. X-Axis/Y-Axis/Z-Axis
                    // 2. StationConfig
                    //Example of the key (Could be Any):
                    // 1. Position
                    // 2. Speed
                    // 3. Acc
                    // 4. Dcc
                    string key = velocityvariable.Name;
                    string value = GetValue(section, key);

                    Debug.Assert(value != null, "Class variable not able to find in the .txt file, Ensure the name between .txt and class name is exactly the same!");
                    // Get the PropertyInfo for the current property
                    FieldInfo propertyInfo = profiletype.GetField(key);

                    if (propertyInfo != null)
                    {
                        // Convert the string value to the property type
                        object convertedValue = Convert.ChangeType(value, propertyInfo.FieldType);

                        // Set the value of the property in the typeofprofile instance
                        propertyInfo.SetValue(typeofprofile, convertedValue);
                    }
                    else
                    {
                        // Handle the case where the property with the specified key is not found
                        // You may want to log or throw an exception depending on your requirements
                        Console.WriteLine($"Property with key '{key}' not found in the VeloctiySetting class.");
                    }
                }
            }

            catch(Exception ex)
            {
                //frmMain.frmMsg.ShowMsg($"ReadFileConverttoDynamicClass: {ex.Message}", frmMessaging.TMsgBtn.smbOK);

            }
            return typeofprofile;
        }

        public dynamic ReadTxtFile(dynamic typeofprofile, string path, string filename)
        {
            try
            {
                Dictionary<string, Dictionary<string, string>> parsedIni;


                path = path + filename;
                string iniContent = File.ReadAllText(path);
                parsedIni = ParseIni(iniContent);


                List<string> Item = new List<string>() { };
                foreach (var section in parsedIni)
                {
                    Item.Clear();
                    foreach (var item in section.Value)
                    {
                        Item.Add(item.Value);
                    }

                    ParseToList(typeofprofile, Item);
                }
                
            }

            catch (Exception ex)
            {
                //frmMain.frmMsg.ShowMsg($"ReadFileConverttoDynamicClass: {ex.Message}", frmMessaging.TMsgBtn.smbOK);

            }
            return typeofprofile;
        }

        internal static List<KeyValuePair<string, bool>> ReadFileItemReturnListKey_Value(List<string> lines)
        {
            List<KeyValuePair<string, bool>> itemList = new List<KeyValuePair<string, bool>>();

            foreach (string line in lines)
            {
                // Skip empty lines or lines that do not contain '='
                if (string.IsNullOrWhiteSpace(line) || !line.Contains("="))
                    continue;

                // Split the line into key-value pair
                string[] parts = line.Split('=');
                string itemName = parts[0].Trim();
                bool itemValue = bool.Parse(parts[1].Trim());

                // Add the key-value pair to the list
                itemList.Add(new KeyValuePair<string, bool>(itemName, itemValue));
            }

            return itemList;
        }

        #region Using [Section] to find the [key] and [value] 

        static Dictionary<string, Dictionary<string, string>> iniData;

        public void ParseIniFile(string[] lines)
        {
            iniData = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
            string currentSection = null;

            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();

                if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
                {
                    currentSection = trimmedLine.Substring(1, trimmedLine.Length - 2);
                    iniData[currentSection] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                }
                else if (!string.IsNullOrWhiteSpace(currentSection))
                {
                    string[] parts = trimmedLine.Split(new[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();
                        iniData[currentSection][key] = value;
                    }
                }
            }
        }

        public List<ErrData> ParseToList(List<ErrData> testerlist, List<string> inifile)
        {
            //Sort the List of list into only a list
            //To Load .ini file with variance of key name(unknown)
            //Example 1:
            //[TestParam]
            //Name = NTC_Resistance1
            //RejectCode = 11011
            //[TestParam]
            //Name = NTC_Resistance2
            //RejectCode = 11012

            ////////////////////////////////
            //Example 2:            
            //[GROUND_Resistance1]
            //CustomTestName=GROUND_Resistance1
            //TestResult = 0.11986
            //[RCODE_Resistance1]
            //CustomTestName=RCODE_Resistance1
            //TestResult = 4.15916
            //[RCODE_Capacitance1]
            //CustomTestName=RCODE_Capacitance1
            //TestResult = -0.31728
            int Station = 0;
            int EnumErr = 1;
            int Definition = 2;
            int Action = 3;
            //int lowerlimitvalue = 2;
            //int upperlimitcode = 3;
            //int lowerlimitcode = 4;
            //int rejcodeposition = 5;
            //Sort The IniItem into "UnitTesterParameter" array

            Debug.Assert(testerlist != null, "Tester list was not instantiated!");

            // To Read the .ini file with TesterParam, RejectCode and Upper/Lower Limit

            ErrData temp = new ErrData();

            temp.Station = inifile[Station];
            temp.EnumErr = inifile[EnumErr];
            temp.Definition = inifile[Definition];
            temp.Action = inifile[Action];

            testerlist.Add(temp);


            return testerlist;
        }

        public Dictionary<string, Dictionary<string, string>> ParseIni(string content)
        {
            //Example:
            //To Load .ini file with variance number of key(unknown)
            //[TestParam]
            //Name = NTC_Resistance1
            //RejectCode = 11011
            //[TestParam]
            //Name = NTC_Resistance2
            //RejectCode = 11012

            var result = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, string> currentSection = null;
            int TestParamterCount = 0;

            foreach (var line in GetLines(content))
            {
                if (IsCommentOrWhitespace(line))
                    continue;

                if (IsSection(line))
                {
                    currentSection = StartNewSection(TestParamterCount, line, result);
                    TestParamterCount++;
                }
                else if (currentSection != null && IsKeyValuePair(line))
                {
                    AddKeyValuePairToSection(line, currentSection);
                }
            }

            return result;
        }

        private IEnumerable<string> GetLines(string content)
        {
            return content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        }

        private bool IsCommentOrWhitespace(string line)
        {
            return string.IsNullOrWhiteSpace(line) || line.StartsWith(";");
        }

        private bool IsSection(string line)
        {
            return line.StartsWith("[") && line.EndsWith("]");
        }

        private Dictionary<string, string> StartNewSection(int count, string line, Dictionary<string, Dictionary<string, string>> container)
        {
            var sectionName = line.Substring(1, line.Length - 2);
            var newSection = new Dictionary<string, string>();
            container[sectionName + count] = newSection;
            return newSection;
        }

        private bool IsKeyValuePair(string line)
        {
            return line.Contains("=");
        }

        private void AddKeyValuePairToSection(string line, Dictionary<string, string> section)
        {
            var kvp = line.Split(new[] { '=' }, 2);
            if (kvp.Length == 2)
            {
                var key = kvp[0].Trim();
                var value = kvp[1].Trim();
                section[key] = value;
            }
        }

        static List<KeyValuePair<string, bool>> ParseIniFile(List<string> lines)
        {
            List<KeyValuePair<string, bool>> itemList = new List<KeyValuePair<string, bool>>();

            foreach (string line in lines)
            {
                // Skip empty lines or lines that do not contain '='
                if (string.IsNullOrWhiteSpace(line) || !line.Contains("="))
                    continue;

                // Split the line into key-value pair
                string[] parts = line.Split('=');
                string itemName = parts[0].Trim();
                bool itemValue = bool.Parse(parts[1].Trim());

                // Add the key-value pair to the list
                itemList.Add(new KeyValuePair<string, bool>(itemName, itemValue));
            }

            return itemList;
        }

        public static string GetValue(string section, string key)
        {
            if (iniData.TryGetValue(section, out var sectionData))
            {
                if (sectionData.TryGetValue(key, out var value))
                {
                    return value;
                }
            }

            return null; // Key or section not found
        }
        #endregion
    }
}
