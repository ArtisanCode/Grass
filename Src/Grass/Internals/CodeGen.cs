using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GrassTemplate.Internals
{
    public abstract class CodeGen : GrassTemplate.Internals.ICodeGen
    {
        public abstract string FileExtension { get; }

        public abstract CodeDomProvider CreateCodeDomProvider();

        public Tuple<string, CodeCompileUnit> EmitInterface(string targetNamespace, ClassDefinition staticClass, Visibility minimumVisibility)
        {
            CodeCompileUnit targetUnit = new CodeCompileUnit();
            CodeNamespace emittedNamespace = GenerateEmittedNamespace(targetNamespace, staticClass);

            string outputFileName = string.Format("{0}.{1}", staticClass.InterfaceName, FileExtension);
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
            string outputFileName = string.Format("{0}.{1}", className, FileExtension);
            CodeTypeDeclaration targetStaticWrapper = GenerateCodeType(className, minimumVisibility);
            targetStaticWrapper.IsClass = true;
            targetStaticWrapper.IsPartial = true;

            var interfaceRef = new CodeTypeReference(new CodeTypeParameter(staticClass.InterfaceName));

            // Inherit from Object (Required for the VB CodeDom to generate the Implements keyword)
            targetStaticWrapper.BaseTypes.Add(new CodeTypeReference(typeof(System.Object)));
            targetStaticWrapper.BaseTypes.Add(interfaceRef);
            
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
                var method = EmitFunctionSignature(staticClass, m);

                targetInterface.Members.Add(method);
            }
        }

        private static CodeMemberMethod EmitFunctionSignature(ClassDefinition staticClass, MethodSignature m)
        {
            var method = new CodeMemberMethod
            {
                Name = m.Name,
                ReturnType = new CodeTypeReference(m.Info.ReturnType)                
            };

            foreach (var p in m.Parameters)
            {
                var parameterType = p.AsType;
                if (parameterType.IsByRef)
                {
                    parameterType = p.AsType.GetElementType();
                }
                var parameterSignature = new CodeParameterDeclarationExpression(parameterType, p.Name);

                if (p.Info.ParameterType.IsByRef && p.Info.IsOut)
                {
                    parameterSignature.Direction = FieldDirection.Out;
                }
                else if (p.Info.ParameterType.IsByRef)
                {
                    parameterSignature.Direction = FieldDirection.Ref;
                }
                else if(p.Info.IsIn)
                {
                    parameterSignature.Direction = FieldDirection.In;
                }
                else if(p.Info.IsOut)
                {
                    parameterSignature.Direction = FieldDirection.Out;
                }

                method.Parameters.Add(parameterSignature);
            }

            return method;
        }

        
        public void EmitStaticWrapperClassMethods(ref CodeTypeDeclaration targetStaticClass, ClassDefinition staticClass, Visibility minimumVisibility)
        {
            foreach (var m in staticClass.Methods.Where(x => x.Accessability >= minimumVisibility).OrderBy(x => x.Name))
            {
                CodeMemberMethod method = EmitFunctionSignature(staticClass, m);
                method.Attributes = MemberAttributes.Public;
                method.ImplementationTypes.Add(new CodeTypeReference(new CodeTypeParameter(staticClass.InterfaceName)));


                CodeExpression[] methodParameters = m.Parameters.Select(x => GenParameterExpression(x)).ToArray();

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

        private static CodeExpression GenParameterExpression(ParameterSignature p)
        {
            FieldDirection direction = FieldDirection.In;
            var paramSnippet = new CodeSnippetExpression(p.Name);
            
            if (p.Info.IsOut || p.Info.ParameterType.IsByRef && p.Info.IsOut)
            {
                direction = FieldDirection.Out;
            }
            else if (p.Info.ParameterType.IsByRef)
            {
                direction = FieldDirection.Ref;
            }

            if(direction != FieldDirection.In)
            {
                return new CodeDirectionExpression(direction, paramSnippet); 
            }

            return paramSnippet;
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
