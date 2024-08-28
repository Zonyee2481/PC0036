using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotionIODevice.IO
{
    public class TOutput
    {
        private ushort w_boardID;
        private ushort w_axisPort;
        private ushort w_mask;
        private string s_label;
        private string s_name;
        private string s_disname;
        private bool b_status;
        private int w_bit;
        private int i_module = -1;


        public int Module { get { return i_module; } set { i_module = value; } }
        public ushort BoardID { get { return w_boardID; } set { w_boardID = value; } }

        public int Bit { get { return w_bit; } set { w_bit = value; } }
        public ushort AxisPort { get { return w_axisPort; } set { w_axisPort = value; } }
        public ushort Mask { get { return w_mask; } set { w_mask = value; } }
        public string Label { get { return s_label; } set { s_label = value; } }
        public string Name { get { return s_name; } set { s_name = value; } }
        public string DisName { get { return s_disname; } set { s_disname = value; } }
        public bool Status { get { return b_status; } set { b_status = value; } }

        public TOutput(int Module, ushort BoardID, int Bit, byte Axis_Port, ushort Mask, string Label, string Name, string Displayname = null)
        {
            /// <param name="Bit"></param>
            /// <param name="Axis_Port"></param>
            /// <param name="Mask"></param>
            /// <summary>
            /// ViE iD3802-00:
            /// Bit             = IO Port
            /// Axis_Port       = IO Board ID
            /// Mask            = IO Module ID
            /// 0x00 ~ 0x09     = 0 ~ 9
            /// 0x0A ~0x0F      = 10 ~15
            /// 0x10 ~0x19      = 16 ~25
            /// 0x1A ~0x1F      = 26 ~31
            /// 0x20 ~0x29      = 32 ~41
            /// 0x2A ~0x2F      = 42 ~47
            /// 0x30 ~0x39      = 48 ~57
            /// 0x3A ~0x3F      = 58 ~63
            /// Example         : 
            /// IN_1 = 0x00, IN_2 = 0x01, IN_3 = 0x02, ... IN_9 = 0x09
            /// OUT_1 = 0x0A, OUT_2 = 0x0B, ... OUT_6 = 0x0F
            /// </summary>

            this.BoardID = BoardID;
            this.AxisPort = Axis_Port;
            this.Mask = Mask;
            this.Label = Label;
            this.Name = Name;
            this.DisName = Displayname;
            this.Status = false;
            this.Bit = Bit;
            this.Module = Module;
        }

        public TOutput(ushort BoardID, int Bit, byte Axis_Port, ushort Mask, string Label, string Name, string Displayname = null)
        {
            this.BoardID = BoardID;
            this.AxisPort = Axis_Port;
            this.Mask = Mask;
            this.Label = Label;
            this.Name = Name;
            this.DisName = Displayname;
            this.Status = false;
            this.Bit = Bit;
        }
        public TOutput(byte Axis_Port, ushort Mask, string Label, string Name)
        {
            this.AxisPort = Axis_Port;
            this.Mask = Mask;
            this.Label = Label;
            this.Name = Name;
            this.Status = false;
        }
        /// <param name="Device"></param>
        /// <param name="Axis_Port"></param>
        /// <param name="Mask"></param>
        public TOutput(byte Axis_Port, ushort Mask)
        {
            this.AxisPort = Axis_Port;
            this.Mask = Mask;
            this.Label = "";
            this.Name = "";
            this.Status = false;
        }
    }
}
