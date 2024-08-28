using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MotionIODevice.IO
{
    public enum TModuleType { Input, Output };

    public class TModuleIO
    {
        private ushort w_boardID;
        private ushort w_moduleID;
        private string s_label;
        private string s_name;
        private TModuleType t_moduleType;
        private bool b_enabled = false;

        public ushort BoardID { get { return w_boardID; } set { w_boardID = value; } }
        public ushort ModuleID { get { return w_moduleID; } set { w_moduleID = value; } }
        public string Label { get { return s_label; } set { s_label = value; } }
        public string Name { get { return s_name; } set { s_name = value; } }
        public TModuleType ModuleType { get { return t_moduleType; } set { t_moduleType = value; } }
        public bool Enabled { get { return b_enabled; } set { b_enabled = value; } }

        public TModuleIO(ushort BoardID, ushort ModuleID, string Label, string Name, TModuleType ModuleType)
        {
            this.BoardID = BoardID;
            this.ModuleID = ModuleID;
            this.Label = Label;
            this.Name = Name;
            this.ModuleType = ModuleType;
            this.Enabled = false;
        }
    }    
}
