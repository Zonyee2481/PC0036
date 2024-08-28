using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeqServer
{
    public enum EV_TYPE
    {
        Restart,
        BeginSeq,
        InitSeq,
        FarProcReady,
        FarProcComp,
        FarProcComp2,
        FarProcBusy,
        FarProcAbort,
        FarProcSkip,
        FarProcStart,
        FarProcCont,
        FarEOLProcComp,
        MCResumeSeq,
        SSRSeq,
        FarBypassStation,
        InitDone,
        EndLotReq,
        HouseKeeping,
        ReqNewItem,
        NewItemAvail,
        ItemTaken,
        ItemGiven,
        SeqIntLChk,
        SeqIntLComp,
        BeginInitClrUnit,          // Clear turn table unit 
        BeginInitClrUnit2,         // Clear Shuttle 1 unit 
        BeginInitClrUnit3,         // Clear Shuttle 2 unit 
        StartClrUnitSeq,
        ClrUnitSeqComp,
        RuntimeFault,
        RuntimeSecure,
        ChkUnitPsn,
        ChkDoubleUnit,
        IndexComp,
        ChkUnitPsnComp,
        ChkDoubleUnitComp,
        ExtTestRunBegin, // -->
        ExtTestRunAbort, // -->
        ExtTestRunComp, // <--
        ExtTestRunErr,  // <--
        EndReelReq,
        EndReelStart,
        EndReelComp,
        EndGRRReq,
        FetchNextNewUnit,
        SpecialEOLEnd,
        MCStopReq,
        RbtStopReq,
        Kill,
        TriggerConvIn,
        TriggerConvOut,
        InitRobot,
        StartRobot,
        ManualTriggerRobot,

        //Interlock Use -- JX 
        PnPTrafficIntlChk,
        PnPTrafficIntlComp,
        DonePnpInterLChk,
        DonePnpInterLComp,
        ConvIntLChk,
        ConvIntLComp,

        //Additional Bit
        WorkReq,
        ItemOut,
        ItemOutApprove,
        ItemReceived,
        RetryReq,
        ManualSeq,
        Handshake1,
        Handshake1Done,
    }
}
