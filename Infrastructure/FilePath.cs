using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class FilePath
    {
        public const string machineFolder = @"C:\Machine\";
        public const string iniFilePath = @"C:\Machine\Ini\";
        public const string axisconfigIni = @"axisConfig.ini";
        public const string axisMaxMinIni = @"axisMaxMinPos.ini";
        public const string recipePath = @"C:\Machine\Recipe\";
        public const string teachPath = @"C:\Machine\TeachPoint\";
        public const string configIni = @"C:\Machine\Ini\config.ini";
        public const string runtimeIni = @"C:\Machine\Ini\runtime.ini";
        public const string machineIni = @"C:\Machine\Ini\Machine.ini";
        public const string offsetIni = @"C:\Machine\Ini\MasterOff.ini";
        public const string databaseIni = @"C:\Machine\Ini\Database.ini";

        public const string MECH_DIST = "Mechanical Design Distance Common";
    }

    public class CONST
    {
        public const string MECH_DIST = "Mechanical Design Distance Common";
        public const string WAFER_ARM = "Wafer Arm";
        public const string XYR_TABLE = "XYR Table";
    }


}
