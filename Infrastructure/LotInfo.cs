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
        public RecipeInfo _RecipeInfo = new RecipeInfo();
        public bool InitialRun = false;
        //public string[] Combinations = new string[1679616];
    }

    public class RecipeInfo
    {
        public string DeviceID = "";
        public int RunHz_1st = -1;
        public int RunHz_2nd = -1;
        public int TimeLimit_1st = 0;
        public int TimeLimit_2nd = 0;
        public bool MasterProduct = false;

    }

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
