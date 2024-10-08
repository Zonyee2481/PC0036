using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Management;
using System.Globalization;

namespace RegistryKeyTools
{
    public class RegistryClass : CodeData
    {
        private DateTime currentDate;
        private DateTime systemDate;
        private DateTime licenseDate;
        private bool licenseStatus;
        CultureInfo provider = CultureInfo.InvariantCulture;
        private string licenseKey;
        private string registryPath;

        public DateTime CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; }
        }

        public DateTime SystemDate
        {
            get { return systemDate; }
            set { systemDate = value; }
        }

        public DateTime LicenseDate
        {
            get { return licenseDate; }
            set { licenseDate = value; }
        }

        public bool LicenseStatus
        {
            get { return licenseStatus; }
            set { licenseStatus = value; }
        }

        public string LicenseKey
        {
            get { return licenseKey; }
            set { licenseKey = value; }
        }

        List<string> combinations = new List<string>();

        string texts = "";

        public RegistryClass()
        {
            //MessageBox.Show(GetMacAddress());
            foreach (var specialChar in SpecialCharList)
            {
                combinations.Add(specialChar);
            }

            texts = LetterList;

            registryPath = RegistryPath;
        }

        public bool CreateLicenseKey(string key) // Function to create license key in registry
        {
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(registryPath); // Registry location path
                registryKey.SetValue("LicenseKey", key); // Encode the license key set it as data, value as "LicenseKey" in registry (CurrentUser)
                LicenseKey = key;
                registryKey.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool CheckLicenseKeyDate(string key)
        {
            string decodeKey;
            if (!DecodeText(key, out decodeKey))
            {
                SetLicenseStatus(false);
                return false;
            }
            string sKeyDate = decodeKey.Substring(3, 8);
            DateTime keyDate = DateTime.ParseExact(sKeyDate,
                new string[] { "MM/dd/yyyy", "MM-dd-yyy", "MM.dd.yyyy", "MMddyyyy" }, 
                provider, DateTimeStyles.None);
            if (DateTime.Today >= keyDate)
            {
                SetLicenseStatus(false);
                return false;
            }
            SetLicenseStatus(true);
            SetCurrentDate(DateTime.Today.ToString("MMddyyyy"));
            return true;
        }

        public bool SetLicenseStatus(bool Status) // Function to create license key status in registry
        {
            string _sLicenseStatus = "";
            switch (Status)
            {
                case true:
                    _sLicenseStatus = "true";
                    break;
                case false:
                    _sLicenseStatus = "false";
                    break;
            }

            try
            {
                RegistryKey status = Registry.CurrentUser.CreateSubKey(registryPath); // Registry location path
                status.SetValue("LicenseStatus", EncodeText(_sLicenseStatus)); // Encode the license status set it as data, value as "LicenseStatus" in registry (CurrentUser)
                status.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool SetCurrentDate(string Date)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(registryPath); // Registry location path
                key.SetValue("CurrentDate", EncodeText(Date)); // Encode the date set it as data, value as "CurrentDate" in registry (CurrentUser)
                key.Close();

                //RegistryKey status = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\ES");
                //status.SetValue("LicenseStatus", EncodeText("true"));
                //status.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool ReadLicenseKey()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(registryPath); // Registry location path
                LicenseKey = key.GetValue("LicenseKey").ToString();
                string decodeKey;
                if (!DecodeText(LicenseKey, out decodeKey))
                {
                    return false;
                }
                string sLicenseDate = decodeKey.Substring(3, 8);
                LicenseDate = DateTime.ParseExact(sLicenseDate,
                new string[] { "MM/dd/yyyy", "MM-dd-yyy", "MM.dd.yyyy", "MMddyyyy" },
                provider, DateTimeStyles.None);
                CurrentDate = DateTime.ParseExact(ReadCurrentDate(),
                new string[] { "MM/dd/yyyy", "MM-dd-yyy", "MM.dd.yyyy", "MMddyyyy" },
                provider, DateTimeStyles.None);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public string GetLicenseKey()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(registryPath); // Registry location path
                return key.GetValue("LicenseKey").ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return string.Empty;
            }
        }

        public bool ReadLicenseStatus() // Read license key from registry
        {
            bool _bResult = false;
            try
            {
                RegistryKey status = Registry.CurrentUser.OpenSubKey(registryPath);

                switch (DecodeText(status.GetValue("LicenseStatus").ToString())) // Decode the license status
                {
                    case "true":
                        _bResult = true; // Result = true, if the license key status = true
                        break;
                    case "false":
                        _bResult = false; // Result = false, if the license key status = false
                        break;
                }
                return _bResult;
                //return DecodeText(status.GetValue("LicenseStatus").ToString());
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return _bResult;
            }
        }

        public string ReadCurrentDate() // Function to read current date from registry, it is to prevent user manually change date for license hack
        {
            try
            {
                RegistryKey CurrentDate = Registry.CurrentUser.OpenSubKey(registryPath);

                return DecodeText(CurrentDate.GetValue("CurrentDate").ToString()); // Get value current date and decode
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return "Error";
            }
        }

        public string EncodeText(string Text) // Encoding function
        {
            try
            {
                Random random = new Random();
                int index = random.Next(0, combinations.Count);
                //string randomTx = "";
                //for (int i = 0; i < 3; i++)
                //{
                //    char c = texts[random.Next(0, texts.Length)];
                //    randomTx = randomTx + c.ToString();
                //}

                return Convert.ToBase64String(Encoding.UTF8.GetBytes("eS-" + Text + combinations[index]));

                //return Convert.ToBase64String(Encoding.UTF8.GetBytes("eS-" + randomTx + Text + combinations[index]));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Licenses Key! ");
                return "";
            }          
        }

        public bool EncodeText(string Text, out string sResult)
        {
            try
            {
                Random random = new Random();
                int index = random.Next(0, combinations.Count);
                sResult = Convert.ToBase64String(Encoding.UTF8.GetBytes("eS-" + Text + combinations[index]));
                return true;
            }
            catch (Exception ex)
            {
                goto _Err;
            }
            _Err:
            sResult = string.Empty;
            return false;

        }

        public string DecodeText(string EncodedText) // Decoding function
        {
            try
            {
                string result = Encoding.UTF8.GetString(Convert.FromBase64String(EncodedText)).Replace("eS-", "");
                //result = result.Substring(3);
                foreach (string combination in combinations)
                {
                    result = result.Replace(combination, "");
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Licenses Key! ");
                return "";
            }           
        }

        public bool DecodeText(string Text, out string sResult)
        {
            try
            {                
                sResult = Encoding.UTF8.GetString(Convert.FromBase64String(Text));

                if (!sResult.Contains("eS-"))
                {
                    goto _Err;
                }
                sResult = sResult.Replace("eS-", "");
                foreach (string combination in combinations)
                {
                    if (sResult.Contains(combination))
                    {
                        sResult = sResult.Replace(combination, "");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                goto _Err;
            }
            _Err:
            sResult = string.Empty;
            return false;
        }

        public bool VerifyCombinations(ref string Text)
        {
            try
            {
                for (int i = 0; i < combinations.Count(); i++)
                {
                    if (Text.Contains(combinations[i]))
                    {
                        Text = Text.Replace(combinations[i], "");
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Licenses Key! ");
                return false;
            }
        }

        public bool VerifyLicenseDate()
        {
            if (CurrentDate > LicenseDate || !ReadLicenseKey())
            {
                LicenseStatus = false;
                SetLicenseStatus(false);
                return false;
            }

            return true;
        }

        public bool VerifyLicenseDate(DateTime CurrentDate, DateTime LicenseDate) // Function to verify the license date
        {
            if (CurrentDate <= LicenseDate)
            {
                return true; // Return true if current date is smaller than license date, which mean still valid
            }
            else
            {
                return false; // Return false if current date already pass the license date
            }            
        }

        public bool VerifySystemDate()
        {
            DateTime systemDate = SystemDate = DateTime.Today;
            if (CurrentDate > systemDate)
            {
                LicenseStatus = false;
                SetLicenseStatus(false);
                return false;
            }
            CurrentDate = systemDate;
            SetCurrentDate(systemDate.ToString("MMddyyyy"));
            return true;
        }

        public bool VerifySystemDate(DateTime CurrentDate, DateTime SysDate) // Function to verify the system date
        {
            if (CurrentDate <= SysDate) // Return true, if system date is higher than current date
            {
                return true;
            }
            else // Return false, if system date is smaller than current date, which mean user update the system date to increase their license date
            {
                return false;
            }
        }

        public bool CheckRegistryLicenseStatusExist() // Function to check license status from registry location path
        {
            try
            {
                RegistryKey result = Registry.CurrentUser.OpenSubKey(registryPath);
                return (result.GetValueNames().Equals("LicenseStatus")); // Return true if it is exist
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckLicenseKeyExist() // Function to check license key from registry location path
        {
            try
            {
                RegistryKey result = Registry.CurrentUser.OpenSubKey(registryPath);
                return (result.GetValueNames().Contains("LicenseKey")); // Return true if license key already exist in location path
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckRegistryCurrentDate() // Function to check registry current date exist
        {
            try
            {
                RegistryKey result = Registry.CurrentUser.OpenSubKey(registryPath);
                return (result.GetValueNames().Contains("CurrentDate")); // Return true if current date exist in registry location path
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string ConvertLicenseStatus(bool Status) // Function to convert true/false to valid/invalid
        {
            string result = "";
            switch(Status)
            {
                case true:
                    result = "Valid";
                    break;
                case false:
                    result = "Invalid";
                    break;
            }
            return result;
        }

        public string GetMacAddress()
        {
            string result = string.Empty;
            // Get all network interfaces on the local machine
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            // Loop through each network interface
            foreach (var networkInterface in networkInterfaces)
            {
                // Check if the network interface is operational up or down
                if (networkInterface.OperationalStatus == OperationalStatus.Up || 
                    networkInterface.OperationalStatus == OperationalStatus.Down)
                {
                    // Get the MAC address from the network interface
                    var macAddress = networkInterface.GetPhysicalAddress();
                    result = string.Join(":", macAddress.GetAddressBytes().Select(b => b.ToString("X2")));                    
                }

                if (result != null || result != string.Empty)
                {
                    return result;
                }
            }

            return string.Empty;
        }

        public string GetMotherBoardSerialNumber()
        {
            string serialNumber = string.Empty;

            // Querying the Win32_BaseBoard class to get the motherboard details
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard");

            foreach (ManagementObject mo in searcher.Get())
            {
                serialNumber = mo["SerialNumber"].ToString();
            }

            return EncodeText(serialNumber);
        }

        public bool CheckMotherBoardSerialNumber(string key)
        {
            string actSerialNumber = DecodeText(GetMotherBoardSerialNumber());

            string decodeKey = DecodeText(key);

            string keySerialNumber = decodeKey.Substring(0, 3) + decodeKey.Substring(11);

            if (actSerialNumber != keySerialNumber)
            {
                return false;
            }

            return true;
        }

        public void GenerateLicenseKey(string code, DateTime expiredDate, out string key)
        {
            string serialNumber = DecodeText(code);

            string expDate = expiredDate.ToString("MMddyyyy");

            string combine = serialNumber.Substring(0, 3) + expDate + serialNumber.Substring(3);

            key = EncodeText(combine);
        }

        #region RK PC0036
        public bool CheckLicenseKey(string serialNumber)
        {            
            string readKey = DecodeText(GetLicenseKey());
            return serialNumber == readKey;
        }

        public string Process(string tx)
        {
            return tx.Substring(3) + tx.Substring(0, 3);
        }
        #endregion
    }
}
