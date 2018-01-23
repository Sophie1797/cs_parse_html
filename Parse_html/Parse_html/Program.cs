using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Parse_html
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Context> context = new List<Context>();
            string strhtml;
            using (System.IO.StreamReader sr = new System.IO.StreamReader(@"D:\atmp\test.html"))
            {
                strhtml = sr.ReadToEnd();
            }
            Console.WriteLine(strhtml);
            Console.WriteLine("start extracting-------------------");

            HtmlDocument document = new HtmlDocument();//获取HTML，这个可以通过HtmlDocument的Load()或LoadHtml()来加载静态内容，或者也可以HtmlWeb的Get()或Load()方法来加载网络上的URL对应的HTML。
            document.LoadHtml(@strhtml);
            for (int i = 1; ; i++)
            {
                HtmlNodeCollection contextcollection = document.DocumentNode.SelectSingleNode("html//table/tr[" + i + "]//tbody").ChildNodes;
                HtmlNodeCollection titlecollection = document.DocumentNode.SelectSingleNode("html//table/tr[" + i + "]//h4").ChildNodes;

                foreach (HtmlNode node in titlecollection)
                {
                    string[] h4 = node.InnerText.Split(new char[] { '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    Console.WriteLine(h4);
                }
                foreach (HtmlNode node in contextcollection)
                {
                    string[] line = node.InnerText.Split(new char[] { '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (line.Length == 5)
                    {
                        Context c = new Context() { Predicate = line[0], Previous_values = line[1], Current_values = line[2], Change = line[3], Threshold = line[4] };
                        c.ToString();
                    }

                }
            }
        }
    }
   
}
