using System;
namespace GrassTemplate.Internals
{
    interface ICodeGen
    {
        Tuple<string, System.CodeDom.CodeCompileUnit> EmitInterface(string targetNamespace, ClassDefinition staticClass, GrassTemplate.Visibility minimumVisibility);
        Tuple<string, System.CodeDom.CodeCompileUnit> EmitStaticWrapperClass(string targetNamespace, ClassDefinition staticClass, GrassTemplate.Visibility minimumVisibility);
    }
}
