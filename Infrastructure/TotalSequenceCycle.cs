using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public enum Stn0
    {
        //Stn0
        Stn0_LoadProduct,
        MaxModule,
    }

    public enum Barcode
    {
        //OCRReader
        OCR_StartProcess,
        //KeyenceTop
        KeyTop_StartProcess,
        //KeyenceBtm
        KeyBtm_StartProcess,
        MaxModule,
    }

    public enum Laser
    {
        //LaserTop
        LaserTop_StartLaserWCompensation,
        //LaserBtm
        LaserBtm_StartLaserWCompensation,
        MaxModule,
    }

    public enum Reject
    {
        //Reject
        RejectSequence,
        MaxModule,
    }

    public enum UnloadStn
    {
        //UnloadingStn
        UnloadingStn_UnloadProcess,

        MaxModule,
    }

    public enum Robot
    {
        //Robot
        Robot_MoveHomePos,
        Robot_GripperOpen,
        Robot_GripperClose,
        Robot_MoveUnloadRotarySafePos,
        Robot_MovePickUnloadRotaryItem,
        Robot_MoveStrappingMcSafePos,
        Robot_GripperOpenMoveStrappingMcSafePos,
        Robot_MoveStrappingMcGripperPos,
        Robot_MovePickStrappingMcUnit,
        Robot_MoveUnloadConveyorSafePos,
        Robot_GripperOpenMoveUnloadConvSafePos,
        Robot_MovePlaceUnitToUnloadConv,
        StrappingMc_GripperOpenExt,
        StrappingMc_GripperCloseRet,
        StrappingMc_TriggerStart,

        //UnloadConv
        UnloadConv_TransferUnitToHeatShrinkMc,


        MaxModule,
    }
    public enum Pnp
    {
        //Pnp_Left
        PnpLeft_PickMagazinePos,
        PnpLeft_PickMagazine,
        PnpLeft_GoToLoad,
        PnpLeft_UnloadToConvOut,
        //Pnp_Right

        PnpRight_PickMagazinePos,
        PnpRight_PickMagazine,
        PnpRight_GoToLoad,
        PnpRight_UnloadToConvOut,

        MaxModule,
    }

}
