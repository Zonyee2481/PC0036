using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using ES.Net;
using System.Threading;
using System.Windows.Forms;
using Infrastructure;

namespace MotionIODevice
{
    /// <summary>
    /// Common Motion Control
    /// </summary>
    public class Common
    {
        public static bool Delay(int msdelay)
        {
            if (msdelay <= 0) { return true; }
            int t = Environment.TickCount + msdelay;

            while (true)
            {
                if (Environment.TickCount >= t) { break; }
                Thread.Sleep(0);
            }
            return true;
        }

        static Stopwatch sw_TickCount = new Stopwatch();
        public static int GetTickCount()
        {
            if (!sw_TickCount.IsRunning)
            {
                sw_TickCount.Start();
            }

            int D = (int)sw_TickCount.ElapsedMilliseconds;
            return D;
        }

        /// <summary>
        /// 
        /// </summary>
        public enum EDeviceType { NONE, P1240, P1245, P1265, P1285, MoonsSTF, B140 };

        public enum eMOVES
        {
            NA,
            MOVE_IN_POS,
            MOVE_SENS_ON,
            MOVE_SENS_OFF,
            MOVE_FAIL,
            SET_PARAM_FAIL,
            MTR_ALARM,
            ERR,
        }

        private static bool[] DevDependChecked = new bool[9] { false, false, false, false, false, false, false, false, false };

        /// <summary>
        /// Device Structure
        /// </summary>
        public class TDevice
        {
            public EDeviceType Type;
            public string IPAddress;
            public byte ID;
            public string Label;
            public string Name;
            public bool Enabled;
            public int CardNo;
            public bool EType;

            public TDevice(EDeviceType Type, string IPAddress, byte ID, string Label, string Name)
            {
                this.Type = Type;
                this.IPAddress = IPAddress;
                this.ID = ID;
                this.Name = Name;
                this.Label = Label;
                this.Enabled = true;
                this.CardNo = -1;
            }
            public TDevice(EDeviceType Type, byte ID, string Label, string Name, bool EType)
            {
                this.Type = Type;
                this.IPAddress = "192.168.1.100";
                this.ID = ID;
                this.Name = Name;
                this.Label = Label;
                this.Enabled = true;
                this.CardNo = -1;
                this.EType = EType;
            }

            public TDevice(EDeviceType Type, int CardNo, byte ID, string Label, string Name)
            {
                this.Type = Type;
                this.IPAddress = "192.168.1.100";
                this.ID = ID;
                this.Name = Name;
                this.Label = Label;
                this.Enabled = true;
                this.CardNo = CardNo;
            }

            public TDevice(EDeviceType Type, int CardNo, byte ID, string Label, string Name, bool EType)
            {
                this.Type = Type;
                this.IPAddress = "192.168.1.100";
                this.ID = ID;
                this.Name = Name;
                this.Label = Label;
                this.Enabled = true;
                this.CardNo = CardNo;
                this.EType = EType;
            }

