using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotionIODevice.IO.UniDAQ
{
    public class UniDAQBoard
    {


        static Mutex IOMutex = new Mutex();
        private static UniDAQ.IXUD_CARD_INFO[] sCardInfo = new UniDAQ.IXUD_CARD_INFO[UniDAQ.MAX_BOARD_NUMBER];
        private static UniDAQ.IXUD_DEVICE_INFO[] sDeviceInfo = new UniDAQ.IXUD_DEVICE_INFO[UniDAQ.MAX_BOARD_NUMBER];
        private static ushort iTotalBoard;
        private static bool isInitBoardNo = false;
        private static bool isGetGardInfo = false;
        public static bool[] isBoardOpen = new bool[UniDAQ.MAX_BOARD_NUMBER];
        public static bool isBoardOpened = false;
        public static bool InitBoardNo()
        {
            try
            {
                ushort iStatus = UniDAQ.Ixud_DriverInit(ref iTotalBoard);
                if (iStatus == UniDAQ.Ixud_NoErr)
                {
                    isInitBoardNo = true;
                }
            }
            catch (Exception ex) { }
            return true;
        }

        public static bool GetGardInfo()
        {
            if (!isInitBoardNo)
                return false;

            byte[] szModeName = new byte[20];

            for (ushort wBoardIndex = 0; wBoardIndex < iTotalBoard; wBoardIndex++)
            {
                ushort iStatus = UniDAQ.Ixud_GetCardInfo(wBoardIndex, ref sDeviceInfo[wBoardIndex], ref sCardInfo[wBoardIndex], szModeName);
                if (iStatus != UniDAQ.Ixud_NoErr)
                {
                    return false;
                }
            }

            isGetGardInfo = true;
            return true;
        }

        public static void SetBoardOpen()
        {
            for (ushort wBoardIndex = 0; wBoardIndex < iTotalBoard; wBoardIndex++)
            {
                if ((sCardInfo[wBoardIndex].wDIOPorts + sCardInfo[wBoardIndex].wDIPorts) > 0)
                {
                    isBoardOpen[wBoardIndex] = true;
                }
                else
                {
                    isBoardOpen[wBoardIndex] = false;
                }
            }

            isBoardOpened = true;
            //Set DIO mode 1:Output 0:Input
            //ushort iStatus = UniDAQ.Ixud_SetDIOModes32(uBoardNo, uIOMode);

        }

        public static void SetBoardIOMode(ushort uBoardNo, ushort uIOMode)
        {
            ushort iStatus = UniDAQ.Ixud_SetDIOModes32(uBoardNo, uIOMode);
        }

        public static void CloseBoard()
        {
            //ushort iStatus = UniDAQ.Ixud_DriverClose();
            isInitBoardNo = false;
            isGetGardInfo = false;
            isBoardOpened = false;
            for (ushort wBoardIndex = 0; wBoardIndex < iTotalBoard; wBoardIndex++)
            {
                isBoardOpen[wBoardIndex] = false;
            }
        }

        public static ushort GetTotalBoard()
        {
            return iTotalBoard;
        }

        public static bool UpdateOutput(ushort uBoardID, ushort uAxisPort, ref byte cbOut)
        {
            if (!isInitBoardNo || !isGetGardInfo)
                return false;
            IOMutex.WaitOne();
            try
            {
                ushort iStatus = UniDAQ.Ixud_WriteDO(uBoardID, uAxisPort, cbOut);
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
        public static bool UpdateInput(ushort uBoardID, ushort uAxisPort, ref uint uValue)
        {
            if (!isInitBoardNo || !isGetGardInfo)
                return false;

            try
            {
                IOMutex.WaitOne();
                uint DIVal;
                DIVal = 0x0;
                ushort iStatus = UniDAQ.Ixud_ReadDI(uBoardID, uAxisPort, ref DIVal);
                uValue = DIVal;
            }
            catch (Exception ex)
            {
                IOMutex.ReleaseMutex();
                MessageBox.Show(ex.Message);
                return false;
            }

            IOMutex.ReleaseMutex();
            return true;
        }


    }
}
