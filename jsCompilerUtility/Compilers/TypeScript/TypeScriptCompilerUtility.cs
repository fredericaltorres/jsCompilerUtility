using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DynamicSugar;
using System.Reflection;
using System.IO;

namespace jsCompilerUtility
{
    public enum JavaScriptEngine
    {
        NodeJS
    }
    /*
     
C:\Users\frederic.torres\AppData\Roaming\npm\node_modules\typescript\bin>node tsc.js
Syntax:   tsc [options] [file ..]

Examples: tsc hello.ts
          tsc --out foo.js foo.ts
          tsc @args.txt

Options:
  -c, --comments  Emit comments to output
  --declaration   Generates corresponding .d.ts file
  -e, --exec      Execute the script after compilation
  -h, --help      Print this message
  --module KIND   Specify module code generation: "commonjs" (default) or "amd"
  --nolib         Do not include a default lib.d.ts with global declarations
  --out FILE      Concatenate and emit output to single file
  --sourcemap     Generates corresponding .map file
  --target VER    Specify ECMAScript target version: "ES3" (default), or "ES5"
  -w, --watch     Watch output files
  @<file>         Insert command line options and files from a file.

C:\Users\frederic.torres\AppData\Roaming\npm\node_modules\typescript\bin>
     */
    public class TypeScriptCompilerUtility
    {
        const string NodeJSExe = "node.exe";

        public CompilerResultUtility CompileToJavaScript(string typeScriptFile, JavaScriptEngine javaScriptEngine) 
        {
            var r2 = new CompilerResultUtility();
            r2.InputFileName = typeScriptFile;
            var typeScriptCompilerFile = this.ExportTypeScriptCompiler();
            r2.OutputFileName = Path.Combine(Path.GetDirectoryName(typeScriptFile), Path.GetFileNameWithoutExtension(typeScriptFile)+ ".js");
            var commandLine = @" ""{0}"" ""{1}"" -out ""{2}"" ".format(typeScriptCompilerFile, typeScriptFile, r2.OutputFileName);
            var r = Utils.Execute(NodeJSExe, commandLine);
            r2.ConsoleOutput = r.Succeeded ? r.Output : r.ErrorOutput;
            r2.Succeeded = r.Succeeded; 
            return r2;
        }
        private string GetTypeScriptCommandLineFileName() 
        {
            return @"{0}\tsc.js".format(Utils.GetTempFolder());
        }
        private string GetTypeScriptLibDtsFileName()
        {
            return @"{0}\lib.d.ts".format(Utils.GetTempFolder());
        }
        private string ExportTypeScriptCompiler() 
        {
            var f = this.GetTypeScriptCommandLineFileName();
            if (!System.IO.File.Exists(f))
            {
                var source = DS.Resources.GetTextResource("JavaScript.TypeScript.lib.d.ts", Assembly.GetExecutingAssembly());
                System.IO.File.WriteAllText(this.GetTypeScriptLibDtsFileName(), source);

                source = DS.Resources.GetTextResource("JavaScript.TypeScript.tsc.js", Assembly.GetExecutingAssembly());
                System.IO.File.WriteAllText(f, source);
            }
            return f;
        }
    }
}
