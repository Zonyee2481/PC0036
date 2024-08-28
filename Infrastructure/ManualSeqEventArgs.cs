using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    [Serializable]
    public class ManualSeqEventArgs : EventArgs
    {
        public ManualSeqEventArgs()
        {
        }

        public string StationName
        {
            get;
            set;
        }

        public string SeqName
        {
            get;
            set;
        }   

        public DateTime RecipeDateTime { get; set; }

    }


}
