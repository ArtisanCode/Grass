using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using GrassTemplate.Internals;
using System.Text;
using GrassTemplate;

namespace GrassTests.Internals
{
    [TestClass]
    public class TypeHelperTests
    {
        [TestMethod]
        public void DetermineType_ValidTypesRetrieved_NamespacesUpdated()
        {
            DetermineTypeTest<dynamic>("dynamic", "System", new GrassOptions { UseDynamic = true });
            DetermineTypeTest<object>("Object", "System", new GrassOptions { UseDynamic = false });
            DetermineTypeTest<dynamic>("Object", "System", new GrassOptions { UseDynamic = false });
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

        private void DetermineTypeTest<T>(string expectedOutput, string expectedNamespaceAdded, GrassOptions testOptions = null)
        {
            var options = testOptions ?? new GrassOptions();

            var namespaces =  new HashSet<string>();

            var output = TypeHelper.DetermineType(typeof(T), ref namespaces, options);

            Assert.AreEqual(expectedOutput, output);
            Assert.IsTrue(namespaces.Contains(expectedNamespaceAdded));
        }
    }
}
