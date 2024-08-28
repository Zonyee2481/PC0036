using Advantech.Motion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotionIODevice.IO.ViE
{
    public class ViEBoard
    {
        ViE vie = null;
        static Mutex IOMutex = new Mutex();
        private static int iTotalBoard = 0;
        private static int iMode = 0; // iMode =0 -> Auto Board ID
        private static bool isInitBoardNo = false;
        private static int iBusNo = 0; // Fixed Bus ID: 0x0: IO Bus 0x1: Motion Bus
        private static int iDllRevision = 0;
        private static int iBoardRevision = 0;
        //public static bool[] isBoardOpen = new bool[vie.MAX_BOARD_NUMBER];
        //public static bool[] isModuleOnline = new bool[vie.MAX_MODULE_NUMBER];
        public static bool isBoardOpened = false;
    
        public ViEBoard()
        {
            vie = new ViE();
        }

        public static bool InitBoardNo()
        {
            try
            {
                int iStatus = ViE.VMIO_open(ref iTotalBoard, iMode);
            }
            catch (Exception ex) { }
            return true;
        }

        public static bool OpenBoard()
        {
            int iStatus = 0;
            iStatus = ViE.VMIO_open(ref iTotalBoard, iMode);
            ViE.VMIO_board_revision(0, ref iBoardRevision);
            iStatus = ViE.VMIO_dll_revision(ref iDllRevision);
            if (iStatus != ViE.VMIO_SUCCESS)
            {
                return false;
            }
            for (int i = 0; i < iTotalBoard; i++)
            {
                StartStopModule(i, iBusNo, false); // Disable all module 
                Thread.Sleep(10);
                StartStopModule(i, iBusNo, true); // Enable all module

                iStatus = ViE.VMIO_board_revision(i, ref iBusNo);
            }
            if (iStatus == ViE.VMIO_SUCCESS)
            {
                isBoardOpened = true;
                return true;
            }
            return false;
        }

        public static bool CloseBoard()
        {
            for (int i = 0; i < iTotalBoard; i++)
            {
                StartStopModule(i, iBusNo, false);
            }                       
            ViE.VMIO_close();
            isBoardOpened = false;
            return true;
        }

        private static void StartStopModule(int iBoardNo, int iBusNo, bool bEnab)
        {
            if (bEnab)
            {
                ViE.VMIO_start_bus(iBoardNo, iBusNo);
            }
            else
            {
                ViE.VMIO_stop_bus(iBoardNo, iBusNo); 
            }
        }

        public static int GetTotalBoard()
        {
            return iTotalBoard;
        }

        public static void GetBoardRevision(ushort uBoardNo, ref int iBusNo)
        {
            ViE.VMIO_board_revision(uBoardNo, ref iBusNo);
        }

        public static bool GetModuleStatus(ushort uBoardNo, ushort uModuleNo) 
        {
            bool res = false;
            int iOnline = 0;
            int iStatus = ViE.VMIO_get_module_online_status(uBoardNo, iBusNo, uModuleNo, ref iOnline);
            res = BoardStatus(iStatus);
            if (!res) { return res; }
            res = iOnline == 1 ? true : false;
            return res;
        }

        public static bool BoardStatus(int iStatus) 
        {
            return iStatus == ViE.VMIO_SUCCESS ? true : false;
        }

        public static bool UpdateOutput(ushort usBoardNo, ushort usModuleID, ref uint cbOut)
        {
            if (!isBoardOpened)
            {
                return false;
            }
            
            try
            {
                IOMutex.WaitOne();
                int iStatus = ViE.VMIO_d_set_output(usBoardNo, iBusNo, usModuleID, cbOut);
            }
            catch (Exception ex) 
            {
                IOMutex.ReleaseMutex();
                MessageBox.Show(ex.Message);
                throw;
            }
            IOMutex.ReleaseMutex();
            return true;
        }

        public static bool UpdateInput(ushort uBoardNo, ushort uModuleID, int uAxisPort, ref uint uValue)
        {
            if (!isBoardOpened)
            {
                return false;
            }

            try
            {
                IOMutex.WaitOne();
                UInt32 uiInputData = 0;
                int iStatus = ViE.VMIO_d_get_input(uBoardNo, iBusNo, uModuleID, ref uiInputData);
                uiInputData &= 0xFFFFFFFF;
                uValue = uiInputData;
            }
            catch (Exception ex)
            {
                IOMutex.ReleaseMutex();
                return false;
            }
            IOMutex.ReleaseMutex();
            return true;
        }
    }
}
