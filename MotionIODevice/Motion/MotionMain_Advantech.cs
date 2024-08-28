using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MotionIODevice.Common;
using Infrastructure;

namespace MotionIODevice
{
    public class MotionMain_Advantech : IMotionMain
    {
        private List<IMotionControl> motionBoards;
        private List<List<TInput>> inputList = new List<List<TInput>>();        //For UI use
        private List<List<TOutput>> outputList = new List<List<TOutput>>();     //For UI use    

        private Common.TDevice P1245Board_0;
        private Common.TDevice P1245Board_1;

        void BuildMotionCard()
        {
            motionBoards = new List<IMotionControl>() { };
            List<string> m_axisname = new List<string>() { };
            List<List<Common.TInput>> m_inputList = new List<List<Common.TInput>>() { };
            List<List<Common.TOutput>> m_outputList = new List<List<Common.TOutput>>() { };
            List<List<TRunModule>> m_RunModules = new List<List<TRunModule>>();

            BoardConfig();
            ModuleConfig(m_RunModules);

            #region Board 1                        

            #region Axis Name                       
            m_axisname.Add($"{Advantech1_Axis.InpnpLeft_X}");
            m_axisname.Add($"{Advantech1_Axis.InpnpLeft_Z}");
            m_axisname.Add($"{Advantech1_Axis.Rotation}");
            m_axisname.Add($"{Advantech1_Axis.Spare}");
            #endregion

            motionBoards.Add(new P1245(P1245Board_0, m_axisname, m_RunModules[0]));

            #endregion

            #region Board 2      
                       
            #region Axis Name
            m_axisname.Clear();
            m_inputList.Clear();
            m_outputList.Clear();

            m_axisname.Add($"{Advantech2_Axis.InpnpRight_X}");            
            m_axisname.Add($"{Advantech2_Axis.InpnpRight_Z}");
            m_axisname.Add($"{Advantech2_Axis.UnloadRotary}");
            m_axisname.Add($"Spare");
            #endregion           

            motionBoards.Add(new P1245(P1245Board_1, m_axisname, m_RunModules[1]));
            #endregion

            MotionIOConfig();

        }

        EDeviceType IMotionMain.MotionBoardType()
        {
            return P1245Board_0.Type;
        }

