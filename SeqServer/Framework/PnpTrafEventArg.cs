using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeqServer
{
    [Serializable]
    public class PnpTrafEventArg : EventArgs
    {
        public int Seq_ID
        {
            get;
            set;
        }

        public int Target_ID
        {
            get;
            set;
        }
        public Module ModuleSelected { get; set; }

        public TrafficResult Traffice_Result
        {
            get;
            set;
        }        

        public DateTime RecipeDateTime { get; set; }

    }
    public enum Module
    {
        InPnp_Left = 3,
        InPnp_Right = 6,
    }

    public enum TrafficResult
    {
        Proceed,
        Wait,
        Alarm,
    }
}


