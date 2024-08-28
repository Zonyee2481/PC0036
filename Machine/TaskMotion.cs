using MotionIODevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MotionIODevice.Common;

namespace Machine
{
    public class TaskMotion
    {
        public static IMotionMain BoardOption(TAxis SelectedTAxis, int SelectedBoardNo)
        {
            var Taxis = SelectedTAxis;
            var BoardNo = SelectedBoardNo;
            IMotionMain boardselected = null;

            if (frmMain.AdvantechModule.GetBoardNo - 1 >= BoardNo)
            {
                if (frmMain.AdvantechModule.GetAxisList(BoardNo).Any(x => x.Name == SelectedTAxis.Name))
                {
                    boardselected = frmMain.AdvantechModule;
                }
            }

            if (frmMain.MoonStfModule.GetBoardNo - 1 >= BoardNo)
            {
                if (frmMain.MoonStfModule.GetAxisList(BoardNo).Any(x => x.Name == SelectedTAxis.Name))
                {
                    boardselected = frmMain.MoonStfModule;
                }
            }

            if (frmMain.GalilModule.GetBoardNo - 1 >= BoardNo)
            {
                if (frmMain.GalilModule.GetAxisList(BoardNo).Any(x => x.Name == SelectedTAxis.Name))
                {
                    boardselected = frmMain.GalilModule;
                }
            }

            return boardselected;
        }

        public static int GetBoardNo(List<IMotionMain> motionboards)
        {
            int count = 0;
            foreach (var boards in motionboards)
            {
                count += boards.GetBoardNo;
            }
            return count;
        }

        

    }
}
