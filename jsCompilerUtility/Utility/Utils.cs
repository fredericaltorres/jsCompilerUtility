using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using DynamicSugar;

namespace jsCompilerUtility
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

                Output = "";
                ErrorOutput = "";
                Time = -1;
                CommandLine = "";
                ErrorLevel = -1;
            }
        }
        public static ExecutionInfo Execute(string program, string commandLine)
        {

            ExecutionInfo e = new ExecutionInfo();
            e.CommandLine = program + " " + commandLine;
            e.Time = Environment.TickCount;
            e.ErrorLevel = -1;
            StreamReader outputReader = null;
            StreamReader errorReader = null;
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo(program, commandLine);
                processStartInfo.ErrorDialog = false;
                processStartInfo.UseShellExecute = false;
                processStartInfo.RedirectStandardError = true;
                processStartInfo.RedirectStandardInput = false;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.CreateNoWindow = true;
                processStartInfo.WindowStyle = ProcessWindowStyle.Normal;

                Process process = new Process();

                process.StartInfo = processStartInfo;
                bool processStarted = process.Start();

                if (processStarted)
                {
                    outputReader = process.StandardOutput;
                    errorReader = process.StandardError;
                    process.WaitForExit();
                    e.ErrorLevel = process.ExitCode;
                    e.Output = outputReader.ReadToEnd();
                    e.ErrorOutput = errorReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                e.ErrorOutput += "Error lanching the {0} = {1}".format(e.CommandLine, ex.ToString());
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
        public static string GetTempFolder()
        {
            var f = @"{0}\{1}".format(Environment.GetEnvironmentVariable("TEMP"), "jsCompilerUtility");
            if (!System.IO.Directory.Exists(f))
                System.IO.Directory.CreateDirectory(f);
            return f;
        }

    }
}
