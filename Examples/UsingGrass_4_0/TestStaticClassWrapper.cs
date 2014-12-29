
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;

namespace ArtisanCode.UsingGrass
{
    [GeneratedCode("ArtisanCode.Grass","1.0.0.0")]
    public interface ITestStaticClass
    {
        Boolean ReturnBoolNoParameters();
        Boolean ReturnBoolListOfBools(List<Boolean> input);
        Dictionary<Int32, IList<Boolean>> ReturnDictionary();
        Object UsingDynamics(Object input);
        String InternalFunctionWithCrazyParameters(List<List<List<List<List<List<String>>>>>> input);
        void UnusualParameters(Encoding encoding);
    }
}

