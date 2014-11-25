using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ArtisanCode.UsingGrass
{
    public static class TestStaticClass
    {
        public static bool ReturnBoolNoParameters()
        {
            return true;
        }

        public static bool ReturnBoolListOfBools(List<bool> input)
        {
            return true;
        }

        public static Dictionary<int, IList<bool>> ReturnDictionary()
        {
            return null;
        }

        public static dynamic UsingDynamics(dynamic input)
        {
            return null;
        }

        internal static string InternalFunctionWithCrazyParameters(List<List<List<List<List<List<string>>>>>> input)
        {
            return "This is crazy!";
        }
        public static void UnusualParameters(Encoding encoding)
        {
            return;
        }
    }
}