            struct FileVer
            {
                public string FileName;
                public int Major;
                public int Minor;
                public int Build;
                public int Private;
                public FileVer(string FileName, int Major, int Minor, int Build, int Private)
                {
                    this.FileName = FileName;
                    this.Major = Major;
                    this.Minor = Minor;
                    this.Build = Build;
                    this.Private = Private;
                }
                public string FileVersion
                {
                    get { return Major.ToString() + "." + Minor.ToString() + "." + Build.ToString() + "." + Private.ToString(); }
                }
                public bool Validate(bool ShowMessage)
                {
                    if (!File.Exists(FileName))
                    {
                        if (ShowMessage)
                        {
                            MessageBox.Show(FileName + " not found.");
                        }
                        return false;
                    }
                    FileVersionInfo fvi_FoundVersion = FileVersionInfo.GetVersionInfo(FileName);
                    string FoundVersion = fvi_FoundVersion.FileVersion;

                    if (fvi_FoundVersion.FileMajorPart > this.Major)
                    {
                        return true;
                    }
                    else
                        if (fvi_FoundVersion.FileMinorPart > this.Minor)
                    {
                        return true;
                    }
                    else
                            if (fvi_FoundVersion.FileBuildPart > this.Build)
                    {
                        return true;
                    }
                    else
                                if (fvi_FoundVersion.FilePrivatePart >= this.Private)
                    {
                        return true;
                    }
                    if (ShowMessage)
                    {
                        MessageBox.Show(FileName + (char)13 + "Build ver " + FileVersion + ", Current ver " + FoundVersion, "File Version Mismatch");
                    }
                    return true;
                }
            }
            public bool CheckDependencies()
            {
                bool OK = true;

                switch (Type)
                {
                    case EDeviceType.P1240:
                        #region
                        {
                            if (!DevDependChecked[(int)Type])
                            {
                                string ads1240dll = "C:\\WINDOWS\\system32\\ads1240.dll";
                                if (!File.Exists(ads1240dll))
                                {
                                    OK = false;
                                    MessageBox.Show(ads1240dll + " not found.");
                                }
                            }
                        }
                        break;
                    #endregion
                    case EDeviceType.P1245:
                    case EDeviceType.P1265:
                        #region
                        {
                            if (!DevDependChecked[(int)Type])
                            {
                                FileVer file_P1265KernelDriver = new FileVer(@"c:\windows\system32\drivers\PCI1265s.sys", 2, 1, 4, 1);
                                if (OK) OK = file_P1265KernelDriver.Validate(true);

                                FileVer file_P1265UserDriver = new FileVer(@"c:\windows\system32\PCI1265.dll", 2, 1, 7, 1);
                                if (OK) OK = file_P1265UserDriver.Validate(true);

                                FileVer file_CommonMotionLibrary = new FileVer(@"c:\windows\system32\ADVMOT.dll", 1, 2, 3, 1);
                                if (OK) OK = file_CommonMotionLibrary.Validate(true);

                                FileVer file_DotNETLibrary = new FileVer(Path.GetDirectoryName(Application.ExecutablePath) + @"\AdvMotAPI.dll", 1, 1, 7, 1);
                                if (OK) OK = file_DotNETLibrary.Validate(true);

                                DevDependChecked[(int)Type] = true;
                            }
                            break;
                        }
                    #endregion
                    case EDeviceType.P1285:
                        #region
                        {
                            if (!DevDependChecked[(int)Type])
                            {
                                FileVer file_P1265KernelDriver = new FileVer(@"c:\windows\system32\drivers\PCI1285s.sys", 1, 2, 2, 1);
                                if (OK) OK = file_P1265KernelDriver.Validate(true);

                                FileVer file_P1265UserDriver = new FileVer(@"c:\windows\system32\PCI1285.dll", 1, 2, 2, 1);
                                if (OK) OK = file_P1265UserDriver.Validate(true);

                                FileVer file_CommonMotionLibrary = new FileVer(@"c:\windows\system32\ADVMOT.dll", 1, 4, 1, 1);
                                if (OK) OK = file_CommonMotionLibrary.Validate(true);

                                FileVer file_DotNETLibrary = new FileVer(Application.ExecutablePath + @"\AdvMotAPI.dll", 1, 2, 1, 1);
                                if (OK) OK = file_DotNETLibrary.Validate(true);

                                DevDependChecked[(int)Type] = true;
                            }
                            break;
                        }
                        #endregion
                }
                return OK;
            }
        }

        public class TMotion
        {
            public int CardNo;
            public int AxisNo;
            public TMotion(int CardNo, int AxisNo)
            {
                this.CardNo = CardNo;
                this.AxisNo = AxisNo;
            }
        }

