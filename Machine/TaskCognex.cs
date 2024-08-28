using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;


namespace Machine
{
  class TaskCognex
  {
    static TaskCognex()
    {
      bgw_AutoRun = new BackgroundWorker()
      {
        WorkerSupportsCancellation = true
      };
      bgw_AutoRun.DoWork += new DoWorkEventHandler(bgw_AutoRun_DoWork);
      bgw_AutoRun.ProgressChanged += new ProgressChangedEventHandler(bgw_AutoRun_ProgressChanged);
      bgw_AutoRun.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_AutoRun_RunWorkerCompleted);
      bgw_AutoRun.WorkerReportsProgress = true;
    }
    public static GDefine.StModule Status = GDefine.StModule.Ready;
    public static BackgroundWorker bgw_AutoRun;
    static void bgw_AutoRun_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      bgw_AutoRun.CancelAsync();
    }
    static void bgw_AutoRun_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      throw new NotImplementedException();
    }
    public static void bgw_AutoRun_DoWork(object sender, DoWorkEventArgs e)
    {
      if (bgw_AutoRun.CancellationPending)
      {
        e.Cancel = true;
        return;
      }
      //Thread.Sleep(0);
      //if (Status == GDefine.StModule.Ready)
      {
        //Auto();
        //Run();
      }
    }

    private static bool Delay(int msdelay)
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

    #region TCP Declaration
    public static Socket ClientSocket;
    public static IAsyncResult m_asynResult;
    public static AsyncCallback pfnCallBack;
    public static bool ClearOnce = false;
    public static int NoOfTxt = 0;
    public static int IdxCount = 0;
    public static string RXData = "";
    public static EndPoint m_connectedClientEp = null;
    public static IPEndPoint m_sendEp = null;
    public static Socket m_UdpListenSocket = null;
    public static Socket m_UdpSendSocket = null;
    public static bool TCPIPReceive = false;

    #endregion
    public class CSocketPacket
    {
      public System.Net.Sockets.Socket thisSocket;
      public byte[] dataBuffer = new byte[1];
    }
    public static bool ConnectToHost(ref string IP, ref string Port)
    {
      if (GDefine.OCRConn.Connected) return true;
      GDefine.OCRConn.Connected = false;

      try
      {
        ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        GDefine.OCRConn.IPAddress = IP;
        GDefine.OCRConn.PortNum = Port;
        int alPort = Convert.ToInt32(GDefine.OCRConn.PortNum, 10);

        System.Net.IPAddress remoteIPAddress = System.Net.IPAddress.Parse(GDefine.OCRConn.IPAddress);
        System.Net.IPEndPoint remoteEndPoint = new System.Net.IPEndPoint(remoteIPAddress, alPort);
        ClientSocket.Connect(remoteEndPoint);
        WaitForData();
        TCPIPReceive = true;
      }
      catch (SocketException se)
      {
        uint MsgID = frmMain.frmMsg.ShowMsg("Vision Port Connection: " + se.Message, frmMessaging.TMsgBtn.smbOK);

        return false;
      }

      if (!GDefine.OCRConn.Connected)
      {
        //TaskVisionReceive.ConnectToHost(ref GDefine.IRIP, ref GDefine.IRReceivePort);
      }
      GDefine.OCRConn.Connected = true;
      return true;
    }
    public static bool DisconnectToHost()
    {
      if (!GDefine.OCRConn.Connected) return false;

      ClientSocket.Close();
      GDefine.OCRConn.Connected = false;

      return true;
    }

    public static void WaitForData()
    {
      try
      {
        if (pfnCallBack == null)
        {
          pfnCallBack = new AsyncCallback(OnDataReceived);
        }
        CSocketPacket theSocPkt = new CSocketPacket();
        theSocPkt.thisSocket = ClientSocket;
        // now start to listen for any data...
        m_asynResult = ClientSocket.BeginReceive(theSocPkt.dataBuffer, 0, theSocPkt.dataBuffer.Length, SocketFlags.None, pfnCallBack, theSocPkt);
      }
      catch (SocketException se)
      {
        uint MsgID = frmMain.frmMsg.ShowMsg(se.Message.ToUpper(), frmMessaging.TMsgBtn.smbOK);
        //frmMain.frmMsg.ShowMsg(se.Message.ToUpper(), frmMessaging.TMsgBtn.smbOK);
      }
    }
    public static bool UpdateRXData = false;
    public static bool _receiveResponse = false;
    public static void OnDataReceived(IAsyncResult asyn)
    {
      if (!TCPIPReceive) return;
      try
      {
        CSocketPacket theSockId = (CSocketPacket)asyn.AsyncState;
        //end receive...
        int iRx = 0;
        iRx = theSockId.thisSocket.EndReceive(asyn);

        if (iRx == 0)
        {
          DisconnectToHost();
        }

        if (!ClearOnce)
        {
          ClearOnce = true;
          IdxCount = ClientSocket.Available;
          RXData = "";
        }


        NoOfTxt++;

        char[] chars = new char[iRx + 1];
        System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
        int charLen = d.GetChars(theSockId.dataBuffer, 0, iRx, chars, 0);
        System.String szData = new System.String(chars);
        string ActData = szData.Remove(charLen);
        RXData += ActData;

        WaitForData();
      }
      catch (ObjectDisposedException)
      {
        System.Diagnostics.Debugger.Log(0, "1", "\nOnDataReceived: Socket has been closed\n");
      }
      catch (SocketException se)
      {
        uint MsgID = frmMain.frmMsg.ShowMsg(se.Message.ToUpper(), frmMessaging.TMsgBtn.smbOK);

        DisconnectToHost();
        return;
      }
      try
      {
        if (RXData.Substring(RXData.Length - 1) == ">")
        {
          IdxCount = 0;
          NoOfTxt = 0;
          ClearOnce = false;
          _receiveResponse = true;
          return;
        }
        if ((NoOfTxt - 1) == IdxCount)
        {
          IdxCount = 0;
          NoOfTxt = 0;
          ClearOnce = false;
          _receiveResponse = true;
          return;
        }
      }
      catch
      {
        IdxCount = 0;
        NoOfTxt = 0;
        ClearOnce = false;
        _receiveResponse = true;
      }

    }

    internal static string CR = "\r\n"; // <CR>
    internal static string _ok = "OK";
    internal static string _k = "K";
    public static string _sOCRResult = "";
    public static bool CheckOCRirectFailCounter()
    {
      if (GDefine._iOCRDirectFailCounter >= 5)
      {
        GDefine._iOCRDirectFailCounter = 0;
        uint MsgID = frmMain.frmMsg.ShowMsg(Errcode.Err_OCRDirectFailCounter,
         frmMessaging.TMsgBtn.smbStop |
         frmMessaging.TMsgBtn.smbAlmClr);

        while (!frmMain.frmMsg.ShowMsgClear(MsgID))
        {
          Application.DoEvents();
        }

        switch (frmMain.frmMsg.GetMsgRes(MsgID))
        {
          case frmMessaging.TMsgRes.smrStop:
            return false;
        }
      }
      return true;
    }
    public static bool OCRTrigerAutoMode()
    {
      //if (!TaskVisionZ.VisLoadJob(GDefine._iOCRJobNum)) return false;
      bool _bRetry = false;
      #region Interlock Checking
      _Retry:
      if (!GDefine.OCRConn.Connected)
      {
        uint MsgID = frmMain.frmMsg.ShowMsg("Please Connect To Vision System.",
            frmMessaging.TMsgBtn.smbCancel |
            frmMessaging.TMsgBtn.smbRetry);

        while (!frmMain.frmMsg.ShowMsgClear(MsgID))
        {
          Application.DoEvents();
        }

        switch (frmMain.frmMsg.GetMsgRes(MsgID))
        {
          case frmMessaging.TMsgRes.smrRetry:
            if (!ConnectToHost(ref GDefine.OCRIP, ref GDefine.OCRPort)) return false;
            goto _Retry;
          case frmMessaging.TMsgRes.smrCancel:
            return false;
        }
      }
      #endregion
      try
      {
        _sOCRResult = "";
        GDefine._iCognexStartTime = Environment.TickCount;
        Delay(10);
        _receiveResponse = false;
        string data = "T01" + CR; ;
        //return true;
        RXData = "";
        byte[] byData = System.Text.Encoding.ASCII.GetBytes(data);
        ClientSocket.Send(byData);
        uctrlAuto.Page.AddToLog("H-->V: OCR, Scene: " + data);
        int t = Environment.TickCount + 5000;
        while (true)
        {
          if (_receiveResponse) break;
          if (Environment.TickCount > t) break;
          System.Threading.Thread.Sleep(1);
        }
        if (Environment.TickCount > t)
        {
          if (!_bRetry)
          {
            _bRetry = true;
            goto _Retry;
          }
          uint MsgID3 = frmMain.frmMsg.ShowMsg(Errcode.Err_OCRReadTimeout,
 frmMessaging.TMsgBtn.smbOK | frmMessaging.TMsgBtn.smbRetry |
 frmMessaging.TMsgBtn.smbAlmClr);

          while (!frmMain.frmMsg.ShowMsgClear(MsgID3))
          {
            Application.DoEvents();
          }
          switch (frmMain.frmMsg.GetMsgRes(MsgID3))
          {
            case frmMessaging.TMsgRes.smrRetry:
              goto _Retry;
            case frmMessaging.TMsgRes.smrOK:
              //frmMain.frmOCR.BringToFront();
              //frmMain.frmOCR.ShowDialog();
              return true;
          }
        }
        RXData = RXData.Replace("\t", "").Replace("\r", "").Replace("\n", "");
        _sOCRResult = RXData;
        string[] result = RXData.Split(',');

        GDefine._iCognexEndTime = Environment.TickCount - GDefine._iCognexStartTime;
        //if (TaskInputSingulateIndex._iIS1IndexCounter >= 3 && TaskInputSingulateIndex._iIS1IndexCounter <= 9)
        //{
        //  uctrlAuto.Page.AddToLogCognex("IS 1 Nest " + (TaskInputSingulateIndex._iIS1IndexCounter - 3 + 1).ToString() + ": " + _sOCRResult);
        //}
        //if (TaskInputSingulateIndex._iIS2IndexCounter >= 3 && TaskInputSingulateIndex._iIS2IndexCounter <= 9)
        //{
        //  uctrlAuto.Page.AddToLogCognex("IS 2 Nest " + (TaskInputSingulateIndex._iIS2IndexCounter - 3 + 1).ToString() + ": " + _sOCRResult);
        //}

        //if (result[0] != "OK" || result[1].Contains("?"))
        //{
        //  GDefine._iOCRRejectCounter++;
        //  GDefine._iOCRDirectFailCounter++;
        //  return true;
        //}
        //GDefine._iOCRDirectFailCounter = 0;
        return true;
        if (RXData.Contains("?") || RXData.Contains("Err"))
        {
          if (!_bRetry)
          {
            _bRetry = true;
            goto _Retry;
          }
          uint MsgID5 = frmMain.frmMsg.ShowMsg(Errcode.Err_OCRReceiveOKFail,
          frmMessaging.TMsgBtn.smbOK | frmMessaging.TMsgBtn.smbRetry |
          frmMessaging.TMsgBtn.smbAlmClr);

          while (!frmMain.frmMsg.ShowMsgClear(MsgID5))
          {
            Application.DoEvents();
          }
          switch (frmMain.frmMsg.GetMsgRes(MsgID5))
          {
            case frmMessaging.TMsgRes.smrRetry:
              goto _Retry;
            case frmMessaging.TMsgRes.smrOK:
              //frmMain.frmOCR.BringToFront();
              //frmMain.frmOCR.ShowDialog();
              return true;
          }
        }
        if (result[0] == _ok || result[0] == _k)
        {
          if (result[1] != GDefine.Recipe)
          {
            uint MsgID = frmMain.frmMsg.ShowMsg(Errcode.Err_VisionOCRFail,
            frmMessaging.TMsgBtn.smbCancel |
            frmMessaging.TMsgBtn.smbRetry);

            while (!frmMain.frmMsg.ShowMsgClear(MsgID))
            {
              Application.DoEvents();
            }

            switch (frmMain.frmMsg.GetMsgRes(MsgID))
            {
              case frmMessaging.TMsgRes.smrRetry:
                //i = 0;
                goto _Retry;
              case frmMessaging.TMsgRes.smrCancel:
                return false;
            }
          }
          _sOCRResult = result[1];
          uctrlAuto.Page.AddToLog("V-->H: OCR Receive: " + _sOCRResult);
          return true;
        }
        else
        {
          uint MsgID = frmMain.frmMsg.ShowMsg(Errcode.Err_VisionOrientationOCRFail,
          frmMessaging.TMsgBtn.smbCancel |
          frmMessaging.TMsgBtn.smbRetry);

          while (!frmMain.frmMsg.ShowMsgClear(MsgID))
          {
            Application.DoEvents();
          }

          switch (frmMain.frmMsg.GetMsgRes(MsgID))
          {
            case frmMessaging.TMsgRes.smrRetry:
              goto _Retry;
            case frmMessaging.TMsgRes.smrCancel:
              return false;
          }

        }
        //if (RXData.Contains(("<>")))
        //{
        //  RXData = RXData.Replace("<>", "");
        //}
        //if (RXData.Contains(("<")))
        //{
        //  RXData = RXData.Replace("<", "");
        //}
        //if (RXData.Contains((">")))
        //{
        //  RXData = RXData.Replace(">", "");
        //}
        //string OCRCode = RXData;
        //if (OCRCode.Contains(" "))
        //{
        //  OCRCode = OCRCode.Replace(" ", "-");
        //}
        //uctrlAuto.Page.AddToLog("V-->H: OCR Receive: " + OCRCode);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
        return false;
      }
      return true;
    }

    static bool RunningNum()
    {
      string[] combinations = new string[1679616]; // Array to store combinations
      int count = 0;

      for (int i = 0; i < 36; i++)
      {
        for (int j = 0; j < 36; j++)
        {
          for (int k = 0; k < 36; k++)
          {
            for (int l = 0; l < 36; l++)
            {
              char firstDigit = (char)(i < 10 ? i + 48 : i + 55);
              char secondDigit = (char)(j < 10 ? j + 48 : j + 55);
              char thirdDigit = (char)(k < 10 ? k + 48 : k + 55);
              char fourthDigit = (char)(l < 10 ? l + 48 : l + 55);

              if (firstDigit == '0' && secondDigit == '0' && thirdDigit == '0' && l < 1)
                continue;

              if (firstDigit == '0' && secondDigit == '0' && l < 1)
                continue;

              //if (firstDigit == '0' && k < 1)
              //  continue;

              string combination = $"{firstDigit}{secondDigit}{thirdDigit}{fourthDigit}";

              if (combination.Contains("10") || combination.Contains("20") || combination.Contains("30") || combination.Contains("40") || combination.Contains("50") || combination.Contains("60") || combination.Contains("70") || combination.Contains("80") || combination.Contains("90") ||
                combination.Contains("A0") || combination.Contains("B0") || combination.Contains("C0") || combination.Contains("D0") || combination.Contains("E0") || combination.Contains("F0") || combination.Contains("G0") || combination.Contains("H0") || combination.Contains("I0") ||
                 combination.Contains("J0") || combination.Contains("K0") || combination.Contains("L0") || combination.Contains("M0") || combination.Contains("N0") || combination.Contains("O0") || combination.Contains("P0") || combination.Contains("Q0") || combination.Contains("R0") ||
                  combination.Contains("S0") || combination.Contains("T0") || combination.Contains("U0") || combination.Contains("V0") || combination.Contains("W0") || combination.Contains("X0") || combination.Contains("Y0") || combination.Contains("Z0"))
                continue;

              combinations[count] = $"{firstDigit}{secondDigit}{thirdDigit}{fourthDigit}";
              count++;
            }
          }
        }
      }

      // Generate a text file
      string filePath = "D:\\combinations.txt";
      using (StreamWriter writer = new StreamWriter(filePath))
      {
        foreach (string combination in combinations)
        {
          writer.WriteLine(combination);
        }
      }
      return true;
    }

    public static Proc Process = Proc.None;
    public enum Proc
    {
      _eWaitingReadOCR = 0,
      _eStartReadOCR = 1,
      _eReadOCRComplete = 2,
      None = 4
    }

   // static void CheckOCR(int IS, int index)
   // {
   //   string PartNum = TaskLotInfo.LotInfo.PartNum;
   //   string _sProcessPartNum = PartNum.Remove(PartNum.Length - 3);
   //   string[] data = _sOCRResult.Split(',');
   //   string OCR = data[1].Remove(data[1].Length - 3);
   //   char[] _cOCR = OCR.ToCharArray();
   //   char[] _cPartNum = _sProcessPartNum.ToCharArray();
   //   int counter = 0;
   //   try
   //   {
   //     for (int i = 0; i < _cPartNum.Count(); i++)
   //     {
   //       if (_cPartNum[i] != _cOCR[i])
   //       {
   //         counter++;
   //       }
   //       if (counter >= 2)
   //       {
   //         if (IS == 1)
   //         {
   //           TaskInputSingulateIndex._abIS1PassFail[index] = false;
   //           GDefine._iOCRRejectCounter++;
   //           GDefine._iOCRDirectFailCounter++;
   //           break;
   //         }
   //         else if (IS == 2)
   //         {
   //           TaskInputSingulateIndex._abIS2PassFail[index] = false;
   //           GDefine._iOCRRejectCounter++;
   //           GDefine._iOCRDirectFailCounter++;
   //           break;
   //         }
   //       }
   //       else
   //       {
   //         if (IS == 1)
   //         {
   //           TaskInputSingulateIndex._abIS1PassFail[index] = true;
   //         }
   //         else if (IS == 2)
   //         {
   //           TaskInputSingulateIndex._abIS2PassFail[index] = true;
   //         }
   //       }
   //     }
   //   }
   //   catch
   //   {
   //     counter++;
   //     if (counter == 2)
   //     {

   //     }
   //   }
   // }

   // static bool OCRFailAlarm(int IS, int index)
   // {
   //   if (IS == 1)
   //   {
   //     if (!TaskInputSingulateIndex._abIS1PassFail[index])
   //     {
   //       uint MsgID = frmMain.frmMsg.ShowMsg(Errcode.Err_OCRReceiveNG,
   //frmMessaging.TMsgBtn.smbStop |
   //frmMessaging.TMsgBtn.smbAlmClr);

   //       while (!frmMain.frmMsg.ShowMsgClear(MsgID))
   //       {
   //         Application.DoEvents();
   //       }
   //       switch (frmMain.frmMsg.GetMsgRes(MsgID))
   //       {
   //         case frmMessaging.TMsgRes.smrStop:
   //           return false;
   //       }
   //     }
   //   }
   //   if (IS == 2)
   //   {
   //     if (!TaskInputSingulateIndex._abIS2PassFail[index])
   //     {
   //       uint MsgID = frmMain.frmMsg.ShowMsg(Errcode.Err_OCRReceiveNG,
   //frmMessaging.TMsgBtn.smbStop |
   //frmMessaging.TMsgBtn.smbAlmClr);

   //       while (!frmMain.frmMsg.ShowMsgClear(MsgID))
   //       {
   //         Application.DoEvents();
   //       }
   //       switch (frmMain.frmMsg.GetMsgRes(MsgID))
   //       {
   //         case frmMessaging.TMsgRes.smrStop:
   //           return false;
   //       }
   //     }
   //   }
   //   return true;
   // }
   // static void CheckReadBack(int IS, int index)
   // {
   //   try
   //   {
   //     if (IS == 1)
   //     {
   //       string[] data = _sOCRResult.Split(',');
   //       for (int i = 0; i < data.Length; i++)
   //       {
   //         if (i == 0)
   //         {
   //           if (data[i].Contains("NG") || data[i].Contains("N") || data[i].Contains("G"))
   //           {
   //             TaskInputSingulateIndex._abIS1PassFail[index] = false;
   //             GDefine._iOCRRejectCounter++;
   //             GDefine._iOCRDirectFailCounter++;
   //             break;
   //           }
   //         }
   //         if (i == 1)
   //         {
   //           if (TaskLotInfo.LotInfo.PartNum.Contains(data[i]))
   //           {
   //             TaskInputSingulateIndex._abIS1PassFail[index] = true;
   //             GDefine._iOCRDirectFailCounter = 0;
   //           }
   //           else
   //           {
   //             //TaskInputSingulateIndex._abIS1PassFail[index] = false;
   //             //GDefine._iOCRRejectCounter++;
   //             //GDefine._iOCRDirectFailCounter++;
   //             CheckOCR(1, index);
   //             break;
   //           }
   //         }

   //       }
   //     }
   //     else if (IS == 2)
   //     {
   //       string[] data = _sOCRResult.Split(',');
   //       for (int i = 0; i < 4; i++)
   //       {
   //         if (i == 0)
   //         {
   //           if (data[i].Contains("NG") || data[i].Contains("N") || data[i].Contains("G")) //if (data[i] != "OK")
   //           {
   //             TaskInputSingulateIndex._abIS2PassFail[index] = false;
   //             GDefine._iOCRRejectCounter++;
   //             GDefine._iOCRDirectFailCounter++;
   //             break;
   //           }
   //         }
   //         if (i == 1)
   //         {
   //           if (TaskLotInfo.LotInfo.PartNum.Contains(data[i]))
   //           {
   //             TaskInputSingulateIndex._abIS2PassFail[index] = true;
   //             GDefine._iOCRDirectFailCounter = 0;
   //           }
   //           else
   //           {
   //             CheckOCR(2, index);
   //             //TaskInputSingulateIndex._abIS2PassFail[index] = false;
   //             //GDefine._iOCRRejectCounter++;
   //             //GDefine._iOCRDirectFailCounter++;
   //             break;
   //           }
   //         }
   //       }
   //     }
   //   }
   //   catch
   //   {
   //     if (IS == 1)
   //     {
   //       TaskInputSingulateIndex._abIS1PassFail[index] = false;
   //     }
   //     else if (IS == 2)
   //     {
   //       TaskInputSingulateIndex._abIS2PassFail[index] = false;
   //     }
   //   }
   // }
   // public static bool Run()
   // {
   //   if (Process == Proc._eWaitingReadOCR)
   //   {
   //     goto _End;
   //   }
   //   if (Process == Proc._eStartReadOCR)
   //   {
   //     if (GDefine._bEnabCognexOCR)
   //     {
   //       GDefine.Delay(1000);
   //       if (TaskInputSingulateIndex._iIS1IndexCounter >= 3 && TaskInputSingulateIndex._iIS1IndexCounter <= 9)
   //       {
   //         GDefine._iCognexStartTime = Environment.TickCount;
   //         if (!OCRTrigerAutoMode()) goto _Stop;
   //         CheckReadBack(1, TaskInputSingulateIndex._iIS1IndexCounter - 3);
   //         GDefine._iCognexEndTime = Environment.TickCount - GDefine._iCognexStartTime;
   //         if (!OCRFailAlarm(1, TaskInputSingulateIndex._iIS1IndexCounter - 3))
   //         {
   //           Process = Proc._eWaitingReadOCR;
   //           goto _Stop;
   //         }
   //       }
   //       else if (TaskInputSingulateIndex._iIS2IndexCounter >= 3 && TaskInputSingulateIndex._iIS2IndexCounter <= 9)
   //       {
   //         GDefine._iCognexStartTime = Environment.TickCount;
   //         if (!OCRTrigerAutoMode()) goto _Stop;
   //         CheckReadBack(2, TaskInputSingulateIndex._iIS2IndexCounter - 3);
   //         GDefine._iCognexEndTime = Environment.TickCount - GDefine._iCognexStartTime;
   //         if (!OCRFailAlarm(2, TaskInputSingulateIndex._iIS2IndexCounter - 3))
   //         {
   //           Process = Proc._eWaitingReadOCR;
   //           goto _Stop;
   //         }
   //       }
   //       else
   //       {
   //         Delay(100);
   //       }
   //     }
   //     else
   //     {
   //       Delay(100);
   //     }

   //     Process = Proc._eReadOCRComplete;
   //     goto _End;
   //   }
   //   if (Process == Proc._eReadOCRComplete)
   //   {
   //     if (!CheckOCRirectFailCounter()) goto _Stop;
   //     Process = Proc._eWaitingReadOCR;
   //     goto _End;
   //   }

   //   _End:
   //   Status = GDefine.StModule.Ready;
   //   return true;
   //   _Stop:
   //   Status = GDefine.StModule.Stop;
   //   return false;
   // }
  }
}
