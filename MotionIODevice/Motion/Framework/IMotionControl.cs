using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using static MotionIODevice.Common;

namespace MotionIODevice
{
    public interface IMotionControl
    {

        bool OpenBoard();

        bool CloseBoard();        

        bool ReadBit(int bit);

        bool OutBit(int bit, EOutputStatus status);

        bool Home(int Axis, HomingType motorType );

        bool MoveAbs(int Axis, double Value, int Timeout);

        bool MoveAbsSpd(int Axis, double Value, int spdselection ,int Timeout);

        bool MoveRel(int Axis, double Value, int Timeout);

        bool MoveRelSpd(int Axis, double Value, int spdselection, int Timeout);

        bool MoveAbsLine(int Axis1, int Axis2, double Value1, double Value2, int Timeout);

        bool MoveRelLine(int Axis1, int Axis2, double Value1, double Value2, int Timeout);    
        
        bool Jog(int Axis, bool bPositive, bool bSlow);

        bool Jog(int Axis, bool bPositive, int Speed);

        bool ForceStop(int Axis);

        bool Servo(int Axis, bool status);

        bool ReadInput(int Axis, int Input);        

        bool SetLogicPos(int Axis, double Pos);

        bool SetRealPos(int Axis, double Pos); // ZY

        double GetRealPos(int Axis);

        double GetLogicPos(int Axis);

        int GetAxisNum { get; }

        int GetInputNum { get; }

        int GetOutputNum { get; }

        List<TAxis> AxisList { get; }

        void GetMotorSpeedRange(int axisNo, ref double Min, ref double Max);

        void GetMotorAccelRange(int axisNo, ref double Min, ref double Max);

        bool SensLmtP(int nAxis);

        bool SensLmtN(int nAxis);

        bool SensHome(int nAxis);

        bool SensInPos(int nAxis);

        bool SensAlarm(int nAxis);

        void MotorAlmReset(int nAxis);

        void SaveRecipePositon(string RecipeName);

        void LoadRecipePosition(string RecipeName);

        void SaveMotorParameters();
        
        bool MoveAbsLine(int axisNo1, int axisNo2, double dValue1, double dValue2);
        
        bool MoveRelLine(int axisNo1, int axisNo2, double dValue1, double dValue2);
        
        void AxisBusy(int axisNo, ref bool bAxisWait);                     
       

    }
}
