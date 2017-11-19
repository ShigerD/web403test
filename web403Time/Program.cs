using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace web403Time
{
    class Program
    {
        const int INTERVAL = 600;

        bool isCircle = true;
        static WebTrack mWebTrack = new WebTrack();
        static int mInterval = INTERVAL;
        
        private static string mUrl;

        string testUrl = "http://data.eastmoney.com/stock/tradedetail.html";
        public static void trackThread()
        {
            int trackCount = 0;
           
            Console.WriteLine("mInterval=" + mInterval);
            if (mWebTrack.gethtml(mUrl) == 403 || mWebTrack.gethtml(mUrl) == 404)
            {
                return;
            }
            while (mWebTrack.gethtml(mUrl) == 1)
            {
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(mInterval);
                    if (mWebTrack.gethtml(mUrl) == 1)
                    {
                        trackCount++;
                        Console.WriteLine("Interval :" + mInterval + "  Succeed  times :" + trackCount);
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Catch 403 forbidden !");
                        Console.WriteLine("Current limit time Interval is " + mInterval);
                        Console.WriteLine("");
                        break;
                    }
                }
                if (mInterval > 9)
                {
                    mInterval = mInterval - mInterval / 10;
                }
                else
                {
                    mInterval = 0;
                }
                trackCount = 0;
            }

            //while (mWebTrack.gethtml(mUrl)==1)
            //{
            //    trackCount++;
            //    Console.WriteLine("Have succeed tracking times --" + trackCount);
            //}

            mInterval = INTERVAL;
            Console.WriteLine("finish");
            Console.WriteLine(" ");
            Console.WriteLine("\rPlease write the url of your websit, 'ctrl+c' from webBrowser and 'ctrl+v 'to here is ok.\r");
        }

        private void startTrack()
        {

            while (isCircle)
            {
                

                string strRead = Console.ReadLine();
            
                if (strRead.Trim().Contains("exit"))
                {
                    break;
                }
                else if (strRead.Trim().Length >= 6)
                {
                    mUrl = strRead;
                    Console.WriteLine("Your have chosen website :" + mUrl);
                    Console.WriteLine("");
                    Console.WriteLine("Set the reqest Interval such as '600', the default Interval is " + INTERVAL+" ,it will keep default if not choose.");
                    string interval = Console.ReadLine();
                    Console.WriteLine("Your have chosen Interval :" + mInterval);
                    Console.WriteLine("");
                    if (!interval.Trim().Equals("")){
                        try
                        {
                            mInterval = Int32.Parse(interval.Trim());
                        }
                        catch (System.FormatException)
                        {
                            Console.WriteLine("Your do not enter a inter ,so keep the default Interval:" + mInterval);
                        }
                    }

                    Console.WriteLine("Begin to track the website!");
                    Thread trackThead = new Thread(trackThread);
                    trackThead.Start();

                }
                else 
                {
                    Console.WriteLine("Tips!");
                    Console.WriteLine("You can enter the 'url' of the website to start");
                    Console.WriteLine("Or You can enter 'exit' to exit");
                }
 
            }
        }

        static void Main(string[] args)
        {
    

            Console.WriteLine("Welcome!");
            Console.WriteLine("");
            Console.WriteLine("Your could copy the url of your target website here direcrly");
            Console.WriteLine("Enter the 'enter' key to confire.");
            Console.WriteLine("");
            Program program = new Program();
            program.startTrack();
        }


    }
}