        /// <summary>
        /// Type of TAxis
        /// </summary>
        public class TAxis
        {
            public TDevice Device;
            public byte Mask;
            public string Label;
            public string Name;
            public TAxisPara Para;
            public bool Enabled;
            public ushort NodeID;
            public bool ZEnable;
            public CardNumber CardNo;

            public int RunModule { get; set; }
            public int RunAxis { get; set; }



            public List<string> AxisPosName;
            public List<double> AxisPos;


            /// <summary>
            /// TAxis Structure by Device, Mask, Label, Name
            /// P1240 Axis = 0x01, 0x02, 0x04, 0x08
            /// P1245 Axis = 0 ~ 3
            /// P1265 Axis = 0 ~ 5
            /// ZKAZM30x Axis = 1 ~ 4,
            /// ZKAPCIe1245 Axis = 0 ~ 3
            /// </summary>
            /// <param name="Device"></param>
            /// <param name="Mask"></param>
            /// <param name="Label"></param>
            /// <param name="Name"></param>
            public TAxis(TDevice Device, byte Mask, string Label, string Name, bool ZEnable, CardNumber CardNo)
            {
                this.Device = Device;
                this.Mask = Mask;
                this.Label = Label;
                this.Name = Name;
                this.NodeID = 0;
                this.Para = new TAxisPara();
                this.Para.DeviceName = Device.Name;
                this.Para.AxisName = Name;
                this.Enabled = true;
                this.ZEnable = ZEnable;
                this.CardNo = CardNo;

                this.RunModule = -1;
                this.RunAxis = -1;

                AxisPosName = new List<string>();
                AxisPos = new List<double>();
            }
            /// <summary>
            /// TAxis Structure by Device, Mask, Label, Name
            /// P1240 Axis = 0x01, 0x02, 0x04, 0x08
            /// P1245 Axis = 0 ~ 3
            /// P1265 Axis = 0 ~ 5
            /// ZKAZM30x Axis = 1 ~ 4,
            /// ZKAPCIe1245 Axis = 0 ~ 3
            /// </summary>
            /// <param name="Device"></param>
            /// <param name="Mask"></param>
            /// <param name="Label"></param>
            /// <param name="Name"></param>
            public TAxis(TDevice Device, ushort _uNodeID, byte Mask, string Label, string Name)
            {
                this.Device = Device;
                this.Mask = Mask;
                this.Label = Label;
                this.Name = Name;
                this.NodeID = _uNodeID;
                this.Para = new TAxisPara();
                this.Para.DeviceName = Device.Name;
                this.Para.AxisName = Name;
                this.Enabled = true;

                this.RunModule = -1;
                this.RunAxis = -1;


                AxisPosName = new List<string>();
                AxisPos = new List<double>();
            }
            /// <summary>
            /// TAxis Structure by Device, Mask
            /// P1240 Axis = 0x01, 0x02, 0x04, 0x08
            /// P1245 Axis = 0 ~ 3
            /// P1265 Axis = 0 ~ 5
            /// ZKAZM30x Axis = 1 ~ 4,
            /// ZKAPCIe1245 Axis = 0 ~ 3
            /// </summary>
            /// <param name="Device"></param>
            /// <param name="Mask"></param>
            /// <param name="Label"></param>
            /// <param name="Name"></param>
            public TAxis(TDevice Device, byte Mask)
            {
                this.Device = Device;
                this.Mask = Mask;
                this.Label = "";
                this.Name = "";
                this.Para = new TAxisPara();
                this.Enabled = true;
                this.NodeID = 0;

                this.RunModule = -1;
                this.RunAxis = -1;


                AxisPosName = new List<string>();
                AxisPos = new List<double>();
            }
        }

        /// <summary>
        /// Type of TInput
        /// </summary>
        public class TInput
        {
            public TDevice Device;
            public byte Axis_Port;
            public ushort Mask;
            //public ushort Match;
            public string Label;
            public string Name;
            public string DisName;
            public bool Status;
            public bool Enabled;
            public ushort NodeID;

