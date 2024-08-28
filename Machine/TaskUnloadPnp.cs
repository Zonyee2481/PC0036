using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ES.CMotion;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace Machine
{
  class TaskUnloadPnp
  {
    private static bool Delay(int msdelay)
    {
      if (msdelay <= 0) { return true; }
      int t = Environment.TickCount + msdelay;

      while (true)
      {
        if (Environment.TickCount >= t) { break; }
        Thread.Sleep(0);
      }

      return true;
    }

  }
}
