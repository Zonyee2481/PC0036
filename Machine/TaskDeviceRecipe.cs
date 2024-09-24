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
        public static int[] aiAssignedNo = new int[arrMax];
        //public static int[] aiCounter = new int[arrMax];
        public static double[] adDuration = new double[arrMax];
        public static int iHour = 0, iMin = 0, iSec = 0;

        public static void LoadDeviceTimeLimit(string Path, string FileName)
        {
            try
            {
                ES.Net.IniFile IniFile = new ES.Net.IniFile();
                IniFile.Create(Path + @"\", FileName);

                int timeLimit= IniFile.ReadInteger("DeviceRecipe", "TimeLimit", 0);
                TimeSpan timeSpan = TimeSpan.FromMilliseconds(timeLimit);

                iHour = timeSpan.Hours;
                iMin = timeSpan.Minutes;
                iSec = timeSpan.Seconds;

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
                _LotInfo._RecipeInfo.Index = IniFile.ReadInteger("DeviceRecipe", "AssignedCode", 0);
                //_LotInfo._RecipeInfo.Counter = IniFile.ReadInteger("DeviceRecipe", "Counter", 0);
                _LotInfo._RecipeInfo.TimeLimit = IniFile.ReadInteger("DeviceRecipe", "TimeLimit", 0);
                TimeSpan timeSpan = TimeSpan.FromMilliseconds(_LotInfo._RecipeInfo.TimeLimit);

                iHour = timeSpan.Hours;
                iMin = timeSpan.Minutes;
                iSec = timeSpan.Seconds;

                frmMain.SequenceRun.RecipeInfo(_LotInfo);
            }
            catch { }
        }

        public static void LoadDeviceRecipe()
        {
            try
            {
                asDeviceID = new string[arrMax];
                aiAssignedNo = new int[arrMax];
                //aiCounter = new int[arrMax];
                adDuration = new double[arrMax];

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
                    aiAssignedNo[i] = IniFile.ReadInteger("DeviceRecipe", "AssignedCode", 0);
                    //aiCounter[i] = IniFile.ReadInteger("DeviceRecipe", "Counter", 0);
                    adDuration[i] = IniFile.ReadDouble("DeviceRecipe", "TimeLimit", 0);
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
                IniFile.WriteInteger("DeviceRecipe", "AssignedCode", aiAssignedNo[i]);
                //IniFile.WriteInteger("DeviceRecipe", "Counter", aiCounter[i]);
                IniFile.WriteDouble("DeviceRecipe", "TimeLimit", adDuration[i]);
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
                IniFile.WriteInteger("DeviceRecipe", "AssignedCode", _LotInfo._RecipeInfo.Index);
                IniFile.WriteInteger("DeviceRecipe", "Counter", _LotInfo._RecipeInfo.Counter);
                IniFile.WriteDouble("DeviceRecipe", "TimeLimit", _LotInfo._RecipeInfo.TimeLimit);

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
                    IniFile.WriteInteger("DeviceRecipe", "AssignedCode", aiAssignedNo[i]);
                    //IniFile.WriteInteger("DeviceRecipe", "Counter", aiCounter[i]);
                    IniFile.WriteDouble("DeviceRecipe", "TimeLimit", adDuration[i]);
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
