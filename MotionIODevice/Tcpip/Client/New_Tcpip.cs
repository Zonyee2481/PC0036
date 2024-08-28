using Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotionIODevice.Tcpip
{
    public class New_Tcpip : IClient
    {
        private TcpClient Client;
        private NetworkStream Streaming;
        private StreamWriter Writer;
        private TcpModuleID _TcpModule;
        private string m_ip;
        private string m_Port;

        private Queue m_QueueSzData = new Queue();
        public Queue QueueSzData { get { return m_QueueSzData; } set { m_QueueSzData = value; } }

        private List<Command> _enumcommand = new List<Command>();

        private const int BufferSize = 8 * 1024;

        protected object m_SyncLog = new object();
        private bool _IsConnect = false;
        private bool _IsReceive = false;
        private string RXData = "";

        public bool IsConnect()
        {
            return _IsConnect;
        }

        public New_Tcpip(TcpModuleID TcpModule, string IP, string Port)
        {
            try
            {
                Task.Run(() =>
                {
                    try
                    {
                        _TcpModule = TcpModule;
                        Client = new TcpClient();
                        Client.Connect(IP, Convert.ToInt32(Port));
                        Writer = new StreamWriter(Client.GetStream());

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

        public void ConnectModule(string IP, string Port)
        {
            try
            {
                Task.Run(() =>
                {
                    try
                    {
                        Client = new TcpClient();
                        Client.Connect(IP, Convert.ToInt16(Port));
                        Writer = new StreamWriter(Client.GetStream());

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
                byte[] buffer = new byte[BufferSize];

                Client.Client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, OnDataReceived, buffer);
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message.ToString());
            }
            catch (Exception ex)
            {
                // Log any other exceptions and handle them appropriately
                Log(_TcpModule.ToString(), $"Error occurred while waiting for data: {ex.Message}");
            }
        }

        //Old
        //public void OnDataReceived(IAsyncResult asyn)
        //{
        //    if (!_IsConnect) return;
        //    try
        //    {
        //        int iRx = Client.Client.EndReceive(asyn);
        //        byte[] byteData = asyn.AsyncState as byte[];
        //        int i = byteData.Length - 1;

        //        while (byteData[i] == 0)
        //        {
        //            if (i <= 0)
        //            {
        //                break;
        //            }
        //            else
        //            {
        //                --i;
        //            }
        //        }

        //        byte[] Decode = new byte[i + 1];
        //        Array.Copy(byteData, Decode, i + 1);

        //        RXData = Encoding.Default.GetString(Decode);
        //        Log(_TcpModule.ToString(), RXData.ToString());

        //        RXData = RXData.Replace("\t", "").Replace("\r", "").Replace("\n", "");

        //        if (i > 0)
        //        {
        //            _IsReceive = true;
        //        }

        //        m_QueueSzData.Enqueue(RXData);

        //        WaitForData();
        //    }
        //    catch (ObjectDisposedException ex)
        //    {
        //        _IsConnect = false;
        //        //ConnectModule(m_ip, m_Port);
        //        Log(_TcpModule.ToString(), ex.ToString());
        //    }
        //    catch (SocketException ex)
        //    {
        //        _IsConnect = false;
        //        //ConnectModule(m_ip, m_Port);
        //        Log(_TcpModule.ToString(), ex.ToString());
        //    }
        //}


        //Old
        public void SendMsg(string msg)
        {
            Writer.WriteLine(msg);
            Log(_TcpModule.ToString(), $"Send Msg = {msg}");
            Writer.Flush();
        }

        public void OnDataReceived(IAsyncResult asyn)
        {
            if (!_IsConnect) return;

            try
            {
                int iRx = Client.Client.EndReceive(asyn);

                if (iRx == 0)
                {
                    // Connection closed by remote host
                    _IsConnect = false;
                    Log(_TcpModule.ToString(), "Connection closed by remote host.");
                    return;
                }

                byte[] byteData = asyn.AsyncState as byte[];

                // Decode received data
                byte[] decode = new byte[iRx];
                Array.Copy(byteData, decode, iRx);
                RXData = Encoding.Default.GetString(decode).TrimEnd('\0', '\r', '\n', '\t');

                if (!string.IsNullOrEmpty(RXData))
                {
                    // Process received data
                    Log(_TcpModule.ToString(), RXData);
                    m_QueueSzData.Enqueue(RXData);
                    _IsReceive = true;
                }

                // Continue waiting for data
                WaitForData();
            }
            catch (ObjectDisposedException ex)
            {
                // Log the exception, but avoid unnecessary reconnection attempts
                Log(_TcpModule.ToString(), ex.ToString());
            }
            catch (SocketException ex)
            {
                // Log the exception, but avoid unnecessary reconnection attempts
                Log(_TcpModule.ToString(), ex.ToString());
            }
        }

        //public void SendMsg(string msg)
        //{
        //    try
        //    {
        //        if (Writer == null)
        //        {
        //            Log(_TcpModule.ToString(), "Error: StreamWriter is null. Message not sent.");
        //            return;
        //        }

        //        if (!Client.Connected)
        //        {
        //            Log(_TcpModule.ToString(), "Error: TcpClient is not connected. Message not sent.");
        //            return;
        //        }

        //        Writer.WriteLine(msg);
        //        Writer.Flush();
        //        Log(_TcpModule.ToString(), $"Send Msg = {msg}");
        //    }
        //    catch (IOException ex)
        //    {
        //        // Handle IOException (e.g., connection closed unexpectedly)
        //        Log(_TcpModule.ToString(), $"IOException occurred while sending message: {ex.Message}");
        //    }
        //    catch (ObjectDisposedException ex)
        //    {
        //        // Handle ObjectDisposedException (e.g., TcpClient or StreamWriter disposed)
        //        Log(_TcpModule.ToString(), $"ObjectDisposedException occurred while sending message: {ex.Message}");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle other exceptions
        //        Log(_TcpModule.ToString(), $"Error occurred while sending message: {ex.Message}");
        //    }
        //}

        private bool Disconnect()
        {
            if (!_IsConnect) return false;

            _IsConnect = false;
            Client.Close();

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
            var getenum = _enumcommand.FirstOrDefault(x => x == enumcommand);
            string strcommand = CommonFunc.GetEnumDescription(getenum);

            SendMsg(strcommand);
        }

        void IClient.SendCustomizedCommand(string strcommand)
        {
            SendMsg(strcommand);
        }

        void IClient.AddCommand(Command commands)
        {
            _enumcommand.Add(commands);
        }

        internal protected void Log(string module, string msg)
        {
            try
            {
                //lock (m_SyncLog)
                //{
                //    string logFilePath = @"C:\Machine\Tcpip\" + module + ".ini";
                //    if (!File.Exists(logFilePath))
                //    {
                //        // If the file does not exist, create a new file
                //        StreamWriter sw = File.CreateText(logFilePath);

                //        sw.WriteLine($"Log file created on {DateTime.Now.ToString("HH:mm:ss:ffff")}");

                //    }

                //    // Append the log entry to the file
                //    StreamWriter sw1 = File.AppendText(logFilePath);

                //    sw1.WriteLine($"{DateTime.Now.ToString("HH:mm:ss:ffff")}: Module: {module}: {msg}");

                //}
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
    }
}
