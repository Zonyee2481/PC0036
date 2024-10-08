using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegistryKeyTools;

namespace Machine
{
    public class TaskLicense
    {
        RegistryClass lkt = new RegistryClass();
        public bool CheckLicenseKeyExist()
        {
            return lkt.CheckLicenseKeyExist();
        }

        public bool CheckLicenseKey()
        {
            if (!CheckLicenseKeyExist()) { return false; }
            string serialNumber = lkt.Process(lkt.DecodeText(lkt.GetMotherBoardSerialNumber()));
            return lkt.CheckLicenseKey(serialNumber);
        }
    }
}
