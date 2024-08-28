using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeqServer.Utilities.ModEventArg
{
    [Serializable]
    public class CModEventArg : EventArgs
    {
        internal CModEventArg()
        {
        }

        internal bool Proc_Comp
        {
            set;
            get;
        }

        internal bool Proc_Ready
        {
            set;
            get;
        }

        internal bool Proc_Busy
        {
            set;
            get;
        }

        internal bool Proc_Abort
        {
            set;
            get;
        }

        internal bool Proc_Skip
        {
            set;
            get;
        }

        internal bool Init_Success
        {
            set;
            get;
        }        

        internal bool SeqIntL_Ok
        {
            set;
            get;
        }

        internal bool InitFail_ForceEOS
        {
            set;
            get;
        }

        internal int Seq_ID
        {
            set;
            get;
        }       

        internal int Subscriber_ID
        {
            set;
            get;
        }

        internal string SeqName
        {
            get;
            set;
        }        
    }
}
