using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DynamicSugar;

namespace jsCompilerUtility
{
    public class Program
    {
        private static void Print(string s)
        {
            Console.WriteLine(s);
        }
        public static void Main(string[] args)
        {
            Print("jsCompilerUtility");
            var exitCode = 0;
            var cmdLine = new CommandLine(args);
            if (cmdLine.Exist("-typescript"))
            {   
                var tsFile = cmdLine.Arguments("-typescript");
                Print("Compiling to JavaScript TypeScript file:{0}".format(tsFile));
                var c = new TypeScriptCompilerUtility().CompileToJavaScript(tsFile, JavaScriptEngine.NodeJS);
                if (c.Succeeded)
                {
                    if (cmdLine.Exist("-run")) 
                    {
                        Print("Running file:{0}".format(c.OutputFileName));
                        var x = new JavaScriptRunner().Run(c.OutputFileName, JavaScriptEngine.NodeJS);
                        if (x.Succeeded)
                        {
                            Print(x.Output);
                        }
                        else
                        {
                            Print("{0} execution failed\n{1}".format(c.OutputFileName, x.Output + x.ErrorOutput));
                        }
                    }
                }
                else
                {
                    Print(c.ConsoleOutput);
                    exitCode = 1;
                }
            }
            if (cmdLine.Exist("-pause"))
            {
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
            System.Environment.ExitCode = exitCode;
        }
    }
}
