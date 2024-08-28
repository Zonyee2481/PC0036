using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MotionIODevice.Common;
using Infrastructure;

namespace MotionIODevice
{
    public class MotionMain_MoonsSTF : IMotionMain
    {
        private List<IMotionControl> motionBoards;
        private List<TAxis> _AxisList = new List<TAxis>();
        private List<List<TInput>> inputList = new List<List<TInput>>();
        private List<List<TOutput>> outputList = new List<List<TOutput>>();
        private List<TRunModule> _RunModules = new List<TRunModule>();
        private Common.TDevice Axis1 = null;
        private Common.TDevice Axis2 = null;
        private Common.TDevice Axis3 = null;
        private Common.TDevice Axis4 = null;

        void BuildMotionCard()
        {
            motionBoards = new List<IMotionControl>();
            //Axis1 = new Common.TDevice(Common.EDeviceType.MoonsSTF, "192.168.0.40", 1, "", "MoonsStf_01");
            //Axis2 = new Common.TDevice(Common.EDeviceType.MoonsSTF, "192.168.0.50", 2, "", "MoonsStf_02");
            //Axis3 = new Common.TDevice(Common.EDeviceType.MoonsSTF, "192.168.0.60", 3, "", "MoonsStf_03");
            //Axis4 = new Common.TDevice(Common.EDeviceType.MoonsSTF, "192.168.0.70", 4, "", "MoonsStf_04");

            ModuleConfig();
            MotionIOConfig();
            //_AxisList.Add(new Common.TAxis(Axis1, 1, 0x00, "", $"{eSCLLibHelper_Axis.LoadingStnZ}"));
            //_AxisList.Add(new Common.TAxis(Axis2, 2, 0x01, "", $"{eSCLLibHelper_Axis.UnloadStnZ}"));
            //_AxisList.Add(new Common.TAxis(Axis3, 3, 0x02, "", $"{eSCLLibHelper_Axis.LaserTopZ}"));
            //_AxisList.Add(new Common.TAxis(Axis4, 4, 0x03, "", $"{eSCLLibHelper_Axis.LaserBtmZ}"));


            motionBoards.Add(new MoonSTF(_AxisList, _RunModules));
        }


        EDeviceType IMotionMain.MotionBoardType()
        {
            return Axis1.Type;
        }

        bool IMotionMain.InitMotion()
        {
            BuildMotionCard();

            foreach (var motionBoard in motionBoards)
            {
                motionBoard.OpenBoard();
            }

            return true;
        }
        
        IMotionControl IMotionMain.GetMotionBoard(int BoardNo)
        {
            return motionBoards[BoardNo];
        }
        
        public List<Common.TInput> GetInputList(int BoardNo)
        {
            return inputList[BoardNo];
        }

        public List<Common.TOutput> GetOutputList(int BoardNo)
        {
            return outputList[BoardNo];
        }
        
        public List<TAxis> GetAxisList(int BoardNo)
        {
            return motionBoards[BoardNo].AxisList;
        }

        private void ModuleConfig()
        {
            //_RunModules.Add(new TRunModule(CardNumber.eSCLLibHelper, (int)TotalModule.Stn0, (int)eSCLLibHelper_Axis.LoadingStnZ, true));
            //_RunModules.Add(new TRunModule(CardNumber.eSCLLibHelper,(int)TotalModule.UnloadingStn, (int)eSCLLibHelper_Axis.UnloadStnZ, true));
            //_RunModules.Add(new TRunModule(CardNumber.eSCLLibHelper,(int)TotalModule.TopLaserMark, (int)eSCLLibHelper_Axis.LaserTopZ, true));
            //_RunModules.Add(new TRunModule(CardNumber.eSCLLibHelper,(int)TotalModule.BtmLaserMark, (int)eSCLLibHelper_Axis.LaserBtmZ, true));

            #region Axis 1
            //_RunModules[0].AddPos("Stn 0 Up To Pick From Mag Pos");
            //_RunModules[0].AddPos("Stn 0 Up To Pick Mag Indx Pos");
            //_RunModules[0].AddPos("Stn 0 Down Cylinder Close Pos");
            //_RunModules[0].AddPos("Stn 0 Down Cylinder TurnTbl Pos");
            //_RunModules[0].AddPos("Stn 0 Down Cylinder");
            #endregion

            #region Axis 2
            //_RunModules[1].AddPos("Unloading Stn Up to TurnTbl Pos");
            //_RunModules[1].AddPos("Unloading Stn Up to Mag");
            //_RunModules[1].AddPos("Unloading Stn Down");

            #endregion

            #region Axis 3
            //_RunModules[2].AddPos("Laser Top Laser Pos");
            #endregion

            #region Axis 2
            //_RunModules[3].AddPos("Laser Btm Laser Pos");
            #endregion

        }

