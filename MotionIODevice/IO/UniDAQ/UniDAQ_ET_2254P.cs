using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotionIODevice.IO
{
    public class UniDAQ_ET_2254P : IBoardIO
    {
        private List<IBoardIO> ioBoards;
        private List<TInput> inputList = new List<TInput>();
        private List<TOutput> outputList = new List<TOutput>();
        private ushort uBoardNo;
        private byte bytSlaveAddr;
        private string sIPAddress = string.Empty;
        private int iPort = 0;
        private bool isBoardOpened = false;
        #region Modbus TCPIP
        TcpClient tcpClient;
        ModbusIpMaster master;
        #endregion

        public bool IsBoardOpened
        {
            get { return isBoardOpened; }
            set { isBoardOpened = value; }
        }

        public string IPAddress
        {
            get { return sIPAddress; }
            set { sIPAddress = value; }
        }

        public int Port
        {
            get { return iPort; }
            set { iPort = value; }
        }

        void BuildIOCard()
        {
            ioBoards = new List<IBoardIO>() { };
            BoardConfig();
        }

        void BoardConfig()
        {
            ioBoards.Clear();

            ioBoards.Add(new UniDAQ_ET_2254P(0, "192.168.1.11", 502));
            //ioBoards.Add(new UniDAQ_ET_2254P(1, "192.168.1.12", 502));
        }

        public List<TInput> GetInputList
        {
            get { return inputList; }
        }

        public List<TOutput> GetOutputList
        {
            get { return outputList; }
        }

        public void UpdateInputInfo(int bit, TInput input)
        {
            inputList[bit] = input;
        }

        public void UpdateOutInfo(int bit, TOutput output)
        {
            outputList[bit] = output;
        }

        public UniDAQ_ET_2254P(ushort shBoardNo, string IpAddress, int port)
        {
            uBoardNo = shBoardNo;
            int iSlaveAddr = (int)shBoardNo + 1;
            bytSlaveAddr = (byte)iSlaveAddr;
            sIPAddress = IpAddress;
            iPort = port;
            InitIO();
        }

        public void InitIO()
        {
            // Input
            inputList.Add(new TInput(uBoardNo, 0, 0, 0x00, "", "", ""));
            inputList.Add(new TInput(uBoardNo, 1, 0, 0x01, "", "", ""));
            inputList.Add(new TInput(uBoardNo, 2, 0, 0x02, "", "", ""));
            inputList.Add(new TInput(uBoardNo, 3, 0, 0x03, "", "", ""));
            inputList.Add(new TInput(uBoardNo, 4, 0, 0x04, "", "", ""));
            inputList.Add(new TInput(uBoardNo, 5, 0, 0x05, "", "", ""));
            inputList.Add(new TInput(uBoardNo, 6, 0, 0x06, "", "", ""));
            inputList.Add(new TInput(uBoardNo, 7, 0, 0x07, "", "", ""));

            outputList.Add(new TOutput(uBoardNo, 8, 0, 0x00, "", "", ""));
            outputList.Add(new TOutput(uBoardNo, 9, 0, 0x01, "", "", ""));
            outputList.Add(new TOutput(uBoardNo, 10, 0, 0x02, "", "", ""));
            outputList.Add(new TOutput(uBoardNo, 11, 0, 0x03, "", "", ""));
            outputList.Add(new TOutput(uBoardNo, 12, 0, 0x04, "", "", ""));
            outputList.Add(new TOutput(uBoardNo, 13, 0, 0x05, "", "", ""));
            outputList.Add(new TOutput(uBoardNo, 14, 0, 0x06, "", "", ""));
            outputList.Add(new TOutput(uBoardNo, 15, 0, 0x07, "", "", ""));
        }

        public bool UpdateInput(ref TInput Input)
        {
            if (!IsBoardOpened) { return false; }
            try
            {
                byte slaveAddr = bytSlaveAddr;
                bool[] status = master.ReadInputs(slaveAddr, 0, (ushort)inputList.Count);
                for (int i = 0; i < inputList.Count; i++)
                {
                    inputList[i].Status = status[i];
                }
            }
            catch (Exception ex)
            {
                if (ex.Source.Equals("System"))
                {
                    MessageBox.Show("Disconnected " + DateTime.Now.ToString());
                }
                if (ex.Source.Equals("nModbusPC"))
                {
                    string str = ex.Message;
                    int FunctionCode;
                    string ExceptionCode;

                    str = str.Remove(0, str.IndexOf("\r\n") + 17);
                    FunctionCode = Convert.ToInt16(str.Remove(str.IndexOf("\r\n")));

                    str = str.Remove(0, str.IndexOf("\r\n") + 17);
                    ExceptionCode = str.Remove(str.IndexOf("-"));

                    switch (ExceptionCode.Trim())
                    {
                        case "1":
                            {
                                MessageBox.Show("Exception Code: " + ExceptionCode.Trim() + "----> Illegal function!");
                            }
                            break;
                        case "2":
                            {
                                MessageBox.Show("Exception Code: " + ExceptionCode.Trim() + "----> Illegal data address!");
                            }
                            break;
                        case "3":
                            {
                                MessageBox.Show("Exception Code: " + ExceptionCode.Trim() + "----> Illegal data value!");
                            }
                            break;
                        case "4":
                            {
                                MessageBox.Show("Exception Code: " + ExceptionCode.Trim() + "----> Slave device failure!");
                            }
                            break;
                    }
                }
                return false;
            }
            return true;
        }

        public bool OpenBoard()
        {
            try
            {
                tcpClient = new TcpClient();
                IAsyncResult asyncResult = tcpClient.BeginConnect(sIPAddress, iPort, null, null);
                asyncResult.AsyncWaitHandle.WaitOne(3000, true); //wait for 3 sec
                if (!asyncResult.IsCompleted)
                {
                    tcpClient.Close();
                    Console.WriteLine(DateTime.Now.ToString() + ":Cannot connect to server.");
                    IsBoardOpened = false;
                    return false;
                }
                master = ModbusIpMaster.CreateIp(tcpClient);
                master.Transport.Retries = 0;
                master.Transport.ReadTimeout = 1500;
                MessageBox.Show("Connected! " + DateTime.Now.ToString());
                IsBoardOpened = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CloseBoard()
        {
            tcpClient.Close();
            tcpClient.Dispose();
            IsBoardOpened = false;
            return true;
        }

        public bool ReadBit(int eBit)
        {
            int bit = eBit;
            if (!IsBoardOpened) { return false; }
            try
            {
                TInput data = inputList[bit];
                UpdateInput(ref data);
                return data.Status;
            }
            catch (Exception ex)
            { }
            return false;
        }

        public bool OutBit(int eBit, TOutputStatus state)
        {
            if (eBit >= 8) { eBit -= 8; }
            int bit = eBit;
            if (!IsBoardOpened) { return false; }
            TOutput data = outputList[bit];
            switch (state)
            {
                case TOutputStatus.Hi:
                    {
                        UpdateOutputHi(ref data);
                    }                    
                    break;

                case TOutputStatus.Lo:
                    {
                        UpdateOutputLo(ref data);
                    }                    
                    break;
            }
            return data.Status;
        }

        public bool UpdateOutputHi(ref TOutput Output)
        {
            if (!IsBoardOpened) { return false; }
           
            try
            {
                ushort index = (ushort)Output.Bit;
                master.WriteSingleCoil(bytSlaveAddr, index, true);
                Output.Status = true;
            }
            catch
            {
                return false;
            }

            return true;
        }
        public bool UpdateOutputLo(ref TOutput Output)
        {
            if (!IsBoardOpened) { return false; }

            try
            {
                ushort index = (ushort)Output.Bit;
                master.WriteSingleCoil(bytSlaveAddr, index, false);
                Output.Status = false;
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void UpdateViEInputInfo(int bit, TInput_ViE input)
        {
            throw new NotImplementedException();
        }

        public void UpdateViEOutInfo(int bit, TOutput_ViE output)
        {
            throw new NotImplementedException();
        }
    }
}
