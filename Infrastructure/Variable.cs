using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Veloctiy
    {
        public double PositionValue = 0;
        public double Speed = 0;
        public double Acc = 0;
        public double Dcc = 0;
    }

    public class CycleTime
    {
        public List<CycletimeInfo> BreakDowncycleTime;
        public CycleTime ()
        {
            BreakDowncycleTime = new List<CycletimeInfo>();
        }
    }

    public class CycletimeInfo
    {
        public string Name;
        public int Time;
    }

    public class IPPort
    {
        public string IpAdress;
        public string Port;
    }

    public class StationCfg
    {
        public string StationName;
        public int SeqID;
        public int NestID;
        public bool IsBypass;
        public int SeqOrder;
        public int SeqNext;
        public int SeqPrev;
    }

    public class StationData
    {
        public bool IsBypass;
        public bool IsPsnt;
        public UnitStatus UnitStatus;
    }

    public class ErrData
    {
        public string Station;
        public string EnumErr;
        public string Definition;
        public string Action;
    }

    public class FixZone
    {
        public int Nest_ID;
        public bool IsBypass;
        public Unit Unit;
    }

    public class Unit
    {
        public bool IsPsnt;
        public UnitStatus UnitStatus;
        public int LotCounter;
        public string GeneratedBarcode;
        public double[] NestHeightOffset = new double[(int)TotalLaser.TotalLaser];
        public string[] MarkingOffset = new string[(int)TotalLaser.TotalLaser];
    }

    public class UnloadStation
    {
        public bool IsPsnt = false;
        public int Counter = 0;
        public int Count = 0;
    }


    public class NestReadTxtData
    {
        public bool IsBypass;
        public Unit Unit;
    }
    public enum UnitStatus
    {
        Empty,
        New,
        Pass,
        OcrFail,
        OCRMisMatch,
        LaserFailTop,
        LaserFailBtm,
        KeyenceFailTop,
        KeyenceFailBtm,
        KeyenceBarcodeMisMatch,
        StrappingDone,

        NestBypass,
    }
    
    public enum HomingType
    {
        NPHType,
        Without_NP,
        LimitType,
    }    

    public enum CardNumber
    {
        None = -1,
        Advantech_1,
        Advantech_2,
        eSCLLibHelper,
        Galil_1,
    }

    public enum SpeedSelection
    {
        Slow,
        Fast,
        Med,
    }

    public enum SwitchType
    {
        DirectCommand,
        TellSwitch,
    }

    public class BitCodeInfo
    {
        public int Index;
        public bool BitCode_1;
        public bool BitCode_2;
        public bool BitCode_4;
        public bool BitCode_8;
        public string Description;

        public BitCodeInfo(int index, bool bitCode_1, bool bitCode_2, bool bitCode_4, bool bitCode_8, string description)
        {
            this.Index = index;
            this.BitCode_1 = bitCode_1;
            this.BitCode_2 = bitCode_2;
            this.BitCode_4 = bitCode_4;
            this.BitCode_8 = bitCode_8;
            this.Description = description;
        }
    }
}
