using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace web403Time
{
    class WebTrack
    {


        public int gethtml(string webSourse)
        {
            String strOut = String.Empty;
            HttpWebRequest req = null;
            try
            {
                req = (HttpWebRequest)WebRequest.Create(webSourse);
            }
            catch(System.UriFormatException ex)
            {
                //Console.WriteLine(ex.ToString());
                Console.WriteLine("404 not found ,pleasse check the url and try again .");
                return 404;
            }
           
            //use GET method to get url's html
            req.Method = "GET";
            //use request to get response
            HttpWebResponse resp;
            try
            {
                resp = (HttpWebResponse)req.GetResponse();
                string htmlCharset = "utf-8"; //"utf-8";                                               
                Encoding htmlEncoding = Encoding.GetEncoding(htmlCharset);
                StreamReader sr = new StreamReader(resp.GetResponseStream(), htmlEncoding);
                //read out the returned html
                strOut = sr.ReadToEnd();
                //   richTextBox3 .Text = respHtml;   
                return 1;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                Console.WriteLine(ex.ToString());
                return 403;
            }
        }
    }
}
