using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;
using System.Net.Http;
using System.Web.Mvc;
using System.Net;

namespace Parse_html
{
    public class Parse_Operation
    {
        public string Read_html(string htmlFilePath)
        {
            string strhtml;
            using (StreamReader sr = new StreamReader(htmlFilePath))
            {
                strhtml = sr.ReadToEnd();
            }
            Console.WriteLine(strhtml);
            return strhtml;
        }
        public void Local_Parse_Funcion(string strhtml)
        {
            strhtml = strhtml.Replace(@"\r\n", "");
            List<Block> block = new List<Block>();
            Console.WriteLine("*start extracting*******************************************\n");

            HtmlDocument document = new HtmlDocument();//获取HTML，这个可以通过HtmlDocument的Load()或LoadHtml()来加载静态内容，或者也可以HtmlWeb的Get()或Load()方法来加载网络上的URL对应的HTML。
            document.LoadHtml(@strhtml);
            HtmlNodeCollection countcollection = document.DocumentNode.SelectNodes("html/body/div[2]/table/tr");
            int length_collection = countcollection.Count; 
            //
            for (int i = 1; i <= length_collection; i++)
            {
                Block b = new Block();
                b.Table_context3 = new List<Context3>();
                b.Table_context5 = new List<Context5>();
                HtmlNodeCollection contextcollection = document.DocumentNode.SelectSingleNode("html//table[1]/tr[" + i + "]//tbody").ChildNodes;
                HtmlNode titleh4 = document.DocumentNode.SelectSingleNode("html//table[1]/tr[" + i + "]//h4");
                HtmlNode link = document.DocumentNode.SelectSingleNode("html//table[1]/tr[" + i + "]//a");
                if (link!=null && link.InnerText != null)
                {
                    b.Link = link.GetAttributeValue("href","there is no link");   
                }
                b.Title = titleh4.InnerText;
                int flag = 0;
                foreach (HtmlNode node in contextcollection)
                {
                    string[] line = node.InnerText.Split(new char[] { '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (line!=null && line.Length == 5)
                    {
                        Context5 c = new Context5()
                        { Predicate = line[0], Previous_values = line[1], Current_values = line[2], Change = line[3], Threshold = line[4] };                        
                        b.Table_context5.Add(c);
                        flag = 5;
                    }
                    else if (line != null && line.Length == 3)
                    {
                        Context3 c = new Context3()
                        { ValidatorName = line[0], PassPercentage = line[1], MinPercentage = line[2] };
                        b.Table_context3.Add(c);
                        flag = 3;
                    }
                }
                b.PrintString();
                Console.WriteLine("\n");
                
            }
        }
       
        public async void Web_Parse_Funcion_httpwebrequest(string web_url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://wrapstar.bing.net/Model/Detail/64542?environment=WrapStar");
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string data = "";
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(@data);

            HtmlNode version = htmlDocument.DocumentNode.SelectSingleNode("html/body//table[@class='basicinfo']/tr[5]/td[2]");
            Console.WriteLine("version: " + version.InnerHtml);

            string target_table = "html/body//table[@class='webgrid_table'][1]/tbody";
            List<string> result = new List<string>();

            HtmlNodeCollection countcollection = htmlDocument.DocumentNode.SelectNodes(target_table + "/tr");
            int length_collection = countcollection.Count;
            for (int i = 1; i <= length_collection; i++)
            {
                HtmlNode result_node = htmlDocument.DocumentNode.SelectSingleNode(target_table + "/tr[" + i + "]/td[2]/a");
                result.Add(result_node.InnerHtml);
            }
            Console.WriteLine("version");
            foreach (string s in result)
            {
                Console.WriteLine(s);
            }

        }
        public async void Web_Parse_Funcion_httpclient(string web_url)
        {
            string resultset = "";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = client.GetAsync(web_url).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        resultset = content.ReadAsStringAsync().Result;
                    }
                }
            }
            Console.WriteLine(resultset);
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(@resultset);

            HtmlNode version = htmlDocument.DocumentNode.SelectSingleNode("html/body//table[@class='basicinfo']/tr[5]/td[2]");
            Console.WriteLine("version: " + version.InnerHtml.Trim());

            string target_table = "html/body//table[@class='webgrid_table'][1]/tbody";
            List<string> result = new List<string>();

            HtmlNodeCollection countcollection = htmlDocument.DocumentNode.SelectNodes(target_table + "/tr");
            int length_collection = countcollection.Count;
            for (int i = 1; i <= length_collection; i++)
            {
                HtmlNode result_node = htmlDocument.DocumentNode.SelectSingleNode(target_table + "/tr[" + i + "]/td[2]/a");
                result.Add(result_node.InnerHtml);
            }
            Console.WriteLine("version");
            foreach (string s in result)
            {
                Console.WriteLine(s);
            }

        }
        public void test(string strhtml)
        {
            //strhtml = strhtml.Replace(@"\r\n", "");
            List<Block> block = new List<Block>();
            Console.WriteLine("*start extracting*******************************************\n");

            HtmlDocument htmlDocument = new HtmlDocument();//获取HTML，这个可以通过HtmlDocument的Load()或LoadHtml()来加载静态内容，或者也可以HtmlWeb的Get()或Load()方法来加载网络上的URL对应的HTML。
            htmlDocument.LoadHtml(@strhtml);

            HtmlNode version = htmlDocument.DocumentNode.SelectSingleNode("html/body//table[@class='basicinfo']/tr[5]/td[2]");
            Console.WriteLine("version: " + version.InnerHtml);
            
            string target_table = "html/body//table[@class='webgrid_table'][1]/tbody";
            List<string> result = new List<string>();
           
            HtmlNodeCollection countcollection = htmlDocument.DocumentNode.SelectNodes(target_table+"/tr");
            int length_collection = countcollection.Count;
            for (int i = 1; i <= length_collection; i++)
            {
                HtmlNode result_node = htmlDocument.DocumentNode.SelectSingleNode(target_table + "/tr[" + i + "]/td[2]/a");
                result.Add(result_node.InnerHtml);
            }
            Console.WriteLine("version");
            foreach (string s in result)
            {
                Console.WriteLine(s);
            }

        }
    }
}
