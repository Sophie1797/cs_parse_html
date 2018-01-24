using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse_html
{
    public class Block
    {
        public string Title { get; set; } 
        public List<Context3>  Table_context3 { get; set; }
        public List<Context5>  Table_context5 { get; set; }
        public string Link { get; set; }
        public Block()
        {
            this.Link = "There is no debug link";
        }
        public void PrintString()
        {
            Console.WriteLine("--------------------" + this.Title + "------------------------");

                foreach(Context3 c in this.Table_context3)
                {
                    Console.WriteLine(c.ToString());
                }
            

                foreach (Context5 c in this.Table_context5)
                {
                    Console.WriteLine(c.ToString());
                }
            

            if(this.Link!=null) Console.WriteLine("dubug stream: " + this.Link+"\n");
        }
   
    }
}
