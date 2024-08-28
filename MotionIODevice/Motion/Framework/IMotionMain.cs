using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MotionIODevice.Common;

namespace MotionIODevice
{
    public interface IMotionMain
    {                
        bool InitMotion();

        EDeviceType MotionBoardType();

        IMotionControl GetMotionBoard(int BoardNo);
       
        List<IMotionControl> GetBoardList();
        
        List<TAxis> GetAxisList(int BoardNo);

        List<Common.TInput> GetInputList(int BoardNo);

        List<Common.TOutput> GetOutputList(int BoardNo);

        bool ReadBit(int BoardNo, int input);

        bool OutBit(int BoardNo, int bit, EOutputStatus state);

        bool SensLmtP(int BoardNo, int nAxis);

        bool SensLmtN(int BoardNo, int nAxis);
        
        bool SensHome(int BoardNo, int nAxis);

        bool SensInPos(int BoardNo, int nAxis);

        bool SensAlarm(int BoardNo, int nAxis);

        void SaveRecipe(string RecipeName);

        void SaveParameters(int BoardNo);

        int GetBoardNo { get; }

        double GetLogicPos(int board, int axisno);

        double GetRealPos(int board, int axisno);

        void GetMotorSpeedRange(int board, int axisno, ref double Min, ref double Max);

        void GetMotorAccelRange(int board, int axisno, ref double Min, ref double Max);
        
        void LoadRecipe(string RecipeName);
    }
}
