using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace REGEX3
{
    public class Verifier : ICorrector
    {
        public Regex Reg;
        public Exception Ex = new Exception();
        public Verifier()
        {
            this.Reg = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }

        public void DisplayWarning()
        {           
            Console.WriteLine(Ex.Message);
        }
        public virtual bool CorrectEmail(string email)
        {
            if (!Reg.IsMatch(email))
            {
                this.Ex = new Exception(email + ": geçerli bir email değildir");
                this.DisplayWarning();
                return false;
            }
            return true;
        }
    }
}
