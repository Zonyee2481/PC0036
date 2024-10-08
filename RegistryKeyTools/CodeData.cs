using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistryKeyTools
{
    public abstract class CodeData
    {
        List<string> lsSpecialChar = new List<string>() { "@", "#", "$", "%", "^", "&", "*", "<", ">", "?", "[", "]", "{", "}", "`", "|" };

        string sLetter = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";

        string sRegistryPath = @"SOFTWARE\ES";

        public List<string> SpecialCharList
        {
            get { return lsSpecialChar; }
        }

        public string LetterList
        {
            get { return sLetter; }
        }

        public string RegistryPath
        {
            get { return sRegistryPath; }
        }
    }
}
