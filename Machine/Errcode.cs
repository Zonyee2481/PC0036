using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine
{
  class Errcode
  {
    public static string

        //Motor Alarm Error
        Err_IS1MtrAlm = "ERR CODE" + (char)9 + "[0101] " + (char)10 + "Input Singulate 1 Axis Alarm. Please Check Motor Driver Status",
        Err_IS2MtrAlm = "ERR CODE" + (char)9 + "[0102] " + (char)10 + "Input Singulate 2 Axis Alarm. Please Check Motor Driver Status",
        Err_ILZMtrAlm = "ERR CODE" + (char)9 + "[0103] " + (char)10 + "Input Laser Z Axis  Alarm. Please Check Motor Driver Status",
        Err_OS1MtrAlm = "ERR CODE" + (char)9 + "[0104] " + (char)10 + "Ouput Singulate 1 Axis  Alarm. Please Check Motor Driver Status",
        Err_OS2MtrAlm = "ERR CODE" + (char)9 + "[0105] " + (char)10 + "Ouput Singulate 2 Axis  Alarm. Please Check Motor Driver Status",
        Err_OLZMtrAlm = "ERR CODE" + (char)9 + "[0106] " + (char)10 + "Output Laser Z Axis  Alarm. Please Check Motor Driver Status",
        Err_URMtrAlm = "ERR CODE" + (char)9 + "[0107] " + (char)10 + "Unload Rotary Axis  Alarm. Please Check Motor Driver Status",

        Err_IS1HomeTimeout = "ERR CODE" + (char)9 + "[0204]" + (char)10 + "Input Singulate 1 Axis- Home/Initialize Timeout!" + (char)13 + "Please Check Motor Driver Status!",
        Err_IS2HomeTimeout = "ERR CODE" + (char)9 + "[0205]" + (char)10 + "Input Singulate 2 Axis- Home/Initialize Timeout!" + (char)13 + "Please Check Motor Driver Status!",
        Err_ILZHomeTimeout = "ERR CODE" + (char)9 + "[0206]" + (char)10 + "Input Laser Z Axis- Home/Initialize Timeout!" + (char)13 + "Please Check Motor Driver Status!",
        Err_OS1HomeTimeout = "ERR CODE" + (char)9 + "[0207]" + (char)10 + "Output Singulate 1 Axis- Home/Initialize Timeout!" + (char)13 + "Please Check Motor Driver Status!",
        Err_OS2HomeTimeout = "ERR CODE" + (char)9 + "[0208]" + (char)10 + "Output Singulate 2 Axis- Home/Initialize Timeout!" + (char)13 + "Please Check Motor Driver Status!",
        Err_OLZHomeTimeout = "ERR CODE" + (char)9 + "[0209]" + (char)10 + "Output Laser Z Axis - Home/Initialize Timeout!" + (char)13 + "Please Check Motor Driver Status!",
        Err_URHomeTimeout = "ERR CODE" + (char)9 + "[0210]" + (char)10 + "Unload Rotary Axis - Home/Initialize Timeout!" + (char)13 + "Please Check Motor Driver Status!",

        //Module/Move Safe Check
        Err_InputLeftConvInModuleNotReady = "ERR CODE" + (char)9 + "[0501]" + (char)10 + "Input Left Conveyorv - Module Not Ready!" + (char)13 + "Please Proceed System Initialize Before Move/Run.",
        Err_InputRightConvInModuleNotReady = "ERR CODE" + (char)9 + "[0502]" + (char)10 + "Input RIght Conveyorv - Module Not Ready!" + (char)13 + "Please Proceed System Initialize Before Move/Run.",
        Err_InputMagazineIndexModuleNotReady = "ERR CODE" + (char)9 + "[0503]" + (char)10 + "Input MagazineIndex - Module Not Ready!" + (char)13 + "Please Proceed System Initialize Before Move/Run.",
        Err_InputSingulate1ModuleNotReady = "ERR CODE" + (char)9 + "[0504]" + (char)10 + "Input Singulate 1 - Module Not Ready!" + (char)13 + "Please Proceed System Initialize Before Move/Run.",
        Err_InputSingulate2ModuleNotReady = "ERR CODE" + (char)9 + "[0505]" + (char)10 + "Input Singulate 2 - Module Not Ready!" + (char)13 + "Please Proceed System Initialize Before Move/Run.",
        Err_OutputSingulate1ModuleNotReady = "ERR CODE" + (char)9 + "[0506]" + (char)10 + "Output Singulate 1 - Module Not Ready!" + (char)13 + "Please Proceed System Initialize Before Move/Run.",
        Err_OutputSingulate2ModuleNotReady = "ERR CODE" + (char)9 + "[0507]" + (char)10 + "Output Singulate 2 - Module Not Ready!" + (char)13 + "Please Proceed System Initialize Before Move/Run.",
        Err_InputLeftFlipperModuleNotReady = "ERR CODE" + (char)9 + "[0508]" + (char)10 + "Input Left Flipper - Module Not Ready!" + (char)13 + "Please Proceed System Initialize Before Move/Run.",
        Err_OutputRightFlipperModuleNotReady = "ERR CODE" + (char)9 + "[0509]" + (char)10 + "Output Left Flipper - Module Not Ready!" + (char)13 + "Please Proceed System Initialize Before Move/Run.",
        Err_RejectPnpModuleNotReady = "ERR CODE" + (char)9 + "[0510]" + (char)10 + "Reject Pnp - Module Not Ready!" + (char)13 + "Please Proceed System Initialize Before Move/Run.",
        Err_UnloadRotaryModuleNotReady = "ERR CODE" + (char)9 + "[0511]" + (char)10 + "Unload Rotary - Module Not Ready!" + (char)13 + "Please Proceed System Initialize Before Move/Run.",
        Err_InputLaserModuleNotReady = "ERR CODE" + (char)9 + "[0512]" + (char)10 + "Input Laser - Module Not Ready!" + (char)13 + "Please Proceed System Initialize Before Move/Run.",
        Err_OutputLaserModuleNotReady = "ERR CODE" + (char)9 + "[0513]" + (char)10 + "Output Laser - Module Not Ready!" + (char)13 + "Please Proceed System Initialize Before Move/Run.",



        Err_InputSingulate1MainNest12DownNotSafe = "ERR CODE" + (char)9 + "[0600]" + (char)10 + "Input Singulate 1 & 2- Main Nest!" + (char)13 + "Detect Both Nest Up, Please manually move to safety position before move or initialize.",
        Err_InputSingulate1MainNest12UpNotSafe = "ERR CODE" + (char)9 + "[0601]" + (char)10 + "Input Singulate 1 & 2- Main Nest!" + (char)13 + "Detect Both Nest Down, Please manually move to safety position before move or initialize.",
        Err_OutputSingulate1MainNest12DownNotSafe = "ERR CODE" + (char)9 + "[0602]" + (char)10 + "Output Singulate 1 & 2- Main Nest!" + (char)13 + "Detect Both Nest Up, Please manually move to safety position before move or initialize.",
        Err_OutputSingulate1MainNest12UpNotSafe = "ERR CODE" + (char)9 + "[0603]" + (char)10 + "Output Singulate 1 & 2- Main Nest!" + (char)13 + "Detect Both Nest Down, Please manually move to safety position before move or initialize.",

        Err_InputSingulate1MainNest12UpDownNostatus = "ERR CODE" + (char)9 + "[0604]" + (char)10 + "Input Singulate 1 & 2- Main Nest!" + (char)13 + "No Detect Both Nest Status, Please manually move to safety position before move or initialize.",
        Err_OutputSingulate1MainNest12UpDownNostatus = "ERR CODE" + (char)9 + "[0605]" + (char)10 + "Output Singulate 1 & 2- Main Nest!" + (char)13 + "No Detect Both Nest Status, Please manually move to safety position before move or initialize.",

        //Laser
        Err_LaserInGetStatusTimeout = "ERR CODE" + (char)9 + "[5001] " + (char)10 + "Laser Input (Top) - Ready Timeout!" + (char)13 + "Laser Not Ready!",
        Err_LaserInChangeModelReceiveFail = "ERR CODE" + (char)9 + "[5002] " + (char)10 + "Laser Input (Top) - Laser Change Model Fail!" + (char)13 + "Please Make Sure Laser Program Model Have Been Created And Program File Path Selected Correct!",
        Err_LaserInMarkFail = "ERR CODE" + (char)9 + "[5003] " + (char)10 + "Laser Input (Top) - Laser Mark Fail!" + (char)13 + "Receive NG From Laser!",
        Err_LaserInNotReady = "ERR CODE" + (char)9 + "[5004] " + (char)10 + "Laser Input (Top) - Laser Not Ready!" + (char)13 + "Please Check Laser Status!",
        Err_LaserInChangeFileFail = "ERR CODE" + (char)9 + "[5005] " + (char)10 + "Laser Input (Top) - Laser Change File Fail!" + (char)13 + "Please Make Sure Laser Program File Have Been Created And Program File Path Selected Correct!",
        Err_LaserInDisconnected = "ERR CODE" + (char)9 + "[5006] " + (char)10 + "Laser Input (Top) - Laser Disconnected!" + (char)13 + "Please Check Laser Conveyordition!" + (char)13 + "Press Retry To Reconnect and Conveyortitnue Process Else Press Cancel To Stop Machine",
        Err_LaserInMarkTimeout = "ERR CODE" + (char)9 + "[5007] " + (char)10 + "Laser Input (Top) - Laser Mark Timeout!" + (char)13 + "Press Stop to Check the Laser and Laser Software Conveyordition!",

        Err_LaserOutGetStatusTimeout = "ERR CODE" + (char)9 + "[5011] " + (char)10 + "Laser Output (Bottom) - Ready Timeout!" + (char)13 + "Laser Not Ready!",
        Err_LaserOutChangeModelReceiveFail = "ERR CODE" + (char)9 + "[5012] " + (char)10 + "Laser Output (Bottom) - Laser Change Model Fail!" + (char)13 + "Please Make Sure Laser Program Model Have Been Created And Program File Path Selected Correct!",
        Err_LaserOutMarkFail = "ERR CODE" + (char)9 + "[5013] " + (char)10 + "Laser Output (Bottom) - Laser Mark Fail!" + (char)13 + "Receive NG From Laser!",
        Err_LaserOutNotReady = "ERR CODE" + (char)9 + "[5014] " + (char)10 + "Laser Output (Bottom) - Laser Not Ready!" + (char)13 + "Please Check Laser Status!",
        Err_LaserOutChangeFileFail = "ERR CODE" + (char)9 + "[5015] " + (char)10 + "Laser Output (Bottom) - Laser Change File Fail!" + (char)13 + "Please Make Sure Laser Program File Have Been Created And Program File Path Selected Correct!",
        Err_LaserOutDisconnected = "ERR CODE" + (char)9 + "[5016] " + (char)10 + "Laser Output (Bottom) - Laser Disconnected!" + (char)13 + "Please Check Laser Conveyordition!" + (char)13 + "Press Retry To Reconnect and Conveyortitnue Process Else Press Cancel To Stop Machine",
        Err_LaserOutMarkTimeout = "ERR CODE" + (char)9 + "[5017] " + (char)10 + "Laser Output (Bottom) - Laser Mark Timeout!" + (char)13 + "Press Stop to Check the Laser and Laser Software Conveyordition!",

        Err_VisionOCRFail = "ERR CODE" + (char)9 + "[6000] " + (char)10 + "Vision - Product OCR Fail!" + (char)13 + "Vision OCR Check Fail, Product Not Matched!",
        Err_OCRReadError = "ERR CODE" + (char)9 + "[6001] " + (char)10 + "OCR Camera- OCR Read Error!" + (char)13 + "Press Retry - To Read Again" + (char)13 + "Press Stop - To Stop.",

            Err_OCRReadTimeout = "ERR CODE" + (char)9 + "[6002] " + (char)10 + "OCR Camera- Read Timeout!" + (char)13 + "Press Ok To Manual Key In OCR." + (char)13 + "Press Stop - To Stop.",
            Err_OCRReceiveOKFail = "ERR CODE" + (char)9 + "[6003] " + (char)10 + "OCR Camera- Receive OK Fail!" + (char)13 + "Press Ok To Manual Key In OCR else Press Retry to Re-Inspect.",
            Err_KeyenceInReadNG = "ERR CODE" + (char)9 + "[6004] " + (char)10 + "Keyence In Camera- 2DID Read NG!" + (char)13 + "Press Stop To Stop The Machine Operation.",
            Err_KeyenceInReadError = "ERR CODE" + (char)9 + "[6011] " + (char)10 + "Keyence In Camera- 2DID Read Error!" + (char)13 + "Press Retry - To Read Again" + (char)13 + "Press OK - To Force Pass" + (char)13 + "Press Cancel - To Force Fail.",
            Err_KeyenceInReadTimeout = "ERR CODE" + (char)9 + "[6012] " + (char)10 + "Keyence In Camera- 2DID Read Timeout!" + (char)13 + "Press Ok To Manual Key In 2DID.",
            Err_KeyenceInReceiveOKFail = "ERR CODE" + (char)9 + "[6013] " + (char)10 + "Keyence In Camera- Receive OK Fail!" + (char)13 + "Press Ok To Manual Key In 2DID else Press Retry to Re-Inspect.",
            Err_KeyenceOutReadError = "ERR CODE" + (char)9 + "[6021] " + (char)10 + "Keyence Out Camera- 2DID Read Error!" + (char)13 + "Press Retry - To Read Again" + (char)13 + "Press OK - To Force Pass" + (char)13 + "Press Cancel - To Force Fail.",
            Err_KeyenceOutReadTimeout = "ERR CODE" + (char)9 + "[6022] " + (char)10 + "Keyence Out Camera- 2DID Read Timeout!" + (char)13 + "Press Ok To Manual Key In 2DID.",
            Err_KeyenceOutReadNG = "ERR CODE" + (char)9 + "[6023] " + (char)10 + "Keyence Out Camera- 2DID Read NG!" + (char)13 + "Press Stop To Stop The Machine Operation.",
            Err_KeyenceOutReceiveOKFail = "ERR CODE" + (char)9 + "[6024] " + (char)10 + "Keyence Out Camera- Receive OK Fail!" + (char)13 + "Press Ok To Manual Key In 2DID else Press Retry to Re-Inspect.",
            Err_VisionOrientationOCRFail = "ERR CODE" + (char)9 + "[6025] " + (char)10 + "Vision - Product Orientation OCR Fail!" + (char)13 + "Receive NG From Vision!",
            Err_OCRDirectFailCounter = "ERR CODE" + (char)9 + "[6026] " + (char)10 + "OCR Camera- Direct Fail Counter 5 Times Detected!" + (char)13 + "Please Check the Product Load Is Correct Part Number.",
            Err_In2DIDDirectFailCounter = "ERR CODE" + (char)9 + "[6027] " + (char)10 + "Keyence In Camera- Direct Fail Counter 5 Times Detected!" + (char)13 + "Please Check the Input Marking Quality and Marking Position Condition.",
            Err_Out2DIDDirectFailCounter = "ERR CODE" + (char)9 + "[6028] " + (char)10 + "Keyence Out Camera- Direct Fail Counter 5 Times Detected!" + (char)13 + "Please Check the Input Marking Quality and Marking Position Condition.",

            Err_OCRReceiveNG = "ERR CODE" + (char)9 + "[6029] " + (char)10 + "OCR Camera- Receive NG!" + (char)13 + "Wrong Product Load. Press Stop to Stop the Machine and Check The Product.",

    #region 1000 Left Magazine
                Err_SensLeftLoadNestLifterDownFail = "ERR CODE" + (char)9 + "[1000] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                        "Left Load Nest Lifter Failed To Down!",
                Err_SensLeftLoadNestLifterUpFail = "ERR CODE" + (char)9 + "[1001] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                        "Left Load Nest Lifter Failed To Up!",
                Err_SensLeftLoadMagStopperRetFail = "ERR CODE" + (char)9 + "[1002] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                        "Left Load Magazine Stopper Failed To Retract!",
                Err_SensLeftLoadMagStopperExtFail = "ERR CODE" + (char)9 + "[1003] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                        "Left Load Magazine Stopper Failed To Extend!",
                Err_SensLeftLoadMagStanbyMissing = "ERR CODE" + (char)9 + "[1004] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                "Left Load Magazine Standby Missing!",
                Err_SensLeftLoadMagStanbyPresent = "ERR CODE" + (char)9 + "[1005] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                "Left Load Magazine Standby Present!",
                Err_SensLeftLoadInputMissing = "ERR CODE" + (char)9 + "[1006] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                            "Left Load Input Missing!",
                Err_SensLeftLoadInputPresent = "ERR CODE" + (char)9 + "[1007] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                "Left Load Input Present!",
                Err_SensLeftLoadOutputMissing = "ERR CODE" + (char)9 + "[1008] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                            "Left Load Output Missing!",
                Err_SensLeftLoadOutputPresent = "ERR CODE" + (char)9 + "[1009] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                "Left Load Output Present!",
                Err_SensLeftLoadMagMissing = "ERR CODE" + (char)9 + "[1010] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                "Left Load Magazine Missing!",
                Err_SensLeftLoadMagPresent = "ERR CODE" + (char)9 + "[1011] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                "Left Load Magazine Present!",
                Err_SensLeftLoadMagLifterDownFail = "ERR CODE" + (char)9 + "[1012] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                    "Left Load Magazine Lifter Failed To Down!",
                Err_SensLeftLoadMagLifterUpFail = "ERR CODE" + (char)9 + "[1013] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                    "Left Load Magazine Lifter Failed To Up!",
                Err_SensLeftLoadWaffleMissing = "ERR CODE" + (char)9 + "[1014] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                "Left Load Magazine Missing!",
                Err_SensLeftLoadWafflePresent = "ERR CODE" + (char)9 + "[1015] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                "Left Load Magazine Present!",

                Err_SensLeftLoadInJam = "ERR CODE" + (char)9 + "[1100] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                "Left Load in Magazine Jam!",
                Err_SensLeftMoveToStandbyPosJam = "ERR CODE" + (char)9 + "[1101] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                "Left Load in To Standby Pos Magazine Jam!",
                Err_SensLeftMoveToInPosJam = "ERR CODE" + (char)9 + "[1102] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                "Left Load in Ready Pos Magazine Jam!",

           Err_SensLeftInputLoadNotPresent = "ERR CODE" + (char)9 + "[1103] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                "Left Input Load Not Present!",
           Err_SensLeftInputMagStandbyNotPresent = "ERR CODE" + (char)9 + "[1104] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                "Left Input Magazine Standby Not Present!",
           Err_SensLeftInputMagPursherNotInReadyPos = "ERR CODE" + (char)9 + "[1105] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                "Left Input Magazine Pursher Not In Ready Pos!",
           Err_SensLeftInputMagPusherNotInPos = "ERR CODE" + (char)9 + "[1106] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                "Left Input Magazine Pusher Not In Pos, Magazine Trasnfer Not Safe!",

           Err_SensLeftLoadMagPresentBeforeInit = "ERR CODE" + (char)9 + "[1150] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                "Left Load Magazine Present!" + (char)13 +
                                                "Please Remove Before Init!",
          Err_SensLeftIS1LoadNestLifterUpMoveNotSafe = "ERR CODE" + (char)9 + "[1151] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                        "Left IS1 Nest Up Detected! Input Singulate 1 Motor Move Not Safe",
          Err_SensLeftIS2LoadNestLifterUpMoveNotSafe = "ERR CODE" + (char)9 + "[1152] " + (char)10 + "Left Magazine Conveyor" + (char)13 +
                                                        "Left IS2 Nest Up Detected! Input Singulate 2 Motor Move Not Safe",
          Err_SensClamperInClampingMoveNotSafe = "ERR CODE" + (char)9 + "[1153] " + (char)10 + "Input Singulator" + (char)13 +
                                                        "Clamper In Clamping Condition, Motor Move Not Safe.",

    #endregion

    #region 1500 Right Magazine
                Err_SensRightLoadNestLifterDownFail = "ERR CODE" + (char)9 + "[1500] " + (char)10 + "Right Magazine Conveyor" + (char)13 +
                                                        "Right Load Nest Lifter Failed To Down!",
                Err_SensRightLoadNestLifterUpFail = "ERR CODE" + (char)9 + "[1501] " + (char)10 + "Right Magazine Conveyor" + (char)13 +
                                                    "Right Load Nest Lifter Failed To Up!",
                Err_SensRightLoadMagStopperRetFail = "ERR CODE" + (char)9 + "[1502] " + (char)10 + "Right Magazine Conveyor" + (char)13 +
                                                    "Right Load Magazine Stopper Failed To Retract!",
                Err_SensRightLoadMagStopperExtFail = "ERR CODE" + (char)9 + "[1503] " + (char)10 + "Right Magazine Conveyor" + (char)13 +
                                                    "Right Load Magazine Stopper Failed To Extend!",
                Err_SensRightLoadMagStanbyMissing = "ERR CODE" + (char)9 + "[1504] " + (char)10 + "Right Magazine Conveyor" + (char)13 +
                                                    "Right Load Magazine Standby Missing!",
                Err_SensRightLoadMagStanbyPresent = "ERR CODE" + (char)9 + "[1505] " + (char)10 + "Right Magazine Conveyor" + (char)13 +
                                                    "Right Load Magazine Standby Present!",
                Err_SensRightLoadInputMissing = "ERR CODE" + (char)9 + "[1506] " + (char)10 + "Right Magazine Conveyor" + (char)13 +
                                                "Right Load Input Missing!",
                Err_SensRightLoadInputPresent = "ERR CODE" + (char)9 + "[1507] " + (char)10 + "Right Magazine Conveyor" + (char)13 +
                                                "Right Load Input Present!",
                Err_SensRightLoadOutputMissing = "ERR CODE" + (char)9 + "[1508] " + (char)10 + "Right Magazine Conveyor" + (char)13 +
                                                "Right Load Output Missing!",
                Err_SensRightLoadOutputPresent = "ERR CODE" + (char)9 + "[1509] " + (char)10 + "Right Magazine Conveyor" + (char)13 +
                                                "Right Load Output Present!",
                Err_SensRightLoadMagMissing = "ERR CODE" + (char)9 + "[1510] " + (char)10 + "Right Magazine Conveyor" + (char)13 +
                                                "Right Load Magazine Missing!",
                Err_SensRightLoadMagPresent = "ERR CODE" + (char)9 + "[1511] " + (char)10 + "Right Magazine Conveyor" + (char)13 +
                                                "Right Load Magazine Present!",
                Err_SensRightLoadMagLifterDownFail = "ERR CODE" + (char)9 + "[1512] " + (char)10 + "Right Magazine Conveyor" + (char)13 +
                                                        "Right Load Magazine Lifter Failed To Down!",
                Err_SensRightLoadMagLifterUpFail = "ERR CODE" + (char)9 + "[1513] " + (char)10 + "Right Magazine Conveyor" + (char)13 +
                                                    "Right Load Magazine Lifter Failed To Up!",

                Err_SensRightLoadInJam = "ERR CODE" + (char)9 + "[1600] " + (char)10 + "Right Magazine Conveyor" + (char)13 +
                                                "Right Load in Magazine Jam!",
                Err_SensRightMoveToStandbyPosJam = "ERR CODE" + (char)9 + "[1601] " + (char)10 + "Right Magazine Conveyor" + (char)13 +
                                                "Right Load in To Standby Pos Magazine Jam!",
                Err_SensRightMoveToInPosJam = "ERR CODE" + (char)9 + "[1602] " + (char)10 + "Right Magazine Conveyor" + (char)13 +
                                                "Right Load in Ready Pos Magazine Jam!",


           Err_SensRightInputLoadNotPresent = "ERR CODE" + (char)9 + "[1603] " + (char)10 + "Right Magazine Conveyor" + (char)13 +
                                                "Right Input Load Not Present!",
           Err_SensRightInputMagStandbyNotPresent = "ERR CODE" + (char)9 + "[1604] " + (char)10 + "Right Magazine Conveyor" + (char)13 +
                                                "Left Input Magazine Standby Not Present!",
           Err_SensRightInputMagPursherNotInReadyPos = "ERR CODE" + (char)9 + "[1605] " + (char)10 + "Right Magazine Conveyor" + (char)13 +
                                                "Right Input Magazine Pursher Not In Ready Pos!",
           Err_SensRightInputMagPusherNotInPos = "ERR CODE" + (char)9 + "[1606] " + (char)10 + "Right Magazine Conveyor" + (char)13 +
                                                "Right Input Magazine Pusher Not In Pos, Magazine Trasnfer Not Safe!",


           Err_SensRightLoadMagPresentBeforeInit = "ERR CODE" + (char)9 + "[1650] " + (char)10 + "Right Magazine Conveyor" + (char)13 +
                                                "Right Load Magazine Present!" + (char)13 +
                                                "Please Remove Before Init!",



    #endregion

    #region 2000 Magazine Indexer
                Err_SensMag1Missing = "ERR CODE" + (char)9 + "[2000] " + (char)10 + "Mag Indexer" + (char)13 +
                                                        "Magazine 1 Missing!",
                Err_SensMag1Present = "ERR CODE" + (char)9 + "[2001] " + (char)10 + "Mag Indexer" + (char)13 +
                                                    "Magazine 1 Present! Please Remove Magazine Manually Before Initialize",
                Err_SensMag2Missing = "ERR CODE" + (char)9 + "[2002] " + (char)10 + "Mag Indexer" + (char)13 +
                                                        "Magazine 2 Missing!",
                Err_SensMag2Present = "ERR CODE" + (char)9 + "[2003] " + (char)10 + "Mag Indexer" + (char)13 +
                                                    "Magazine 2 Present! Please Remove Magazine Manually Before Initialize",
                Err_SvMovingMag1LockFail = "ERR CODE" + (char)9 + "[2010] " + (char)10 + "Mag Indexer" + (char)13 +
                                            "Failed To Moving Magazine 1 Lock!",
                Err_SvMovingMag1ReleaseFail = "ERR CODE" + (char)9 + "[2011] " + (char)10 + "Mag Indexer" + (char)13 +
                                                "Failed To Moving Magazine 1 Release!",
                Err_SvFixMag1LockFail = "ERR CODE" + (char)9 + "[2012] " + (char)10 + "Mag Indexer" + (char)13 +
                                                        "Failed To Fix Magazine 1 Lock!",
                Err_SvFixMag1ReleaseFail = "ERR CODE" + (char)9 + "[2013] " + (char)10 + "Mag Indexer" + (char)13 +
                                                    "Failed To Fix Magazine 1 Release!",
                Err_SvMovingBtmWaffle1SupportFail = "ERR CODE" + (char)9 + "[2014] " + (char)10 + "Mag Indexer" + (char)13 +
                                                        "Failed To Moving Bottom Waffle 1 Support!",
                Err_SvMovingBtmWaffle1UnsupportFail = "ERR CODE" + (char)9 + "[2015] " + (char)10 + "Mag Indexer" + (char)13 +
                                                        "Failed To Moving Bottom Waffle 1 Unsupport!",
                Err_SvFixBtmWaffleSupportFail = "ERR CODE" + (char)9 + "[2016] " + (char)10 + "Mag Indexer" + (char)13 +
                                                "Failed To Fix Bottom Waffle Support!",
                Err_SvFixBtmWaffleUnsupportFail = "ERR CODE" + (char)9 + "[2017] " + (char)10 + "Mag Indexer" + (char)13 +
                                                    "Failed To Fix Bottom Waffle Unsupport!",
                Err_SvMagLockerRetFail = "ERR CODE" + (char)9 + "[2018] " + (char)10 + "Mag Indexer" + (char)13 +
                                                        "Failed To Retract Magazine Locker!",
                Err_SvMagLockerExtFail = "ERR CODE" + (char)9 + "[2019] " + (char)10 + "Mag Indexer" + (char)13 +
                                                    "Failed To Extend Magazine Locker!",
                Err_SvMovingMag2LockFail = "ERR CODE" + (char)9 + "[2020] " + (char)10 + "Mag Indexer" + (char)13 +
                                            "Failed To Moving Magazine 2 Lock!",
                Err_SvMovingMag2ReleaseFail = "ERR CODE" + (char)9 + "[2021] " + (char)10 + "Mag Indexer" + (char)13 +
                                                "Failed To Moving Magazine 3 Release!",
                Err_SvMovingBtmWaffle2SupportFail = "ERR CODE" + (char)9 + "[2022] " + (char)10 + "Mag Indexer" + (char)13 +
                                                    "Failed To Moving Bottom Waffle 2 Support!",
                Err_SvMovingBtmWaffle2UnsupportFail = "ERR CODE" + (char)9 + "[2023] " + (char)10 + "Mag Indexer" + (char)13 +
                                                        "Failed To Moving Bottom Waffle 2 Unsupport!",
                Err_SvMagSideStopper1RetFail = "ERR CODE" + (char)9 + "[2024] " + (char)10 + "Mag Indexer" + (char)13 +
                                                "Magazine Side Stopper 1 Failed To Retract!",
                Err_SvMagSideStopper1ExtFail = "ERR CODE" + (char)9 + "[2025] " + (char)10 + "Mag Indexer" + (char)13 +
                                                "Magazine Side Stopper 1 Failed To Extend!",
                Err_SvMagSideStopper2RetFail = "ERR CODE" + (char)9 + "[2016] " + (char)10 + "Mag Indexer" + (char)13 +
                                                "Magazine Side Stopper 2 Failed To Retract!",
                Err_SvMagSideStopper2ExtFail = "ERR CODE" + (char)9 + "[2017] " + (char)10 + "Mag Indexer" + (char)13 +
                                                "Magazine Side Stopper 2 Failed To Extend!",
                Err_SvMagFrontEject1RetFail = "ERR CODE" + (char)9 + "[2022] " + (char)10 + "Mag Indexer" + (char)13 +
                                                "Failed To Magazine Front Eject 1 Retract!",
                Err_SvMagFrontEject1ExtFail = "ERR CODE" + (char)9 + "[2023] " + (char)10 + "Mag Indexer" + (char)13 +
                                                "Failed To Magazine Front Eject 1 Extend!",
                Err_SvMagFrontEject2RetFail = "ERR CODE" + (char)9 + "[2024] " + (char)10 + "Mag Indexer" + (char)13 +
                                                "Failed To Magazine Front Eject 2 Retract!",
                Err_SvMagFrontEject2ExtFail = "ERR CODE" + (char)9 + "[2025] " + (char)10 + "Mag Indexer" + (char)13 +
                                                "Failed To Magazine Front Eject 2 Extend!",
                Err_SvMagSideEject1RetFail = "ERR CODE" + (char)9 + "[2026] " + (char)10 + "Mag Indexer" + (char)13 +
                                                "Failed To Magazine Side Eject 1 Retract!",
                Err_SvMagSideEject1ExtFail = "ERR CODE" + (char)9 + "[2027] " + (char)10 + "Mag Indexer" + (char)13 +
                                                "Failed To Magazine Side Eject 1 Extend!",
                Err_SvMagSideEject2RetFail = "ERR CODE" + (char)9 + "[2028] " + (char)10 + "Mag Indexer" + (char)13 +
                                                "Failed To Magazine Side Eject 2 Retract!",
                Err_SvMagSideEject2ExtFail = "ERR CODE" + (char)9 + "[2029] " + (char)10 + "Mag Indexer" + (char)13 +
                                                "Failed To Magazine Side Eject 2 Extend!",
                Err_SvMagSideMoveLeftFail = "ERR CODE" + (char)9 + "[2030] " + (char)10 + "Mag Indexer" + (char)13 +
                                                "Magazine Failed To Side Move To Left!",
                Err_SvMagSideMoveRightFail = "ERR CODE" + (char)9 + "[2031] " + (char)10 + "Mag Indexer" + (char)13 +
                                                "Magazine Failed To Side Move To Right!",

          Err_SensLeftMagDetectBeforeInit = "ERR CODE" + (char)9 + "[2050] " + (char)10 + "Mag Indexer" + (char)13 +
                                                        "Magazine Detected in Mag Indexer!" + (char)13 +
                                                        "Please remove Magazine Before Initialize",
          Err_SensMagFixLockerNotDetectBeforeMove = "ERR CODE" + (char)9 + "[2051] " + (char)10 + "Mag Indexer" + (char)13 +
                                                        "Magazine Fix Locker Not Detected Before Move!" + (char)13 +
                                                        "Please Manually Move the Fix Locker Retract Before Moving",

          Err_SensWaffleNotPresentOrMagazineEmpty = "ERR CODE" + (char)9 + "[2052] " + (char)10 + "Mag Indexer" + (char)13 +
                                                        "Load Waffle Not Present In The Nest! Please Check Waffle Condition In The Nest!" + (char)13 +
                                                        "Press Retry - Recheck The Waffle Present!" + (char)13 +
                                                        "Press Stop - To Stop The Machine!" + (char)13 +
                                                         "Press Cancel - To Unload Empty Magazine!",

          Err_SensWaffleDoubleUnitInNest = "ERR CODE" + (char)9 + "[2053] " + (char)10 + "Mag Indexer" + (char)13 +
                                                        "Load Double Waffle In The Nest! Please Stop the Machine And Manually Remove The Waffle" + (char)13,

                Err_SensMag1PresentMoveNotSafe = "ERR CODE" + (char)9 + "[2054] " + (char)10 + "Mag Indexer" + (char)13 +
                                                    "Magazine 1 Present! Main Side Move Right Not Safe" + (char)13 +
                                                    "Please Remove The Magazine First.",
                Err_SensMag2PresentMoveNotSafe = "ERR CODE" + (char)9 + "[2055] " + (char)10 + "Mag Indexer" + (char)13 +
                                                    "Magazine 2 Present! Main Side Move Left Not Safe" + (char)13 +
                                                    "Please Remove The Magazine First.",
    #endregion

    #region 3000 Input Left Fliper
                Err_SvLeftMainHeadRetFail = "ERR CODE" + (char)9 + "[3000] " + (char)10 + "Input Left Fliper" + (char)13 +
                                            "Left Main Head Failed To Retract!",
                Err_SvLeftMainHeadExtFail = "ERR CODE" + (char)9 + "[3001] " + (char)10 + "Input Left Fliper" + (char)13 +
                                            "Left Main Head Failed To Extend!",
                Err_SvLeftSecHeadRetFail = "ERR CODE" + (char)9 + "[3002] " + (char)10 + "Input Left Fliper" + (char)13 +
                                            "Left Sec Head Failed To Retract!",
                Err_SvLeftSecHeadExtFail = "ERR CODE" + (char)9 + "[3003] " + (char)10 + "Input Left Fliper" + (char)13 +
                                            "Left Sec Head Failed To Extend!",
                Err_SvLeftFlipperRetFail = "ERR CODE" + (char)9 + "[3004] " + (char)10 + "Input Left Fliper" + (char)13 +
                                            "Left Flipper Failed To Retract!",
                Err_SvLeftFlipperExtFail = "ERR CODE" + (char)9 + "[3005] " + (char)10 + "Input Left Fliper" + (char)13 +
                                            "Left Flipper Failed To Extend!",


                Err_SvLeftFlipperVacNotOn = "ERR CODE" + (char)9 + "[3006] " + (char)10 + "Input Left Fliper" + (char)13 +
                                            "Vacuum No On! Please Check Pick Waffle Condition.",


                Err_SensLeftFlipperVacOnDetected = "ERR CODE" + (char)9 + "[3010] " + (char)10 + "Input Left Fliper" + (char)13 +
                                            "Vacuum Detected On! Please Manually Remove the Product Before Initialize",


                Err_SvLeftFlipperExtMainHeadExtFail = "ERR CODE" + (char)9 + "[3050] " + (char)10 + "Input Left Fliper" + (char)13 +
                                            "Left Flipper Extend Detected! Main Head Fail to Extend due To Safety Concern.",
                Err_SvLeftExtMainHeadExtFail = "ERR CODE" + (char)9 + "[3051] " + (char)10 + "Input Left Fliper" + (char)13 +
                                            "Left Main Head Extend Detected! Flipper Fail to Extend due To Safety Concern.",
    #endregion

    #region 3500 Output Right Fliper
                Err_SvRightMainHeadRetFail = "ERR CODE" + (char)9 + "[3500] " + (char)10 + "Output Right Fliper" + (char)13 +
                                            "Right Main Head Failed To Retract!",
                Err_SvRightMainHeadExtFail = "ERR CODE" + (char)9 + "[3501] " + (char)10 + "Output Right Fliper" + (char)13 +
                                            "Right Main Head Failed To Extend!",
                Err_SvRightSecHeadRetFail = "ERR CODE" + (char)9 + "[3502] " + (char)10 + "Output Right Fliper" + (char)13 +
                                            "Right Sec Head Failed To Retract!",
                Err_SvRightSecHeadExtFail = "ERR CODE" + (char)9 + "[3503] " + (char)10 + "Output Right Fliper" + (char)13 +
                                            "Right Sec Head Failed To Extend!",
                Err_SvRightFlipperRetFail = "ERR CODE" + (char)9 + "[3504] " + (char)10 + "Output Right Fliper" + (char)13 +
                                            "Right Flipper Failed To Retract!",
                Err_SvRightFlipperExtFail = "ERR CODE" + (char)9 + "[3505] " + (char)10 + "Output Right Fliper" + (char)13 +
                                            "Right Flipper Failed To Extend!",
                Err_SvRightFlipperVacNotOn = "ERR CODE" + (char)9 + "[3506] " + (char)10 + "Output Right Fliper" + (char)13 +
                                            "Vacuum No On! Please Check Pick Waffle Condition.",


                Err_SensRightFlipperVacOnDetected = "ERR CODE" + (char)9 + "[3510] " + (char)10 + "Output Right Fliper" + (char)13 +
                                            "Vacuum Detected On! Please Manually Remove the Product Before Initialize",

                Err_SvRightFlipperExtMainHeadExtFail = "ERR CODE" + (char)9 + "[3550] " + (char)10 + "Output Right Fliper" + (char)13 +
                                            "Right Flipper Extend Detected! Main Head Fail to Extend due To Safety Concern.",


                Err_SvRightExtMainHeadExtFail = "ERR CODE" + (char)9 + "[3551] " + (char)10 + "Output Right Fliper" + (char)13 +
                                            "Left Main Head Extend Detected! Flipper Fail to Extend due To Safety Concern.",
    #endregion

    #region 4000 Top Laser Mark

    #endregion

    #region 4500 Bottom Laser Mark

    #endregion

    #region 5000 Input Singu Indexer 1
            Err_SvIS1PreciseExtFail = "ERR CODE" + (char)9 + "[5000] " + (char)10 + "INPUT SINGU INDEXER 1" + (char)13 +
                                            "Precisor Extend Fail!",
            Err_SvIS1PreciseRetFail = "ERR CODE" + (char)9 + "[5001] " + (char)10 + "INPUT SINGU INDEXER 1" + (char)13 +
                                            "Precisor Retract Fail!",

            Err_SvInMainNest1LifterUpFail = "ERR CODE" + (char)9 + "[5002] " + (char)10 + "INPUT SINGU INDEXER 1" + (char)13 +
                                            "Input Main Nest 1 Lifter Failed To Up!",
            Err_SvInMainNest1LifterDownFail = "ERR CODE" + (char)9 + "[5003] " + (char)10 + "INPUT SINGU INDEXER 1" + (char)13 +
                                            "Input Main Nest 1 Lifter Failed To Down!",
    #endregion

    #region 5200 Input Singu Indexer 2
           Err_SvInd2PreciseExtFail = "ERR CODE" + (char)9 + "[5200] " + (char)10 + "INPUT SINGU INDEXER 2" + (char)13 +
                                            "Precisor Extend Fail!",
            Err_SvInd2PreciseRetFail = "ERR CODE" + (char)9 + "[5201] " + (char)10 + "INPUT SINGU INDEXER 2" + (char)13 +
                                            "Precisor Retract Fail!",
            Err_SvInMainNest2LifterUpFail = "ERR CODE" + (char)9 + "[5202] " + (char)10 + "INPUT SINGU INDEXER 2" + (char)13 +
                                            "Input Main Nest 2 Lifter Failed To Up!",
            Err_SvInMainNest2LifterDownFail = "ERR CODE" + (char)9 + "[5203] " + (char)10 + "INPUT SINGU INDEXER 2" + (char)13 +
                                            "Input Main Nest 2 Lifter Failed To Down!",


            Err_SvInputClampFail = "ERR CODE" + (char)9 + "[5204] " + (char)10 + "Input Singulator" + (char)13 +
                                            "Input Waffle Clamp Fail!",
            Err_SvInputUnClampFail = "ERR CODE" + (char)9 + "[5205] " + (char)10 + "Input Singulator" + (char)13 +
                                            "Input Waffle Unclamp Fail!",
    #endregion

    #region 5400 Output Singulate Index 1
            Err_SvOS1PreciseExtFail = "ERR CODE" + (char)9 + "[5400] " + (char)10 + "Output Singulate Index 1" + (char)13 +
                                            "Precisor Extend Fail!",
            Err_SvOS1PreciseRetFail = "ERR CODE" + (char)9 + "[5401] " + (char)10 + "Output Singulate Index 1" + (char)13 +
                                            "Precisor Retract Fail!",

            Err_SvOutMainNest1LifterUpFail = "ERR CODE" + (char)9 + "[5402] " + (char)10 + "Output Singulate Index 1" + (char)13 +
                                            "Output Main Nest 1 Lifter Failed To Up!",
            Err_SvOutMainNest1LifterDownFail = "ERR CODE" + (char)9 + "[5403] " + (char)10 + "Output Singulate Index 1" + (char)13 +
                                            "Output Main Nest 1 Lifter Failed To Down!",


            Err_SvOS1NestLifterUpFail = "ERR CODE" + (char)9 + "[5404] " + (char)10 + "Output Singulate Index 1" + (char)13 +
                                            "Output Nest Lifter Failed To Up!",
            Err_SvOS1NestLifterDownFail = "ERR CODE" + (char)9 + "[5405] " + (char)10 + "Output Singulate Index 1" + (char)13 +
                                            "Output Nest Lifter Failed To Down!",

            Err_SvOS2NestLifterUpFail = "ERR CODE" + (char)9 + "[5406] " + (char)10 + "Output Singulate Index 2" + (char)13 +
                                            "Output Nest Lifter Failed To Up!",
            Err_SvOS2NestLifterDownFail = "ERR CODE" + (char)9 + "[5407] " + (char)10 + "Output Singulate Index 2" + (char)13 +
                                            "Output Nest Lifter Failed To Down!",
            Err_SensOS1LoadNestLifterUpMoveNotSafe = "ERR CODE" + (char)9 + "[5450] " + (char)10 + "Output Singulate Index" + (char)13 +
                                                        "Output OS1 Nest Up Detected! Output Singulate 1 Motor Move Not Safe",

            Err_SensOS2LoadNestLifterUpMoveNotSafe = "ERR CODE" + (char)9 + "[5451] " + (char)10 + "Output Singulate Index" + (char)13 +
                                                        "Output OS2 Nest Up Detected! Output Singulate 2 Motor Move Not Safe",

            Err_SvOutputClampFail = "ERR CODE" + (char)9 + "[5452] " + (char)10 + "Output Singulator" + (char)13 +
                                            "Output Waffle Clamp Fail!",
            Err_SvOutputUnClampFail = "ERR CODE" + (char)9 + "[5453] " + (char)10 + "Output Singulator" + (char)13 +
                                            "Output Waffle Unclamp Fail!",

            Err_SvOutputClamperClampMoveUnsafe = "ERR CODE" + (char)9 + "[5454] " + (char)10 + "Output Singulator" + (char)13 +
                                            "Clamper In Clamping Condition, Motor Move Not Safe.",
    #endregion

    #region 5600 Output Singulate Index 2
            Err_SvOS2PreciseExtFail = "ERR CODE" + (char)9 + "[5400] " + (char)10 + "Output Singulate Index 2" + (char)13 +
                                            "Precisor Extend Fail!",
            Err_SvOS2PreciseRetFail = "ERR CODE" + (char)9 + "[5401] " + (char)10 + "Output Singulate Index 2" + (char)13 +
                                            "Precisor Retract Fail!",
            Err_SvOutMainNest2LifterUpFail = "ERR CODE" + (char)9 + "[5600] " + (char)10 + "Output Singulate Index 2" + (char)13 +
                                            "Output Main Nest 2 Lifter Failed To Up!",
            Err_SvOutMainNest2LifterDownFail = "ERR CODE" + (char)9 + "[5601] " + (char)10 + "Output Singulate Index 2" + (char)13 +
                                            "Output Main Nest 2 Lifter Failed To Down!",
    #endregion

    #region 6000 Pnp Reject
                Err_SvRejectIndexLeftFail = "ERR CODE" + (char)9 + "[6000] " + (char)10 + "Pnp Reject" + (char)13 +
                                            "Pnp Reject Failed To Index Left!",
                Err_SvRejectIndexRightFail = "ERR CODE" + (char)9 + "[6001] " + (char)10 + "Pnp Reject" + (char)13 +
                                            "Pnp Reject Failed To Index Right!",
                Err_SvPickheadZUpFail = "ERR CODE" + (char)9 + "[6002] " + (char)10 + "Pnp Reject" + (char)13 +
                                            "Failed To Up Pickhead Z!",
                Err_SvPickheadZDownFail = "ERR CODE" + (char)9 + "[6003] " + (char)10 + "Pnp Reject" + (char)13 +
                                            "Failed To Down Pickhead Z!",

                Err_SensVacOnDetected = "ERR CODE" + (char)9 + "[6010] " + (char)10 + "Pnp Reject" + (char)13 +
                                            "Vacuum Detected On! Please Manually Remove the Product Before Initialize",
                Err_SensVacOnNoDetected = "ERR CODE" + (char)9 + "[6011] " + (char)10 + "Pnp Reject" + (char)13 +
                                            "Vacuum No Detected! Please Check Pick Waffle Condition.",
                Err_SensVacOnDetectedWhenPlace = "ERR CODE" + (char)9 + "[6012] " + (char)10 + "Pnp Reject" + (char)13 +
                                            "Vacuum On Detected When Place! Please Check Place Waffle Condition.",
    #endregion

    #region 7000 Unload Rotary
                Err_SensUnloadInputTopWaffleMissing = "ERR CODE" + (char)9 + "[7000] " + (char)10 + "Unload Rotary" + (char)13 +
                                                        "Unload Input Top Waffle Missing!",
                Err_SensUnloadInputTopWafflePresent = "ERR CODE" + (char)9 + "[7001] " + (char)10 + "Unload Rotary" + (char)13 +
                                                        "Unload Input Top Waffle Present!",
                Err_SensUnloadOutputTopWaffleMissing = "ERR CODE" + (char)9 + "[7002] " + (char)10 + "Unload Rotary" + (char)13 +
                                                        "Unload Output Top Waffle Missing!",
                Err_SensUnloadOutputTopWafflePresent = "ERR CODE" + (char)9 + "[7003] " + (char)10 + "Unload Rotary" + (char)13 +
                                                        "Unload Output Top Waffle Present!",
                Err_SvStackDoorCloseFail = "ERR CODE" + (char)9 + "[7004] " + (char)10 + "Unload Rotary" + (char)13 +
                                            "Failed To Open Stacker Door!",
                Err_SvStackDoorOpenFail = "ERR CODE" + (char)9 + "[7005] " + (char)10 + "Unload Rotary" + (char)13 +
                                            "Failed To Close Stacker Door!",
                Err_SvUnloadWaffleFail = "ERR CODE" + (char)9 + "[7006] " + (char)10 + "Unload Rotary" + (char)13 +
                                            "Failed To Unload Waffle In Rotary Tower!" + (char)13 +
                                            "Please Manually unload the waffle to Rotary Tower",
                Err_SensWafferTowerPreent = "ERR CODE" + (char)9 + "[7007] " + (char)10 + "Unload Rotary" + (char)13 +
                                            "Rotary Tower Detect Waffle Present!" + (char)13 +
                                            "Please Manually Remove The Product Into The Rotary Tower",//SensUnloadInputTopWafflePresent

    #endregion

    #region 8000 Unload Pack
                Err_SvMainGripperYRetFail = "ERR CODE" + (char)9 + "[8000] " + (char)10 + "Unload Pack" + (char)13 +
                                        "Failed To Retract Main Gripper Y (Unload Pick)!",
                Err_SvMainGripperYExtFail = "ERR CODE" + (char)9 + "[8001] " + (char)10 + "Unload Pack" + (char)13 +
                                        "Failed To Extend Main Gripper Y (Unload Place)!",
                Err_SvMainZUpFail = "ERR CODE" + (char)9 + "[8002] " + (char)10 + "Unload Pack" + (char)13 +
                                    "Failed To Retract Main Z (Up)!",
                Err_SvMainZDownFail = "ERR CODE" + (char)9 + "[8003] " + (char)10 + "Unload Pack" + (char)13 +
                                    "Failed To Extend Main Z (Down)!",
                Err_SvGripperYRetFail = "ERR CODE" + (char)9 + "[8004] " + (char)10 + "Unload Pack" + (char)13 +
                                        "Failed To Retract Gripper Y!",
                Err_SvGripperYExtFail = "ERR CODE" + (char)9 + "[8005] " + (char)10 + "Unload Pack" + (char)13 +
                                        "Failed To Extend Gripper Y!",
                Err_SvGripperUpFail = "ERR CODE" + (char)9 + "[8006] " + (char)10 + "Unload Pack" + (char)13 +
                                            "Gripper Failed To Up!",
                Err_SvGripperDownFail = "ERR CODE" + (char)9 + "[8007] " + (char)10 + "Unload Pack" + (char)13 +
                                            "Gripper Failed To Down!",
                Err_SvGripperFlipVerticalFail = "ERR CODE" + (char)9 + "[8008] " + (char)10 + "Unload Pack" + (char)13 +
                                            "Gripper Failed To Flip Vertical!",
                Err_SvGripperFlipHorizontalFail = "ERR CODE" + (char)9 + "[8009] " + (char)10 + "Unload Pack" + (char)13 +
                                            "Gripper Failed To Flip Horizontal!",
                Err_SvMagGripperOpenFail = "ERR CODE" + (char)9 + "[8010] " + (char)10 + "Unload Pack" + (char)13 +
                                            "Failed To Open Gripper!",
                Err_SvMagGripperCloseFail = "ERR CODE" + (char)9 + "[8011] " + (char)10 + "Unload Pack" + (char)13 +
                                            "Failed To Close Gripper!",
                Err_SvMainGripperYNotSafeMoveFail = "ERR CODE" + (char)9 + "[8012] " + (char)10 + "Unload Pack" + (char)13 +
                                                    "Main Gripper Y Not Safe To Move!" + (char)13 +
                                                    "Gripper Z Is Down!",
                Err_SvMainGripperYNotSafeMove2Fail = "ERR CODE" + (char)9 + "[8013] " + (char)10 + "Unload Pack" + (char)13 +
                                                    "Main Gripper Y Not Safe To Move!" + (char)13 +
                                                    "Unload Pick Y No Retract!",
        Err_RotaryMoveNotSafe = "ERR CODE" + (char)9 + "[8014] " + (char)10 + "Unload Rotary" + (char)13 +
                                                    "Unload Stack Door Opened!" + (char)13 +
                                                    "Unload Rotary Move Not Safe!";
    #endregion

    #region 9000

    #endregion


  }
}
