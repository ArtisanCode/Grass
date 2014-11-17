using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GrassTemplate;

namespace GrassTests
{
    [TestClass]
    public class GrassTests
    {
        [TestMethod]
        public void GetClassName_ValidAssemblyReferences_ClassNameRetrieved()
        {
            Test_GetClassName("System.IO.File", "File");
            Test_GetClassName("ArtisanCode.UsingGrass.TestStaticClass, ArtisanCode.UsingGrass", "TestStaticClass");
        }

        private void Test_GetClassName(string input, string expectedOutput)
        {
            var output = Grass.GetClassName(input);
            Assert.AreEqual(expectedOutput, output);
        }
    }
}