            /// <summary>
            /// TInput Structure by Device, Axis_Port, Mask, Label, Name
            /// P1240 Axis = 0x01, 0x02, 0x04, 0x08, Mask 0x[nil, nil, nEmg, nAlarm][nHLmtN, nHLmtP, nSLmtN, nSLmtP][nAlm, nInp, nEXN, nEXP][nIn3~nIn0]
            /// P1245 Axis = 0~3, Mask 0x[nil, nil, nEmg, nil][nHLmtN, nHLmtP, nSLmtN, nSLmtP][nAlm, nInp, nEXN, nEXP][nIn3~nIn0]
            /// P1265 Axis = 0~5, Mask 0x[nil, nil, nEmg, nil][nHLmtN, nHLmtP, nSLmtN, nSLmtP][nAlm, nInp, nEXN, nEXP][nIn3~nIn0]; Port = 6, Mask 0x[DI7~4][DI3~0]
            /// P1285 Axis = 0~7, Mask 0x[nil, nil, nEmg, nil][nHLmtN, nHLmtP, nSLmtN, nSLmtP][nAlm, nInp, nEXN, nEXP][nIn3~nIn0]
            /// PISO Port = 0~3, Mask = 0x[DI7~4][DI3~0]
            /// ZKAZM30x Axis = 1 ~ 4, Mask = MotorIn 0x16, 0x0[In4 ~ In1]
            /// ZKAZM30x Port = 0, Mask = 0 ~ 6
            /// ZKAZI3001 Port = 0, Mask = 0 ~ 31
            /// ZKAPCIe1245 Axis = 0 ~ 3
            /// </summary>
            /// <param name="Device"></param>
            /// <param name="Axis_Port"></param>
            /// <param name="Mask"></param>
            /// <param name="Label"></param>
            /// <param name="Name"></param>
            public TInput(TDevice Device, byte Axis_Port, ushort Mask, string Label, string Name, string DisplayName = null)
            {
                this.Device = Device;
                this.Axis_Port = Axis_Port;
                this.Mask = Mask;
                //if ((Device.Type == EDeviceType.P1240) && (Mask <= 8))
                //    this.Match = 0x0000;
                //else
                //    this.Match = Mask;
                this.Label = Label;
                this.Name = Name;
                this.DisName = DisplayName;
                this.Status = false;
                this.Enabled = true;
                this.NodeID = 0;
            }
            /// <summary>
            /// TInput Structure by Device, Axis_Port, Mask
            /// P1240 Axis = 0x01, 0x02, 0x04, 0x08, Mask 0x[nil, nil, nEmg, nAlm][nHLmtN, nHLmtP, nSLmtN, nSLmtP][nAlm, nInp, nEXN, nEXP][nIn3~nIn0]
            /// P1245 Axis = 0~3, Mask 0x[nil, nil, nEmg, nil][nHLmtN, nHLmtP, nSLmtN, nSLmtP][nAlm, nInp, nEXN, nEXP][nIn3~nIn0]
            /// P1265 Axis = 0~5, Mask 0x[nil, nil, nEmg, nil][nHLmtN, nHLmtP, nSLmtN, nSLmtP][nAlm, nInp, nEXN, nEXP][nIn3~nIn0]; Port = 6, Mask 0x[DI7~4][DI3~0]
            /// P1285 Axis = 0~7, Mask 0x[nil, nil, nEmg, nil][nHLmtN, nHLmtP, nSLmtN, nSLmtP][nAlm, nInp, nEXN, nEXP][nIn3~nIn0]
            /// PISO Port = 0~3, Mask 0x[DI7~4][DI3~0]
            /// ZKAZM30x Axis = 1 ~ 4, Mask = MotorIn 0x16, 0x0[In4 ~ In1]
            /// ZKAZM30x Port = 0, Mask = 0 ~ 6
            /// ZKAZI3001 Port = 0, Mask = 0 ~ 31
            /// ZKAPCIe1245 Axis = 0 ~ 3
            /// </summary>
            /// <param name="Device"></param>
            /// <param name="Axis_Port"></param>
            /// <param name="Mask"></param>
            public TInput(TDevice Device, byte Axis_Port, ushort Mask)
            {
                this.Device = Device;
                this.Axis_Port = Axis_Port;
                this.Mask = Mask;
                //if ((Device.Type == EDeviceType.P1240) && (Mask <= 8))
                //    this.Match = 0x0000;
                //else
                //    this.Match = Mask;
                this.Label = "";
                this.Name = "";
                this.Status = false;
                this.Enabled = true;
                this.NodeID = 0;
            }
            /// <summary>
            /// TInput Structure by Device, Axis_Port, Mask
            /// MoonsStf NodeId = 1, 2, 3, 4 and soo on
            /// Mask 0x[nil, nil, nEmg, nAlm][nHLmtN, nHLmtP, nSLmtN, nSLmtP][nAlm, nInp, nEXN, nEXP][nIn3~nIn0]

