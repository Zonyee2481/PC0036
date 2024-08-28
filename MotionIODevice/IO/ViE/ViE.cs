using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;

namespace MotionIODevice.IO.ViE
{
    public class ViE
    {
        public const uint MAX_BOARD_NUMBER = 15;
        public const uint MAX_MODULE_NUMBER = 64;

        // Remote Module type
        public const Int32 DEV_D8IO = 0x10;
        public const Int32 DEV_D16IO = 0x13;
        public const Int32 DEV_D32I = 0x16;
        public const Int32 DEV_D32O = 0x17;
        public const Int32 DEV_D20IO = 0x18;        // new added for remote configurable IO
        public const Int32 DEV_UNKNOWN = 0x00;

        // ViNET Board Model
        public const Int32 BOARD_DAQEs2GPCI64C = 0x60;

        // ViNET Bus IO
        public const Int32 VMIO_BUS_IO = 0x00;
        public const Int32 VMIO_BUS_MOTION = 0x01;

        // ViNET Controller Parameter ID definition
        public const Int32 VMIO_TRANSFER_RATE = 0x01;

        // ViNET Controller Parameter Value Define (for VMIO_TRANSFER_RATE)
        public const Int32 VTR_2_5_MEGA_BIT_PSEC = 0x00; // 2.5Mbps
        public const Int32 VTR_5_MEGA_BIT_PSEC = 0x01; // 5Mbps
        public const Int32 VTR_10_MEGA_BIT_PSEC = 0x02; // 10Mbps
        public const Int32 VTR_20_MEGA_BIT_PSEC = 0x03; // 20Mbps -DEFAULT

        //--------------- The following section of definition is added for new remote configurable IO ---------------//
        // MNET Controller operation mode define
        public const Int32 VMIO_AUTO_BOARD_ID = 0x00;
        public const Int32 VMIO_MANUAL_BOARD_ID = 0x01;

        // R20IO IO pins group definition
        public const Int32 R20IO_ICOM = 0x00;
        public const Int32 R20IO_PCOM = 0x01;

        // Return Errors
        public const Int32 VMIO_SUCCESS = 0;
        public const Int32 VERR_DRIVER_FAILED = -1;
        public const Int32 VERR_TIMEOUT = -2;
        public const Int32 VERR_NO_BOARD = -3;
        public const Int32 VERR_INVALID_PARAM = -4;
        public const Int32 VERR_DUPLICATE_BOARD_ID = -5;
        public const Int32 VERR_DOUBLE_INITIALED = -6;
        public const Int32 VERR_NO_MODULE = -7;
        public const Int32 VERR_BOARD_ID_OVERFLOW = -8;
        public const Int32 VERR_SYSTEM = -9;
        public const Int32 VERR_MODULE_COMM = -10;
        public const Int32 VERR_DRIVER_MEMORY = -11;
        public const Int32 VERR_API_NOT_AVAILABLE = -12;
        public const Int32 VERR_OPERATION_FAILED = -13;
        public const Int32 VERR_BOARD_NOT_INITIALED = -14;
        public const Int32 VERR_API_NOT_SUPPORTED = -15;

        //private const string DLL_FILENAME = "Vmio64.dll";
        private const string DLL_FILENAME = @"C:\Users\Administrator\Desktop\Galil DLL - Backup\DLL\\Vmio.dll";

        // System General Libraries (x32bit)
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_open(ref System.Int32 iBoardTotal, System.Int32 iMode);
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_close();
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_start_bus(System.Int32 iBoardID, System.Int32 iBusNo);
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_stop_bus(System.Int32 iBoardID, System.Int32 iBusNo);
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_set_bus_parameter(System.Int32 iBoardID, System.Int32 iBusNo, System.Int32 iParamCode, System.Int32 iParam);
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_get_bus_parameter(System.Int32 iBoardID, System.Int32 iBusNo, System.Int32 iParamCode, ref System.Int32 iParam);
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_get_bus_status(System.Int32 iBoardID, System.Int32 iBusNo, ref System.Int32 iBusStatus);
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_get_bus_cycle_time(System.Int32 iBoardID, System.Int32 iBusNo, ref System.Int32 iCycleTime);
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_system_reset(System.Int32 iBoardID, System.Int32 iBusNo);
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_firmware_revision(System.Int32 iBoardID, ref System.Int32 iRev);
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_board_revision(System.Int32 iBoardID, ref System.Int32 iRev);
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_dll_revision(ref System.Int32 iRev);
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_get_module_name(System.Int32 iBoardID, System.Int32 iBusNo, System.Int32 iModuleNo, ref System.Int32 iModuleName);
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_get_module_online_status(System.Int32 iBoardID, System.Int32 iBusNo, System.Int32 iModuleNo, ref System.Int32 iOnline);
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_get_module_error(System.Int32 iBoardID, System.Int32 iBusNo, ref System.UInt64 uiErrBoard);

        // IO Features Libraries
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_d_get_output(System.Int32 iBoardID, System.Int32 iBusNo, System.Int32 iModuleNo, ref System.UInt32 uiDO);
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_d_set_output(System.Int32 iBoardID, System.Int32 iBusNo, System.Int32 iModuleNo, System.UInt32 uiDO);
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_d_get_input(System.Int32 iBoardID, System.Int32 iBusNo, System.Int32 iModuleNo, ref System.UInt32 uiDI);
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_d_set_output_bit(System.Int32 iBoardID, System.Int32 iBusNo, System.Int32 iModuleNo, System.Int32 iBitNo, System.Int32 iBitStatus);
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_d_get_input_bit(System.Int32 iBoardID, System.Int32 iBusNo, System.Int32 iModuleNo, System.Int32 iBitNo, ref System.Int32 iBitStatus);

        //--------------- The following section of definition is added for new remote configurable IO ---------------//
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_retrieve_module_configuration(System.Int32 iBoardID, System.Int32 iBusNo, System.Int32 iModuleNo);
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_get_input_inverse_setting(System.Int32 iBoardID, System.Int32 iBusNo, System.Int32 iModuleNo, ref System.UInt32 uiStatus);
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_get_io_pin_config(System.Int32 iBoardID, System.Int32 iBusNo, System.Int32 iModuleNo, System.Int32 iGroupNo, ref System.UInt32 uiPinConfig);
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_get_input_latch_setting(System.Int32 iBoardID, System.Int32 iBusNo, System.Int32 iModuleNo, ref System.UInt32 uiStatus);
        [DllImport(DLL_FILENAME, CallingConvention = CallingConvention.Cdecl)] public static extern Int32 VMIO_clear_latch(System.Int32 iBoardID, System.Int32 iBusNo, System.Int32 iModuleNo, System.UInt32 uiStatus);
    }
}
