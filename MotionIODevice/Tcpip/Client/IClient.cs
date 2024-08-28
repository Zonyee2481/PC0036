using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotionIODevice.Tcpip
{
    public interface IClient
    {
        void ConnectModule(string IP, string Port);
        void AddCommand(Command commands);
        bool DisconnectToHost();
        bool IsConnect();
        bool IsReceive();
        void ResetReceive();
        void SendCommand(Command enumcommand);

        void SendCustomizedCommand(string command);

        string GetMessage();
    }
}
