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
    public interface ITestStaticClass
    {
        
        void OutParameters(out System.Text.Encoding encoding);
        
        void RefParameters(ref System.Text.Encoding encoding);
        
        bool ReturnBoolListOfBools(System.Collections.Generic.IList<bool> input);
        
        bool ReturnBoolNoParameters();
        
        System.Collections.Generic.Dictionary<int, System.Collections.Generic.IList<bool>> ReturnDictionary();
        
        object UsingDynamics(object input);
    }
}
