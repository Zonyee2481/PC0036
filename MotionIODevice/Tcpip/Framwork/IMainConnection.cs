using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotionIODevice
{
    public interface IMainConnection
    {
        Task BuildAllConnectionAsync(List<IPPort> ip);

        void BuildSingleConnection(TcpModuleID id, string ipAddress, string port);

        void DcSingleConnection(TcpModuleID id);

        bool ModuleIsReceived(TcpModuleID id);
        string ModuleFeedBack(TcpModuleID id);

        void SendModuleCommand(TcpModuleID id, Command enumcommand);

        void SendCustomCommand(TcpModuleID id, string command);

        bool ModuleIsConnect(TcpModuleID id);

    }
}
