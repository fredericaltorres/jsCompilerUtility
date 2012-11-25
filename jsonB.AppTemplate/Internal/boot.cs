/*
 jsonB - Windows App
 (C) Frederic Torres 2012
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;
using Nancy.Hosting.Self;
using Nancy;
using System.Net;

namespace jsonB.App
{
    public class Utils 
    {
        public static List<string> GetComputerIp()
        {
            var l    = new List<string>();
            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
                if (ip.AddressFamily.ToString() == "InterNetwork")
                    l.Add(ip.ToString());

            return l;
        }
    }
    public class Program : NancyModule
    {
        const int DefaultPort = 1964;

        static string _jsonDataFile;
        static string _jsonMetaDataFile;
        static string _appFolder;

        static string FormatForOuput(string s) 
        {
            return String.Format("[{0}]{1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), s); 
        }
        static void ConsoleOut(string s, bool format = true) 
        {
            if(format)
                Console.WriteLine(FormatForOuput(s));
            else
                Console.WriteLine(s);
        }
        public Program()
        {
            Get["/"] = x => {
                ConsoleOut(FormatForOuput("/"));
                var b = new StringBuilder(1000);
                b.Append(FormatForOuput("<b>jsonB - Local Server</b>")).AppendLine();
                b.Append(FormatForOuput(String.Format("Data:{0} ", _jsonDataFile))).AppendLine();
                b.Append(FormatForOuput(String.Format("MetaData:{0} ", _jsonMetaDataFile))).AppendLine();
                b.Append(FormatForOuput(String.Format("Machine:{0} ", Environment.MachineName))).AppendLine();
                return b.ToString().Replace(Environment.NewLine, "</BR>");
            };
            Get["/data"] = x =>
            {
                ConsoleOut("/data");
                return System.IO.File.ReadAllText(_jsonDataFile);
            };
            Get["/metadata"] = x =>
            {
                ConsoleOut("/metadata");
                return System.IO.File.ReadAllText(_jsonMetaDataFile);
            };
            Get["/name"] = x =>
            {
                ConsoleOut("/name");
                return Application.ProductName;
            };            
        }
        static void Main(string[] args)
        {
            _appFolder        = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _jsonDataFile     = String.Format(@"{0}\data.js", _appFolder);
            _jsonMetaDataFile = String.Format(@"{0}\metadata.ts", _appFolder);

            ConsoleOut(String.Format("\njsonB local server - App:{0}\n", Application.ProductName), false);
            ConsoleOut(String.Format("Data file    :{0}", _jsonDataFile), false);
            ConsoleOut(String.Format("Metadata file:{0}\n", _jsonMetaDataFile), false);

            var ips = Utils.GetComputerIp();
            ConsoleOut("Machine IP"+ (ips.Count>1 ? "s" : "")+":", false);
            foreach (var ip in Utils.GetComputerIp()) {

                ConsoleOut(String.Format("    {0}", ip), false);
            }

            ConsoleOut("\nWaiting for iOS device - Press any key to stop", false);
            
            var host = new NancyHost(new Uri("http://localhost:" + DefaultPort.ToString()));
            host.Start();            
            Console.ReadKey();
            host.Stop();
        }
    }
}
 