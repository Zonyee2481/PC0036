using Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine
{
    class TaskBitCode
    {
        public static int arrMax = 999;
        public static string[] asDeviceName = new string[arrMax];
        public static int[] aiAssignedNo = new int[arrMax];
        public static string[] asBitCodeDescription = new string[arrMax];
        public static List<BitCodeInfo> lBitCodes = new List<BitCodeInfo>();
        public static void LoadBitCodeRecipe()
        {
            try
            {
                asDeviceName = new string[arrMax];
                aiAssignedNo = new int[arrMax];

                DirectoryInfo d = new DirectoryInfo(GDefine.BitCodePath); // Assuming Test is your Folder

                if (!Directory.Exists(GDefine.BitCodePath))
                {
                    Directory.CreateDirectory(GDefine.BitCodePath);
                }

                ES.Net.IniFile IniFile = new ES.Net.IniFile();
                IniFile.Create(GDefine.BitCodePath + @"\", "Default.ini");

                if (!File.Exists(GDefine.BitCodePath + @"\Default.ini"))
                {
                    SaveBitCodeRecipe();
                }

                for (int i = 0; i < lBitCodes.Count(); i++)
                {
                    lBitCodes[i].Description = IniFile.ReadString("BitCode", i.ToString(), "");
                    asBitCodeDescription[i] = lBitCodes[i].Description;
                }
            }
            catch { }
        }

        public static void SaveBitCodeRecipe()
        {
            try
            {
                ES.Net.IniFile IniFile = new ES.Net.IniFile();
                IniFile.Create(GDefine.BitCodePath + @"\", "Default.ini");

                for (int i = 0; i < lBitCodes.Count(); i++)
                {
                    IniFile.WriteString("BitCode", i.ToString(), lBitCodes[i].Description);
                }
            }
            catch { }
        }

        public static void InitBitCode()
        {
            if (lBitCodes.Count > 0)
            {
                lBitCodes.Clear();
                lBitCodes = new List<BitCodeInfo>();
            }

            lBitCodes.Add(new BitCodeInfo(0, false, false, false, false, ""));
            lBitCodes.Add(new BitCodeInfo(1, true, false, false, false, ""));
            lBitCodes.Add(new BitCodeInfo(2, false, true, false, false, ""));
            lBitCodes.Add(new BitCodeInfo(3, true, true, false, false, ""));
            lBitCodes.Add(new BitCodeInfo(4, false, false, true, false, ""));
            lBitCodes.Add(new BitCodeInfo(5, true, false, true, false, ""));
            lBitCodes.Add(new BitCodeInfo(6, false, true, true, false, ""));
            lBitCodes.Add(new BitCodeInfo(7, true, true, true, false, ""));
            lBitCodes.Add(new BitCodeInfo(8, false, false, false, true, ""));
            lBitCodes.Add(new BitCodeInfo(9, true, false, false, true, ""));
            lBitCodes.Add(new BitCodeInfo(10, false, true, false, true, ""));
            lBitCodes.Add(new BitCodeInfo(11, true, true, false, true, ""));
            lBitCodes.Add(new BitCodeInfo(12, false, false, true, true, ""));
            lBitCodes.Add(new BitCodeInfo(13, true, false, true, true, ""));
            lBitCodes.Add(new BitCodeInfo(14, false, true, true, true, ""));
            lBitCodes.Add(new BitCodeInfo(15, true, true, true, true, ""));
        }
    }
}