            /// </summary>
            /// <param name="Device"></param>
            /// <param name="Axis_Port"></param>
            /// <param name="Mask"></param>
            public TInput(TDevice Device, ushort NodeID, byte Axis_Port, ushort Mask, string Label, string Name)
            {
                this.Device = Device;
                this.Axis_Port = Axis_Port;
                this.Mask = Mask;
                //if ((Device.Type == EDeviceType.P1240) && (Mask <= 8))
                //    this.Match = 0x0000;
                //else
                //    this.Match = Mask;
                this.Label = Label;
                this.Name = Name;
                this.Status = false;
                this.Enabled = true;
                this.NodeID = NodeID;
            }

            public TInput()
            {

            }
        }
        /// <summary>
        /// Type of TOutput
        /// </summary>
        public class TOutput
        {
            public TDevice Device;
            public byte Axis_Port;
            public UInt32 Mask;
            public string Label;
            public string Name;
            public string DisName;
            public bool Status;
            public bool Enabled;
            public ushort NodeID;

            /// <summary>
            /// TOuput Structure by Device, Axis_Port, Mask, Label, Name
            /// P1240 Axis = 0x01, 0x02, 0x04, 0x08, , Mask 0x0[nOut7~nOut4]
            /// P1245 Axis = 0~3, Mask 0x0[nOut7~nOut4]
            /// P1265 Axis = 0~5, Mask 0x0[nOut7~nOut4]; Port=6, Mask 0x[DO7~DO4][DO3~DO0]
            /// P1285 Axis = 0~7, Mask 0x0[nOut7~nOut4]
            /// PISO Port = 0~3, Mask 0x[DO7~DO4][DO3~DO0]
            /// ZKAZM30x Port = 0, Mask = 0x01 ~ 0x80
            /// ZKAZI3001 Port = 0, Mask = 0x01 ~ 0x0400
            /// ZKAPCIe1245 Axis = 0 ~ 3
            /// </summary>
            /// <param name="Device"></param>
            /// <param name="Axis_Port"></param>
            /// <param name="Mask"></param>
            /// <param name="Label"></param>
            /// <param name="Name"></param>
            public TOutput(TDevice Device, byte Axis_Port, UInt32 Mask, string Label, string Name, string DisplayName = null)
            {
                this.Device = Device;
                this.Axis_Port = Axis_Port;
                this.Mask = Mask;
                this.Label = Label;
                this.Name = Name;
                this.DisName = DisplayName;
                this.Status = false;
                this.Enabled = true;
            }
            /// <summary>
            /// TOuput Structure by Device, Axis_Port, Mask
            /// P1240 Axis = 0x01, 0x02, 0x04, 0x08, , Mask 0x0[nOut7~nOut4]
            /// P1245 Axis = 0~3; Mask 0x0[nOut7~nOut4]
            /// P1265 Axis = 0~5, Mask 0x0[nOut7~nOut4]; Port=6, Mask 0x[DO7~DO4][DO3~DO0]
            /// P1285 Axis = 0~5, Mask 0x0[nOut7~nOut4]
            /// PISO Port = 0 ~ 3, Mask = 0x01 ~ 0x80
            /// ZKAZM30x Port = 0, Mask = 0x01 ~ 0x80
            /// ZKAZI3001 Port = 0, Mask = 0x01 ~ 0x0400
            /// ZKAPCIe1245 Axis = 0 ~ 3
            /// </summary>
            /// <param name="Device"></param>
            /// <param name="Axis_Port"></param>
            /// <param name="Mask"></param>
            public TOutput(TDevice Device, byte Axis_Port, UInt32 Mask)
            {
                this.Device = Device;
                this.Axis_Port = Axis_Port;
                this.Mask = Mask;
                this.Label = "";
                this.Name = "";
                this.Status = false;
                this.Enabled = true;
            }

