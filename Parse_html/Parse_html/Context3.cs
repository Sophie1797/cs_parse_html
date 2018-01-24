using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse_html
{
    public class Context3
    {
        public string ValidatorName { get; set; }
        public string PassPercentage { get; set; }
        public string MinPercentage { get; set; }

        new public string ToString()
        {
            return (this.ValidatorName + "   ---   " + this.PassPercentage + "   ---   " + this.MinPercentage);
        }
    }
}
