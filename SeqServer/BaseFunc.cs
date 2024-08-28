using Infrastructure;
using KAEventPool;
using MotionIODevice;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using SeqServer.Utilities.ModEventArg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static MotionIODevice.Common;

namespace SeqServer
{
    public abstract class BaseFunc
    {
        public struct MyVariable
        {
            public bool InitDone;
            public bool ProcStart;
            public bool ProcReady;
            public bool ProcBusy;
            public bool ProcComp;
            public bool ProcSkip;
            public bool ProcAbort;
            public bool ProcComp2;
            //Handshake Flag
            public bool ItemReq;
            public bool ItemAvail;
            public bool ItemTaken;
            public bool ItemGiven;
            public bool SeqIntLComp;

            //New
            public bool PnPTrafficIntlComp;
            public bool PnPDone;
            public bool WorkReq;
            public bool ItemOut;
            public bool Handshake1;
            public bool Handshake1Done;
            //public bool ItemOutApprove;
            public bool ItemReceived;
            public bool UIReply;
            public bool MCResumeSeq;
        }

        internal bool m_Enable = false;
        internal MyVariable m_ModFlag = new MyVariable();
        public static tagSeqFlag m_SeqFlag = new tagSeqFlag();

        internal Thread bgw_AutoRun;
        protected CModEventArg m_ModEvArg = new CModEventArg();

        protected object m_SyncEvent = new object();
        protected object m_SyncSN = new object();
        protected object m_SyncLog = new object();

        internal static LotInfo m_LotInfo = new LotInfo();
        internal static List<IMotionControl> m_motionBoards = null;
        internal static List<IBoardIO> m_iocontrols = null;
        internal static IMainConnection m_communicationcontrols = null;
        internal EventPool m_UIEventPool = null;
        internal EventPool m_SeqEventPool = new EventPool();
        internal event EventHandler OnSeq2UI = null;

        internal List<FixZone> m_FixZoneData;
        internal List<StationCfg> m_StationData;
        internal StationCfg m_StationCdf;
        internal List<TAxis> m_Axes = new List<TAxis>();
        internal List<MotionIODevice.IO.TInput> m_Inputs = new List<MotionIODevice.IO.TInput>();
        internal List<MotionIODevice.IO.TOutput> m_Outputs=  new List<MotionIODevice.IO.TOutput>();               
        internal List<ErrData> m_ErrorData = new List<ErrData>() { };
        internal CycleTime m_CycleTime;
        internal List<BitCodeInfo> m_BitCode = new List<BitCodeInfo>() { };

        /// <summary>
		/// Issue a move relative command. Typical usage: Row / Column repeatative indexing.
		/// </summary>
		/// <param name="axis">Motion Config Setting</param>
		/// <param name="startv">Velocity Profile</param>
		/// <param name="driveV">Position Profile</param>
		/// <param name="accel">Direction</param>
        internal virtual void SetMotionParam(Veloctiy axis, string startv, string driveV, string accel, string path, string txtfilename)
        {
            ReadWritefile readWritefile = new ReadWritefile();
            // Soon to convert the parameter all to the type class
            // 1. axis - axis class from ttot
            // 2. axis.tostring() - at txt file to be converted and use the same as axis class from ttot.
            // 3. txtfilename - the file name of the txt file (assume all the txt file is stored under same path)
            readWritefile.ReadDynamicClassAndWritevariable(axis, axis.ToString(), path, txtfilename);

            //string ExMsg = "SetMotionParam";

            //double d_StartV = StartV;
            //double d_DriveV = DriveV;

            //if (d_StartV > d_DriveV) d_StartV = d_DriveV;

            //try
            //{
            //    TaskP1245.P1245.SetStartV(Axis, d_StartV);
            //    TaskP1245.P1245.SetDriveV(Axis, d_DriveV);
            //    TaskP1245.P1245.SetAccel(Axis, Accel);
            //}
            //catch (Exception Ex)
            //{
            //    ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
            //    //throw new Exception(ExMsg);
            //    MessageBox.Show(ExMsg);
            //}
            //return true;
        }

        internal virtual void StartThread()
        {

        }

        internal virtual void ThreadDeclaration_Normal(Action run)
        {
            bgw_AutoRun = new Thread(() => run());
            bgw_AutoRun.Priority = ThreadPriority.Normal;
            bgw_AutoRun.IsBackground = true;
            bgw_AutoRun.Start();
        }