            /// <summary>
            /// TOuput Structure by Device, NodeID, Axis_Port, Mask
            /// P1240 Axis = 0x01, 0x02, 0x04, 0x08, , Mask 0x0[nOut7~nOut4]
            /// P1245 Axis = 0~3; Mask 0x0[nOut7~nOut4]
            /// P1265 Axis = 0~5, Mask 0x0[nOut7~nOut4]; Port=6, Mask 0x[DO7~DO4][DO3~DO0]
            /// P1285 Axis = 0~5, Mask 0x0[nOut7~nOut4]
            /// PISO Port = 0 ~ 3, Mask = 0x01 ~ 0x80
            /// ZKAZM30x Port = 0, Mask = 0x01 ~ 0x80
            /// ZKAZI3001 Port = 0, Mask = 0x01 ~ 0x0400
            /// ZKAPCIe1245 Axis = 0 ~ 3
            /// </summary>
            /// <param name="Device"></param>
            /// <param name="NodeID"></param>
            /// <param name="Axis_Port"></param>
            /// <param name="Mask"></param>
            public TOutput(TDevice Device, ushort NodeID, byte Axis_Port, ushort Mask, string Label, string Name)
            {
                this.Device = Device;
                this.Axis_Port = Axis_Port;
                this.Mask = Mask;
                this.Label = Label;
                this.Name = Name;
                this.Status = false;
                this.Enabled = true;
                this.NodeID = NodeID;
            }
        }
        public enum EOutputStatus { Lo, Hi, St };

        public class TRunModule
        {
            public int RunModule;
            public int MtrAxis;
            public bool ZEnable;
            public CardNumber CardNo;

            public List<string> AxisPosName;
            public List<double> AxisPos;

            public TRunModule(CardNumber CardNo, int RunModule, int MtrAxis, bool ZEnable = false)
            {
                this.RunModule = RunModule;
                this.MtrAxis = MtrAxis;
                this.ZEnable = ZEnable;
                this.CardNo = CardNo;

                AxisPosName = new List<string>();
                AxisPos = new List<double>();
            }

            public void AddPos(string PosName)
            {
                AxisPosName.Add(PosName);
                AxisPos.Add(0.00);
            }
        }

