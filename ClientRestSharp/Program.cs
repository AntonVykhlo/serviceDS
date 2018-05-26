using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClientRestSharp
{
    class Program
    {
        public static void testPostAsync(int value)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = "textField=" + value;
            byte[] data = encoding.GetBytes(postData);

            HttpWebRequest myRequest =
              (HttpWebRequest)WebRequest.Create("http://localhost:49214/SendAsync/Index/");
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = data.Length;
            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("ready");
            Console.ReadLine();
            while (Console.ReadLine() != "q")
            {
                testPostAsync(132);
                Console.WriteLine("Done");
                Console.ReadLine();
            }
        }
    }
}
