using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace REGEX3
{
    public class UpperCaseVerifier : Verifier
    {
        public readonly string _test;
        public UpperCaseVerifier()
        {
            this.Reg = new Regex(@"(?-i:\b\p{Lu}+\b)");
        }

        public override bool CorrectEmail(string email)
        {
            if (Reg.IsMatch(email))
            {
                this.Ex = new Exception(email + ": upper case Hatası");
                this.DisplayWarning();
                return false;
            }
            return true;
        }
    
    }
}