        internal virtual void ThreadDeclaration_HighSpeed(Action run)
        {
            bgw_AutoRun = new Thread(() => run());
            bgw_AutoRun.Priority = ThreadPriority.AboveNormal;
            bgw_AutoRun.IsBackground = true;
            bgw_AutoRun.Start();
        }

        internal event EventHandler UI_Event
        {
            add { OnSeq2UI += value; }
            remove { OnSeq2UI -= value; }
        }

        protected void FireEvent2UI(EventArgs evArg)
        {
            if (OnSeq2UI != null && evArg != null)
            {
                OnSeq2UI(this, evArg);
            }
        }

        internal virtual void OnBegin(object sender, EventArgs args)
        {
        }        

        internal virtual void OnKill(object sender, EventArgs args)
        {

        }                       

        internal virtual void OnInit(object sender, EventArgs args)
        {
        }
       
        internal virtual void OnInitDone(object sender, EventArgs args)
        {
            m_ModFlag.InitDone = true;
        }

        internal virtual void OnFarProcReady(object sender, EventArgs args)
        {
            // Default Implementation
            m_ModFlag.ProcReady = true;
        }

        // Handle the event when sender module has completed its process sequence
        internal virtual void OnFarProcComp(object sender, EventArgs args)
        {
            // Default Implementation
            m_ModFlag.ProcComp = true;
        }

        internal virtual void OnFarProcComp2(object sender, EventArgs args)
        {
            // Default Implementation
            m_ModFlag.ProcComp2 = true;
        }
            
        // Handle the event when sender module is busy executing sequence.
        internal virtual void OnFarProcBusy(object sender, EventArgs args)
        {
            // Default Implementation
            m_ModFlag.ProcBusy = true;
        }

        // Handle the event when sender module has aborted its current process sequence.
        internal virtual void OnFarProcAbort(object sender, EventArgs args)
        {
            // Default Implementation
            m_ModFlag.ProcAbort = true;
        }

        // Handle the event when sender module request to skip the current module's process sequence.
        internal virtual void OnFarProcSkip(object sender, EventArgs args)
        {            
            // Default Implementation
            m_ModFlag.ProcSkip = true;
        }

        internal virtual void OnFarProcStart(object sender, EventArgs args)
        {
            m_ModFlag.ProcStart = true;
        }

        internal virtual void OnWorkReq(object sender, EventArgs args)
        {
            // Default Implementation
            m_ModFlag.WorkReq = true;
        }

        internal virtual void OnNewItemReq(object sender, EventArgs args)
        {
            // Default Implementation
            m_ModFlag.ItemReq = true;
        }

        internal virtual void OnNewItemAvail(object sender, EventArgs args)
        {
            // Default Implementation
            m_ModFlag.ItemAvail = true;
        }

        internal virtual void OnItemTaken(object sender, EventArgs args)
        {
            // Default Implementation
            m_ModFlag.ItemTaken = true;
        }

        internal virtual void OnItemGiven(object sender, EventArgs args)
        {
            // Default Implementation
            m_ModFlag.ItemGiven = true;
        }

        internal virtual void OnPnPTrafficIntlChk(object sender, EventArgs args)
        {

        }

        internal virtual void OnPnPTrafficIntlComp(object sender, EventArgs args)
        {
            m_ModFlag.PnPTrafficIntlComp = true;
        }

        internal virtual void OnDonePnpInterLChk(object sender, EventArgs args)
        {

        }

        internal virtual void OnDonePnpInterLComp(object sender, EventArgs args)
        {
            m_ModFlag.PnPDone = true;
        }
        
        internal virtual void OnItemOut(object sender, EventArgs args)
        {
            m_ModFlag.ItemOut = true;
        }

        internal virtual void OnItemReceived(object sender, EventArgs args)
        {
            m_ModFlag.ItemReceived = true;
        }

        internal virtual void OnSeqIntLChk(object sender, EventArgs args)
        {
        }       

        internal virtual void OnConvIntLComp(object sender, EventArgs args)
        {
        }

        internal virtual void OnConvIntLChk(object sender, EventArgs args)
        {
        }

        internal virtual void OnSeqIntLComp(object sender, EventArgs args)
        {
            m_ModFlag.SeqIntLComp = true;
        }
        
        internal virtual void OnRetryReq(object sender, EventArgs args)
        {           
        }

        internal virtual void OnMCStopSeq(object sender, EventArgs args)
        {

        }

        internal virtual void OnMCResumeSeq(object sender, EventArgs args)
        {
            m_ModFlag.MCResumeSeq = true;
        }

