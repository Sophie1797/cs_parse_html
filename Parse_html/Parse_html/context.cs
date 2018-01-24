using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse_html
{
    public class Context5
    {
        public string Predicate { get; set; }
        public string Previous_values { get; set; }
        public string Current_values { get; set; }
        public string Change { get; set; }
        public string Threshold { get; set; }
     
        new public string ToString()
        {
            return (this.Predicate + "   ---   " + this.Previous_values + "   ---   " + this.Current_values + "   ---   " + this.Change + "   ---   " + this.Threshold);
        }
    }
}
