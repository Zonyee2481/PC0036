using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Infrastructure;
using OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;
using OfficeOpenXml.Style;

namespace Machine
{
    public class GDefine
    {
        public static string UserName = "OPERATOR";
        public static int UserLevel = 0;
        public const string AppPath = @"C:\Machine";
        public const string AppPath2 = @"C:\Machine";
        public const string MarkingOffsetPath = @"C:\Machine\MarkingOffset";

        public const string MarkInfoFile = "MarkInfo.txt";

        public const string ReportPath = AppPath + @"\OutQCReport";
        public const string RecipePath = AppPath + @"\Recipe";
        public const string DevicePath = AppPath + @"\DeviceRecipe";
        public const string BitCodePath = AppPath + @"\BitCode";
        public const string MatrixTablePath = AppPath + @"\MatrixTable";
        public const string ConfigPath = AppPath + @"\Setting";
        public const string ResrcPath = AppPath + @"\\Resources";
        public const string Configini = "Config";
        public static string Recipe = "Default";
        public static string GoldenWaferRecipe = "GoldenRecipe" + RecipeExt;
        public static string DeviceRecipeExt = ".ini";
        public static string RecipeExt = ".ini";
        public static string LogPath = AppPath + @"\Log\"; //folder
        public static string AlignLogPath = AppPath + @"\AlignLog\"; //folder
        public static string LineScanLogPath = AppPath + @"\LineScanLog\"; //folder
        public static string OCRLogPath = AppPath + @"\OCRLog\"; //folder
        public const string OveralLogPath = AppPath + @"\LogEvent\OverallErr";
        public const string OveralMonthLogPath = AppPath + @"\LogEvent\OverallMonthErr";
        public static string LotInfoFolder = @"\LotInfo";
        public static string RecordData = @"\RecordData";
        public static string OutQCFolder = @"\OutQC";
        public static string _sLaserMarkFile = GDefine.AppPath2;
        public static string _sLaserMarkContentFile = GDefine.AppPath2;
        public static string LaserCodePathWifName = AppPath2 + "\\Laser Code.csv";
        public static string _sSqlServerName = System.Environment.MachineName + @"\SQLEXPRESS";//Data Source
        public static string _sSqlDatabaseName = "PC001";//Initial Catalog
        public static string _sConnStr = @"Data Source=" + GDefine._sSqlServerName + ";Initial Catalog=" + GDefine._sSqlDatabaseName + ";Integrated Security=True;Pooling=False";

        public static List<IPPort> _IpPort = new List<IPPort>();
        public static string OCRIP = "192.168.100.97";
        public static string OCRPort = "8888";
        public static string KeyenceInIP = "192.168.100.98";
        public static string KeyenceInPort = "8888";
        public static string KeyenceOutIP = "192.168.100.99";
        public static string KeyenceOutPort = "8888";
        public static string LaserInIP = "192.168.100.100";
        public static string LaserInPort = "8888";
        public static string LaserOutIP = "192.168.100.101";
        public static string LaserOutPort = "8888";
        public static string CognexMainIP = "127.0.0.1";
        public static string CognexMainPort = "0023";

        public static ES.UserCtrl.UserCtrlTools UserCtrl = new ES.UserCtrl.UserCtrlTools();
        #region Auto
        public static bool _bPanelUnlock = true;
        public static int _iMaxCounter = 1;
        public static string _sCurrentDate = null;
        #endregion