        public class TAxisUnit
        {
            public string Name;
            public double Resolution;
            public string IP;
        }
        public enum EHomeDir { N, P };
        public class TAxisHomePara
        {
            public double SlowV, FastV;
            public int Timeout;
            public EHomeDir Dir;
            public bool InvertHomeLmt;
        }
        public class TAxisJogPara
        {
            public double SlowV, MedV, FastV;
            public double Sel;
        }
        public enum EAxisSwLimitType { Disable, Logical, Real };
        public class TAxisSwLimit
        {
            public EAxisSwLimitType LimitType;
            public double PosN, PosP;
        }
        public class TAxisHwLimit
        {
            public bool Invert;
        }
        public class TAxisPsnt
        {
            public double StartV;
            public double DriveV;
            public double Accel;
            public double Decel;
        }
        public enum EMotorAlarmType { None, NC, NO };
        public class TAxisPara
        {
            //public TAxis Axis;
            public string DeviceName;
            public string AxisName;
            public bool InvertMtrOn;
            public bool InvertPulse;
            public bool Encoder;
            public TAxisUnit Unit;
            public uint Multiplier;
            public double Accel, StartV, SlowV, MedV, FastV;
            public TAxisPsnt Psnt;
            public TAxisHomePara Home;
            public TAxisJogPara Jog;
            public TAxisSwLimit SwLimit;
            public TAxisHwLimit HwLimit;
            public EMotorAlarmType MotorAlarmType;

            public TAxisPara()
            {
                DeviceName = "Device";
                AxisName = "Axis";
                InvertMtrOn = false;
                InvertPulse = false;
                Encoder = true;
                Unit = new TAxisUnit { Name = "mm", Resolution = (float)0.001 };
                Multiplier = 1;
                Accel = 1;
                StartV = 1;
                SlowV = 1;
                MedV = 1;
                FastV = 1;
                Psnt = new TAxisPsnt { StartV = 1, DriveV = 1, Accel = 1 };
                Home = new TAxisHomePara { SlowV = 1, FastV = 1, Timeout = 10 };
                Jog = new TAxisJogPara { SlowV = 1, MedV = 1, FastV = 1 };
                SwLimit = new TAxisSwLimit { LimitType = EAxisSwLimitType.Disable, PosN = 0, PosP = 0 };
                HwLimit = new TAxisHwLimit { Invert = false };
                MotorAlarmType = EMotorAlarmType.NO;
            }