        internal virtual void OnStepChange(object sender, EventArgs args)
        {

        }

        internal virtual void OnTriggerConvIn(object sender, EventArgs args)
        {

        }

        internal virtual void OnTriggerConvOut(object sender, EventArgs args)
        {

        }

        internal virtual void OnManualSeq(object sender, EventArgs args)
        {

        }

        internal virtual void OnHandhsake1(object sender, EventArgs args)
        {
            m_ModFlag.Handshake1 = true;
        }
        internal virtual void OnHandhsake1Done(object sender, EventArgs args)
        {
            m_ModFlag.Handshake1Done = true;
        }

        internal virtual bool OutBit_Mot(int m_axis, TOutputStatus st)
        {
#if SIMULATION
            return true;
#else
            var Axis = m_Axes[m_axis];
            bool status = st == TOutputStatus.Hi ? true : false;

            return m_motionBoards[(int)Axis.CardNo].Servo(m_axis, status);
#endif
        }

        internal virtual bool SensHome(int m_axis)
        {
#if SIMULATION
            return true;
#else
            var Axis = m_Axes[m_axis];
            return m_motionBoards[(int)Axis.CardNo].SensHome(Axis.RunAxis);
#endif
        }

        internal virtual bool SensNLimit(int m_axis)
        {
#if SIMULATION
            return true;
#else
            var Axis = m_Axes[m_axis];
            return m_motionBoards[(int)Axis.CardNo].SensLmtN(Axis.RunAxis);
#endif
        }

        internal virtual bool ForceAxisStop(int m_axis)
        {
#if SIMULATION
            return true;
#else
            var Axis = m_Axes[m_axis];
            return m_motionBoards[(int)Axis.CardNo].ForceStop(Axis.RunAxis);
#endif
        }
        
        internal virtual bool Home(int m_axis)
        {
            //var Axis = m_Axes.FirstOrDefault(x => x.Name == m_axis.ToString());
#if SIMULATION
            return true;
#else
            var Axis = m_Axes[m_axis];

            HomingType motortype = new HomingType();

            var AxisName = (TotalAxis)Enum.Parse(typeof(TotalAxis), Axis.Name);

            switch (AxisName)
            {
                case TotalAxis.Rotation:
                    {
                        motortype = HomingType.Without_NP;
                    }
                    break;
                case TotalAxis.UnloadRotary:
                    {
                        motortype = HomingType.Without_NP;
                    }
                    break;
                case TotalAxis.LoadingStnZ:
                    {
                        motortype = HomingType.LimitType;
                    }
                    break;
                case TotalAxis.UnloadStnZ:
                    {
                        motortype = HomingType.LimitType;
                    }
                    break;
            }

            return m_motionBoards[(int)Axis.CardNo].Home(Axis.RunAxis, motortype);
#endif
        }

        internal virtual bool MoveRelative(int m_axis, int posname, int Timeout)
        {

#if SIMULATION
            return true;
#else
            //var Axis = m_Axes.FirstOrDefault(x => x.Name == m_axis.ToString());
            var Axis = m_Axes[m_axis];
            return m_motionBoards[(int)Axis.CardNo].MoveRel(Axis.RunAxis, Axis.AxisPos[posname], Timeout);
#endif
        }

        internal virtual bool MoveAbs_Pos(int m_axis, int posname , int Timeout)
        {
#if SIMULATION
            return true;
#else
            //var Axis = m_Axes.FirstOrDefault(x => x.Name == m_axis.ToString());
            var Axis = m_Axes[m_axis];
            return m_motionBoards[(int)Axis.CardNo].MoveAbs(Axis.RunAxis, Axis.AxisPos[posname], Timeout);
#endif
        }

        internal virtual bool MoveAbs_Spd(int m_axis, int posname, int spdselection ,int Timeout)
        {
#if SIMULATION
            return true;
#else
            //spdselection:
            //1. Slow
            //2. Fast
            //3. Med

            //var Axis = m_Axes.FirstOrDefault(x => x.Name == m_axis.ToString());
            var Axis = m_Axes[m_axis];
            return m_motionBoards[(int)Axis.CardNo].MoveAbsSpd(Axis.RunAxis, Axis.AxisPos[posname], spdselection, Timeout);
#endif
        }

