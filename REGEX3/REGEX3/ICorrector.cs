using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REGEX3
{
    public interface ICorrector
    {        
        void DisplayWarning();
        bool CorrectEmail(string email);       
    }
}
