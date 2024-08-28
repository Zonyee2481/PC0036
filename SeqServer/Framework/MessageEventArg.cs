using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeqServer
{
    [Serializable]
    public class MessageEventArg : EventArgs
    {
        public MessageEventArg()
        {
        }

        // Shared property do not require any thread synchronization
        // because each of them appear as a separate instance in its own thread
        public string StationName
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public Module ModuleSelected { get; set; }

        public TrafficResult Traffic_Result { get; set; }

        public eMcState MachineStatus { get; set; }

        public DateTime RecipeDateTime { get; set; }

        public DialogResult dialogResult { get; set; }


    }

   
}
