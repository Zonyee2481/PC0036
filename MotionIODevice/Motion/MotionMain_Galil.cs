using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MotionIODevice.Common;
using Infrastructure;

namespace MotionIODevice
{
    public class MotionMain_Galil : IMotionMain
    {
        private List<IMotionControl> motionBoards;
        private List<List<TInput>> inputList = new List<List<TInput>>();        //For UI use
        private List<List<TOutput>> outputList = new List<List<TOutput>>();     //For UI use   

        private Common.TDevice B140Board_0;

        void BuildMotionCard()
        {
            motionBoards = new List<IMotionControl>() { };
            List<string> m_axisname = new List<string>() { };
            List<List<Common.TInput>> m_inputList = new List<List<Common.TInput>>() { };
            List<List<Common.TOutput>> m_outputList = new List<List<Common.TOutput>>() { };
            List<List<TRunModule>> m_RunModules = new List<List<TRunModule>>();

            BoardConfig();
            ModuleConfig(m_RunModules);

            #region Axis Name
            m_axisname.Clear();
            m_inputList.Clear();
            m_outputList.Clear();

            m_axisname.Add($"{Galil1_Axis.GalilAxis_1}");
            m_axisname.Add($"{Galil1_Axis.GalilAxis_2}");
            m_axisname.Add($"{Galil1_Axis.Spare}");
            m_axisname.Add($"{Galil1_Axis.Spare}");
            #endregion

            motionBoards.Add(new B140(B140Board_0, m_axisname, m_RunModules[0]));

            MotionIOConfig();
        }

        void ModuleConfig(List<List<TRunModule>> m_RunModules)
        {
            List<TRunModule> B140Board_0_RunMoodule = new List<TRunModule>();
            //B140Board_0_RunMoodule.Add(new TRunModule(CardNumber.Galil_1, (int)TotalModule.Robot, (int)Galil1_Axis.GalilAxis_1, true));
            //B140Board_0_RunMoodule.Add(new TRunModule(CardNumber.Galil_1, (int)TotalModule.Robot, (int)Galil1_Axis.GalilAxis_2));
            //B140Board_0_RunMoodule.Add(new TRunModule(CardNumber.Galil_1, (int)TotalModule.Spare, (int)Galil1_Axis.Spare));
            //B140Board_0_RunMoodule.Add(new TRunModule(CardNumber.Galil_1, (int)TotalModule.Spare, (int)Galil1_Axis.Spare));
            
            #region Board 1 Axis 1
            //B140Board_0_RunMoodule[0].AddPos("Galil Axis 1 XXXX Pos");
            #endregion

            #region Board 1 Axis 2
            //B140Board_0_RunMoodule[1].AddPos("Galil Axis 2 XXXX Pos");
            #endregion

            //m_RunModules.Add(B140Board_0_RunMoodule);
        }

        void MotionIOConfig()
        {
            #region Board 1
            List<TInput> ipList = new List<TInput>();

            //ipList.Add(new Common.TInput(B140Board_0, 1, 0x00, 0x0002, "", "Home"));
            //ipList.Add(new Common.TInput(B140Board_0, 2, 0x00, 0x0004, "", "Inpos"));
            //ipList.Add(new Common.TInput(B140Board_0, 3, 0x00, 0x0008, "", "Alarm"));
            //ipList.Add(new Common.TInput(B140Board_0, 4, 0x00, 0x0040, "", "LmtP"));
            //ipList.Add(new Common.TInput(B140Board_0, 5, 0x00, 0x0080, "", "LmtN"));

            //ipList.Add(new Common.TInput(B140Board_0, 1, 0x01, 0x0002, "", "Home"));
            //ipList.Add(new Common.TInput(B140Board_0, 2, 0x01, 0x0004, "", "Inpos"));
            //ipList.Add(new Common.TInput(B140Board_0, 3, 0x01, 0x0008, "", "Alarm"));
            //ipList.Add(new Common.TInput(B140Board_0, 4, 0x01, 0x0040, "", "LmtP"));
            //ipList.Add(new Common.TInput(B140Board_0, 5, 0x01, 0x0080, "", "LmtN"));

            //inputList.Add(ipList);

            List<TOutput> opList = new List<TOutput>();

            //opList.Add(new Common.TOutput(B140Board_0, 1, 0x00, 0x01, "", "Motor On"));
            //opList.Add(new Common.TOutput(B140Board_0, 2, 0x00, 0x02, "", "Alarm Clear"));

            //opList.Add(new Common.TOutput(B140Board_0, 1, 0x01, 0x01, "", "Motor On"));
            //opList.Add(new Common.TOutput(B140Board_0, 2, 0x01, 0x02, "", "Alarm Clear"));

            //outputList.Add(opList);
            #endregion

        }

        void BoardConfig()
        {
            //B140Board_0 = new Common.TDevice(Common.EDeviceType.B140, "192.168.1.11", 0, "", "B140_0");
        }

        public int GetBoardNo
        {
            get { return motionBoards.Count; }
        }
        
        public List<IMotionControl> GetBoardList()
        {
            return motionBoards;
        }

        IMotionControl IMotionMain.GetMotionBoard(int BoardNo)
        {
            return motionBoards[BoardNo];
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

        public void AddBoard(IMotionControl motionBoard)
        {
            motionBoards.Add(motionBoard);
        }

        public double GetLogicPos(int board, int axisno)
        {
            return motionBoards[board].GetLogicPos(axisno);
        }

        public double GetRealPos(int board, int axisno)
        {
            return motionBoards[board].GetRealPos(axisno);
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

        EDeviceType IMotionMain.MotionBoardType()
        {
            return B140Board_0.Type;
        }

        public bool OutBit(int BoardNo, int bit, Common.EOutputStatus state)
        {
            bool bOn = motionBoards[BoardNo].OutBit(bit, state);
            return bOn;
        }

        public bool ReadBit(int BoardNo, int input)
        {
            return motionBoards[BoardNo].ReadBit(input);
        }

        public void SaveRecipe(string RecipeName)
        {
            foreach (var motionBoard in motionBoards)
            {
                motionBoard.SaveRecipePositon(RecipeName);
            }
        }

        public void LoadRecipe(string RecipeName)
        {
            foreach (var motionBoard in motionBoards)
            {
                motionBoard.LoadRecipePosition(RecipeName);
            }
        }

        public void SaveParameters(int BoardNo)
        {
            motionBoards[BoardNo].SaveMotorParameters();
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
    }
}