        public static void SaveDefaultFile()
        {
            ES.Net.IniFile IniFile = new ES.Net.IniFile();
            IniFile.Create(AppPath + @"\", "Machine.def");

            IniFile.WriteString("Default", "Recipe", TaskDeviceRecipe._LotInfo._RecipeInfo.DeviceID);
            IniFile.WriteInteger("Default", "MaxCounter", _iMaxCounter);
            IniFile.WriteString("Default", "CurrentDate", _sCurrentDate);
            IniFile.WriteInteger("Default", "LotNoCoolingPeriodInHour", 0);
            IniFile.WriteInteger("Default", "LotNoCoolingPeriodInMinute", 0);

            //ParseToSequence();
            LoadDefaultFile();
        }

        public static void ParseToSequence()
        {
            //frmMain.SequenceRun.EnableStation((int)TotalModule.TopLaserMark, _bEnabInLaser);
            //frmMain.SequenceRun.EnableStation((int)TotalModule.InPnp_Left, _bEnableLeftPnp);
            //frmMain.SequenceRun.EnableStation((int)TotalModule.InPnp_Right, _bEnableRightPnp);
            //frmMain.SequenceRun.EnableStation((int)TotalModule.BtmLaserMark, _bEnabOutLaser);
            //frmMain.SequenceRun.EnableStation((int)TotalModule.OCR_Reader, _bEnabCognexOCR);
            //frmMain.SequenceRun.EnableStation((int)TotalModule.KeyenceTop, _bEnabInKeyence);
            //frmMain.SequenceRun.EnableStation((int)TotalModule.KeyenceBtm, _bEnabOutKeyence);
            //frmMain.SequenceRun.EnableStation((int)TotalModule.UnloadingRtr, _bEnableUnloadRtr);
            //frmMain.SequenceRun.EnableStation((int)TotalModule.RejectBin, !_bFailUnitUnloadToPass);
            //frmMain.SequenceRun.MachineConfig = frmMain.mytagSeqFlag;
            //frmMain.SequenceRun.BypassStation((int)TotalModule.Robot, _bEnabRobot);
        }
        public static void LoadDefaultFile()
        {
            ES.Net.IniFile IniFile = new ES.Net.IniFile();
            IniFile.Create(AppPath + @"\", "Machine.def");

            if (!File.Exists(AppPath + @"\Machine.def"))
            {
                SaveDefaultFile();
            }

            _iMaxCounter = IniFile.ReadInteger("Default", "MaxCounter", _iMaxCounter);            
            SM.RecipeName = TaskDeviceRecipe._LotInfo.PartNum = IniFile.ReadString("Default", "Recipe", TaskDeviceRecipe._LotInfo.PartNum);
            _sCurrentDate = IniFile.ReadString("Default", "CurrentDate", _sCurrentDate);
            SM.LotNoCoolingPeriodInHour = IniFile.ReadInteger("Default", "LotNoCoolingPeriodInHour", SM.LotNoCoolingPeriodInHour);
            SM.LotNoCoolingPeriodInMinute = IniFile.ReadInteger("Default", "LotNoCoolingPeriodInMinute", SM.LotNoCoolingPeriodInMinute);
        }

        public static void LoadCurrentDate()
        {
            ES.Net.IniFile IniFile = new ES.Net.IniFile();
            IniFile.Create(AppPath + @"\", "Machine.def");
            _sCurrentDate = TaskDeviceRecipe._LotInfo.PartNum = IniFile.ReadString("Default", "CurrentDate", _sCurrentDate);
        }

        public static void SaveCurrentDate()
        {
            ES.Net.IniFile IniFile = new ES.Net.IniFile();
            IniFile.Create(AppPath + @"\", "Machine.def");

            IniFile.WriteString("Default", "CurrentDate", _sCurrentDate);

            LoadCurrentDate();
        }


        #region Machine Performance
        public static int McUpTime = 0;
        public static int McRunTime = 0;
        public static int McOpTime = 0;
        public static int McAssistTime = 0;
        public static int McAssistCount = 0;
        public static int McCellCycleTime = 0;
        public static int McCycleTime = 0;
        public static int McIdleTime = 0;
        public static double McActualTime = 0;
        public static Stopwatch _swIdleTime = new Stopwatch();
        public static Stopwatch _swIdleTime2 = new Stopwatch();
        #endregion

        public static void SaveComParam()
        {
            ES.Net.IniFile IniFile = new ES.Net.IniFile();
            IniFile.Create(AppPath + @"\", "Communication.ini");

            IniFile.WriteString("Config", "LaserInIP", LaserInIP);
            IniFile.WriteString("Config", "LaserInPort", LaserInPort);
            IniFile.WriteString("Config", "LaserOutIP", LaserOutIP);
            IniFile.WriteString("Config", "LaserOutPort", LaserOutPort);
            IniFile.WriteString("Config", "OCRIP", OCRIP);
            IniFile.WriteString("Config", "OCRPort", OCRPort);
            IniFile.WriteString("Config", "KeyenceInIP", KeyenceInIP);
            IniFile.WriteString("Config", "KeyenceInPort", KeyenceInPort);
            IniFile.WriteString("Config", "KeyenceOutIP", KeyenceOutIP);
            IniFile.WriteString("Config", "KeyenceOutPort", KeyenceOutPort);
            IniFile.WriteString("Config", "CognexMainIP", CognexMainIP);
            IniFile.WriteString("Config", "CognexMainPort", CognexMainPort);

        }
        public static void LoadComParam()
        {
            ES.Net.IniFile IniFile = new ES.Net.IniFile();
            IniFile.Create(AppPath + @"\", "Communication.ini");

            LaserInIP = IniFile.ReadString("Config", "LaserInIP", LaserInIP);
            LaserInPort = IniFile.ReadString("Config", "LaserInPort", LaserInPort);
            LaserOutIP = IniFile.ReadString("Config", "LaserOutIP", LaserOutIP);
            LaserOutPort = IniFile.ReadString("Config", "LaserOutPort", LaserOutPort);
            OCRIP = IniFile.ReadString("Config", "OCRIP", OCRIP);
            OCRPort = IniFile.ReadString("Config", "OCRPort", OCRPort);
            KeyenceInIP = IniFile.ReadString("Config", "KeyenceInIP", KeyenceInIP);
            KeyenceInPort = IniFile.ReadString("Config", "KeyenceInPort", KeyenceInPort);
            KeyenceOutIP = IniFile.ReadString("Config", "KeyenceOutIP", KeyenceOutIP);
            KeyenceOutPort = IniFile.ReadString("Config", "KeyenceOutPort", KeyenceOutPort);
            CognexMainIP = IniFile.ReadString("Config", "CognexMainIP", CognexMainIP);
            CognexMainPort = IniFile.ReadString("Config", "CognexMainPort", CognexMainPort);
            ParseIP();
        }

        public static void ParseIP()
        {
            //0
            GDefine._IpPort.Add(
                new IPPort
                {
                    IpAdress = GDefine.LaserInIP,
                    Port = GDefine.LaserInPort
                });
            //1
            GDefine._IpPort.Add(
                new IPPort
                {
                    IpAdress = GDefine.LaserOutIP,
                    Port = GDefine.LaserOutPort
                });
            //2
            GDefine._IpPort.Add(
                new IPPort
                {
                    IpAdress = GDefine.OCRIP,
                    Port = GDefine.OCRPort
                });
            //3
            GDefine._IpPort.Add(
                new IPPort
                {
                    IpAdress = GDefine.CognexMainIP,
                    Port = GDefine.CognexMainPort
                });
            //4
            GDefine._IpPort.Add(
                new IPPort
                {
                    IpAdress = GDefine.KeyenceInIP,
                    Port = GDefine.KeyenceInPort
                });
            //5
            GDefine._IpPort.Add(
                new IPPort
                {
                    IpAdress = GDefine.KeyenceOutIP,
                    Port = GDefine.KeyenceOutPort
                });
        }

        public enum StModule
        {
            ErrorInit = 0,
            Ready = 1,
            Stop = 2,
            Busy = 3,
            Error = 4
        }

        private static readonly object WriteLock = new object();
        public static void AppProMsg()
        {
            lock (WriteLock)
            {
                //Application.DoEvents();
                Thread.Sleep(1);
            }
        }
        static bool block = false;
        public static void AppProMsg2()
        {
            if (!block)
            {
                block = true;
                Thread.Sleep(1);
                Application.DoEvents();
                block = false;
            }
        }

        public static TTCPTPCon LaserInConn;
        public static TTCPTPCon LaserOutConn;
        public static TTCPTPCon OCRConn;
        public static TTCPTPCon OCRMainConn;
        public static TTCPTPCon KeyenceInConn;
        public static TTCPTPCon KeyenceOutConn;
        public struct TTCPTPCon
        {
            public bool Connected;
            public string IPAddress;
            public string PortNum;
        }
        static Stopwatch sw_TickCount = new Stopwatch();
        internal static int GetTickCount()
        {
            if (!sw_TickCount.IsRunning)
            {
                sw_TickCount.Start();
            }

            int D = (int)sw_TickCount.ElapsedMilliseconds;
            return D;
        }
        public static bool Delay(int msdelay)
        {
            if (msdelay <= 0) { return true; }
            int t = Environment.TickCount + msdelay;

            while (true)
            {
                if (Environment.TickCount >= t) { break; }
                Thread.Sleep(0);
                Application.DoEvents();
            }

            return true;

        }

        #region Waffle pack barcode spec
        //public const string WP = "WP";
        //public const string OutSource = "Y";
        //public static string _2DID = "";
        //public static string Barcode = "";
        //public static string[] Combinations = new string[1679616];
        //public static int _iMarkCounter = 1;
        #endregion
        //public const string CounterPath = @"..\Counter\";

        //public static string IndexFile = "index.txt";
        //public static void LoadCounter()
        //{
        //    IndexFile = TaskDeviceRecipe._sDeviceID + "_" + GetMonth();
        //    ES.Net.IniFile IniFile = new ES.Net.IniFile();
        //    if (!File.Exists(CounterPath + IndexFile + ".txt"))
        //    {
        //        IniFile.Create(CounterPath + IndexFile);
        //        IniFile.WriteInteger("Counter", "Index", 0);
        //    }

        //    _iCounter = IniFile.ReadInteger("Counter", "Index", _iCounter);
        //}

        //public static void SaveCounter()
        //{
        //    IndexFile = TaskDeviceRecipe._sDeviceID + "_" + GetMonth();
        //    ES.Net.IniFile IniFile = new ES.Net.IniFile();
        //    if (!File.Exists(CounterPath + IndexFile + ".txt"))
        //    {
        //        IniFile.Create(CounterPath + IndexFile);
        //    }

        //    IniFile.WriteInteger("Counter", "Index", _iCounter);
        //}

        //public static string GetMonth()
        //{
        //    string month = DateTime.Now.ToString("%M");
        //    switch (month)
        //    {
        //        case "10":
        //            month = "0";
        //            break;
        //        case "11":
        //            month = "A";
        //            break;
        //        case "12":
        //            month = "B";
        //            break;
        //    }
        //    return month;
        //}
        //public static string GenerateBarcode()
        //{
        //    string FixCode = GDefine.WP;

        //    string _sYear = DateTime.Now.ToString("yy");
        //    string _sMonth = GetMonth();
        //    string _sRowCol = TaskDeviceRecipe._iRow.ToString() + TaskDeviceRecipe._iCol.ToString();
        //    string _OutsourceCode = GDefine.OutSource;
        //    string _RunningNum = GDefine.Combinations[TaskDeviceRecipe._LotInfo.Counter - 1];

        //    return FixCode + _sYear + _sMonth + _sRowCol + _OutsourceCode + _RunningNum;
        //}

        //public static void MarkInfo(string[] _asOffset, int _iIndex)
        //{
        //  string _sMarkInfo = "Y:\\" + MarkInfoFile;
        //  ES.Net.IniFile IniFile = new ES.Net.IniFile();
        //  if (File.Exists(_sMarkInfo))
        //  {
        //    File.Delete(_sMarkInfo);
        //  }
        //  GDefine.Barcode = GenerateBarcode();

        //  if (TaskInputSingulateIndex._iIS1IndexCounter >= 4 && TaskInputSingulateIndex._iIS1IndexCounter <= 10)// Urgent Temp 10
        //  {
        //    TaskInputSingulateIndex._asIS1MarkInfo[TaskInputSingulateIndex._iIS1IndexCounter - 4] = GDefine.Barcode;
        //  }
        //  else if (TaskInputSingulateIndex._iIS2IndexCounter >= 4 && TaskInputSingulateIndex._iIS2IndexCounter <= 10)// Urgent Temp 10
        //  {
        //    TaskInputSingulateIndex._asIS2MarkInfo[TaskInputSingulateIndex._iIS2IndexCounter - 4] = GDefine.Barcode;
        //  }
        //  string Offset = _asOffset[_iIndex].ToString();
        //  StreamWriter sw = new StreamWriter(_sMarkInfo, true);
        //  sw.WriteLine(GDefine.Barcode);
        //  sw.WriteLine(Offset);// + "0,0,0;0,0,0;0,0,0;0,0,0;0,0,0;");
        //  sw.Close();

        //}
        //public static void MarkInfo()
        //{
        //  string _sMarkInfo = "D:\\" + MarkInfoFile;
        //  ES.Net.IniFile IniFile = new ES.Net.IniFile();
        //  if (File.Exists(_sMarkInfo))
        //  {
        //    File.Delete(_sMarkInfo);
        //  }
        //  GDefine.Barcode = GenerateBarcode();
        //  if (TaskInputSingulateIndex._iIS1IndexCounter >= 4 && TaskInputSingulateIndex._iIS1IndexCounter <= 10)// Urgent Temp 10
        //  {
        //    TaskInputSingulateIndex._asIS1MarkInfo[TaskInputSingulateIndex._iIS1IndexCounter - 4] = GDefine.Barcode;
        //  }
        //  else if (TaskInputSingulateIndex._iIS2IndexCounter >= 4 && TaskInputSingulateIndex._iIS2IndexCounter <= 10)// Urgent Temp 10
        //  {
        //    TaskInputSingulateIndex._asIS2MarkInfo[TaskInputSingulateIndex._iIS2IndexCounter - 4] = GDefine.Barcode;
        //  }
        //  StreamWriter sw = new StreamWriter(_sMarkInfo, true);
        //  sw.WriteLine(GDefine.Barcode);
        //  sw.WriteLine("0,0,0;0,0,0;0,0,0;0,0,0;0,0,0;0,0,0;");
        //  sw.Close();

        //}

        //public static string _sBtmcode = "";
        //public static void MarkInfoBottom(int _iIndex, int OS, string[] _asOffset)
        //{
        //  string _sMarkInfo = "Z:\\" + MarkInfoFile;
        //  ES.Net.IniFile IniFile = new ES.Net.IniFile();
        //  if (File.Exists(_sMarkInfo))
        //  {
        //    File.Delete(_sMarkInfo);
        //  }
        //  if (OS == 1)
        //  {
        //    _sBtmcode = TaskOutputSingulateIndex.OS1MarkInfo[_iIndex];
        //  }
        //  else if (OS == 2)
        //  {
        //    _sBtmcode = TaskOutputSingulateIndex.OS2MarkInfo[_iIndex];
        //  }
        //  if (_sBtmcode == null) _sBtmcode = "WP23623Y0001";
        //  StreamWriter sw = new StreamWriter(_sMarkInfo, true);
        //  sw.WriteLine(_sBtmcode);
        //  sw.WriteLine(_asOffset[_iIndex]);// + "0,0,0;0,0,0;0,0,0;0,0,0;0,0,0;");
        //  sw.Close();

        //}
        public static bool ReadOffsetFile(string Filename, ref string[] Arr)
        {
            try
            {
                var FileUrl = MarkingOffsetPath + "\\" + Filename;

                //file lines
                string[] lines = File.ReadAllLines(FileUrl);

                //loop through each file line
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
                Arr = new string[7];
                for (int i = 0; i < 7; i++)
                {
                    Arr[i] = lines[i].Substring(2, lines[i].Length - 2);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return true;
        }
        //public static void ResetCounterFile()
        //{
        //    Combinations = new string[1679616];

        //    int count = 0;

        //    for (int i = 0; i < 36; i++)
        //    {
        //        for (int j = 0; j < 36; j++)
        //        {
        //            for (int k = 0; k < 36; k++)
        //            {
        //                for (int l = 0; l < 36; l++)
        //                {
        //                    char firstDigit = (char)(i < 10 ? i + 48 : i + 55);
        //                    char secondDigit = (char)(j < 10 ? j + 48 : j + 55);
        //                    char thirdDigit = (char)(k < 10 ? k + 48 : k + 55);
        //                    char fourthDigit = (char)(l < 10 ? l + 48 : l + 55);

        //                    if (firstDigit == '0' && secondDigit == '0' && thirdDigit == '0' && l < 1)
        //                        continue;

        //                    string combination = $"{firstDigit}{secondDigit}{thirdDigit}{fourthDigit}";

        //                    if (combination.Contains("10") || combination.Contains("20") || combination.Contains("30") || combination.Contains("40") || combination.Contains("50") || combination.Contains("60") || combination.Contains("70") || combination.Contains("80") || combination.Contains("90") ||
        //                      combination.Contains("A0") || combination.Contains("B0") || combination.Contains("C0") || combination.Contains("D0") || combination.Contains("E0") || combination.Contains("F0") || combination.Contains("G0") || combination.Contains("H0") || combination.Contains("I0") ||
        //                       combination.Contains("J0") || combination.Contains("K0") || combination.Contains("L0") || combination.Contains("M0") || combination.Contains("N0") || combination.Contains("O0") || combination.Contains("P0") || combination.Contains("Q0") || combination.Contains("R0") ||
        //                        combination.Contains("S0") || combination.Contains("T0") || combination.Contains("U0") || combination.Contains("V0") || combination.Contains("W0") || combination.Contains("X0") || combination.Contains("Y0") || combination.Contains("Z0"))
        //                        continue;

        //                    Combinations[count] = $"{firstDigit}{secondDigit}{thirdDigit}{fourthDigit}";
        //                    //InsertData(count, "WP24343Y" + Combinations[count]);
        //                    //Log(Combinations[count]);
        //                    count++;
        //                }
        //            }
        //        }
        //    }

            //ArrangeData(Combinations);
            //string path = Path.Combine(CounterPath, CounterFile);
            //if (File.Exists(path))
            //{
            //  File.Delete(path);
            //}
            //else
            //{
            //  Directory.CreateDirectory(CounterPath);
            //}

            //using (StreamWriter writer = new StreamWriter(Path.Combine(CounterPath, CounterFile)))
            //{
            //  foreach (string combination in Combinations)
            //  {
            //    writer.WriteLine(combination);
            //  }
            //}
        //}

        public static void Log(string text)
        {
            string logFilePath = @"C:\Machine\ResetCounterFile" + ".csv";
            if (!File.Exists(logFilePath))
            {
                // If the file does not exist, create a new file
                using (StreamWriter sw = File.CreateText(logFilePath))
                {
                    sw.WriteLine($"Log file created on {DateTime.Now.ToString("HH:mm:ss:ffff")}");
                }
            }

            using (StreamReader reader = new StreamReader(logFilePath))
            {
                int lineNumber = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lineNumber++;   
                    Console.WriteLine($"Line {lineNumber}: {line}");
                }

                Console.WriteLine($"Total lines: {lineNumber}");
            }

            // Append the log entry to the file
            using (StreamWriter sw = File.AppendText(logFilePath))
            {
                sw.WriteLine($"{text}");
            }
        }

        public static int numRowsPerPage = 45; // Change as needed
        public static int numColsPerPage = 20; // Change as needed
        public static int FindLastUsedRow = 1;
        // Determine the last used row for the first column
        public static int FindLastUsedColumn = 1;
        public static int currentRow = 1;
        public static int currentColumn = 1;
        //static void InsertData(string newData)
        //{
        //    string excelFilePath = @"C:\Machine\ResetCounterFile.xlsx";
        //    FileInfo excelFile = new FileInfo(excelFilePath);
        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        //    using (ExcelPackage package = new ExcelPackage(excelFile))
        //    {
        //        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

        //        if (worksheet == null)
        //        {
        //            // If worksheet doesn't exist, create a new one
        //            worksheet = package.Workbook.Worksheets.Add("Sheet1");
        //        }

        //        if (worksheet.Dimension != null)
        //        {
        //            FindLastUsedColumn = worksheet.Dimension.End.Column;
        //        }
        //        else
        //        {
        //            FindLastUsedColumn = 1;
        //        }

        //        //FindLastUsedRow = worksheet.Cells[worksheet.Dimension?.End.Row ?? 1, currentColumn].End.Row;
                
        //        if (currentRow >= numRowsPerPage)
        //        {
        //            currentColumn = FindLastUsedColumn + 1;
        //        }
        //        else
        //        {
        //            //currentRow = FindLastUsedRow + 1;
        //            currentColumn = FindLastUsedColumn;
        //        }


        //        if (worksheet.Dimension != null)
        //        {
        //            if (currentRow >= numRowsPerPage)
        //            {
        //                currentRow = 1;
        //            }
        //            else
        //            {
        //                currentRow = FindLastUsedRow + 1;
        //            }
        //        }
        //        else
        //        {
        //            currentRow = 1;
        //        }

        //        if (worksheet.Dimension != null)
        //        {
        //            FindLastUsedRow = worksheet.Cells[currentRow, currentColumn].End.Row;
        //        }
        //        else
        //        {
        //            FindLastUsedRow = 1;
        //        }

        //        //currentColumn = currentRow >= numRowsPerPage ? FindColumn + 1 : FindColumn;
        //        // Fill the data into the next available cell
        //        worksheet.Cells[currentRow, currentColumn].Value = newData;

        //        // Save the changes to the Excel file
        //        package.Save();
        //    }
        //}


        static void InsertData(int count,string newData)
        {
            string excelFilePath = @"C:\Machine\ResetCounterFile.xlsx";
            FileInfo excelFile = new FileInfo(excelFilePath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(excelFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                if (worksheet == null)
                {
                    // If worksheet doesn't exist, create a new one
                    worksheet = package.Workbook.Worksheets.Add("Sheet1");
                    //worksheet.HeaderFooter.OddHeader.CenteredText = $"Laser Marking 2DID Running No     LotNumber: {TaskDeviceRecipe._LotInfo.LotNum}     " +
                    //    $"PartNum: {TaskDeviceRecipe._LotInfo.PartName}     " +
                    //    $"Row:{TaskDeviceRecipe._LotInfo._RecipeInfo._iRow.ToString()}        " +
                    //    $"Column: {TaskDeviceRecipe._LotInfo._RecipeInfo._iCol.ToString()}      ";
                    //worksheet.Cells[1, 1].Value = "Initial Data";
                    //// Set font size for a single cell
                    //worksheet.Cells[1, 1].Style.Font.Size = 8;

                    //worksheet.Cells[2, 1].Value = "Initial Data2";
                    //// Set font size for a single cell
                    //worksheet.Cells[2, 1].Style.Font.Size = 8;

                    //worksheet.Cells[3, 1].Value = "Initial Data3";
                    //// Set font size for a single cell
                    //worksheet.Cells[3, 1].Style.Font.Size = 8;
                    worksheet.Column(1).AutoFit();
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
                        if (FindLastUsedRow == 50)
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
                worksheet.Cells[currentRow, currentColumn].Value = $"{count}: {newData}| ";


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


                package.Save();
            }
        }



        //static void ArrangeData(string[] data)
        //{
        //    Excel.Application excelApp = new Excel.Application();
        //    excelApp.Visible = true;
        //    Excel.Workbook workbook = excelApp.Workbooks.Open(@"C:\Machine\ResetCounterFile.xlsx");
        //    Excel.Worksheet wsDest;
        //    long lastRow;
        //    long numRowsPerPage = 45; // Change as needed
        //    long numColsPerPage = 20; // Change as needed
        //    long numRows;
        //    long numPages;
        //    long i, j, k;

        //    lastRow = data.Length; // Total number of elements in the data array

        //    numPages = (long)Math.Ceiling((double)lastRow / (numRowsPerPage * numColsPerPage));

        //    for (i = 1; i <= numPages; i++)
        //    {
        //        wsDest = (Excel.Worksheet)workbook.Sheets.Add(After: workbook.Sheets[workbook.Sheets.Count]);
        //        wsDest.Name = "Page " + i;

        //        for (j = 1; j <= numColsPerPage; j++)
        //        {
        //            for (k = 1; k <= numRowsPerPage; k++)
        //            {
        //                numRows = ((i - 1) * numRowsPerPage * numColsPerPage) + ((j - 1) * numRowsPerPage) + k;

        //                if (numRows > lastRow) break;

        //                // Calculate index in the data array
        //                int dataIndex = (int)(numRows - 1);

        //                wsDest.Cells[k, j].Value = data[dataIndex];
        //            }
        //        }
        //    }
        //}


        //Module Cycle Time
        public static int _iInMtrIndexStartTime = 0;
        public static int _iInMtrIndexEndTime = 0;

        public static int _iOutMtrIndexStartTime = 0;
        public static int _iOutMtrIndexEndTime = 0;

        public static int _iLeftMagLoadWaffleToNestStartTime = 0;
        public static int _iLeftMagLoadWaffleToNestEndTime = 0;

        public static int _iCognexStartTime = 0;
        public static int _iCognexEndTime = 0;

        public static int _iLaserInStartTime = 0;
        public static int _iLaserInEndTime = 0;

        public static int _iLaserOutStartTime = 0;
        public static int _iLaserOutEndTime = 0;

        public static int _iKeyenceInStartTime = 0;
        public static int _iKeyenceInEndTime = 0;

        public static int _iKeyenceOutStartTime = 0;
        public static int _iKeyenceOutEndTime = 0;

        public static int _iLeftFlipPnpStartTime = 0;
        public static int _iLeftFlipPnpEndTime = 0;

        public static int _iRightFlipPnpStartTime = 0;
        public static int _iRightFlipPnpEndTime = 0;

        public static int _iOutRejectStartTime = 0;
        public static int _iOutRejectEndTime = 0;

        public static int _iOutUnloadStartTime = 0;
        public static int _iOutUnloadEndTime = 0;

    }
}
