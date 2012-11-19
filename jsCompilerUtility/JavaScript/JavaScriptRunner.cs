using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DynamicSugar;
using System.Reflection;
using System.IO;

namespace jsCompilerUtility
{
 
    public class JavaScriptRunner
    {
        public Utils.ExecutionInfo Run(string javaScriptFile, JavaScriptEngine javaScriptEngine) 
        {
            var r2 = new Utils.ExecutionInfo();
            var commandLine = @" ""{0}"" ".format(javaScriptFile);
            var r = Utils.Execute("node.exe", commandLine);
            return r;
        }
   
    }
}
