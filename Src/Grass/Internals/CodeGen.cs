using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GrassTemplate.Internals
{
    public class CodeGen : GrassTemplate.Internals.ICodeGen
    {

        public Tuple<string, CodeCompileUnit> EmitInterface(string targetNamespace, ClassDefinition staticClass, Visibility minimumVisibility)
        {
            CodeCompileUnit targetUnit = new CodeCompileUnit();
            CodeNamespace emittedNamespace = GenerateEmittedNamespace(targetNamespace, staticClass);

            string outputFileName = string.Format("{0}.cs", staticClass.InterfaceName);
            CodeTypeDeclaration targetInterface = GenerateCodeType(staticClass.InterfaceName, minimumVisibility);

            targetInterface.IsInterface = true;
            EmitInterfaceMethods(ref targetInterface, staticClass, minimumVisibility);

            emittedNamespace.Types.Add(targetInterface);

            targetUnit.Namespaces.Add(emittedNamespace);

            return new Tuple<string, CodeCompileUnit>(outputFileName, targetUnit);
        }

        public Tuple<string, CodeCompileUnit> EmitStaticWrapperClass(string targetNamespace, ClassDefinition staticClass, Visibility minimumVisibility)
        {
            CodeCompileUnit targetUnit = new CodeCompileUnit();
            CodeNamespace emittedNamespace = GenerateEmittedNamespace(targetNamespace, staticClass);

            string className = string.Format("{0}Wrapper", staticClass.ClassName);
            string outputFileName = string.Format("{0}.cs", className);
            CodeTypeDeclaration targetStaticWrapper = GenerateCodeType(className, minimumVisibility);
            targetStaticWrapper.IsClass = true;
            targetStaticWrapper.IsPartial = true;

            targetStaticWrapper.BaseTypes.Add(new CodeTypeReference(staticClass.InterfaceName));

            EmitStaticWrapperClassMethods(ref targetStaticWrapper, staticClass, minimumVisibility);

            emittedNamespace.Types.Add(targetStaticWrapper);

            targetUnit.Namespaces.Add(emittedNamespace);

            return new Tuple<string, CodeCompileUnit>(outputFileName, targetUnit);
        }

        public CodeTypeDeclaration GenerateCodeType(string name, Visibility minimumVisibility)
        {
            CodeTypeDeclaration targetInterface = new CodeTypeDeclaration(name);

            if (minimumVisibility.HasFlag(Visibility.Public))
            {
                targetInterface.TypeAttributes = TypeAttributes.Public;
            }
            else
            {
                targetInterface.TypeAttributes = TypeAttributes.NestedAssembly | TypeAttributes.NotPublic;
            }

            targetInterface.CustomAttributes.Add(new CodeAttributeDeclaration("GeneratedCode", new CodeAttributeArgument(new CodePrimitiveExpression(Assembly.GetAssembly(typeof(Grass)).GetName().Version.ToString())), new CodeAttributeArgument(new CodePrimitiveExpression("ArtisanCode.Grass"))));
            return targetInterface;
        }

        public void EmitInterfaceMethods(ref CodeTypeDeclaration targetInterface, ClassDefinition staticClass, Visibility minimumVisibility)
        {
            foreach (var m in staticClass.Methods.Where(x => x.Accessability >= minimumVisibility).OrderBy(x => x.Name))
            {
                CodeMemberMethod method = EmitFunctionSignature(m);

                targetInterface.Members.Add(method);
            }
        }

        private static CodeMemberMethod EmitFunctionSignature(MethodSignature m)
        {
            CodeMemberMethod method = new CodeMemberMethod();

            method.Name = m.Name;
            method.ReturnType = new CodeTypeReference(m.Info.ReturnType);

            foreach (var p in m.Parameters)
            {
                method.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(p.Info.ParameterType), p.Name));
            }

            return method;
        }

        
        public void EmitStaticWrapperClassMethods(ref CodeTypeDeclaration targetStaticClass, ClassDefinition staticClass, Visibility minimumVisibility)
        {
            foreach (var m in staticClass.Methods.Where(x => x.Accessability >= minimumVisibility).OrderBy(x => x.Name))
            {
                CodeMemberMethod method = EmitFunctionSignature(m);
                method.Attributes = MemberAttributes.Public;
                method.ImplementationTypes.Add(new CodeTypeReference(staticClass.AsType));

                CodeExpression[] methodParameters = m.Parameters.Select(x => new CodeSnippetExpression(x.Name)).ToArray();

                var callStaticMethodExpr = new CodeMethodInvokeExpression(
                        new CodeTypeReferenceExpression(staticClass.ClassName), m.Name, methodParameters);

                if (m.ReturnType != "void")
                {
                    method.Statements.Add(new CodeMethodReturnStatement(callStaticMethodExpr));
                }
                else
                {
                    method.Statements.Add(callStaticMethodExpr);
                }
       
                targetStaticClass.Members.Add(method);
            }
        }

        public CodeNamespace GenerateEmittedNamespace(string targetNamespace, ClassDefinition staticClass)
        {
            CodeNamespace result = new CodeNamespace(targetNamespace);

            var ns = staticClass.GetRequiredNamespaces();
            ns.Add("System.CodeDom.Compiler"); // Required for the class generated attribute"

            ns.OrderBy(x => x).ToList().ForEach(n => result.Imports.Add(new CodeNamespaceImport(n)));

            return result;

        }
    }
}
