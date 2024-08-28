using MotionIODevice.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotionIODevice
{
    public enum TOutputStatus { Lo, Hi, St };
    public interface IBoardIO
    {
        bool OpenBoard();
        bool CloseBoard();

        bool ReadBit(int bit);

        bool OutBit(int bit, TOutputStatus state);

        List<IO.TInput> GetInputList { get; }

        List<IO.TOutput> GetOutputList { get; }

        bool IsBoardOpened { get; set; }

        void UpdateInputInfo(int bit, IO.TInput input);

        void UpdateOutInfo(int bit, IO.TOutput output);

        void UpdateViEInputInfo(int bit, IO.TInput_ViE input);

        void UpdateViEOutInfo(int bit, IO.TOutput_ViE output);
    }
}
