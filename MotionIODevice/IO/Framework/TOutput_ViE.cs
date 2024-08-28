using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotionIODevice.IO
{
    public class TOutput_ViE
    {
        private ushort w_boardID;
        private int i_module = -1;
        private int i_bit;
        private int i_axisPort;
        private ushort w_moduleID;
        private ushort w_mask;
        private string s_label;
        private string s_name;
        private string s_disname;
        private bool b_status;
        public ushort BoardID { get { return w_boardID; } set { w_boardID = value; } }
        public int Module { get { return i_module; } set { i_module = value; } }
        public int Bit { get { return i_bit; } set { i_bit = value; } }
        public int AxisPort { get { return i_axisPort; } set { i_axisPort = value; } }
        public ushort ModuleID { get { return w_moduleID; } set { w_moduleID = value; } }
        public ushort Mask { get { return w_mask; } set { w_mask = value; } }
        public string Label { get { return s_label; } set { s_label = value; } }
        public string Name { get { return s_name; } set { s_name = value; } }
        public string DisName { get { return s_disname; } set { s_disname = value; } }
        public bool Status { get { return b_status; } set { b_status = value; } }

        public TOutput_ViE(int Module, ushort BoardID, int AxisPort, ushort ModuleID, int Bit, string Label, string Name, ushort Mask = 0, string DisplayName = null)
        {
            this.Module = Module;
            this.BoardID = BoardID;
            this.AxisPort = AxisPort;
            this.ModuleID = ModuleID;
            this.Bit = Bit;
            this.Label = Label;
            this.Name = Name;
            this.Mask = Mask;
            this.DisName = DisplayName;
            this.Status = false;
        }
    }
}
