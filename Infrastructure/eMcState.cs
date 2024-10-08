using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public enum eMcState
    {
        MC_BEGIN,
        MC_NON_INITIALIZED,
        MC_INITIALIZING,
        MC_INITIALIZED,
        MC_START_RUN,
        MC_RUNNING,
        MC_STOP_INIT,
        MC_STOP,
        MC_INIT_ERR,
        MC_WARNING,
        MC_RUN_SOFTJAM,
        MC_RUN_HARDJAM,
        MC_MAINTENACE,
        MC_STOPPING,
        MC_IDLE,
        MC_RUN_MANUAL,

        MC_OPT_REQ,

        MC_RUN_TIMESUP,
    };
}