        internal virtual bool MoveAbs_PosWOffset(int m_axis, int posname ,int spdselection, double offset, int Timeout)
        {

#if SIMULATION
            return true;
#else
            var Axis = m_Axes[m_axis];
            var offsetpos = Axis.AxisPos[posname] + offset;
            //spdselection
            //0 = slow
            //1 = fast
            return m_motionBoards[(int)Axis.CardNo].MoveAbsSpd(Axis.RunAxis, offsetpos, spdselection, Timeout);
#endif
            }

        internal virtual bool MoveAbsLine(int m_axis1, int m_axis2, int posname1, int posname2, int Timeout)
        {

#if SIMULATION
            return true;
#else
            var Axis1 = m_Axes[m_axis1];
            var Axis2 = m_Axes[m_axis2];
            return m_motionBoards[Axis1.Device.CardNo].MoveAbsLine(Axis1.RunAxis, Axis2.RunAxis, Axis1.AxisPos[posname1], Axis1.AxisPos[posname2], Timeout);
#endif
            //var Axis1 = m_Axes.FirstOrDefault(x => x.Name == m_axis1.ToString());
            //var Axis2 = m_Axes.FirstOrDefault(x => x.Name == m_axis2.ToString());

        }

        internal virtual bool Jog(int m_axis, bool bPositive, int spd)
        {
            //var Axis = m_Axes.FirstOrDefault(x => x.Name == m_axis.ToString());
            var Axis = m_Axes[m_axis];
            return m_motionBoards[(int)Axis.CardNo].Jog(Axis.RunAxis, bPositive, spd);
        }

        internal virtual void SetPos(int m_axis, double pos, bool bLogic)
        {
#if SIMULATION
            return;
#else
            var Axis = m_Axes[m_axis];
            if (bLogic)
            {
                m_motionBoards[(int)Axis.CardNo].SetLogicPos(Axis.RunAxis, pos);
            }
            else
            {
                m_motionBoards[(int)Axis.CardNo].SetRealPos(Axis.RunAxis, pos);
            }
#endif
        }

        internal virtual double GetPos(int m_axis, bool bLogic)
        {
#if SIMULATION
            return 0.000;
#else
            var Axis = m_Axes[m_axis];
            if (bLogic)
            {
                return m_motionBoards[(int)Axis.CardNo].GetLogicPos(Axis.RunAxis);
            }
            else
            {
                return m_motionBoards[(int)Axis.CardNo].GetRealPos(Axis.RunAxis);
            }
#endif
        } // ZY

        internal virtual bool ReadBit(string Input)
        {
            //var _input = (MotionIODevice.IO.TInput)Input;

            //_input = m_Inputs.FirstOrDefault(x => x.Name == _input.ToString());
#if SIMULATION
            return true;
#else
            var _input = m_Inputs.FirstOrDefault(x => x.Name == Input);
            return m_iocontrols[_input.BoardID].ReadBit(_input.Bit);
#endif
        }

        internal virtual bool OutBit(string Output, TOutputStatus status)
        {
            //var _output = (MotionIODevice.IO.TOutput)Output;

            //_output = m_Outputs.FirstOrDefault(x => x.Name == _output.ToString());
#if SIMULATION
            return true;
#else
            var _output = m_Outputs.FirstOrDefault(x => x.Name == Output);
            return m_iocontrols[_output.BoardID].OutBit(_output.Bit, status);
#endif

        }        

        internal virtual string GetSeqNum()
        {
            return string.Empty;
        }

        internal virtual void BypassStation(bool _enable)
        {
            m_Enable = _enable;
        }

        internal virtual int GetProdCntNum()
        {
            return 0;
        }

        internal virtual void SetProdCntNum()
        {
        }

        internal virtual double GetCycleTime()
        {
            return 0.0;
        }

        internal virtual LotCounter LotCounter
        {
            get;set;
        }

        private int FindLastUsedRow = 1;
        private int FindLastUsedColumn = 1;
        private int currentRow = 1;
        private int currentColumn = 1;
        private int TotalRow = 50;

