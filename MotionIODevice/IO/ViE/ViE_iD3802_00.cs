using Infrastructure;
using MotionIODevice.IO.ViE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MotionIODevice.IO
{
    public class ViE_iD3802_00 : IBoardIO

    {
        public enum eCardType { Input, Output };
        private eCardType cardType = eCardType.Input;
        private ushort uBoardNo;
        private List<TInput> InputList = new List<TInput>();
        private List<TOutput> OutputList = new List<TOutput>();
        private List<TInput_ViE> ViE_InputList = new List<TInput_ViE>();
        private List<TOutput_ViE> OutputList_ViE = new List<TOutput_ViE>();
        private uint[,] OutStatus = new uint[ViE.ViE.MAX_BOARD_NUMBER, ViE.ViE.MAX_MODULE_NUMBER];
        private List<TModuleIO> IOModuleList = new List<TModuleIO>();

        public List<TInput> GetInputList
        {
            get { return InputList; }
        }

        public List<TOutput> GetOutputList
        {
            get { return OutputList; }
        }

        public List<TModuleIO> GetIOModuleList
        {
            get { return IOModuleList; }
        }

        public bool IsBoardOpened { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ViE_iD3802_00(ushort shBoardNo, List<TModuleIO> iomoduleList)
        {
            uBoardNo = shBoardNo;
            IOModuleList = iomoduleList;
            InitIO();
        }

        public void InitIO()
        {
            foreach (TModuleIO iomodule in IOModuleList)
            {
                if (iomodule.ModuleType == TModuleType.Input)
                {
                    InputList.Clear();
                    InitInput(iomodule);
                }
                else
                {
                    OutputList.Clear();
                    InitOutput(iomodule);
                }
            }
        }

        public void InitInput(TModuleIO ioModule)
        {
            ushort usModuleID = ioModule.ModuleID;           

            for (int x = 0; x < 4; x++)
            {
                // Input
                InputList.Add(new TInput(uBoardNo, (0 + (x * 8)), (byte)uBoardNo, usModuleID, "", "", ""));
                InputList.Add(new TInput(uBoardNo, (1 + (x * 8)), (byte)uBoardNo, usModuleID, "", "", ""));
                InputList.Add(new TInput(uBoardNo, (2 + (x * 8)), (byte)uBoardNo, usModuleID, "", "", ""));
                InputList.Add(new TInput(uBoardNo, (3 + (x * 8)), (byte)uBoardNo, usModuleID, "", "", ""));
                InputList.Add(new TInput(uBoardNo, (4 + (x * 8)), (byte)uBoardNo, usModuleID, "", "", ""));
                InputList.Add(new TInput(uBoardNo, (5 + (x * 8)), (byte)uBoardNo, usModuleID, "", "", ""));
                InputList.Add(new TInput(uBoardNo, (6 + (x * 8)), (byte)uBoardNo, usModuleID, "", "", ""));
                InputList.Add(new TInput(uBoardNo, (7 + (x * 8)), (byte)uBoardNo, usModuleID, "", "", ""));
            }
        }

        public void InitOutput(TModuleIO ioModule) 
        {
            ushort usModuleID = ioModule.ModuleID;

            for (int x = 0; x < 4; x++)
            {
                // Output
                OutputList.Add(new TOutput(uBoardNo, (0 + (x * 8)), (byte)uBoardNo, usModuleID, "", "", ""));
                OutputList.Add(new TOutput(uBoardNo, (1 + (x * 8)), (byte)uBoardNo, usModuleID, "", "", ""));
                OutputList.Add(new TOutput(uBoardNo, (2 + (x * 8)), (byte)uBoardNo, usModuleID, "", "", ""));
                OutputList.Add(new TOutput(uBoardNo, (3 + (x * 8)), (byte)uBoardNo, usModuleID, "", "", ""));
                OutputList.Add(new TOutput(uBoardNo, (4 + (x * 8)), (byte)uBoardNo, usModuleID, "", "", ""));
                OutputList.Add(new TOutput(uBoardNo, (5 + (x * 8)), (byte)uBoardNo, usModuleID, "", "", ""));
                OutputList.Add(new TOutput(uBoardNo, (6 + (x * 8)), (byte)uBoardNo, usModuleID, "", "", ""));
                OutputList.Add(new TOutput(uBoardNo, (7 + (x * 8)), (byte)uBoardNo, usModuleID, "", "", ""));
            }
        }

        public bool UpdateInput(ref TInput Input)
        {
            if (!ViEBoard.isBoardOpened) { return false; }
            try
            {
                uint DIVal = 0;
                bool bSuceed = ViEBoard.UpdateInput(Input.AxisPort, Input.Mask, Input.Bit, ref DIVal);
                for (int i = 0; i < InputList.Count; i++)
                {
                    if (i == Input.Bit)
                    {
                        UInt32 uiBitData = 0;
                        uiBitData = DIVal;
                        uiBitData >>= i;
                        uiBitData &= 0x0001;
                        if (uiBitData == 1)
                        { Input.Status = true; }
                        else
                        { Input.Status = false; }
                        break;
                    }                    
                }
                return bSuceed;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CloseBoard()
        {
            if (ViEBoard.isBoardOpened) 
            {
                ViEBoard.CloseBoard();
            }
            return true;
        }

        public bool OpenBoard()
        {
            if (!ViEBoard.isBoardOpened)
            {
                ViEBoard.OpenBoard();
            }
            return true;
        }

        public bool OutBit(int eBit, TOutputStatus state)
        {
            int bit = eBit;
            if (!ViEBoard.isBoardOpened) { return false; };
            TOutput data = OutputList[bit];

            switch (state)
            {
                case TOutputStatus.Hi:
                    {
                        UpdateOutputHi(ref data);
                    }
                    break;
                case TOutputStatus.Lo:
                    {
                        UpdateOutputLo(ref data);
                    }
                    break;
            }
            return data.Status;
        }

        public bool ReadBit(int eBit)
        {
            int bit = eBit;
            if (!ViEBoard.isBoardOpened) { return false; }
            try
            {
                TInput data = InputList[bit];
                UpdateInput(ref data);
                return data.Status;
            }
            catch (Exception ex) { }
            return false;
        }

        public bool UpdateOutputHi(ref TOutput Output)
        {
            if (!ViEBoard.isBoardOpened) { return false; }

            try
            {
                //OutStatus[uBoardNo, Output.Mask] |= (byte)(1u << OutStatus[uBoardNo, Output.Mask]);
                OutStatus[uBoardNo, Output.Mask] |= (1u << Output.Bit);
                bool bSuceed = ViEBoard.UpdateOutput(Output.AxisPort, (ushort)Output.Mask, ref OutStatus[uBoardNo, Output.Mask]);
                Output.Status = true;
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool UpdateOutputLo(ref TOutput Output)
        {
            if (!ViEBoard.isBoardOpened) { return false; }

            try
            {
                //OutStatus[uBoardNo, Output.Mask] &= (byte)(1u << OutStatus[uBoardNo, Output.Mask] & Output.Mask);
                OutStatus[uBoardNo, Output.Mask] &= ~(1u << Output.Bit);
                bool bSuceed = ViEBoard.UpdateOutput(Output.AxisPort, (ushort)Output.Mask, ref OutStatus[uBoardNo, Output.Mask]);
                Output.Status = false;
            }
            catch
            {
                return false;
            }
            return true;
        }
        
        public void UpdateInputInfo(int bit, TInput input)
        {
            InputList[bit] = input;
        }

        public void UpdateViEInputInfo(int bit, TInput_ViE input)
        {
            ViE_InputList[bit] = input;
        }

        public void UpdateOutInfo(int bit, TOutput output)
        {
            OutputList[bit] = output;
        }

        public void UpdateViEOutInfo(int bit, TOutput_ViE output)
        {
            OutputList_ViE[bit] = output;
        }
    }
}