            public bool ReadInifile(string FileName, string SectionName)
            {
                IniFile Inifile = new IniFile();

                try
                {
                    Inifile.Create(FileName);
                    InvertMtrOn = Inifile.ReadBool(SectionName, "InvertMtrOn", false);
                    InvertPulse = Inifile.ReadBool(SectionName, "InvertPulse", false);
                    Encoder = Inifile.ReadBool(SectionName, "Encoder", true);
                    Unit.Name = Inifile.ReadString(SectionName, "UnitName", "mm");
                    Unit.Resolution = Inifile.ReadDouble(SectionName, "UnitResolution", 0.001);
                    Multiplier = (uint)Inifile.ReadInteger(SectionName, "Multiplier", 1);
                    Accel = Inifile.ReadDouble(SectionName, "Accel", 1);
                    StartV = Inifile.ReadDouble(SectionName, "StartV", 1);
                    SlowV = Inifile.ReadDouble(SectionName, "SlowV", 1);
                    MedV = Inifile.ReadDouble(SectionName, "MedV", 1);
                    FastV = Inifile.ReadDouble(SectionName, "FastV", 1);
                    Home.SlowV = Inifile.ReadDouble(SectionName, "HomeSlowV", 1);
                    Home.FastV = Inifile.ReadDouble(SectionName, "HomeFastV", 1);
                    Home.Timeout = Inifile.ReadInteger(SectionName, "HomeTimeout", 10);
                    Home.Dir = (EHomeDir)Inifile.ReadInteger(SectionName, "HomeDir", 0);
                    Home.InvertHomeLmt = Inifile.ReadBool(SectionName, "InvertHomeLimit", false);
                    Jog.SlowV = Inifile.ReadDouble(SectionName, "JogSlowV", 1);
                    Jog.MedV = Inifile.ReadDouble(SectionName, "JogMedV", 1);
                    Jog.FastV = Inifile.ReadDouble(SectionName, "JogFastV", 1);
                    //SwLimit.Enable = Inifile.ReadBool(SectionName, "SwLimitEnable", false);
                    SwLimit.LimitType = (EAxisSwLimitType)Inifile.ReadInteger(SectionName, "SwLimitType", 0);
                    SwLimit.PosN = Inifile.ReadDouble(SectionName, "SwLimitPosN", 1);
                    SwLimit.PosP = Inifile.ReadDouble(SectionName, "SwLimitPosP", 1);
                    HwLimit.Invert = Inifile.ReadBool(SectionName, "HwLimitInvert", false);
                    MotorAlarmType = (EMotorAlarmType)Inifile.ReadInteger(SectionName, "MotorAlarmType", (int)EMotorAlarmType.NO);
                }
                catch { throw; };

                return true;
            }
            public bool WriteInifile(string FileName, string SectionName)
            {
                IniFile Inifile = new IniFile();

                try
                {
                    Inifile.Create(FileName);
                    Inifile.WriteString(SectionName, "AxisName", AxisName);
                    Inifile.WriteBool(SectionName, "InvertMtrOn", InvertMtrOn);
                    Inifile.WriteBool(SectionName, "InvertPulse", InvertPulse);
                    Inifile.WriteBool(SectionName, "Encoder", Encoder);
                    Inifile.WriteString(SectionName, "UnitName", Unit.Name);
                    Inifile.WriteDouble(SectionName, "UnitResolution", Unit.Resolution);
                    Inifile.WriteInteger(SectionName, "Multiplier", (int)Multiplier);
                    Inifile.WriteDouble(SectionName, "Accel", Accel);
                    Inifile.WriteDouble(SectionName, "StartV", StartV);
                    Inifile.WriteDouble(SectionName, "SlowV", SlowV);
                    Inifile.WriteDouble(SectionName, "MedV", MedV);
                    Inifile.WriteDouble(SectionName, "FastV", FastV);
                    Inifile.WriteDouble(SectionName, "HomeSlowV", Home.SlowV);
                    Inifile.WriteDouble(SectionName, "HomeFastV", Home.FastV);
                    Inifile.WriteInteger(SectionName, "HomeTimeout", (int)Home.Timeout);
                    Inifile.WriteInteger(SectionName, "HomeDir", (int)Home.Dir);
                    Inifile.WriteBool(SectionName, "InvertHomeLimit", Home.InvertHomeLmt);
                    Inifile.WriteDouble(SectionName, "JogSlowV", Jog.SlowV);
                    Inifile.WriteDouble(SectionName, "JogMedV", Jog.MedV);
                    Inifile.WriteDouble(SectionName, "JogFastV", Jog.FastV);
                    //Inifile.WriteBool(SectionName, "SwLimitEnable", SwLimit.Enable);
                    Inifile.WriteInteger(SectionName, "SwLimitType", (int)SwLimit.LimitType);
                    Inifile.WriteDouble(SectionName, "SwLimitPosN", SwLimit.PosN);
                    Inifile.WriteDouble(SectionName, "SwLimitPosP", SwLimit.PosP);
                    //Inifile.WriteBool(SectionName, "HwLimitEnable", HwLimit.Enable);
                    Inifile.WriteBool(SectionName, "HwLimitInvert", HwLimit.Invert);
                    Inifile.WriteInteger(SectionName, "MotorAlarmType", (int)MotorAlarmType);
                }
                catch { throw; }

                return true;
            }

            /*
            public bool LoadLocal()
            {
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string file = Path.GetFileNameWithoutExtension(Assembly.GetAssembly(typeof(P1265)).CodeBase);

                return ReadInifile(path + "\\" + file + ".ini", DeviceName + "_" + AxisName);
            }
            public bool SaveLocal()
            {
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string file = Path.GetFileNameWithoutExtension(
                                     Assembly.GetAssembly(typeof(P1265)).CodeBase);

                return WriteInifile(path + "\\" + file + ".ini", DeviceName + "_" + AxisName);
            }
            */
        }
    }
}
