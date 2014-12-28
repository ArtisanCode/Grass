using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using GrassTemplate.Internals;
using System.Text;

namespace GrassTests.Internals
{
    [TestClass]
    public class TypeHelperTests
    {
        [TestMethod]
        public void DetermineType_ValidTypesRetrieved_NamespacesUpdated()
        {
            DetermineTypeTest<dynamic>("Object", "System"); // NB: can't test for dynamic, therefore just use Object
            DetermineTypeTest<object>("Object", "System");
            DetermineTypeTest<string>("String", "System");
            DetermineTypeTest<int>("Int32", "System");
            DetermineTypeTest<short>("Int16", "System");
            DetermineTypeTest<long>("Int64", "System");
            DetermineTypeTest<byte>("Byte", "System");
            DetermineTypeTest<decimal>("Decimal", "System");
            DetermineTypeTest<float>("Single", "System");
            DetermineTypeTest<List<string>>("List<String>", "System.Collections.Generic");
            DetermineTypeTest<Tuple<string, int, long>>("Tuple<String, Int32, Int64>", "System");
            DetermineTypeTest<List<List<List<List<List<List<string>>>>>>>("List<List<List<List<List<List<String>>>>>>", "System.Collections.Generic");
            DetermineTypeTest<Encoding>("Encoding", "System.Text");
        }

        private void DetermineTypeTest<T>(string expectedOutput, string expectedNamespaceAdded)
        {
            var namespaces =  new GrassTemplate.Internals.HashSet<string>();

            var output = TypeHelper.DetermineType(typeof(T), ref namespaces);

            Assert.AreEqual(expectedOutput, output);
            Assert.IsTrue(namespaces.Contains(expectedNamespaceAdded));
        }
    }
}
