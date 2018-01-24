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
            Parse_Operation po = new Parse_Operation();
            //string strhtml = po.Read_html(@"D:\atmp\test.html");
            //po.Local_Parse_Funcion(strhtml);
            //po.Web_Parse_Funcion(@"https://wrapstar.bing.net/Model/Detail/64542?environment=WrapStar");
            po.Web_Parse_Funcion_httpclient(@"https://wrapstar.bing.net/Model/Detail/64542?environment=WrapStar");
            //string strhtml = po.Read_html(@"D:\atmp\test3.html");
            //po.test(strhtml);
        }
    }  
}