        internal virtual void WriteInfo(string PassFail, string RunningNum, string Barcode)
        {
            string MM = DateTime.Now.Month.ToString();
            if (MM.Length == 1) { MM = "0" + MM; }
            string YYYY = DateTime.Now.Year.ToString();
            string DD = DateTime.Now.Day.ToString();
            if (DD.Length == 1) { DD = "0" + DD; }

            DateTime time = DateTime.Now;

            string DataDir = @"C:\Machine\OutQCReport\" + YYYY + MM + DD + @"\";
            //if (header) if (Directory.Exists(DataDir)) return;
            if (!Directory.Exists(DataDir))
            {
                Directory.CreateDirectory(DataDir);

            }

            string PassFailDir = DataDir + $"{PassFail}" + @"\";
            if (!Directory.Exists(PassFailDir))
            {
                Directory.CreateDirectory(PassFailDir);

            }

            string excelFilePath = PassFailDir + m_LotInfo.LotNum + ".xlsx";
            FileInfo excelFile = new FileInfo(excelFilePath);
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(excelFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                if (worksheet == null)
                {
                    FindLastUsedRow = 1;
                    // If worksheet doesn't exist, create a new one
                    worksheet = package.Workbook.Worksheets.Add("Sheet1");
                    //worksheet.HeaderFooter.OddHeader.CenteredText = $"Laser Marking 2DID Running No  LotNumber: {m_LotInfo.LotNum}     " +
                    //    $"Model: {m_LotInfo.PartName}   " +
                    //    $"PartNo: {m_LotInfo.PartNum}   " +
                        //$"R: {m_LotInfo._RecipeInfo._iRow.ToString()}  " +
                        //$"C: {m_LotInfo._RecipeInfo._iCol.ToString()}";
                        //$"Start Time: {DateTime.Now.Date.ToString("dd-MM-yyyy")}, {DateTime.Now.ToString("HH: mm: ss tt")}";

                    //worksheet.Cells[1, 1].Value = "LotNumber:" + "," + m_LotInfo.LotNum;
                    //worksheet.Cells[2, 1].Value = "PartNum:" + "," + m_LotInfo.PartNum;
                    //worksheet.Cells[3, 1].Value = "Row:" + "," + m_LotInfo._RecipeInfo._iRow.ToString();
                    //worksheet.Cells[4, 1].Value = "Column:" + "," + m_LotInfo._RecipeInfo._iCol.ToString();
                    //worksheet.Cells[5, 1].Value = "Start Time:" + "," + DateTime.Now.Date.ToString("dd-MM-yyyy") + "," + DateTime.Now.ToString("HH:mm:ss tt");
                    //worksheet.Cells[6, 1].Value = " ";
                    //worksheet.Column(1).AutoFit();
                }

                if (worksheet.Dimension != null)
                {
                    FindLastUsedColumn = worksheet.Dimension.End.Column;
                }
                else
                {
                    FindLastUsedColumn = 1;
                }

                while (true)
                {
                    ExcelRange cell = worksheet.Cells[FindLastUsedRow, FindLastUsedColumn];
                    if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
                    {
                        currentRow = FindLastUsedRow;
                        currentColumn = FindLastUsedColumn;
                        break;
                    }
                    else
                    {
                        if (FindLastUsedRow == TotalRow) //finish row and proceed next column
                        {
                            FindLastUsedColumn++;
                            FindLastUsedRow = 1;
                        }
                        else
                        {
                            FindLastUsedRow++;
                        }
                    }
                }
                //currentColumn = currentRow >= numRowsPerPage ? FindColumn + 1 : FindColumn;
                // Fill the data into the next available cell
                worksheet.Cells[currentRow, currentColumn].Value = $"{RunningNum} :{Barcode}";

                worksheet.Cells[currentRow, currentColumn].Style.Font.Size = 6;
                worksheet.Column(currentColumn).Width = 12.5;
                worksheet.Row(currentRow).Height = 9;

                ExcelRange range = worksheet.Cells[currentRow, currentColumn];
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
                worksheet.PrinterSettings.PaperSize = ePaperSize.A4;

                // Save the changes to the Excel file
                package.Save();
            }

        }
        internal virtual List<CycletimeInfo> CycleTime()
        {
            return new List<CycletimeInfo>();
        }

        internal virtual List<string> UnloadedProdList { get; set; }

        internal protected void Log(string module, string Step)
        {
            lock (m_SyncLog)
            {
                string logFilePath = @"C:\Machine\SequenceLog\" + module + ".ini";
                if (!File.Exists(logFilePath))
                {
                    // If the file does not exist, create a new file
                    using (StreamWriter sw = File.CreateText(logFilePath))
                    {
                        sw.WriteLine($"Log file created on {DateTime.Now.ToString("HH:mm:ss:ffff")}");
                    }
                }

                // Append the log entry to the file
                using (StreamWriter sw = File.AppendText(logFilePath))
                {
                    sw.WriteLine($"{DateTime.Now.ToString("HH:mm:ss:ffff")}: {module}: {Step}");
                }
            }
        }
        internal protected string EnumParseDescription(object EnumCode)
        {
            Debug.Assert(EnumCode != null && EnumCode.GetType().IsEnum, "Enum Invalid");

            var EnumDesc = EnumCode.GetType().GetField(EnumCode.ToString());
            DescriptionAttribute attribute = (DescriptionAttribute)EnumDesc.GetCustomAttribute(typeof(DescriptionAttribute), false);
            return attribute != null ? attribute.Description : EnumCode.ToString();
        }

