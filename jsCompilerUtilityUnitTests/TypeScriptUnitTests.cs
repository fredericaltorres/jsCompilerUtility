using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DynamicSugar;
using System.Reflection;
using jsCompilerUtility;

namespace jsCompilerUtilityUnitTests
{
    [TestClass]
    public class TypeScriptUnitTests
    {

        const string Test01_Ouptut = @"0
1
2
3
4
5
6
7
8
9
";
        private string GetLocalPath() {

            return System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
        private string GetTestScriptFile(string scriptName) {

            return System.IO.Path.Combine(this.GetLocalPath(), @"Scripts\" + scriptName);
        }
        [TestMethod]
        public void CompileToTypeScript()
        {
            var c = new TypeScriptCompilerUtility().CompileToJavaScript(GetTestScriptFile("Test01.ts"), JavaScriptEngine.NodeJS);
            Assert.IsTrue(c.Succeeded);
        }
        [TestMethod]
        public void CompileToTypeScriptWithInvalidSyntax()
        {
            var c = new TypeScriptCompilerUtility().CompileToJavaScript(GetTestScriptFile("Test02.ts"), JavaScriptEngine.NodeJS);
            Assert.IsFalse(c.Succeeded);
        }
        [TestMethod]
        public void CompileToTypeScriptAndRun()
        {
            var c = new TypeScriptCompilerUtility().CompileToJavaScript(GetTestScriptFile("Test01.ts"), JavaScriptEngine.NodeJS);
            Assert.IsTrue(c.Succeeded);
            var r = new JavaScriptRunner().Run(c.OutputFileName, JavaScriptEngine.NodeJS);
            Assert.IsTrue(r.Succeeded);
            Assert.AreEqual(Test01_Ouptut.Replace("\r",""), r.Output);
        }
    }
}
