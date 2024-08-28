using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MotionIODevice.Tcpip
{
    public class Common
    {
        public CSocketPacket thissocket;
        //public Socket ClientSocket;
        public IPAddress IpAddress;
        public IPEndPoint EndPoint;
        //public byte[] dataBuffer = new byte[4096];
    }
    public class CSocketPacket
    {
        public System.Net.Sockets.Socket thisSocket;
        public byte[] dataBuffer = new byte[1];
    }


}
