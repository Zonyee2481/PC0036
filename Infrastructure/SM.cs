using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class SM
    {
        public static string RecipeName;
        public static int McUpTime = 0;
        public static int McRunTime = 0;
        public static int McOpTime = 0;
        public static int McAssistTime = 0;
        public static int McAssistCount = 0;
        public static int McCellCycleTime = 0;
        public static int McCycleTime = 0;
        public static int McIdleTime = 0;
        public static double McActualTime = 0;
        public static double OutputCounter = 0;
        public static int Index = 0;
        public static string Connection = "";
        public static string UserName = "";
        public static int McProcessTime = 0;
        public static int McRunningHz = 0;
        public static bool StartCount = false;
        public static int LotNoCoolingPeriodInHour = 0;
        public static int LotNoCoolingPeriodInMinute = 0;

        public static eMcState McState = eMcState.MC_BEGIN;

        public static RecipeInfo Recipe;
        
    }
}
