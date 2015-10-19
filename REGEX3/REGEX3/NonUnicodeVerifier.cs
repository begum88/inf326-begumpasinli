using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace REGEX3
{
    public class NonUnicodeVerifier : Verifier
    {
        public NonUnicodeVerifier()
        {
            this.Reg = new Regex(@"[^\x00-\x7F]");
        }
        public override bool CorrectEmail(string email)
        {
            if (Reg.IsMatch(email))
            {
                this.Ex = new Exception(email + ": non unicode Hatası");
                this.DisplayWarning();
                return false;
            }
            return true;
        }
    }
}
