//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ArtisanCode.UsingGrass
{
    using ArtisanCode.UsingGrass;
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Text;
    
    
    [GeneratedCode("0.0.0.1", "ArtisanCode.Grass")]
    public partial class TestStaticClassWrapper : ITestStaticClass
    {
        
        public virtual bool ReturnBoolListOfBools(System.Collections.Generic.IList<bool> input)
        {
            return TestStaticClass.ReturnBoolListOfBools(input);
        }
        
        public virtual bool ReturnBoolNoParameters()
        {
            return TestStaticClass.ReturnBoolNoParameters();
        }
        
        public virtual System.Collections.Generic.Dictionary<int, System.Collections.Generic.IList<bool>> ReturnDictionary()
        {
            return TestStaticClass.ReturnDictionary();
        }
        
        public virtual void UnusualParameters(System.Text.Encoding encoding)
        {
            TestStaticClass.UnusualParameters(encoding);
        }
        
        public virtual object UsingDynamics(object input)
        {
            return TestStaticClass.UsingDynamics(input);
        }
    }
}
