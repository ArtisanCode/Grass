using GrassTemplate.Internals;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GrassTests.Internals
{
    [TestClass]
    public class ParameterSignatureTests
    {
        [TestMethod]
        public void ToParameterDefinition_ValidParameters_StringValuesGenerated()
        {
            ToParameterDefinition_StringsReturnedCorrectly<string>("inputFile", "String inputFile");
            ToParameterDefinition_StringsReturnedCorrectly<int>("count", "Int32 count");
        }

        private void ToParameterDefinition_StringsReturnedCorrectly<T>(string name, string expectedOutput)
        {
            var testParameterSig = new ParameterSignature();
            var ns = testParameterSig.RequiredNamespaces;

            testParameterSig.Type = TypeHelper.DetermineType(typeof(T), ref ns);
            testParameterSig.Name = name;

            var output = testParameterSig.ToParameterDefinition();
            Assert.AreEqual(expectedOutput, output);
        }
    }
}
