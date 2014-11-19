using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GrassTemplate;
using System.Reflection;
using System.Collections.Generic;

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
        
        [TestMethod]
        public void GetMethodVisibility_ValidAssemblyReferences_VisibilityRetrieved()
        {
            GetMethodVisibilityTest_GetClassName(typeof(TestClass).GetMethod("PublicMethod"), "public");
            GetMethodVisibilityTest_GetClassName(typeof(TestClass).GetMethod("InternalMethod", BindingFlags.NonPublic | BindingFlags.Instance), "internal");
            GetMethodVisibilityTest_GetClassName(typeof(TestClass).GetMethod("ProtectedMethod", BindingFlags.NonPublic | BindingFlags.Instance), "protected");
            GetMethodVisibilityTest_GetClassName(typeof(TestClass).GetMethod("PrivateMethod", BindingFlags.NonPublic | BindingFlags.Instance), "private");
        }

        private void GetMethodVisibilityTest_GetClassName(MethodInfo method, string expectedOutput)
        {
            var output = Grass.GetMethodVisibility(method);
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        public void DetermineType_ValidTypesRetrieved_NamespacesUpdated()
        {           
            DetermineTypeTest<string>("String", "System");
            DetermineTypeTest<HashSet<string>>("HashSet<String>", "System.Collections.Generic");
            DetermineTypeTest<Tuple<string, int, long>>("Tuple<String, Int32, Int64>", "System");
        }

        private void DetermineTypeTest<T>(string expectedOutput, string expectedNamespaceAdded, HashSet<string> existingNamespaces = null )
        {
            var namespaces = existingNamespaces ?? new HashSet<string>();

            var output = Grass.DetermineType(typeof(T), ref namespaces);

            Assert.AreEqual(expectedOutput, output);
            Assert.IsTrue(namespaces.Contains(expectedNamespaceAdded));
        }
    }
}
