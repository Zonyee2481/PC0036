using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public struct tagSeqFlag
    {
        public bool Dryrun;
        public bool IsManualMode;
        public bool IsAutoMode;
        public bool StartManualMode;
        public double VacOnInterval;
        public double VacOffInterval;
        public double CylTimeOut;
        public int SettlingiTime;
        public int CylBuffer;
        public bool FlushUnitOut;
        public int InProdCount;
        public int OutProdCount;
        
        //internal bool RTScanErr;
        //internal bool RTScanOnceOK;
        //internal bool SeqRun;
        //internal bool LMShuttle1Byp;
        //internal bool LMShuttle2Byp;
        //internal bool IsLMBusy;

        //internal int DefaultKeySwitch;
    }
}