        private void MotionIOConfig()
        {
            List<TInput> ipList = new List<TInput>();

            //Axis 1
            //ipList = new List<TInput>();
            //ipList.Add(new Common.TInput(Axis1, 0x00, 1, "", "IN1"));
            //ipList.Add(new Common.TInput(Axis1, 0x00, 2, "", "IN2"));
            //ipList.Add(new Common.TInput(Axis1, 0x00, 3, "", "P_LIMIT"));
            //ipList.Add(new Common.TInput(Axis1, 0x00, 4, "", "N_LIMIT"));
            //ipList.Add(new Common.TInput(Axis1, 0x00, 5, "", "IN3"));
            //ipList.Add(new Common.TInput(Axis1, 0x00, 6, "", "IN4"));
            //ipList.Add(new Common.TInput(Axis1, 0x00, 7, "", "SENSE_HOME"));
            //ipList.Add(new Common.TInput(Axis1, 0x00, 8, "", "Spare"));

            //Axis 2
            //ipList.Add(new Common.TInput(Axis2, 0x01, 1, "", "IN1"));
            //ipList.Add(new Common.TInput(Axis2, 0x01, 2, "", "IN2"));
            //ipList.Add(new Common.TInput(Axis2, 0x01, 3, "", "P_LIMIT"));
            //ipList.Add(new Common.TInput(Axis2, 0x01, 4, "", "N_LIMIT"));
            //ipList.Add(new Common.TInput(Axis2, 0x01, 5, "", "IN3"));
            //ipList.Add(new Common.TInput(Axis2, 0x01, 6, "", "IN4"));
            //ipList.Add(new Common.TInput(Axis2, 0x01, 7, "", "SENSE_HOME"));
            //ipList.Add(new Common.TInput(Axis2, 0x01, 8, "", "IN5"));

            //Axis 3
            //ipList.Add(new Common.TInput(Axis3, 0x02, 1, "", "IN1"));
            //ipList.Add(new Common.TInput(Axis3, 0x02, 2, "", "IN2"));
            //ipList.Add(new Common.TInput(Axis3, 0x02, 3, "", "P_LIMIT"));
            //ipList.Add(new Common.TInput(Axis3, 0x02, 4, "", "N_LIMIT"));
            //ipList.Add(new Common.TInput(Axis3, 0x02, 5, "", "IN3"));
            //ipList.Add(new Common.TInput(Axis3, 0x02, 6, "", "IN4"));
            //ipList.Add(new Common.TInput(Axis3, 0x02, 7, "", "SENSE_HOME"));
            //ipList.Add(new Common.TInput(Axis3, 0x02, 8, "", "IN5"));

            //Axis 4
            //ipList.Add(new Common.TInput(Axis4, 0x03, 1, "", "IN1"));
            //ipList.Add(new Common.TInput(Axis4, 0x03, 2, "", "IN2"));
            //ipList.Add(new Common.TInput(Axis4, 0x03, 3, "", "P_LIMIT"));
            //ipList.Add(new Common.TInput(Axis4, 0x03, 4, "", "N_LIMIT"));
            //ipList.Add(new Common.TInput(Axis4, 0x03, 5, "", "IN3"));
            //ipList.Add(new Common.TInput(Axis4, 0x03, 6, "", "IN4"));
            //ipList.Add(new Common.TInput(Axis4, 0x03, 7, "", "SENSE_HOME"));
            //ipList.Add(new Common.TInput(Axis4, 0x03, 8, "", "IN5"));
            //inputList.Add(ipList);

            List<TOutput> opList = new List<TOutput>();
            //opList.Add(new Common.TOutput(Axis1, 0x00, 1, "", "Out4"));
            //opList.Add(new Common.TOutput(Axis1, 0x00, 2, "", "Brake"));
            //opList.Add(new Common.TOutput(Axis1, 0x00, 3, "", "Out5"));
            //opList.Add(new Common.TOutput(Axis1, 0x00, 4, "", "Out6"));


            //opList.Add(new Common.TOutput(Axis2, 0x01, 1, "", "Out4"));
            //opList.Add(new Common.TOutput(Axis2, 0x01, 2, "", "Brake"));
            //opList.Add(new Common.TOutput(Axis2, 0x01, 3, "", "Out5"));
            //opList.Add(new Common.TOutput(Axis2, 0x01, 4, "", "Out6"));


            //opList.Add(new Common.TOutput(Axis3, 0x02, 1, "", "Out4"));
            //opList.Add(new Common.TOutput(Axis3, 0x02, 2, "", "Brake"));
            //opList.Add(new Common.TOutput(Axis3, 0x02, 3, "", "Out5"));
            //opList.Add(new Common.TOutput(Axis3, 0x02, 4, "", "Out6"));


            //opList.Add(new Common.TOutput(Axis4, 0x03, 1, "", "Out4"));
            //opList.Add(new Common.TOutput(Axis4, 0x03, 2, "", "Brake"));
            //opList.Add(new Common.TOutput(Axis4, 0x03, 3, "", "Out5"));
            //opList.Add(new Common.TOutput(Axis4, 0x03, 4, "", "Out6"));
            //outputList.Add(opList);
        }

