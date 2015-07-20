using System;
using System.CodeDom.Compiler;
namespace GrassTemplate.Internals
{
    public interface ICodeGen
    {
        CodeDomProvider CreateCodeDomProvider();
        Tuple<string, System.CodeDom.CodeCompileUnit> EmitInterface(string targetNamespace, ClassDefinition staticClass, GrassOptions options);
        Tuple<string, System.CodeDom.CodeCompileUnit> EmitStaticWrapperClass(string targetNamespace, ClassDefinition staticClass, GrassOptions options);
    }
}
