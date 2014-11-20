using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GrassTemplate.Internals;
using System.Reflection;
using System.Collections.Generic;
using GrassTemplate;

namespace GrassTests.Internals
{
    [TestClass]
    public class MethodSignatureTests
    {
        MethodSignature _target;
        
        [TestMethod]
        public void GetMethodVisibility_ValidAssemblyReferences_VisibilityRetrieved()
        {
            GetMethodVisibilityTest_GetClassName(typeof(TestClass).GetMethod("PublicMethod"), Visibility.Public);
            GetMethodVisibilityTest_GetClassName(typeof(TestClass).GetMethod("InternalMethod", BindingFlags.NonPublic | BindingFlags.Instance), Visibility.Internal);
            GetMethodVisibilityTest_GetClassName(typeof(TestClass).GetMethod("ProtectedMethod", BindingFlags.NonPublic | BindingFlags.Instance), Visibility.Protected);
            GetMethodVisibilityTest_GetClassName(typeof(TestClass).GetMethod("PrivateMethod", BindingFlags.NonPublic | BindingFlags.Instance), Visibility.Private);
        }

        private void GetMethodVisibilityTest_GetClassName(MethodInfo method, Visibility expectedOutput)
        {
            var output = _target.GetMethodVisibility(method);
            Assert.AreEqual(expectedOutput, output);
        }

        [TestInitialize]
        public void __init()
        {
            _target = new MethodSignature();
        }
    }
}
