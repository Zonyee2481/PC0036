using Infrastructure;
using KAEventPool;
using MotionIODevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MotionIODevice.Common;

namespace SeqServer
{
    public interface IMainSeq
    {
        void BuildSequence();
        void BuildData();

        List<IMotionControl> SetMotionBoardList { set; }

        List<IBoardIO> SetIOBoardInputList { set; }

        IMainConnection SetMainConnection { set; }

        BaseFunc[] GetSeqModule { get; }

        EventPool SetEventPool_UI2Seq { set; }

        void RecipeInfo(LotInfo lotinfo);

        void EnableStation(int station, bool enabel);

        tagSeqFlag MachineConfig { set; }

        List<string> GetSeqNum();

        List<BitCodeInfo> BitCode { get; set; }

        LotCounter LotCounter { get; set; }

        List<CycletimeInfo> CycleTime(int station);

        List<string> UnloadProdList { get; set; }

        int GetProdCntNum(int station);

        void ResetProdCntNum(int station);

        double GetCycleTime(int station);

        List<FixZone> FixZoneData { get; set; }

        void SaveFixZoneData();

        List<UnloadStation> UnloadStationData { get; set; }
    }
}
