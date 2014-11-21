using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GrassTemplate.Internals;
using System.Reflection;
using System.Collections.Generic;
using GrassTemplate;
using FizzWare.NBuilder;

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
        
        [TestMethod]
        public void GetParameterList_ValidParameters_ParameterListReturned()
        {
            GetMethodVisibilityTest_GetClassName("");
            GetMethodVisibilityTest_GetClassName("Type1 Name1", Builder<ParameterSignature>.CreateListOfSize(1).Build());
            GetMethodVisibilityTest_GetClassName("Type1 Name1, Type2 Name2", Builder<ParameterSignature>.CreateListOfSize(2).Build());
            GetMethodVisibilityTest_GetClassName("Type1 Name1, Type2 Name2, Type3 Name3", Builder<ParameterSignature>.CreateListOfSize(3).Build());
        }

        private void GetMethodVisibilityTest_GetClassName(string expectedOutput, IList<ParameterSignature> parameters = null)
        {
            if (parameters != null)
            {
                _target.Parameters = new List<ParameterSignature>(parameters);
            }

            var output = _target.GetParameterList();
            Assert.AreEqual(expectedOutput, output);
        }
                
        [TestMethod]
        public void ToClassDefinition_DefinitionCreatedSucessfully()
        {
            ToClassDefinitionTest(
                Visibility.Public, 
                true, 
                "void", 
                "MethodSignatureTests", 
                new List<ParameterSignature>(){new ParameterSignature() {Name = "path", Type="String"}}, 
                "public virtual void MethodSignatureTests(String path)");
            
            ToClassDefinitionTest(
                Visibility.Internal,
                false,
                "void",
                "MethodSignatureTests",
                new List<ParameterSignature>(),
                "internal void MethodSignatureTests()");

            ToClassDefinitionTest(
                Visibility.Protected,
                false,
                "List<String>",
                "MethodSignatureTests",
                new List<ParameterSignature>(Builder<ParameterSignature>.CreateListOfSize(3).Build()),
                "protected List<String> MethodSignatureTests(Type1 Name1, Type2 Name2, Type3 Name3)");

            ToClassDefinitionTest(
                Visibility.Private,
                true,
                "Int64",
                "MethodSignatureTests",
                new List<ParameterSignature>(Builder<ParameterSignature>.CreateListOfSize(2).Build()),
                "private virtual Int64 MethodSignatureTests(Type1 Name1, Type2 Name2)");

        }

        private void ToClassDefinitionTest(Visibility vis, bool virt, string returnType, string name, List<ParameterSignature> param, string expectedOutput)
        {
            var localTestObj = new MethodSignature();

            localTestObj.Accessability = vis;
            localTestObj.Virtual = virt;
            localTestObj.ReturnType = returnType;
            localTestObj.Name = name;
            localTestObj.Parameters = param;

            var actualOutput = localTestObj.ToClassDefinition();

            Assert.AreEqual(expectedOutput, actualOutput);
        }
        
        [TestInitialize]
        public void __init()
        {
            _target = new MethodSignature();
            _target.Name = "MethodSignatureTests";
        }
    }
}