        public bool ReadBit(int BoardNo, int input)
        {
            return motionBoards[BoardNo].ReadBit(input);
        }

        public bool OutBit(int BoardNo, int bit, EOutputStatus state)
        {
            bool bOn = motionBoards[BoardNo].OutBit(bit, state);
            return bOn;
        }

        public int GetBoardNo
        {
            get { return motionBoards.Count; }
        }

        public List<IMotionControl> GetBoardList()
        {
            return motionBoards;
        }

        public double GetLogicPos(int board, int axisno)
        {
            return motionBoards[board].GetLogicPos(axisno);
        }

        public double GetRealPos(int board, int axisno)
        {
            throw new NotImplementedException();
        }

        public bool SensLmtP(int BoardNo, int nAxis)
        {
            return motionBoards[BoardNo].SensLmtP(nAxis);
        }

        public bool SensLmtN(int BoardNo, int nAxis)
        {
            return motionBoards[BoardNo].SensLmtN(nAxis);
        }

        public bool SensHome(int BoardNo, int nAxis)
        {
            return motionBoards[BoardNo].SensHome(nAxis);

        }

        public bool SensInPos(int BoardNo, int nAxis)
        {
            return motionBoards[BoardNo].SensInPos(nAxis);
        }

        public bool SensAlarm(int BoardNo, int nAxis)
        {
            return motionBoards[BoardNo].SensAlarm(nAxis);
        }

        public void SaveRecipe(string RecipeName)
        {
            foreach (var motionBoard in motionBoards)
            {
                motionBoard.SaveRecipePositon(RecipeName);
            }
        }

        public void SaveParameters(int BoardNo)
        {
            motionBoards[BoardNo].SaveMotorParameters();
        }

        public void GetMotorSpeedRange(int board, int axisno, ref double Min, ref double Max)
        {
            try
            {
                motionBoards[board].GetMotorSpeedRange(axisno, ref Min, ref Max);
            }
            catch (Exception Ex)
            {
            }
        }
        
        public void GetMotorAccelRange(int board, int axisno, ref double Min, ref double Max)
        {
            try
            {
                motionBoards[board].GetMotorAccelRange(axisno, ref Min, ref Max);
            }
            catch (Exception Ex)
            {

            }
        }

        public void LoadRecipe(string RecipeName)
        {
            foreach (var motionBoard in motionBoards)
            {
                motionBoard.LoadRecipePosition(RecipeName);
            }
        }
    }
}

