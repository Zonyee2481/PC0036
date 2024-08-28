using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    #region IO
    public enum Input : int
    {
        IP_NULL = -1,

        #region ET-2254P 1
        AUTO,
        START_TIMER,
        END_LOT,
        #endregion
    }

    public enum Output : int
    {
        OP_NULL = -1,

        #region ET-2254P 1        
        TIMESUP,
        BITCODE_1,
        BITCODE_2,
        BITCODE_4,
        BITCODE_8,
        START_CONTROL_RELAY,
        START_BUTTON_LED,
        #endregion
    }
    #endregion
}
