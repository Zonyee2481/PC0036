using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class LotInfo
    {
        public string LotNum;
        public string PartNum;
        public string PartName;
        public int Counter;  //Load from C:\\Machine\\LotInfo\\23\\B\\0_0.ini
        public RecipeInfo _RecipeInfo = new RecipeInfo();
        //public string[] Combinations = new string[1679616];
    }

    public class RecipeInfo
    {
        public string DeviceID = "";
        public int Index = -1;
        public int Counter = 0;
        public int TimeLimit = 0;
    }

    //public class BitCodeInfo
    //{
    //    public int Index;
    //    public bool BitCode_1;
    //    public bool BitCode_2;
    //    public bool BitCode_4;
    //    public bool BitCode_8;
    //    public string Description;

    //    public BitCodeInfo(int index, bool bitCode_1, bool bitCode_2, bool bitCode_4, bool bitCode_8, string description)
    //    {
    //        this.Index = index;
    //        this.BitCode_1 = bitCode_1;
    //        this.BitCode_2 = bitCode_2;
    //        this.BitCode_4 = bitCode_4;
    //        this.BitCode_8 = bitCode_8;
    //        this.Description = description;
    //    }
    //}

    public class LotCounter
    {
        public int OCRFail;
        public int InKeyenceFail;
        public int OutKeyenceFail;
    }

    public class PartClass
    {
        public string PartNo;
        public string PartName;
    }

    public class Part
    {
        public List<PartClass> _Partnumber = new List<PartClass> { };

        public Part()
        {

            _Partnumber.Add(
                new PartClass
                {
                    PartNo = "280424D",
                    PartName = "Stone SP5 CCD"
                }
                );

            _Partnumber.Add(
                new PartClass
                {
                    PartNo = "301429D",
                    PartName = "Bergamo SP5 CCD"
                }
                );
        }
    }
}
