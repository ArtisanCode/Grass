using System;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace GrassTemplate.Internals.Generation
{
    public interface ICodeGen
    {
        CodeDomProvider CreateCodeDomProvider();
        CodeGenerationResult EmitInterface(string targetNamespace, ClassDefinition staticClass, GrassOptions options);
        CodeGenerationResult EmitStaticWrapperClass(string targetNamespace, ClassDefinition staticClass, GrassOptions options);
    }
}
