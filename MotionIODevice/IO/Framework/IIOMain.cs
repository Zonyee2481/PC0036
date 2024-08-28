using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotionIODevice
{
    public interface IIOMain
    {
        bool InitIO();

        void CloseBoard();

        bool ReadBit(int board, int bit);

        bool OutBit(int board, int bit, TOutputStatus state);

        int GetBoardNo { get; }

        List<IBoardIO> GetBoardList();

        List<IO.TInput> GetBoardInputList(int BoardNo);

        List<IO.TOutput> GetBoardOutputList(int BoardNo);
        
    }
}
