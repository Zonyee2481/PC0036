using MotionIODevice.IO.UniDAQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotionIODevice.IO
{
    public class UniDAQ_PEX_P32C32U : IBoardIO
    {
        private List<TInput> InputList = new List<TInput>();
        private List<TOutput> OutputList = new List<TOutput>();
        private byte[,] OutStatus = new byte[16, 4];
        private ushort uBoardNo;

        public List<TInput> GetInputList
        {
            get { return InputList; }
        }

        public List<TOutput> GetOutputList
        {
            get { return OutputList; }
        }

        public bool IsBoardOpened { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void UpdateInputInfo(int bit, TInput input)
        {
            InputList[bit] = input;
            //InputList[bit].Label = strLabel;
            //InputList[bit].Name = strName; 
        }

        public void UpdateOutInfo(int bit, TOutput output)
        {
            OutputList[bit] = output;
            //OutputList[bit].Label = strLabel;
            //OutputList[bit].Name = strName;
        }
        public UniDAQ_PEX_P32C32U(ushort shBoardNo)
        {
            uBoardNo = shBoardNo;
            InitIO();
        }

        public void InitIO()
        {
            for (int x = 0; x < 4; x++)
            {
                // Input
                InputList.Add(new TInput(uBoardNo, (0 + (x * 8)), (byte)x, 0x00, "", "", ""));
                InputList.Add(new TInput(uBoardNo, (1 + (x * 8)), (byte)x, 0x01, "", "", ""));
                InputList.Add(new TInput(uBoardNo, (2 + (x * 8)), (byte)x, 0x02, "", "", ""));
                InputList.Add(new TInput(uBoardNo, (3 + (x * 8)), (byte)x, 0x03, "", "", ""));
                InputList.Add(new TInput(uBoardNo, (4 + (x * 8)), (byte)x, 0x04, "", "", ""));
                InputList.Add(new TInput(uBoardNo, (5 + (x * 8)), (byte)x, 0x05, "", "", ""));
                InputList.Add(new TInput(uBoardNo, (6 + (x * 8)), (byte)x, 0x06, "", "", ""));
                InputList.Add(new TInput(uBoardNo, (7 + (x * 8)), (byte)x, 0x07, "", "", ""));

                // Output
                OutputList.Add(new TOutput(uBoardNo, (0 + (x * 8)), (byte)x, 0x01, "", "", ""));
                OutputList.Add(new TOutput(uBoardNo, (1 + (x * 8)), (byte)x, 0x02, "", "", ""));
                OutputList.Add(new TOutput(uBoardNo, (2 + (x * 8)), (byte)x, 0x04, "", "", ""));
                OutputList.Add(new TOutput(uBoardNo, (3 + (x * 8)), (byte)x, 0x08, "", "", ""));
                OutputList.Add(new TOutput(uBoardNo, (4 + (x * 8)), (byte)x, 0x10, "", "", ""));
                OutputList.Add(new TOutput(uBoardNo, (5 + (x * 8)), (byte)x, 0x20, "", "", ""));
                OutputList.Add(new TOutput(uBoardNo, (6 + (x * 8)), (byte)x, 0x40, "", "", ""));
                OutputList.Add(new TOutput(uBoardNo, (7 + (x * 8)), (byte)x, 0x80, "", "", ""));
            }

        }

        public bool UpdateInput(ref TInput Input)
        {
            if (!UniDAQBoard.isBoardOpened) { return false; }
            try
            {

                uint DIVal;
                DIVal = 0x0;
                bool bSuceeed = UniDAQBoard.UpdateInput(Input.BoardID, Input.AxisPort, ref DIVal);

                for (int index = 0; index < 32; index++)
                {
                    if (index == Input.Mask)
                    {
                        if ((DIVal & (1 << (Input.Mask))) == 0)
                        {
                            Input.Status = true;
                        }
                        else
                        {
                            Input.Status = false;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool UpdateOutputHi(ref TOutput Output)
        {
            if (!UniDAQBoard.isBoardOpened) { return false; }

            OutStatus[0, Output.AxisPort] = (byte)(OutStatus[0, Output.AxisPort] | Output.Mask);
            try
            {
                bool bSuceeed = UniDAQBoard.UpdateOutput(Output.BoardID, Output.AxisPort, ref OutStatus[0, Output.AxisPort]);
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
            if (!UniDAQBoard.isBoardOpened) { return false; }

            OutStatus[0, Output.AxisPort] = (byte)(OutStatus[0, Output.AxisPort] & (Output.Mask ^ 0xFF));
            try
            {
                bool bSuceeed = UniDAQBoard.UpdateOutput(Output.BoardID, Output.AxisPort, ref OutStatus[0, Output.AxisPort]);
                Output.Status = false;
            }
            catch
            {
                return false;

            }

            return true;
        }

        public bool ReadBit(int eBit)
        {
            int bit = eBit;
            if (!UniDAQBoard.isBoardOpened) { return false; }
            try
            {
                TInput data = InputList[bit];
                UpdateInput(ref data);
                return data.Status;
            }
            catch (Exception ex) { }
            return false;
        }
        public bool OutBit(int eBit, TOutputStatus state)
        {
            int bit = eBit;
            if (!UniDAQBoard.isBoardOpened) { return false; }
            TOutput data = OutputList[bit];

            switch (state)
            {
                case TOutputStatus.Hi:// PISO.TOutputStatus.Hi:
                    UpdateOutputHi(ref data);
                    //OutputList[bit] = data;
                    break;

                case TOutputStatus.Lo:// PISO.TOutputStatus.Lo:
                    UpdateOutputLo(ref data);
                    //OutputList[bit] = data;
                    break;
            }
            return data.Status;
        }

        //Call once during init
        public bool OpenBoard()
        {
            if (!UniDAQBoard.isBoardOpened)
            {
                UniDAQBoard.InitBoardNo();
                UniDAQBoard.GetGardInfo();
                UniDAQBoard.SetBoardOpen();
            }

            return true;
        }

        //Call once during init
        public bool CloseBoard()
        {
            if (UniDAQBoard.isBoardOpened)
            {
                UniDAQBoard.CloseBoard();
            }

            return true;
        }

        public void UpdateViEInputInfo(int bit, TInput_ViE input)
        {
            throw new NotImplementedException();
        }

        public void UpdateViEOutInfo(int bit, TOutput_ViE output)
        {
            throw new NotImplementedException();
        }
    }
}
