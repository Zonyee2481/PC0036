using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core.Database;

namespace Machine
{
    class TaskLotInfo
    {
        public struct TLotInfo
        {
            public bool Activated;
            public string LotNum;
            public string PartNum;
            public int HertzCode;
            public string Hertz;
            public int RunCounter;
            public string DateIn;
            public string TimeIn;
            public string DateOut;
            public string TimeOut;
        }

        public static TLotInfo LotInfo;

        public static string TempLotLine = "";

        public static void SaveLotInfo(bool ELot)
        {
            int tD;
            int tM;
            int tY;
            int tHour;
            int tMin;
            int tSec;

            tD = Convert.ToInt32(LotInfo.DateIn[0].ToString() + LotInfo.DateIn[1].ToString());
            tM = Convert.ToInt32(LotInfo.DateIn[3].ToString() + LotInfo.DateIn[4].ToString());
            tY = Convert.ToInt32(LotInfo.DateIn[6].ToString() + LotInfo.DateIn[7].ToString() +
                                 LotInfo.DateIn[8].ToString() + LotInfo.DateIn[9].ToString());

            tHour = Convert.ToInt32(LotInfo.TimeIn[0].ToString() + LotInfo.TimeIn[1].ToString());
            tMin = Convert.ToInt32(LotInfo.TimeIn[3].ToString() + LotInfo.TimeIn[4].ToString());
            tSec = Convert.ToInt32(LotInfo.TimeIn[6].ToString() + LotInfo.TimeIn[7].ToString());

            DateTime DTIn = new DateTime(tY, tM, tD, tHour, tMin, tSec);

            string D = DTIn.Date.ToString("dd-MM-yyyy");
            string M = DTIn.Month.ToString();
            string Y = DTIn.Year.ToString();

            if (M.Length == 1) { M = "0" + M; }
            string MY = M + "-" + Y;

            string LotDir = GDefine.AppPath + GDefine.LotInfoFolder + "\\" + MY + "\\" + D + "\\";
            string LotFile = LotDir + D + ".csv";

            if (!Directory.Exists(LotDir)) { Directory.CreateDirectory(LotDir); }

            bool NewLotBook = false;
            if (!File.Exists(LotFile)) { NewLotBook = true; }
            FileStream F = new FileStream(LotFile, FileMode.Append, FileAccess.Write, FileShare.Write);
            StreamWriter W = new StreamWriter(F);
            if (NewLotBook)
            {
                string Title;
                Title = "Device ID" + "," +
                        "Lot Number" + "," +
                        "Hertz Number" + "," +
                        "Hertz Running" + "," +
                        "Run Number" + "," +
                        "Date/Time In" + "," +
                        "Date/Time Out";

                W.WriteLine(Title);
            }

            string S;

            S = LotInfo.PartNum + "," +
                LotInfo.LotNum + "," +
                LotInfo.HertzCode + "," +
                LotInfo.Hertz + "," +
                LotInfo.RunCounter + "," +
                LotInfo.DateIn + " / " + LotInfo.TimeIn + "," +
                LotInfo.DateOut + " / " + LotInfo.TimeOut;

            if (!ELot)
            {
                tD = Convert.ToInt32(LotInfo.DateOut[0].ToString() + LotInfo.DateOut[1].ToString());
                tM = Convert.ToInt32(LotInfo.DateOut[3].ToString() + LotInfo.DateOut[4].ToString());
                tY = Convert.ToInt32(LotInfo.DateOut[6].ToString() + LotInfo.DateOut[7].ToString() +
                                     LotInfo.DateOut[8].ToString() + LotInfo.DateOut[9].ToString());

                tHour = Convert.ToInt32(LotInfo.TimeOut[0].ToString() + LotInfo.TimeOut[1].ToString());
                tMin = Convert.ToInt32(LotInfo.TimeOut[3].ToString() + LotInfo.TimeOut[4].ToString());
                tSec = Convert.ToInt32(LotInfo.TimeOut[6].ToString() + LotInfo.TimeOut[7].ToString());

                DateTime DTOut = new DateTime(tY, tM, tD, tHour, tMin, tSec);

                frmMain.dbMain.AddLotRecord(LotInfo.LotNum, DTIn.Date.ToString(frmMain.dbMain.DateFormat) , DTIn.ToString(frmMain.dbMain.TimeFormat),
                    DTOut.Date.ToString(frmMain.dbMain.DateFormat), DTOut.ToString(frmMain.dbMain.TimeFormat));
                W.WriteLine(S);
                W.Close();
                //SaveLotRecordData(DTIn);
                TempLotLine = S;
            }
            else
            {
                W.Close();

                //Search Line and Replace
                StreamReader reader = new StreamReader(LotFile);
                string content = reader.ReadToEnd();
                reader.Close();

                content = Regex.Replace(content, TempLotLine, S);

                StreamWriter writer = new StreamWriter(LotFile);
                writer.Write(content);
                writer.Close();
            }
        }

