using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine
{
  class CognexMainServer
  {
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

    internal static string admin = "admin";
    internal static string password = "";
    internal static string CR = "\r\n"; // <CR>
    internal static string go = "go";
    internal static string so = "so";
    internal static string lf = "lf";
    internal static string small = "small";
    internal static string big = "big";

    public static bool ConnectToHost(ref string IP, ref string Port)
    {
      if (!GDefine._bEnabCognexOCR) return true;

      GDefine.OCRMainConn.Connected = false;

      try
      {
        ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        GDefine.OCRMainConn.IPAddress = IP;
        GDefine.OCRMainConn.PortNum = Port;
        int alPort = Convert.ToInt32(GDefine.OCRMainConn.PortNum, 10);

        System.Net.IPAddress remoteIPAddress = System.Net.IPAddress.Parse(GDefine.OCRMainConn.IPAddress);
        System.Net.IPEndPoint remoteEndPoint = new System.Net.IPEndPoint(remoteIPAddress, alPort);
        ClientSocket.Connect(remoteEndPoint);
        WaitForData();
        RXData = "";
        TCPIPReceive = true;
      }
      catch (SocketException se)
      {
        uint MsgID = frmMain.frmMsg.ShowMsg("Vision OCR Connection: " + se.Message, frmMessaging.TMsgBtn.smbOK);

        return false;
      }

      GDefine.OCRMainConn.Connected = true;
      return true;
    }

    public static bool DisconnectToHost()
    {
      if (!GDefine.OCRMainConn.Connected) return false;

      GDefine.OCRMainConn.Connected = false;
      ClientSocket.Close();

      return true;
    }

    public class CSocketPacket
    {
      public System.Net.Sockets.Socket thisSocket;
      public byte[] dataBuffer = new byte[1];
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
          //txt_Rx.Invoke(new EventHandler(delegate
          //{
          //    txt_Rx.Text    = "";
          //}));
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
        //frmMain.frmMsg.ShowMsg(se.Message.ToUpper(), frmMessaging.TMsgBtn.smbOK);
        DisconnectToHost();
        return;
      }

      if ((NoOfTxt - 1) == IdxCount)
      {
        IdxCount = 0;
        NoOfTxt = 0;
        ClearOnce = false;
        _receiveResponse = true;
                return;
        //TaskVisionComm.RXCheckVisionCmd(RXData);
      }
    }

    public static bool UserLogin()
    {
      if (GDefine._bEnableDryRun || !GDefine._bEnabCognexOCR) { return true; }

      _Retry:
      #region Interlock Checking

      if (!GDefine.OCRMainConn.Connected)
      {
        uint MsgID = frmMain.frmMsg.ShowMsg("Please Connect To Vision OCR Main TCPIP.",
            frmMessaging.TMsgBtn.smbCancel |
            frmMessaging.TMsgBtn.smbRetry);

        while (!frmMain.frmMsg.ShowMsgClear(MsgID))
        {
          Application.DoEvents();
        }

        switch (frmMain.frmMsg.GetMsgRes(MsgID))
        {
          case frmMessaging.TMsgRes.smrRetry:
            if (!ConnectToHost(ref GDefine.CognexMainIP, ref GDefine.CognexMainPort)) goto _Err;
            goto _Retry;
          case frmMessaging.TMsgRes.smrCancel:
            goto _Err;
        }
      }
      #endregion

      try
      {
        RXData = "";
        string data = "";
        _receiveResponse = false;
        data = admin + CR;
        byte[] byData;
        byData = System.Text.Encoding.ASCII.GetBytes(data);
        ClientSocket.Send(byData);
        //uctrlAuto.Page.AddToLog("H-->V: Login Cognex Main IP (User): " + admin);
        GDefine.Delay(50);
        data = password + CR;
        byData = System.Text.Encoding.ASCII.GetBytes(data);
        ClientSocket.Send(byData);
        //uctrlAuto.Page.AddToLog("H-->V: Login Cognex Main IP (Password): " + password);
        GDefine.Delay(50);
        int t = Environment.TickCount + 10000;
        while (true)
        {
          if (_receiveResponse) break;
          if (Environment.TickCount > t) break;
          System.Threading.Thread.Sleep(1);
        }
        if (Environment.TickCount > t)
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Login Cognex main IP timeout!",
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
        RXData = RXData.Replace("\t", "").Replace("\r", "").Replace("\n", "");
        if (RXData != "User Logged In")
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Failed to login Cognex main IP!",
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
        uctrlAuto.Page.AddToLog("H-->V: Successful Login Cognex Main IP");
        return true;
      }
      catch (Exception ex) { MessageBox.Show(ex.Message); goto _Err; }
      _Err:
      return false;
    }

    public static bool GetOnline()
    {
      if (GDefine._bEnableDryRun) { return true; }

      _Retry:
      #region Interlock Checking

      if (!GDefine.OCRMainConn.Connected)
      {
        uint MsgID = frmMain.frmMsg.ShowMsg("Please Connect To Vision OCR Main TCPIP.",
            frmMessaging.TMsgBtn.smbCancel |
            frmMessaging.TMsgBtn.smbRetry);

        while (!frmMain.frmMsg.ShowMsgClear(MsgID))
        {
          Application.DoEvents();
        }

        switch (frmMain.frmMsg.GetMsgRes(MsgID))
        {
          case frmMessaging.TMsgRes.smrRetry:
            if (!ConnectToHost(ref GDefine.CognexMainIP, ref GDefine.CognexMainPort)) goto _Err;
            goto _Retry;
          case frmMessaging.TMsgRes.smrCancel:
            goto _Err;
        }
      }
      #endregion

      try
      {
        RXData = "";
        _receiveResponse = false;
        string data = go + CR;
        byte[] byData = System.Text.Encoding.ASCII.GetBytes(data);
        ClientSocket.Send(byData);
        uctrlAuto.Page.AddToLog("H-->V: Get Online");
        int t = Environment.TickCount + 10000;
        while (true)
        {
          if (_receiveResponse) break;
          if (Environment.TickCount > t) break;
          System.Threading.Thread.Sleep(1);
        }
        if (Environment.TickCount > t)
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Get Cognex online status timeout!",
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
        RXData = RXData.Replace("\t", "").Replace("\r", "").Replace("\n", "");
        if (RXData == "1")
        {
          return true;
        }
        else if (RXData == "0")
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Unrecognized command!",
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
        else if (RXData == "-1")
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Invalid argument!",
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
        else if (RXData == "-2")
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Command could not be executed!",
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
        else if (RXData == "-5")
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Sensor has been put offline manually!",
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
        else if (RXData == "-6")
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("User does not have Full Access to execute the command!",
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
        else
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Unknown error!",
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
        //return true;
      }
      catch (Exception ex) { MessageBox.Show(ex.Message); goto _Err; }
      _Err:
      return false;
    }

    public static bool SetOnline(bool online)
    {
      if (GDefine._bEnableDryRun) { return true; }

      _Retry:
      int st = 0;

      st = (online ? 1 : 0);
      #region Interlock Checking
      if (!GDefine.OCRMainConn.Connected)
      {
        uint MsgID = frmMain.frmMsg.ShowMsg("Please Connect To Vision OCR Main TCPIP.",
            frmMessaging.TMsgBtn.smbCancel |
            frmMessaging.TMsgBtn.smbRetry);

        while (!frmMain.frmMsg.ShowMsgClear(MsgID))
        {
          Application.DoEvents();
        }

        switch (frmMain.frmMsg.GetMsgRes(MsgID))
        {
          case frmMessaging.TMsgRes.smrRetry:
            if (!ConnectToHost(ref GDefine.CognexMainIP, ref GDefine.CognexMainPort)) goto _Err;
            goto _Retry;
          case frmMessaging.TMsgRes.smrCancel:
            goto _Err;
        }
      }
      #endregion

      try
      {
        RXData = "";
        _receiveResponse = false;
        string data = so + st + CR;
        byte[] byData = System.Text.Encoding.ASCII.GetBytes(data);
        ClientSocket.Send(byData);
        uctrlAuto.Page.AddToLog("H-->V: Set Online " + st);
        int t = Environment.TickCount + 10000;
        while (true)
        {
          if (_receiveResponse) break;
          if (Environment.TickCount > t) break;
          System.Threading.Thread.Sleep(1);
        }
        if (Environment.TickCount > t)
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Set Cognex online " + st + " timeout!",
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
        RXData = RXData.Replace("\t", "").Replace("\r", "").Replace("\n", "");
        if (RXData == "1")
        {
          return true;
        }
        else if (RXData == "0")
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Unrecognized command!",
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
        else if (RXData == "-1")
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Invalid argument!",
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
        else if (RXData == "-2")
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Command could not be executed!",
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
        else if (RXData == "-5")
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Sensor has been put offline manually!",
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
        else if (RXData == "-6")
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("User does not have Full Access to execute the command!",
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
        else
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Unknown error!",
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
        //return true;
      }
      catch (Exception ex) { MessageBox.Show(ex.Message); goto _Err; }
      _Err:
      return false;
    }

    public static bool LoadFileBigOCR()
    {
      if (GDefine._bEnableDryRun) { return true; }

      _Retry:
      #region Interlock Checking
      if (!GDefine.OCRMainConn.Connected)
      {
        uint MsgID = frmMain.frmMsg.ShowMsg("Please Connect To Vision OCR Main TCPIP.",
            frmMessaging.TMsgBtn.smbCancel |
            frmMessaging.TMsgBtn.smbRetry);

        while (!frmMain.frmMsg.ShowMsgClear(MsgID))
        {
          Application.DoEvents();
        }

        switch (frmMain.frmMsg.GetMsgRes(MsgID))
        {
          case frmMessaging.TMsgRes.smrRetry:
            if (!ConnectToHost(ref GDefine.CognexMainIP, ref GDefine.CognexMainPort)) goto _Err;
            goto _Retry;
          case frmMessaging.TMsgRes.smrCancel:
            goto _Err;
        }
      }
      #endregion
      try
      {
        RXData = "";
        _receiveResponse = false;
        string data = lf + big + CR;
        byte[] byData = System.Text.Encoding.ASCII.GetBytes(data);
        ClientSocket.Send(byData);
        uctrlAuto.Page.AddToLog("H-->V: Load Big OCR File");
        int t = Environment.TickCount + 10000;
        while (true)
        {
          if (_receiveResponse) break;
          if (Environment.TickCount > t) break;
          System.Threading.Thread.Sleep(1);
        }
        if (Environment.TickCount > t)
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Load big OCR file timeout!",
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
        RXData = RXData.Replace("\t", "").Replace("\r", "").Replace("\n", "");
        if (RXData == "1")
        {
          return true;
        }
        else if (RXData == "0")
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Unrecognized command!",
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
        else if (RXData == "-1")
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Invalid argument!",
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
        else if (RXData == "-2")
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Command could not be executed!",
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
        else if (RXData == "-5")
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Sensor has been put offline manually!",
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
        else if (RXData == "-6")
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("User does not have Full Access to execute the command!",
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
        else
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Unknown error!",
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
        //return true;
      }
      catch (Exception ex) { MessageBox.Show(ex.Message); goto _Err; }
      _Err:
      return false;
    }

    public static bool LoadFileSmallOCR()
    {
      if (GDefine._bEnableDryRun) { return true; }

      _Retry:
      #region Interlock Checking
      if (!GDefine.OCRMainConn.Connected)
      {
        uint MsgID = frmMain.frmMsg.ShowMsg("Please Connect To Vision OCR Main TCPIP.",
            frmMessaging.TMsgBtn.smbCancel |
            frmMessaging.TMsgBtn.smbRetry);

        while (!frmMain.frmMsg.ShowMsgClear(MsgID))
        {
          Application.DoEvents();
        }

        switch (frmMain.frmMsg.GetMsgRes(MsgID))
        {
          case frmMessaging.TMsgRes.smrRetry:
            if (!ConnectToHost(ref GDefine.CognexMainIP, ref GDefine.CognexMainPort)) goto _Err;
            goto _Retry;
          case frmMessaging.TMsgRes.smrCancel:
            goto _Err;
        }
      }
      #endregion
      try
      {
        RXData = "";
        _receiveResponse = false;
        string data = lf + small + CR;
        byte[] byData = System.Text.Encoding.ASCII.GetBytes(data);
        ClientSocket.Send(byData);
        uctrlAuto.Page.AddToLog("H-->V: Load Small OCR File");
        int t = Environment.TickCount + 10000;
        while (true)
        {
          if (_receiveResponse) break;
          if (Environment.TickCount > t) break;
          System.Threading.Thread.Sleep(1);
        }
        if (Environment.TickCount > t)
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Load small OCR file timeout!",
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
        RXData = RXData.Replace("\t", "").Replace("\r", "").Replace("\n", "");
        if (RXData == "1")
        {
          return true;
        }
        else if (RXData == "0")
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Unrecognized command!",
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
        else if (RXData == "-1")
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Invalid argument!",
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
        else if (RXData == "-2")
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Command could not be executed!",
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
        else if (RXData == "-5")
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Sensor has been put offline manually!",
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
        else if (RXData == "-6")
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("User does not have Full Access to execute the command!",
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
        else
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Unknown error!",
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
        //return true;
      }
      catch (Exception ex) { MessageBox.Show(ex.Message); goto _Err; }
      _Err:
      return false;
    }

    public static bool BigOCR()
    {
            if (!UserLogin()) goto _Err;
            if (!SetOnline(false)) goto _Err;
      GDefine.Delay(100);
      if (!LoadFileBigOCR()) goto _Err;
      GDefine.Delay(100);
      if (!SetOnline(true)) goto _Err;
      GDefine.Delay(100);
      return true;
      _Err:
      return false;
    }

    public static bool SmallOCR()
    {
      if (!UserLogin()) goto _Err;
      if (!SetOnline(false)) goto _Err;
      GDefine.Delay(100);
      if (!LoadFileSmallOCR()) goto _Err;
      GDefine.Delay(100);
      if (!SetOnline(true)) goto _Err;
      GDefine.Delay(100);
      return true;
      _Err:
      return false;
    }

  }
}
