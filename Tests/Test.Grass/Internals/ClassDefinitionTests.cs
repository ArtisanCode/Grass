using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GrassTemplate.Internals;
using GrassTemplate;

namespace GrassTests.Internals
{
    [TestClass]
    public class ClassDefinitionTests
    {
        [TestMethod]
        public void GetClassSignature_GeneratedCorrectly()
        {
            TestGetClassSignature("public partial class FileWrapper : IFile", "File", true);
            TestGetClassSignature("public class DirectoryWrapper : IDirectory", "Directory", false);
            TestGetClassSignature("internal partial class WidgetWrapper : IWidget", "Widget", true, "internal");
        }

        private void TestGetClassSignature(string expectedResult, string className, bool isPartial, string accessability = "")
        {
            ClassDefinition local = new ClassDefinition(className, Visibility.Public, isPartial);
            string actualResult;

            if(string.IsNullOrEmpty(accessability))
            {
                actualResult = local.GetClassSignature();
            }
            else
            {
                actualResult = local.GetClassSignature(accessability);
            }

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GetInterfaceSignature_GeneratedCorrectly()
        {
            TestGetInterfaceSignature("public interface IFile", "File");
            TestGetInterfaceSignature("public interface IDirectory", "Directory");
            TestGetInterfaceSignature("internal interface IWidget", "Widget", "internal");
        }

        private void TestGetInterfaceSignature(string expectedResult, string className, string accessability = "")
        {
            ClassDefinition local = new ClassDefinition(className, Visibility.Public, true);
            string actualResult;

            if (string.IsNullOrEmpty(accessability))
            {
                actualResult = local.GetInterfaceSignature();
            }
            else
            {
                actualResult = local.GetInterfaceSignature(accessability);
            }

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GetClassName_ValidAssemblyReferences_ClassNameRetrieved()
        {
            TestGetClassName("System.IO.File", "File", "IFile");
            TestGetClassName("ArtisanCode.UsingGrass.TestStaticClass, ArtisanCode.UsingGrass", "TestStaticClass", "ITestStaticClass");
        }

        private void TestGetClassName(string input, string expectedClassName, string expectedInterfaceName)
        {
            ClassDefinition local = new ClassDefinition("invalidReference", Visibility.Public, true);

            local.QualifiedAssemblyName = input;
            local.GenerateNames();

            Assert.AreEqual(expectedClassName, local.ClassName);
            Assert.AreEqual(expectedInterfaceName, local.InterfaceName);
        }
    }
}