        internal protected string AlarmString(object Enum)
        {
            var alarm = m_ErrorData.FirstOrDefault(x => x.EnumErr == Enum.ToString());
            //Debug.Assert(alarm != null, EnumParseDescription(Enum));
            string stralarm = alarm == null ? $"Error: {EnumParseDescription(Enum.ToString())}" : $"Error: {alarm.Definition} {(char)9} {(char)10} Action: {alarm.Action}";
            return stralarm;
        }

        public static bool ConstructAllStationData(string path, List<StationCfg> StationsData = null)
        {
            try
            {
                // Use to construct the machine data 
                //Example:
                //StationsData[0] - Barcode(IsPsnt,IsSkip,UnitStatus,*StationCfg*)
                //StationsData[1] - LaserMarking(IsPsnt,IsSkip,UnitStatus,*StationCfg*)
                //StationsData[2] - Pnp(IsPsnt,IsSkip,UnitStatus, *StationCfg*)
                //  *StationCfg*:
                //  1. StationName
                //  2. IsBypass
                //  3. SeqOrder
                //  4. SeqNext
                //  5. SeqPrev

                StationsData.Clear();
                string[] files = Directory.GetFiles(path, "*.txt");

                foreach (var file in files)
                {
                    string stationname = Path.GetFileName(file);
                    ReadWritefile readstationdata = new ReadWritefile();
                    StationCfg stationvarcfgs = new StationCfg();
                    var stationcfg = readstationdata.ReadDynamicClassAndWritevariable(stationvarcfgs, stationname, path, stationname);

                    StationsData.Add(stationvarcfgs);
                }


            }
            catch (Exception ex)
            {
                return false;
                //frmMain.frmMsg.ShowMsg($"ConstructAllStationData: {ex.Message}", frmMessaging.TMsgBtn.smbOK);
            }

            return true;
        }

        public static bool ConstructErrorData(string path, List<ErrData> ErrorData = null)
        {
            try
            {
                ErrorData.Clear();
                string[] files = Directory.GetFiles(path, "*.txt");

                foreach (var file in files)
                {
                    string stationname = Path.GetFileName(file);
                    ReadWritefile readstationdata = new ReadWritefile();
                    //List<ErrData> ErrCfg = new List<ErrData>() { };
                    var stationcfg = readstationdata.ReadTxtFile(ErrorData, path, stationname);

                    
                }
            }
            catch (Exception ex)
            {
                return false;
                //frmMain.frmMsg.ShowMsg($"ConstructAllStationData: {ex.Message}", frmMessaging.TMsgBtn.smbOK);
            }

            return true;
        }
    
        public static void ConstructBitCode(List<BitCodeInfo> BitCode)
        {
            try
            {
                BitCode.Add(new BitCodeInfo(0, false, false, false, false, ""));
                BitCode.Add(new BitCodeInfo(1, true, false, false, false, ""));
                BitCode.Add(new BitCodeInfo(2, false, true, false, false, ""));
                BitCode.Add(new BitCodeInfo(3, true, true, false, false, ""));
                BitCode.Add(new BitCodeInfo(4, false, false, true, false, ""));
                BitCode.Add(new BitCodeInfo(5, true, false, true, false, ""));
                BitCode.Add(new BitCodeInfo(6, false, true, true, false, ""));
                BitCode.Add(new BitCodeInfo(7, true, true, true, false, ""));
                BitCode.Add(new BitCodeInfo(8, false, false, false, true, ""));
                BitCode.Add(new BitCodeInfo(9, true, false, false, true, ""));
                BitCode.Add(new BitCodeInfo(10, false, true, false, true, ""));
                BitCode.Add(new BitCodeInfo(11, true, true, false, true, ""));
                BitCode.Add(new BitCodeInfo(12, false, false, true, true, ""));
                BitCode.Add(new BitCodeInfo(13, true, false, true, true, ""));
                BitCode.Add(new BitCodeInfo(14, false, true, true, true, ""));
                BitCode.Add(new BitCodeInfo(15, true, true, true, true, ""));
            }
            catch { }
        }
    }
}