        IMotionControl IMotionMain.GetMotionBoard(int BoardNo)
        {
            return motionBoards[BoardNo];
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

        void ModuleConfig(List<List<TRunModule>> m_RunModules)
        {            
            List<TRunModule> P1245Board_0_RunModule = new List<TRunModule>();
            //P1245Board_0_RunModule.Add(new TRunModule(CardNumber.Advantech_1, (int)TotalModule.InPnp_Left, (int)Advantech1_Axis.InpnpLeft_X));
            //P1245Board_0_RunModule.Add(new TRunModule(CardNumber.Advantech_1, (int)TotalModule.InPnp_Left, (int)Advantech1_Axis.InpnpLeft_Z, true));
            //P1245Board_0_RunModule.Add(new TRunModule(CardNumber.Advantech_1, (int)TotalModule.TurnTable, (int)Advantech1_Axis.Rotation));
            //P1245Board_0_RunModule.Add(new TRunModule(CardNumber.Advantech_1, (int)TotalModule.Spare, (int)Advantech1_Axis.Spare)); ;

            #region Board 1 Axis 1
            //P1245Board_0_RunModule[0].AddPos("InpnpLeft_X Standby Pos");
            //P1245Board_0_RunModule[0].AddPos("InpnpLeft_X Pick Pos");
            //P1245Board_0_RunModule[0].AddPos("InpnpLeft_X Loading Station Pos");
            //P1245Board_0_RunModule[0].AddPos("InpnpLeft_X Unloading Station Pos");
            #endregion

            #region Board 1 Axis 2
            //P1245Board_0_RunModule[1].AddPos("InpnpLeft_Z Standby Pos");
            //P1245Board_0_RunModule[1].AddPos("InpnpLeft_Z Pick Pos");
            //P1245Board_0_RunModule[1].AddPos("InpnpLeft_Z Loading Station Pos");
            //P1245Board_0_RunModule[1].AddPos("InpnpLeft_Z Unloading Station Pos");
            //P1245Board_0_RunModule[1].AddPosInpnp_X("Top Track Wire Mesh Robot Pick Pos");
            #endregion

            #region Board 1 Axis 3
            //P1245Board_0_RunModule[2].AddPos("Rotation Zero Pos");
            //P1245Board_0_RunModule[2].AddPos("Rotation Index");
            #endregion

            #region Board 1 Axis 4

            #endregion
            m_RunModules.Add(P1245Board_0_RunModule);


            List<TRunModule> P1245Board_1_RunModule = new List<TRunModule>();
            //P1245Board_1_RunModule.Add(new TRunModule(CardNumber.Advantech_2, (int)TotalModule.InPnp_Right, (int)Advantech2_Axis.InpnpRight_X));
            //P1245Board_1_RunModule.Add(new TRunModule(CardNumber.Advantech_2, (int)TotalModule.InPnp_Right, (int)Advantech2_Axis.InpnpRight_Z, true));
            //P1245Board_1_RunModule.Add(new TRunModule(CardNumber.Advantech_2, (int)TotalModule.UnloadingRtr, (int)Advantech2_Axis.UnloadRotary));
            //P1245Board_1_RunModule.Add(new TRunModule(CardNumber.Advantech_2, (int)TotalModule.Spare, (int)Advantech2_Axis.Spare));

            #region Board 2 Axis 1
            //P1245Board_1_RunModule[0].AddPos("InpnpRight_X Standby Pos");
            //P1245Board_1_RunModule[0].AddPos("InpnpRight_X Pick Pos");
            //P1245Board_1_RunModule[0].AddPos("InpnpRight_X Loading Station Pos");
            //P1245Board_1_RunModule[0].AddPos("InpnpRight_X Unloading Station Pos");
            #endregion

            #region Board 2 Axis 2
            //P1245Board_1_RunModule[1].AddPos("InpnpRight_Z Standby Pos");
            //P1245Board_1_RunModule[1].AddPos("InpnpRight_Z Pick Pos");
            //P1245Board_1_RunModule[1].AddPos("InpnpRight_Z Loading Station Pos");
            //P1245Board_1_RunModule[1].AddPos("InpnpRight_Z Unloading Station Pos");
            //P1245Board_0_RunModule[1].AddPosInpnp_X("Top Track Wire Mesh Robot Pick Pos");
            #endregion

            #region Board 2 Axis 3
            //P1245Board_1_RunModule[2].AddPos("UnloadRotary Standby 0 Degree");
            //P1245Board_1_RunModule[2].AddPos("UnloadRotary Standby 180 Degree");
            #endregion
            //m_RunModules.Add(P1245Board_1_RunModule);
        }

        void MotionIOConfig()
        {
            #region Board 1
            List<TInput> ipList = new List<TInput>();

            //Axis 1
            //ipList.Add(new Common.TInput(P1245Board_0, 0x00, 0x0002, "", "IN1"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x00, 0x0004, "", "IN2"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x00, 0x0008, "", "Home"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x00, 0x0040, "", "Inpos"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x00, 0x0080, "", "Alarm"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x00, 0x0400, "", "LmtP"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x00, 0x0800, "", "LmtN"));

            //Axis 2
            //ipList.Add(new Common.TInput(P1245Board_0, 0x01, 0x0002, "", "IN1"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x01, 0x0004, "", "IN2"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x01, 0x0008, "", "Home"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x01, 0x0040, "", "Inpos"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x01, 0x0080, "", "Alarm"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x01, 0x0400, "", "LmtP"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x01, 0x0800, "", "LmtN"));

            //Axis 3
            //ipList.Add(new Common.TInput(P1245Board_0, 0x02, 0x0002, "", "IN1"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x02, 0x0004, "", "IN2"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x02, 0x0008, "", "Home"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x02, 0x0040, "", "Inpos"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x02, 0x0080, "", "Alarm"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x02, 0x0400, "", "LmtP"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x02, 0x0800, "", "LmtN"));

            //Axis 4
            //ipList.Add(new Common.TInput(P1245Board_0, 0x03, 0x0002, "", "Spare"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x03, 0x0004, "", "Spare"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x03, 0x0008, "", "Spare"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x03, 0x0040, "", "Spare"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x03, 0x0080, "", "Spare"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x03, 0x0400, "", "Spare"));
            //ipList.Add(new Common.TInput(P1245Board_0, 0x03, 0x0800, "", "Spare"));
            //inputList.Add(ipList);

            List<TOutput> opList = new List<TOutput>();
            //opList.Add(new Common.TOutput(P1245Board_0, 0x00, 0x01, "", "Out4"));
            //opList.Add(new Common.TOutput(P1245Board_0, 0x00, 0x02, "", "Out5"));
            //opList.Add(new Common.TOutput(P1245Board_0, 0x00, 0x04, "", "Motor On"));
            //opList.Add(new Common.TOutput(P1245Board_0, 0x00, 0x08, "", "Alarm Clear"));

            //opList.Add(new Common.TOutput(P1245Board_0, 0x01, 0x01, "", "Out4"));
            //opList.Add(new Common.TOutput(P1245Board_0, 0x01, 0x02, "", "Brake"));
            //opList.Add(new Common.TOutput(P1245Board_0, 0x01, 0x04, "", "Motor On"));
            //opList.Add(new Common.TOutput(P1245Board_0, 0x01, 0x08, "", "Alarm Clear"));

            //opList.Add(new Common.TOutput(P1245Board_0, 0x02, 0x02, "", "Out4"));
            //opList.Add(new Common.TOutput(P1245Board_0, 0x02, 0x02, "", "Out5"));
            //opList.Add(new Common.TOutput(P1245Board_0, 0x02, 0x04, "", "Motor On"));
            //opList.Add(new Common.TOutput(P1245Board_0, 0x02, 0x08, "", "Alarm Clear"));

            //opList.Add(new Common.TOutput(P1245Board_0, 0x03, 0x03, "", "Spare"));
            //opList.Add(new Common.TOutput(P1245Board_0, 0x03, 0x03, "", "Spare"));
            //opList.Add(new Common.TOutput(P1245Board_0, 0x03, 0x04, "", "Spare"));
            //opList.Add(new Common.TOutput(P1245Board_0, 0x03, 0x08, "", "Spare"));
            //outputList.Add(opList);
            #endregion

            #region Board 2

            List<TInput> ipList2 = new List<TInput>();
            //Axis 1
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x00, 0x0002, "", "IN1"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x00, 0x0004, "", "IN2"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x00, 0x0008, "", "Home"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x00, 0x0040, "", "Inpos"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x00, 0x0080, "", "Alarm"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x00, 0x0400, "", "LmtP"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x00, 0x0800, "", "LmtN"));

