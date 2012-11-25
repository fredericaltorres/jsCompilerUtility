using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TypeScriptConsoleApplication
{
    public class Utils
    {
        public class ExecutionInfo
        {
            public string Output;
            public string ErrorOutput;
            public int Time;
            public string CommandLine;
            public int ErrorLevel;

            public bool Succeeded
            {
                get
                {
                    return this.ErrorLevel == 0;
                }
            }

            public ExecutionInfo()
            {
                Output      = "";
                ErrorOutput = "";
                Time        = -1;
                CommandLine = "";
                ErrorLevel  = -1;
            }
        }
        public static ExecutionInfo Execute(string program, string commandLine)
        {
            var e                       = new ExecutionInfo();
            e.CommandLine               = program + " " + commandLine;
            e.Time                      = Environment.TickCount;
            e.ErrorLevel                = -1;
            StreamReader outputReader   = null;
            StreamReader errorReader    = null;
            try
            {
                ProcessStartInfo processStartInfo       = new ProcessStartInfo(program, commandLine);
                processStartInfo.ErrorDialog            = false;
                processStartInfo.UseShellExecute        = false;
                processStartInfo.RedirectStandardError  = true;
                processStartInfo.RedirectStandardInput  = false;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.CreateNoWindow         = true;
                processStartInfo.WindowStyle            = ProcessWindowStyle.Normal;
                
                var process = new Process();
                process.StartInfo   = processStartInfo;
                var processStarted = process.Start();

                if (processStarted)
                {
                    outputReader    = process.StandardOutput;
                    errorReader     = process.StandardError;
                    process.WaitForExit();
                    e.ErrorLevel    = process.ExitCode;
                    e.Output        = outputReader.ReadToEnd();
                    e.ErrorOutput   = errorReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                e.ErrorOutput += String.Format("Error lanching the {0} = {1}",e.CommandLine, ex.ToString());
            }
            finally
            {
                if (outputReader != null)
                    outputReader.Close();
                if (errorReader != null)
                    errorReader.Close();

                if (e.ErrorLevel != 0)
                {
                    e.ErrorOutput = e.Output;
                    e.Output = "";
                }
            }
            e.Time = Environment.TickCount - e.Time;
            return e;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var exe         = @"C:\Users\frederic\Documents\GitHub\jsCompilerUtility\jsCompilerUtility\bin\Debug\jsCompilerUtility.exe";
            var folder      = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var commandLine = String.Format(@"-typescript ""{0}"" -run", Path.Combine(folder, "main.ts") );
            var r           = Utils.Execute(exe, commandLine);
            if (r.Succeeded)
            {
                Console.WriteLine(r.Output);
            }
            else 
            {
                Console.WriteLine(r.ErrorOutput);
            }
            Console.ReadKey();

        }
    }
}
