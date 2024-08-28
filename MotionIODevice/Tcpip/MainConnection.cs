using Infrastructure;
using MotionIODevice.Tcpip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MotionIODevice
{
    public class MainConnection : IMainConnection
    {
        private List<IClient> TotalClients;

        async Task IMainConnection.BuildAllConnectionAsync(List<IPPort> ip)
        {
            var tasks = new List<Task<IClient>>();
            //Laser Top
            tasks.Add(CreateClientAsync(TcpModuleID.LaserTop, ip[0].IpAdress, ip[0].Port, TimeSpan.FromSeconds(3)));
            //Laser Btm
            tasks.Add(CreateClientAsync(TcpModuleID.LaserBtm, ip[1].IpAdress, ip[1].Port, TimeSpan.FromSeconds(3)));
            //Cognex OCR
            tasks.Add(CreateClientAsync2(TcpModuleID.CognexOCR, ip[2].IpAdress, ip[2].Port, TimeSpan.FromSeconds(3)));
            //Cognex Server
            tasks.Add(CreateClientAsync2(TcpModuleID.CognexServer, ip[3].IpAdress, ip[3].Port, TimeSpan.FromSeconds(3)));
            //Keyence Top
            tasks.Add(CreateClientAsync(TcpModuleID.KeyenceTop, ip[4].IpAdress, ip[4].Port, TimeSpan.FromSeconds(3)));
            //Keyence Btm
            tasks.Add(CreateClientAsync(TcpModuleID.KeyenceBtm, ip[5].IpAdress, ip[5].Port, TimeSpan.FromSeconds(3)));

            TotalClients = (await Task.WhenAll(tasks)).ToList();
            BuildCommand();
        }
       
        private void BuildCommand()
        {
            TotalClients[(int)TcpModuleID.LaserTop].AddCommand(Command.CheckStatus);
            TotalClients[(int)TcpModuleID.LaserTop].AddCommand(Command.ChangeModel); 
            TotalClients[(int)TcpModuleID.LaserTop].AddCommand(Command.ChangeFile_DDrive);
            TotalClients[(int)TcpModuleID.LaserTop].AddCommand(Command.Mark);

            TotalClients[(int)TcpModuleID.LaserBtm].AddCommand(Command.CheckStatus);
            TotalClients[(int)TcpModuleID.LaserBtm].AddCommand(Command.ChangeModel);
            TotalClients[(int)TcpModuleID.LaserBtm].AddCommand(Command.ChangeFile_DDrive);
            TotalClients[(int)TcpModuleID.LaserBtm].AddCommand(Command.Mark);

            TotalClients[(int)TcpModuleID.CognexOCR].AddCommand(Command.TriggerAutomode);

            TotalClients[(int)TcpModuleID.CognexServer].AddCommand(Command.Login);
            TotalClients[(int)TcpModuleID.CognexServer].AddCommand(Command.Password);
            TotalClients[(int)TcpModuleID.CognexServer].AddCommand(Command.LoadBigCR);
            TotalClients[(int)TcpModuleID.CognexServer].AddCommand(Command.LoadSmallCR);
            TotalClients[(int)TcpModuleID.CognexServer].AddCommand(Command.SetOnline);
            TotalClients[(int)TcpModuleID.CognexServer].AddCommand(Command.SetOffLine);

            TotalClients[(int)TcpModuleID.KeyenceTop].AddCommand(Command.TriggerSnap);
            TotalClients[(int)TcpModuleID.KeyenceBtm].AddCommand(Command.TriggerSnap);
        }
    

        void IMainConnection.BuildSingleConnection(TcpModuleID id, string ipAddress, string port)
        {
            TotalClients[(int)id].ConnectModule(ipAddress, port);
        }

        void IMainConnection.DcSingleConnection(TcpModuleID id)
        {
            TotalClients[(int)id].DisconnectToHost();
        }

        bool IMainConnection.ModuleIsReceived(TcpModuleID id)
        {
            return TotalClients[(int)id].IsReceive();
        }

        string IMainConnection.ModuleFeedBack(TcpModuleID id)
        {
            return TotalClients[(int)id].GetMessage();
        }

        void IMainConnection.SendModuleCommand(TcpModuleID id, Command enumcommand)
        {
            TotalClients[(int)id].SendCommand(enumcommand);
        }

        void IMainConnection.SendCustomCommand(TcpModuleID id, string command)
        {
            TotalClients[(int)id].SendCustomizedCommand(command);
        }

        bool IMainConnection.ModuleIsConnect(TcpModuleID id)
        {
            if(TotalClients!= null)
            {
                return TotalClients[(int)id].IsConnect();
            }
            else
            {
                return false;
            }          
        }
        
        private async Task<IClient> CreateClientAsync(TcpModuleID id, string ipAddress, string port, TimeSpan timeout)
        {
            var cts = new CancellationTokenSource(timeout);


            return await Task.Run(async () =>
            {
                //return new Client(id, ipAddress, port);
                return new New_Tcpip(id, ipAddress, port);
            });


        }

        private async Task<IClient> CreateClientAsync2(TcpModuleID id, string ipAddress, string port, TimeSpan timeout)
        {
            var cts = new CancellationTokenSource(timeout);


            return await Task.Run(async () =>
            {
                return new Client(id, ipAddress, port);
                //return new New_Tcpip(id, ipAddress, port);
            });


        }
    }
}