        private static void SaveLotRecordData(DateTime dateTime)
        {
            string D = dateTime.Date.ToString("dd-MM-yyyy");
            string M = dateTime.Month.ToString();
            string Y = dateTime.Year.ToString();

            if (M.Length == 1) { M = "0" + M; }
            string MY = M + "-" + Y;

            string RecordDataDir = GDefine.AppPath + GDefine.RecordData + "\\" + MY + "\\" + D + "\\" + LotInfo.PartNum + "\\" + LotInfo.LotNum + "\\";
            string DataFile = RecordDataDir + LotInfo.LotNum + "_" + D + "_" + Convert.ToDateTime(LotInfo.TimeIn).ToString("HHmmss") + ".txt";

            if (!Directory.Exists(RecordDataDir)) { Directory.CreateDirectory(RecordDataDir); }

            FileStream F = new FileStream(DataFile, FileMode.Append, FileAccess.Write, FileShare.Write);
            StreamWriter W = new StreamWriter(F);

            string Title;
            Title = "Device ID" + (char)9 +
                    "Lot Number" + (char)9 +
                    "Hz Code Running" + (char)9 +
                    "Date/Time In" + (char)9 +
                    "Date/Time Out";

            W.WriteLine(Title);

            string S;

            string DO = DateTime.Now.Date.ToString("dd-MM-yyyy");
            string TO = DateTime.Now.ToString("HH:mm:ss tt");

            LotInfo.DateOut = DO;
            LotInfo.TimeOut = TO;

            S = LotInfo.PartNum + (char)9 +
                LotInfo.LotNum + (char)9 +
                LotInfo.HertzCode + (char)9 +
                LotInfo.DateIn + " / " + LotInfo.TimeIn + (char)9 +
                LotInfo.DateOut + " / " + LotInfo.TimeOut;

            W.WriteLine(S);
            W.Close();
        }

        public static bool CheckLotRecordDataCount(string LotNumber, DateTime dateTime)
        {
            if (TaskDeviceRecipe._LotInfo._RecipeInfo.MasterProduct) { return true; }
            
            string D = dateTime.Date.ToString("dd-MM-yyyy");
            string M = dateTime.Month.ToString();
            string Y = dateTime.Year.ToString();

            if (M.Length == 1) { M = "0" + M; }
            string MY = M + "-" + Y;

            string LotDir = GDefine.AppPath + GDefine.RecordData + "\\" + MY + "\\" + D + "\\" + LotNumber.Substring(0, 3) + "\\" + LotNumber;

            int fileCount = 0;

            if (Directory.Exists(LotDir))
            {
                fileCount = Directory.GetFiles(LotDir).Length;
            }

            return (fileCount < GDefine._iMaxCounter);
        }

        public static int LotRecordDataCount(string LotNumber, DateTime dateTime)
        {
            string D = dateTime.Date.ToString("dd-MM-yyyy");
            string M = dateTime.Month.ToString();
            string Y = dateTime.Year.ToString();

            if (M.Length == 1) { M = "0" + M; }
            string MY = M + "-" + Y;

            string LotRecordDataDir = GDefine.AppPath + GDefine.RecordData + "\\" + MY + "\\" + D + "\\" + LotNumber.Substring(0, 3) + "\\" + LotNumber;

            int fileCount = 0;

            if (Directory.Exists(LotRecordDataDir))
            {
                fileCount = Directory.GetFiles(LotRecordDataDir).Length;
            }

            return fileCount;
        }

