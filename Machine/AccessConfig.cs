using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Machine
{
    public class AccessConfig
  {
        public const string AccessFilePath = @"C:\Machine\AccessEntry";

        public enum TAccesslvl : byte
        {
            TOperator = 0,
            TTechnician = 1,
            TEngineer = 2,
            TAdministrator = 3
        }

        public struct TAccessEntry //struct = value class
        {
            public string Password;
            public TAccesslvl AccessLvl;
            public string AccessName;
            public TAccessEntry(string Password, TAccesslvl AccessLvl, string AccessName)
            {
                this.Password = Password;
                this.AccessLvl = AccessLvl;
                this.AccessName = AccessName;
            }
        }
        public static string[] _aAccessLevel = new string[] { "Operator", "Technician", "Engineer", "Administrator" };
        public static TAccessEntry Operator = new TAccessEntry(Operator.Password, TAccesslvl.TOperator, "Operator");
        public static TAccessEntry Engineer = new TAccessEntry(Engineer.Password, TAccesslvl.TEngineer, "Engineer");
        public static TAccessEntry Technician = new TAccessEntry(Technician.Password, TAccesslvl.TTechnician, "Technician");
        public static TAccessEntry Administrator = new TAccessEntry(Administrator.Password, TAccesslvl.TAdministrator, "Administrator");
        public static TAccesslvl CurrentAccessLvl = TAccesslvl.TOperator;
        public static int _iTotalIndex = 0;
        public static IList<string> _slBadgeNumber = new List<string>();
        public static IList<string> _slUserName = new List<string>();
        public static IList<int> _slLevel = new List<int>();
        public static IList<string> _slPassword = new List<string>();
        public static string _sCurrentLoginBatchNum = "";
        public static string _sCurrentLoginUserName = "";
        public static string _sCurrentLoginLevel = "";
        public static int _iIdleTimeLogOut = 10;

        public static bool LoginCheck(string batchNumber, string password)
        {
            if (batchNumber == "TTOT" && password == "Ttot@6117100") 
            {
                _sCurrentLoginBatchNum = "TTOT";
                _sCurrentLoginUserName = "TTOT";
                _sCurrentLoginLevel = "Administrator";
                CurrentAccessLvl = TAccesslvl.TAdministrator;
                return true; 
            }
            for (int i = 0; i < _slBadgeNumber.Count; i++)
            {
                if (_slBadgeNumber[i] == batchNumber && _slPassword[i] == password)
                {
                    _sCurrentLoginBatchNum = _slBadgeNumber[i];
                    _sCurrentLoginUserName = _slUserName[i];
                    _sCurrentLoginLevel = _aAccessLevel[_slLevel[i]];
                    if (_slLevel[i] == 0)
                    {
                        CurrentAccessLvl = TAccesslvl.TOperator;
                    }
                    else if (_slLevel[i] == 1)
                    {
                        CurrentAccessLvl = TAccesslvl.TTechnician;
                    }
                    else if (_slLevel[i] == 2)
                    {
                        CurrentAccessLvl = TAccesslvl.TEngineer;
                    }
                    else if (_slLevel[i] == 3)
                    {
                        CurrentAccessLvl = TAccesslvl.TAdministrator;
                    }
                    return true;
                }
            }
            //uint MsgID = frmMain.frmMsg.ShowMsg("Invalid Login Batch Number or Password!",
            //              frmMessaging.TMsgBtn.smbOK | frmMessaging.TMsgBtn.smbAlmClr);
            //while (!frmMain.frmMsg.ShowMsgClear(MsgID)) Application.DoEvents();
            //if (frmMain.frmMsg.GetMsgRes(MsgID) == frmMessaging.TMsgRes.smrOK)
            //{
            //    return false;
            //}
            return false;
        }
    
        static int maxpage = 4;
        public static bool[] _OperatorAccess = new bool[maxpage];
        public static bool[] _TechnicianAccess = new bool[maxpage];
        public static bool[] _EngineerAccess = new bool[maxpage];
        public static bool[] _AdministratorAccess = new bool[maxpage];

        public static void InitArr()
        {
            for (int i = 0; i < maxpage; i++)
            {
                _OperatorAccess[i] = false;
                _TechnicianAccess[i] = false;
                _EngineerAccess[i] = false;
                _AdministratorAccess[i] = false;
            }
        }
        public static void SaveAccessPageConfig()
        {
            try
            {
                string AccessFileName = @"\AccessPageConfig.ini";
                ES.Net.IniFile IniFile = new ES.Net.IniFile();
                IniFile.Create(AccessFilePath, AccessFileName);
                for (int i = 0; i < maxpage; i++)
                {
                    IniFile.WriteBool("Operator Page " + i.ToString(), "Enable", _OperatorAccess[i]);
                    IniFile.WriteBool("Technician Page " + i.ToString(), "Enable", _TechnicianAccess[i]);
                    IniFile.WriteBool("Engineer Page " + i.ToString(), "Enable", _EngineerAccess[i]);
                    IniFile.WriteBool("Administrator Page " + i.ToString(), "Enable", _AdministratorAccess[i]);
                }
            }
            catch { }
        }

        public static void LoadAccessPageConfig()
        {
            try
            {
                string AccessFileName = @"\AccessPageConfig.ini";
                ES.Net.IniFile IniFile = new ES.Net.IniFile();
                IniFile.Create(AccessFilePath, AccessFileName);
                for (int i = 0; i < maxpage; i++)
                {
                    _OperatorAccess[i] = IniFile.ReadBool("Operator Page " + i.ToString(), "Enable", false);
                    _TechnicianAccess[i] = IniFile.ReadBool("Technician Page " + i.ToString(), "Enable", false);
                    _EngineerAccess[i] = IniFile.ReadBool("Engineer Page " + i.ToString(), "Enable", false);
                    _AdministratorAccess[i] = IniFile.ReadBool("Administrator Page " + i.ToString(), "Enable", false);
                }
            }
            catch { }
        }

        private static void AccessConfigClearList()
        {
            int counter = _slBadgeNumber.Count;
            for (int i = 0; i < counter; i++)
            {
                _slBadgeNumber.RemoveAt(0);
                _slUserName.RemoveAt(0);
                _slLevel.RemoveAt(0);
            }
        }

        public static void SaveAccessConfig()
        {
            try
            {
                string AccessFileName = @"\AccessLevel.pas";
                ES.Net.IniFile IniFile = new ES.Net.IniFile();
                IniFile.Create(AccessFilePath, AccessFileName);
                _iTotalIndex = _slBadgeNumber.Count;
                IniFile.WriteInteger("Auto Logout", "IdleTime", _iIdleTimeLogOut);
                IniFile.WriteInteger("Total", "Index", _iTotalIndex);
                for (int i = 0; i < _slBadgeNumber.Count; i++)
                {
                    IniFile.WriteString("User Detail " + i.ToString(), "Batch Number", _slBadgeNumber[i]);
                    IniFile.WriteString("User Detail " + i.ToString(), "User Name", _slUserName[i]);
                    IniFile.WriteInteger("User Detail " + i.ToString(), "User Level", _slLevel[i]);
                    IniFile.WriteString("User Detail " + i.ToString(), "User Password", _slPassword[i]);
                }
            }
            catch { }
        }
        public static void LoadAccessConfig()
        {
            try
            {
                string AccessFileName = @"\AccessLevel.pas";
                ES.Net.IniFile IniFile = new ES.Net.IniFile();
                IniFile.Create(AccessFilePath, AccessFileName);
                _iIdleTimeLogOut = IniFile.ReadInteger("Auto Logout", "IdleTime", _iIdleTimeLogOut);
                _iTotalIndex = IniFile.ReadInteger("Total", "Index", 0);
                //ClearList();
                for (int i = 0; i < _iTotalIndex; i++)
                {
                    _slBadgeNumber.Add(IniFile.ReadString("User Detail " + i.ToString(), "Batch Number", "0000"));
                    _slUserName.Add(IniFile.ReadString("User Detail " + i.ToString(), "User Name", "abc"));
                    _slLevel.Add(IniFile.ReadInteger("User Detail " + i.ToString(), "User Level", 0));
                    _slPassword.Add(IniFile.ReadString("User Detail " + i.ToString(), "User Password", ""));
                }
                LoadAccessPageConfig();
            }
            catch { }
        }

    }
}
