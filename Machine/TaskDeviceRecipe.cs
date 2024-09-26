using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Management;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Diagnostics;
using Infrastructure;

namespace Machine
{
    class TaskDeviceRecipe
    {
        public static string _sDeviceID = "";
        //public static string _sDeviceName = "";
        //public static int _iRow = 0;
        //public static int _iCol = 0;
        //public static string _sTopMarkFile = GDefine.AppPath2;
        //public static string _sBottomMarkFile = GDefine.AppPath2;
        //public static double _dTopLaserMarkPos = 0;
        //public static double _dBottomLaserMarkPos = 0;
        //public static int _iOCRRecipe = 0;

        public static int arrMax = 999;
        public static int _iTotalCount = 0;
        public static string[] _asDeviceID = new string[arrMax];
        //public static string[] _asDeviceName = new string[arrMax];
        //public static int[] _aiRow = new int[arrMax];
        //public static int[] _aiCol = new int[arrMax];
        //public static string[] _asTopMarkFile = new string[arrMax];
        //public static string[] _asBottomMarkFile = new string[arrMax];
        //public static int[] _aiOCRRecipe = new int[arrMax];
        //public static string[] _aiFixCode = new string[arrMax];
        //public static string[] _aiOutSource = new string[arrMax];
        public static LotInfo _LotInfo = new LotInfo();
        //public static RecipeInfo _RecipeInfo = new RecipeInfo();
        //public static Part Part = new Part();
        public static string[] asDeviceID = new string[arrMax];
        public static int[] aiRunHz_1st = new int[arrMax];
        public static int[] aiRunHz_2nd = new int[arrMax];
        public static double[] adTimeLimit_1st = new double[arrMax];
        public static double[] adTimeLimit_2nd = new double[arrMax];
        public static bool[] abMasterProduct = new bool[arrMax];
        public static int iHour_1st = 0, iMin_1st = 0, iSec_1st = 0;
        public static int iHour_2nd = 0, iMin_2nd = 0, iSec_2nd = 0;