        public static bool CheckLotRecordDataShortly(string LotNumber, int checkHour, int checkMinute)
        {
            DateTime current = DateTime.Now;
            string date = current.Date.ToString("dd-MM-yyyy");
            string M = current.Month.ToString();
            string Y = current.Year.ToString();

            if (M.Length == 1) { M = "0" + M; }
            string MY = M + "-" + Y;

            string LotRecordDataDir = GDefine.AppPath + GDefine.RecordData + "\\" + MY + "\\" + date + "\\" + LotNumber.Substring(0, 3) + "\\" + LotNumber;

            if (!Directory.Exists(LotRecordDataDir)) // Directory not exist, which means the lot number never run before
            {
                return true;
            }

            foreach(var file in Directory.GetFiles(LotRecordDataDir))
            {
                string filePath = file.Replace(".txt", "");
                string time = filePath.Substring(filePath.Length - 6);
                int hour = Convert.ToInt32(time.Substring(0, 2));
                int minute = Convert.ToInt32(time.Substring(2, 2));
                int second = Convert.ToInt32(time.Substring(4, 2));
                if (minute + checkMinute >= 60) { checkMinute = 0; checkHour = 1; } 
                DateTime fileTime = new DateTime(current.Year, current.Month, current.Day, (hour + checkHour), (minute + checkMinute), second);
                if (current < fileTime)
                {
                    return false;
                }
            }

            return true;
        }

        public static void DeleteLotRecordData()
        {
            DateTime current;
            string date, M, Y, MY, MYDir;
            current = DateTime.Now.AddDays(-5);
            date = current.Date.ToString("dd-MM-yyyy");
            M = current.Month.ToString();
            Y = current.Year.ToString();

            if (M.Length == 1) { M = "0" + M; }
            MY = M + "-" + Y;

            MYDir = GDefine.AppPath + GDefine.RecordData + "\\" + MY + "\\" + date;

            if (Directory.Exists(MYDir))
            {
                // Delete every 5 days
                var dir = new DirectoryInfo(MYDir);
                dir.Delete(true);
            }

            current = DateTime.Now.AddMonths(-2);
            date = current.Date.ToString("dd-MM-yyyy");
            M = current.Month.ToString();
            Y = current.Year.ToString();

            if (M.Length == 1) { M = "0" + M; }
            MY = M + "-" + Y;

            MYDir = GDefine.AppPath + GDefine.RecordData + "\\" + MY;

            if (Directory.Exists(MYDir))
            {
                // Delete month folder every 2 months
                var dir = new DirectoryInfo(MYDir);
                dir.Delete(true);
            }
        }

        public static bool CheckLotRecordCount(string LotNumber, DateTime StartDate)
        {
            if (TaskDeviceRecipe._LotInfo._RecipeInfo.MasterProduct) { return true; }
            int count = 0;
            if (!frmMain.dbMain.CountLotRecord(LotNumber, out count))
            {
                return false;
            }
            if (count >= GDefine._iMaxCounter)
            {
                return false;
            }
            return true;
        }

        public static bool GetLotRecordCount(string LotNumber, DateTime StartDate, out int Count)
        {
            Count = 0;
            string startDate = StartDate.ToString(frmMain.dbMain.DateFormat);
            if (!frmMain.dbMain.CountLotRecordByDate(LotNumber, startDate, out Count))
            {
                return false;
            }
            return true;
        }

        public static bool CheckLotRunShortly(string LotNumber, int checkHour, int checkMinute)
        {
            DateTime current = DateTime.Now;
            string[] startTime;
            frmMain.dbMain.GetLotRecordStartTime(LotNumber, current.ToString(frmMain.dbMain.DateFormat), out startTime); 
            if (frmMain.dbMain.DataTable.Rows.Count < 1) { return true; }
            for (int i = 0; i < startTime.Length; i++)
            {
                int hour = Convert.ToInt32(startTime[i].Substring(0, 2));
                int minute = Convert.ToInt32(startTime[i].Substring(3, 2));
                int second = Convert.ToInt32(startTime[i].Substring(6, 2));
                if (minute + checkMinute >= 60) { checkMinute = 0; checkHour = 1; }
                DateTime checkTime = new DateTime(current.Year, current.Month, current.Day, (hour + checkHour), (minute + checkMinute), second);
                if (current < checkTime)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
