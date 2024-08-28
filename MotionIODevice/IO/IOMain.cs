using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.Win32;

namespace MotionIODevice.IO
{
    public class IOMain : IIOMain
    {
        private List<IBoardIO> ioBoards = new List<IBoardIO>();
        private List<TInput> inputList = new List<TInput>();
        private List<TOutput> outputList = new List<TOutput>();
        void BuildIOCard()
        {
            ioBoards.Add(new UniDAQ_ET_2254P(0, "192.168.1.11", 502));

            //ioBoards.Add(new UniDAQ_ET_2254P(1, "192.168.1.11", 502));


            #region UniDAQ ET-2254P Card 0 Input and Output Declaration
            inputList.Add(new IO.TInput((int)TotalModule.GeneralControl, 0, 0, 1, 0x01, "DIO0", "AUTO"));
            inputList.Add(new IO.TInput((int)TotalModule.GeneralControl, 0, 1, 1, 0x02, "DIO1", "START_TIMER"));
            inputList.Add(new IO.TInput((int)TotalModule.GeneralControl, 0, 2, 1, 0x04, "DIO2", "END_LOT"));
            inputList.Add(new IO.TInput((int)TotalModule.GeneralControl, 0, 3, 1, 0x08, "DIO3", "SPARE"));
            inputList.Add(new IO.TInput((int)TotalModule.GeneralControl, 0, 4, 1, 0x010, "DIO4", "SPARE"));
            inputList.Add(new IO.TInput((int)TotalModule.GeneralControl, 0, 5, 1, 0x020, "DIO5", "SPARE"));
            inputList.Add(new IO.TInput((int)TotalModule.GeneralControl, 0, 6, 1, 0x40, "DIO6", "SPARE"));
            inputList.Add(new IO.TInput((int)TotalModule.GeneralControl, 0, 7, 1, 0x80, "DIO7", "SPARE"));

            outputList.Add(new IO.TOutput((int)TotalModule.GeneralControl, 0, 8, 2, 0x01, "DIO8", "TIMESUP"));
            outputList.Add(new IO.TOutput((int)TotalModule.GeneralControl, 0, 9, 2, 0x02, "DIO9", "BITCODE_1"));
            outputList.Add(new IO.TOutput((int)TotalModule.GeneralControl, 0, 10, 2, 0x04, "DIO10", "BITCODE_2"));
            outputList.Add(new IO.TOutput((int)TotalModule.GeneralControl, 0, 11, 2, 0x08, "DIO11", "BITCODE_4"));
            outputList.Add(new IO.TOutput((int)TotalModule.GeneralControl, 0, 12, 2, 0x010, "DIO12", "BITCODE_8"));
            outputList.Add(new IO.TOutput((int)TotalModule.GeneralControl, 0, 13, 2, 0x020, "DIO13", "START_CONTROL_RELAY"));
            outputList.Add(new IO.TOutput((int)TotalModule.GeneralControl, 0, 14, 2, 0x40, "DIO14", "START_BUTTON_LED"));
            outputList.Add(new IO.TOutput((int)TotalModule.GeneralControl, 0, 15, 2, 0x80, "DIO15", "SPARE"));
            #endregion
        }

        bool IIOMain.InitIO()
        {
            BuildIOCard();
            UpdateBoardInfo();
            foreach (var ioBoard in ioBoards)
            {
                ioBoard.OpenBoard();
            }

            return true;
        }

        public void UpdateBoardInfo()
        {            
            int i = 0;
            foreach (var ip in this.inputList)
            {
                var boardNo = ip.BoardID;
                if (i >= ioBoards[boardNo].GetInputList.Count)
                {
                    i = 0;
                }

                //var label = ip.Label;
                //var name = ip.Name;

                ioBoards[boardNo].UpdateInputInfo(i, ip);
                i++;
            }

            //i = 0;
            //foreach (var vip in this.vie_inputList)
            //{
            //    var boardNo = vip.BoardID;
            //    if (i >= ioBoards[boardNo].GetInputList.Count)
            //    {
            //        i = 0;
            //    }

            //    //var label = ip.Label;
            //    //var name = ip.Name;

            //    ioBoards[boardNo].UpdateViEInputInfo(i, vip);
            //    i++;
            //}

            i = 0;
            foreach (var op in this.outputList)
            {
                var boardNo = op.BoardID;
                if (i >= ioBoards[boardNo].GetOutputList.Count)
                {
                    i = 0;
                }


                //var label = op.Label;
                //var name = op.Name;

                ioBoards[boardNo].UpdateOutInfo(i, op);
                i++;
            }

            //i = 0;
            //foreach (var vop in this.vie_outputList)
            //{
            //    var boardNo = vop.BoardID;
            //    if (i >= ioBoards[boardNo].GetOutputList.Count)
            //    {
            //        i = 0;
            //    }


            //    //var label = op.Label;
            //    //var name = op.Name;

            //    ioBoards[boardNo].UpdateViEOutInfo(i, vop);
            //    i++;
            //}
        }

        public List<IBoardIO> GetBoardList()
        {
            return ioBoards;
        }
        public bool ReadBit(int board, int bit)
        {
            try
            {
                bool bOn = ioBoards[board].ReadBit(bit);
                return bOn;
            }
            catch (Exception ex) { }
            return false;
        }

        public bool OutBit(int board, int bit, TOutputStatus state)
        {
            bool bOn = ioBoards[board].OutBit(bit, state);
            return bOn;
        }

        public int GetBoardNo
        {
            get { return ioBoards.Count; }
        }

        public List<TInput> GetBoardInputList(int BoardNo)
        {
            return ioBoards[BoardNo].GetInputList;
        }

        public List<TOutput> GetBoardOutputList(int BoardNo)
        {
            return ioBoards[BoardNo].GetOutputList;
        }
        public bool ReadBit(int bit)
        {
            try
            {
                var input = inputList[bit];
                bool bOn = ioBoards[input.BoardID].ReadBit(input.Bit);
                return bOn;
            }
            catch (Exception ex) { }
            return false;
        }
        public bool OutBit(int bit, TOutputStatus state)
        {
            var output = outputList[bit];
            bool bOn = ioBoards[output.BoardID].OutBit(output.Bit, state);
            return bOn;
        }

        void IIOMain.CloseBoard()
        {
            foreach (var ioBoard in ioBoards)
            {
                ioBoard.CloseBoard();
            }
        }
    }
}