        public static void LoadDeviceTimeLimit(string Path, string FileName)
        {
            try
            {
                ES.Net.IniFile IniFile = new ES.Net.IniFile();
                IniFile.Create(Path + @"\", FileName);
                TimeSpan timeSpan = new TimeSpan();

                int timeLimit_1st = IniFile.ReadInteger("DeviceRecipe", "TimeLimit_1st", 0);
                int timeLimit_2nd = IniFile.ReadInteger("DeviceRecipe", "TimeLimit_2nd", 0);

                timeSpan = TimeSpan.FromMilliseconds(timeLimit_1st);

                iHour_1st = timeSpan.Hours;
                iMin_1st = timeSpan.Minutes;
                iSec_1st = timeSpan.Seconds;

                timeSpan = TimeSpan.FromMilliseconds(timeLimit_2nd);

                iHour_2nd = timeSpan.Hours;
                iMin_2nd = timeSpan.Minutes;
                iSec_2nd = timeSpan.Seconds;

                //frmMain.SequenceRun.RecipeInfo(_LotInfo);
            }
            catch { }
        }

        public static void LoadDeviceRecipe(string Path, string FileName)
        {
            try
            {
                ES.Net.IniFile IniFile = new ES.Net.IniFile();
                IniFile.Create(Path + @"\", FileName);

                _LotInfo._RecipeInfo.DeviceID = IniFile.ReadString("DeviceRecipe", "DeviceID", FileName.Replace(GDefine.DeviceRecipeExt, ""));
                _LotInfo._RecipeInfo.RunHz_1st = IniFile.ReadInteger("DeviceRecipe", "RunningHz_1st", 0);
                _LotInfo._RecipeInfo.RunHz_2nd = IniFile.ReadInteger("DeviceRecipe", "RunningHz_2nd", 0);
                _LotInfo._RecipeInfo.TimeLimit_1st = IniFile.ReadInteger("DeviceRecipe", "TimeLimit_1st", 0);
                _LotInfo._RecipeInfo.TimeLimit_2nd = IniFile.ReadInteger("DeviceRecipe", "TimeLimit_2nd", 0);
                _LotInfo._RecipeInfo.MasterProduct = IniFile.ReadBool("DeviceRecipe", "MasterProduct", false);
                TimeSpan timeSpan = new TimeSpan();
                
                timeSpan = TimeSpan.FromMilliseconds(_LotInfo._RecipeInfo.TimeLimit_1st);

                iHour_1st = timeSpan.Hours;
                iMin_1st = timeSpan.Minutes;
                iSec_1st = timeSpan.Seconds;

                timeSpan = TimeSpan.FromMilliseconds(_LotInfo._RecipeInfo.TimeLimit_2nd);

                iHour_2nd = timeSpan.Hours;
                iMin_2nd = timeSpan.Minutes;
                iSec_2nd = timeSpan.Seconds;

                frmMain.SequenceRun.RecipeInfo(_LotInfo);
            }
            catch { }
        }

        public static void LoadDeviceRecipe()
        {
            try
            {
                asDeviceID = new string[arrMax];
                aiRunHz_1st = new int[arrMax];
                aiRunHz_2nd = new int[arrMax];
                adTimeLimit_1st = new double[arrMax];
                adTimeLimit_2nd = new double[arrMax];
                abMasterProduct = new bool[arrMax];

                DirectoryInfo d = new DirectoryInfo(GDefine.DevicePath);//Assuming Test is your Folder

                if (!Directory.Exists(GDefine.DevicePath))
                {
                    Directory.CreateDirectory(GDefine.DevicePath);
                }

                FileInfo[] Files = d.GetFiles("*.ini"); //Getting Text files
                //string str = "";
                //foreach (FileInfo file in Files)
                //{
                //    str = str + ", " + file.Name;
                //}
                ES.Net.IniFile IniFile = new ES.Net.IniFile();
                //IniFile.Create(GDefine.DevicePath + @"\", FileName);
                for (int i = 0; i < Files.Count(); i++)
                {
                    IniFile.Create(GDefine.DevicePath + @"\", Files[i].ToString());

                    asDeviceID[i] = Files[i].Name.Replace(GDefine.DeviceRecipeExt, "");                  
                    aiRunHz_1st[i] = IniFile.ReadInteger("DeviceRecipe", "RunningHz_1st", 0);
                    aiRunHz_2nd[i] = IniFile.ReadInteger("DeviceRecipe", "RunningHz_2nd", 0);
                    adTimeLimit_1st[i] = IniFile.ReadDouble("DeviceRecipe", "TimeLimit_1st", 0);
                    adTimeLimit_2nd[i] = IniFile.ReadDouble("DeviceRecipe", "TimeLimit_2nd", 0);
                    abMasterProduct[i] = IniFile.ReadBool("DeviceRecipe", "MasterProduct", false);
                }
            }
            catch { }
        }

        public static void SaveDeviceRecipe(string FileName, int i)
        {
            try
            {
                _iTotalCount = _asDeviceID.Count();
                ES.Net.IniFile IniFile = new ES.Net.IniFile();
                IniFile.Create(GDefine.DevicePath + @"\", FileName + ".ini");

                IniFile.WriteString("DeviceRecipe", "DeviceID", asDeviceID[i]);
                IniFile.WriteInteger("DeviceRecipe", "RunningHz_1st", aiRunHz_1st[i]);
                IniFile.WriteInteger("DeviceRecipe", "RunningHz_2nd", aiRunHz_2nd[i]);
                IniFile.WriteDouble("DeviceRecipe", "TimeLimit_1st", adTimeLimit_1st[i]);
                IniFile.WriteDouble("DeviceRecipe", "TimeLimit_2nd", adTimeLimit_2nd[i]);
                IniFile.WriteBool("DeviceRecipe", "MasterProduct", abMasterProduct[i]);
            }
            catch { }
        }

        public static void SaveDeviceRecipe(string Path, string FileName)
        {
            try
            {
                ES.Net.IniFile IniFile = new ES.Net.IniFile();
                IniFile.Create(GDefine.DevicePath + @"\", FileName + ".ini");

                IniFile.WriteString("DeviceRecipe", "DeviceID", _LotInfo._RecipeInfo.DeviceID);
                IniFile.WriteInteger("DeviceRecipe", "RunningHz_1st", _LotInfo._RecipeInfo.RunHz_1st);
                IniFile.WriteDouble("DeviceRecipe", "TimeLimit_1st", _LotInfo._RecipeInfo.TimeLimit_1st);
                IniFile.WriteInteger("DeviceRecipe", "RunningHz_2nd", _LotInfo._RecipeInfo.RunHz_2nd);
                IniFile.WriteDouble("DeviceRecipe", "TimeLimit_2nd", _LotInfo._RecipeInfo.TimeLimit_2nd);
                IniFile.WriteBool("DeviceRecipe", "MasterProduct", _LotInfo._RecipeInfo.MasterProduct);

                LoadDeviceRecipe();
            }
            catch { }
        }

        public static void RenameDeviceRecipe(string OldFileName, string NewFileName)
        {
            try
            {
                File.Move(GDefine.DevicePath + @"\" + OldFileName + ".ini", GDefine.DevicePath + @"\" + NewFileName + ".ini");
            }
            catch { }
        }

        public static void SaveDeviceRecipe()
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(GDefine.DevicePath);//Assuming Test is your Folder

                FileInfo[] Files = d.GetFiles("*.ini"); //Getting Text files

                ES.Net.IniFile IniFile = new ES.Net.IniFile();

                for (int i = 0; i < Files.Count(); i++)
                {
                    IniFile.Create(d + @"\", Files[i].Name);
                    IniFile.WriteString("DeviceRecipe", "DeviceID", asDeviceID[i]);
                    IniFile.WriteInteger("DeviceRecipe", "RunningHz_1st", aiRunHz_1st[i]);
                    IniFile.WriteInteger("DeviceRecipe", "RunningHz_2nd", aiRunHz_2nd[i]);
                    IniFile.WriteDouble("DeviceRecipe", "TimeLimit_1st", adTimeLimit_1st[i]);
                    IniFile.WriteDouble("DeviceRecipe", "TimeLimit_2nd", adTimeLimit_2nd[i]);
                    IniFile.WriteBool("DeviceRecipe", "MasterProduct", abMasterProduct[i]);
                }

                LoadDeviceRecipe();
            }
            catch { }
        }

        public static void ResetFileCounter(string FileName, int i)
        {
            try
            {
                _iTotalCount = _asDeviceID.Count();
                ES.Net.IniFile IniFile = new ES.Net.IniFile();
                IniFile.Create(GDefine.DevicePath + @"\", FileName + ".ini");

                //IniFile.WriteInteger("DeviceRecipe", "Counter", aiCounter[i]);

                LoadDeviceRecipe();
            }
            catch { }
        }
    }
}
