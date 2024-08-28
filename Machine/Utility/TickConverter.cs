using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine
{
    public static class TickConverter
    {
        public static string Convert2DHMS(int TickCount, int T)
        {
            int RD = 0;
            int RH = 0;
            int RM = 0;
            int RS = 0;

            RS = ((TickCount / T) % 60);
            RM = ((TickCount / T) / 60) % 60;
            RH = ((TickCount / T) / 3600) % 24;
            RD = (TickCount / T) / (3600 * 24);

            return (RD + " D " + RH + " H " + RM + " M " + RS + " S ");
        }
    }
}