            //Axis 2
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x01, 0x0002, "", "IN1"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x01, 0x0004, "", "IN2"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x01, 0x0008, "", "Home"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x01, 0x0040, "", "Inpos"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x01, 0x0080, "", "Alarm"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x01, 0x0400, "", "LmtP"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x01, 0x0800, "", "LmtN"));

            //Axis 3
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x02, 0x0002, "", "IN1"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x02, 0x0004, "", "IN2"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x02, 0x0008, "", "Home"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x02, 0x0040, "", "Inpos"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x02, 0x0080, "", "Alarm"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x02, 0x0400, "", "LmtP"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x02, 0x0800, "", "LmtN"));

            //Axis 4
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x03, 0x0002, "", "Spare"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x03, 0x0004, "", "Spare"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x03, 0x0008, "", "Spare"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x03, 0x0040, "", "Spare"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x03, 0x0080, "", "Spare"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x03, 0x0400, "", "Spare"));
            //ipList2.Add(new Common.TInput(P1245Board_1, 0x03, 0x0800, "", "Spare"));
            //inputList.Add(ipList2);

            List<TOutput> opList2 = new List<TOutput>();
            //opList2.Add(new Common.TOutput(P1245Board_1, 0x00, 0x01, "", "Out4"));
            //opList2.Add(new Common.TOutput(P1245Board_1, 0x00, 0x02, "", "Out5"));
            //opList2.Add(new Common.TOutput(P1245Board_1, 0x00, 0x04, "", "Motor On"));
            //opList2.Add(new Common.TOutput(P1245Board_1, 0x00, 0x08, "", "Alarm Clear"));

            //opList2.Add(new Common.TOutput(P1245Board_1, 0x01, 0x01, "", "Out4"));
            //opList2.Add(new Common.TOutput(P1245Board_1, 0x01, 0x02, "", "Brake"));
            //opList2.Add(new Common.TOutput(P1245Board_1, 0x01, 0x04, "", "Motor On"));
            //opList2.Add(new Common.TOutput(P1245Board_1, 0x01, 0x08, "", "Alarm Clear"));

            //opList2.Add(new Common.TOutput(P1245Board_1, 0x02, 0x01, "", "Out4"));
            //opList2.Add(new Common.TOutput(P1245Board_1, 0x02, 0x02, "", "Out5"));
            //opList2.Add(new Common.TOutput(P1245Board_1, 0x02, 0x04, "", "Motor On"));
            //opList2.Add(new Common.TOutput(P1245Board_1, 0x02, 0x08, "", "Alarm Clear"));

            //opList2.Add(new Common.TOutput(P1245Board_1, 0x03, 0x01, "", "Spare"));
            //opList2.Add(new Common.TOutput(P1245Board_1, 0x03, 0x02, "", "Spare"));
            //opList2.Add(new Common.TOutput(P1245Board_1, 0x03, 0x04, "", "Spare"));
            //opList2.Add(new Common.TOutput(P1245Board_1, 0x03, 0x08, "", "Spare"));
            //outputList.Add(opList2);
            #endregion

        }
        void BoardConfig()
        {
            //P1245Board_0 = new Common.TDevice(Common.EDeviceType.P1245, 0, "", "P1245_0", true);
            //P1245Board_1 = new Common.TDevice(Common.EDeviceType.P1245, 1, "", "P1245_1", true);
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
            return motionBoards[board].GetRealPos(axisno);
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
