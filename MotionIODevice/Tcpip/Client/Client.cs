using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MotionIODevice.Tcpip
{
    public class Client : IClient
    {
        private TcpModuleID _TcpModule;
        private Common _Client;
        private List<Command> _enumcommand = new List<Command>();
        private AsyncCallback _pfnCallBack;
        private IAsyncResult m_asynResult;
        private bool _IsConnect = false;
        private bool _IsReceive = false;
        private string RXData = "";
        private bool ClearOnce = false;
        private int IdxCount = 0;
        public static int NoOfTxt = 0;

        public bool IsConnect()
        {
            return _IsConnect;
        }

        public Client(TcpModuleID TcpModule, string IP, string Port)
        {
            try
            {
                _Client = new Common();
                _Client.thissocket = new CSocketPacket();
                _TcpModule = TcpModule;
                var m_Port = Convert.ToInt32(Port);
                _Client.thissocket.thisSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _Client.IpAddress = IPAddress.Parse(IP);
                _Client.EndPoint = new IPEndPoint(_Client.IpAddress, m_Port);
                _Client.thissocket.thisSocket.Connect(_Client.EndPoint);
                WaitForData();
                _IsConnect = true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                _IsConnect = false;
            }
        }

        void IClient.AddCommand(Command commands)
        {
            _enumcommand.Add(commands);
        }

        void IClient.ConnectModule(string IP, string Port)
        {
            try
            {
                _Client = new Common();
                _Client.thissocket = new CSocketPacket();
                var m_Port = Convert.ToInt32(Port);
                _Client.thissocket.thisSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _Client.IpAddress = IPAddress.Parse(IP);
                _Client.EndPoint = new IPEndPoint(_Client.IpAddress, m_Port);
                Task.Run(() =>
                {
                    try
                    {
                        _Client.thissocket.thisSocket.Connect(_Client.EndPoint);
                        _IsConnect = true;
                        WaitForData();
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception within the task
                        MessageBox.Show(ex.Message);
                        _IsConnect = false;
                    }
                }).Wait(TimeSpan.FromSeconds(5));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void WaitForData()
        {
            try
            {
                if (_pfnCallBack == null)
                {
                    _pfnCallBack = new AsyncCallback(OnDataReceived);
                }
                CSocketPacket theSocPkt = new CSocketPacket();
                theSocPkt.thisSocket = _Client.thissocket.thisSocket;
                // now start to listen for any data...
                m_asynResult = _Client.thissocket.thisSocket.BeginReceive(theSocPkt.dataBuffer, 0, theSocPkt.dataBuffer.Length, SocketFlags.None, _pfnCallBack, theSocPkt);
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message.ToString());
            }
        }

        public void OnDataReceived(IAsyncResult asyn)
        {
            if (!_IsConnect) return;
            try
            {
                CSocketPacket theSockId = (CSocketPacket)asyn.AsyncState;
                //end receive...
                int iRx = 0;
                iRx = theSockId.thisSocket.EndReceive(asyn);

                if (iRx == 0)
                {
                    Disconnect();
                }

                if (!ClearOnce)
                {
                    ClearOnce = true;
                    IdxCount = _Client.thissocket.thisSocket.Available;
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
                System.Diagnostics.Debugger.Log(0, "1", "\nKeyence Vision Out: OnDataReceived: Socket has been closed\n");
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message.ToString());

                Disconnect();
                return;
            }
            try
            {
                if (RXData.Substring(RXData.Length - 1) == ">")
                {
                    IdxCount = 0;
                    NoOfTxt = 0;
                    ClearOnce = false;
                    _IsReceive = true;
                    return;
                }
                if ((NoOfTxt - 1) == IdxCount)
                {
                    IdxCount = 0;
                    NoOfTxt = 0;
                    ClearOnce = false;
                    _IsReceive = true;
                    return;
                }
            }
            catch
            {
                IdxCount = 0;
                NoOfTxt = 0;
                ClearOnce = false;
                _IsReceive = true;
            }

        }

        //private void OnDataReceived(IAsyncResult asyn)
        //{
        //    bool ClearOnce = false;
        //    int iRx = 0;
        //    int IdxCount = 0;
        //    int NoOfTxt = 0;
        //    RXData = "";

        //    PortInfo socketid = (PortInfo)asyn.AsyncState;

        //    iRx = socketid.ClientSocket.EndReceive(asyn);

        //    if (iRx == 0)
        //    {
        //        Disconnect();
        //    }

        //    if (!ClearOnce)
        //    {
        //        ClearOnce = true;
        //        IdxCount = _Client.ClientSocket.Available;
        //        //RXData = "";
        //        //txt_Rx.Invoke(new EventHandler(delegate
        //        //{
        //        //    txt_Rx.Text    = "";
        //        //}));
        //    }

        //    char[] chars = new char[iRx + 1];
        //    System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
        //    int charLen = d.GetChars(_Client.dataBuffer, 0, iRx, chars, 0);
        //    System.String szData = new System.String(chars);
        //    string ActData = szData.Remove(charLen);
        //    RXData += ActData;

        //    StartReceiveData();

        //    if ((NoOfTxt - 1) == IdxCount) //if (RXData.Contains("\r\n"))
        //    {
        //        IdxCount = 0;
        //        NoOfTxt = 0;
        //        ClearOnce = false;
        //        _IsReceive = true;
        //        //statusAutoMode(RXData);
        //    }
        //}

        private bool Disconnect()
        {
            if (!_IsConnect) return false;

            _IsConnect = false;
            _Client.thissocket.thisSocket.Close();

            return true;
        }

        bool IClient.DisconnectToHost()
        {
            return Disconnect();
        }
        bool IClient.IsReceive()
        {
            return _IsReceive;
        }

        void IClient.ResetReceive()
        {
            _IsReceive = false;
        }

        string IClient.GetMessage()
        {
            //var Temp = RXData;
            //RXData = "";
            return RXData;
        }

        void IClient.SendCommand(Command enumcommand)
        {
            _IsReceive = false;
            IdxCount = 0;
            NoOfTxt = 0;
            RXData = "";
            var getenum = _enumcommand.FirstOrDefault(x => x == enumcommand);
            string strcommand = CommonFunc.GetEnumDescription(getenum);

            byte[] byData = System.Text.Encoding.ASCII.GetBytes(strcommand);
            _Client.thissocket.thisSocket.Send(byData);
        }

        void IClient.SendCustomizedCommand(string command)
        {
            byte[] byData = System.Text.Encoding.ASCII.GetBytes(command);
            _Client.thissocket.thisSocket.Send(byData);
        }
    }
}
