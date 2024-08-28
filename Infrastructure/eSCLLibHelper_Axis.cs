using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public enum TotalAxis
    {
        [Description("Empty")]
        Spare = -1,

        #region eSCLLibHelper
        [Description("Turntable Load Station Z")]
        LoadingStnZ = 0,

        [Description("Turntable Unload Station Z")]
        UnloadStnZ = 1,

        [Description("Laser Top Z")]
        LaserTopZ = 2,

        [Description("Laser Btm Z")]
        LaserBtmZ = 3,
        #endregion

        #region Advantech 1

        [Description("Input Pnp Left X")]
        InpnpLeft_X = 4,

        [Description("Input Pnp Left Z")]
        InpnpLeft_Z = 5,

        [Description("Turn table Rotary")]
        Rotation = 6,
        #endregion
        
        #region Advantech 2

        [Description("Input Pnp Right X")]
        InpnpRight_X = 7,

        [Description("Input Pnp Right Z")]
        InpnpRight_Z = 8,

        [Description("Unload Rotary")]
        UnloadRotary = 9,
        #endregion
    }

    public enum eSCLLibHelper_Axis
    {
        [Description("Empty")]
        Spare = -1,

        #region eSCLLibHelper
        [Description("Turntable Load Station Z")]
        LoadingStnZ = 0,

        [Description("Turntable Unload Station Z")]
        UnloadStnZ = 1,

        [Description("Laser Top Z")]
        LaserTopZ = 2,

        [Description("Laser Btm Z")]
        LaserBtmZ = 3,
        #endregion

    }

    public enum Advantech1_Axis
    {
        #region Advantech 1
        [Description("Empty")]
        Spare = -1,

        [Description("Input Pnp Left X")]
        InpnpLeft_X = 0,

        [Description("Input Pnp Left Z")]
        InpnpLeft_Z = 1,

        [Description("Turn table Rotary")]
        Rotation = 2,
        #endregion
    }

    public enum Advantech2_Axis
    {
        #region Advantech 2
        [Description("Empty")]
        Spare = -1,

        [Description("Input Pnp Right X")]
        InpnpRight_X = 0,

        [Description("Input Pnp Right Z")]
        InpnpRight_Z = 1,

        [Description("Unload Rotary")]
        UnloadRotary = 2,
        #endregion
    }

    public enum Galil1_Axis
    {
        #region Galil 1
        [Description("Empty")]
        Spare = -1,

        [Description("Galil Axis 1")]
        GalilAxis_1 = 0,

        [Description("Galil Axis 2")]
        GalilAxis_2 = 1,
        #endregion
    }
}
