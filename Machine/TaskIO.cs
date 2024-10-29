using Infrastructure;
using MotionIODevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Machine
{
    public class TaskIO
    {
        private static List<IBoardIO> motionBoards;
        private static int auto, startTimer, endLot;
        private static int timesUp, bitCode_1, bitCode_2, bitCode_4, bitCode_8, startCtrlRelay, startBtnLED, barcodeScanReady;
        public static string ErrMsg = "";

        public static bool OffBuzzer = true;
        private static System.Timers.Timer aTimer;

        public static void SetTimer()
        {
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            //BuzzerOn();
        }

        public static List<IBoardIO> SetIOMain
        {
            set
            {
                motionBoards = value;
            }
        }

        public static void SetTimesUp(int TimesUp)
        {
            timesUp = TimesUp;
        }

        public static void SetStartControlRelay(int StartControlRelay)
        {
            startCtrlRelay = StartControlRelay;
        }

        public static void SetStartButtonLED(int StartButtonLED)
        {
            startBtnLED = StartButtonLED;
        }

        public static void SetBitCodes(int BitCode_1, int BitCode_2, int BitCode_4, int BitCode_8)
        {
            bitCode_1 = BitCode_1;
            bitCode_2 = BitCode_2;
            bitCode_4 = BitCode_4;
            bitCode_8 = BitCode_8;
        }

        public static bool ReadBit(int bit)
        {
            return motionBoards[0].ReadBit(bit);
        }
        
        public static bool ReadBit_AutoMode()
        {
            return motionBoards[0].ReadBit((int)Input.AUTO);
        }

        public static bool ReadBit_McCoverOFF()
        {
            return motionBoards[0].ReadBit((int)Input.MC_COVER_OFF);
        }

        public static bool ReadBit_StartTimer()
        {
            return motionBoards[0].ReadBit((int)Input.START_TIMER);
        }

        public static bool ReadBit_EndLot()
        {
            return motionBoards[0].ReadBit((int)Input.END_LOT);
        }

        public static bool OutBit(int bit, TOutputStatus state)
        {
            return motionBoards[0].OutBit(bit, state);
        }

        public static bool ReadBit(int board, int bit)
        {
            try
            {
                bool bOn = motionBoards[board].ReadBit(bit);
                return bOn;
            }
            catch (Exception ex) { }
            return false;
        }

        public static bool OutBit(int board, int bit, TOutputStatus state)
        {
            bool bOn = motionBoards[board].OutBit(bit, state);
            return bOn;
        }

        public static void ClearBitCodes()
        {
            OutBit(bitCode_1, TOutputStatus.Lo);
            OutBit(bitCode_2, TOutputStatus.Lo);
            OutBit(bitCode_4, TOutputStatus.Lo);
            OutBit(bitCode_8, TOutputStatus.Lo);
        }

        public static void TimesUp(bool On)
        {
            OutBit(timesUp, On ? TOutputStatus.Hi : TOutputStatus.Lo);
        }

        public static void StartControlRelay(bool On)
        {
            OutBit(startCtrlRelay, On ? TOutputStatus.Hi : TOutputStatus.Lo);
        }

        public static void StartButtonLED(bool On)
        {
            OutBit(startBtnLED, On ? TOutputStatus.Hi : TOutputStatus.Lo);
        }

        public static void BarcodeScanReady(bool On)
        {
            OutBit(barcodeScanReady, On ? TOutputStatus.Hi : TOutputStatus.Lo);
        }

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
    }
}
