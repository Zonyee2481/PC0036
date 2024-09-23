using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Machine
{
    class TaskLotInfo
    {
        public struct TLotInfo
        {
            public bool Activated;
            public string LotNum;
            public string PartNum;
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

            tD = Convert.ToInt32(LotInfo.DateIn[0].ToString() + LotInfo.DateIn[1].ToString());
            tM = Convert.ToInt32(LotInfo.DateIn[3].ToString() + LotInfo.DateIn[4].ToString());
            tY = Convert.ToInt32(LotInfo.DateIn[6].ToString() + LotInfo.DateIn[7].ToString() +
                                 LotInfo.DateIn[8].ToString() + LotInfo.DateIn[9].ToString());

            DateTime DTIn = new DateTime(tY, tM, tD);

            string D = DTIn.Date.ToString("dd-MM-yyyy");
            string M = DTIn.Month.ToString();
            string Y = DTIn.Year.ToString();

            if (M.Length == 1) { M = "0" + M; }
            string MY = M + "-" + Y;

            string LotDir = GDefine.AppPath + GDefine.LotInfoFolder + "\\" + MY + "\\" + D + "\\" + LotInfo.LotNum + "\\";
            string LotFile = LotDir + D + "_" + Convert.ToDateTime(LotInfo.TimeIn).ToString("HHmmss") + ".txt";

            if (!Directory.Exists(LotDir)) { Directory.CreateDirectory(LotDir); }

            bool NewLotBook = false;
            if (!File.Exists(LotFile)) { NewLotBook = true; }

            FileStream F = new FileStream(LotFile, FileMode.Append, FileAccess.Write, FileShare.Write);
            StreamWriter W = new StreamWriter(F);
            if (NewLotBook)
            {
                string Title;
                Title = "Device ID" + (char)9 +
                        "Lot Number" + (char)9 +
                        "Date/Time In" + (char)9 +
                        "Date/Time Out";

                W.WriteLine(Title);
            }

            string S;

            string DO = DateTime.Now.Date.ToString("dd-MM-yyyy");
            string TO = DateTime.Now.ToString("HH:mm:ss tt");

            LotInfo.DateOut = DO;
            LotInfo.TimeOut = TO;

            S = LotInfo.PartNum + (char)9 +
                LotInfo.LotNum + (char)9 +
                LotInfo.DateIn + " / " + LotInfo.TimeIn + (char)9 +
                LotInfo.DateOut + " / " + LotInfo.TimeOut;

            if (!ELot)
            {
                W.WriteLine(S);
                W.Close();

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

        public static bool CheckLotCounter(string PartNumber, DateTime dateTime)
        {
            string D = dateTime.Date.ToString("dd-MM-yyyy");
            string M = dateTime.Month.ToString();
            string Y = dateTime.Year.ToString();

            if (M.Length == 1) { M = "0" + M; }
            string MY = M + "-" + Y;

            string LotDir = GDefine.AppPath + GDefine.LotInfoFolder + "\\" + MY + "\\" + D + "\\" + PartNumber + "\\";

            int fileCount = 0;

            if (Directory.Exists(LotDir))
            {
                fileCount = Directory.GetFiles(LotDir).Length;
            }

            return (fileCount < GDefine._iMaxCounter);
        }
    }
}
