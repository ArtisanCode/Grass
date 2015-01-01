using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ArtisanCode.UsingGrass
{
    public static class TestStaticClass
    {
        public static bool ReturnBoolNoParameters()
        {
            return true;
        }

        public static bool ReturnBoolListOfBools(IList<bool> input)
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
        public static void RefParameters(ref Encoding encoding)
        {
            return;
        }
        public static void OutParameters(out Encoding encoding)
        {
            encoding = Encoding.UTF8;
        }
    }
}
