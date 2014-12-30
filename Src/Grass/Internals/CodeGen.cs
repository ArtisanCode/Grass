using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GrassTemplate.Internals
{
    public class CodeGen
    {
        
        public Tuple<string, CodeCompileUnit> EmitInterface(string targetNamespace, ClassDefinition staticClass, Visibility minimumVisibility)
        {
            CodeCompileUnit targetUnit = new CodeCompileUnit();
            CodeNamespace emittedNamespace = GenerateEmittedNamespace(targetNamespace, staticClass);

            string outputFileName = string.Format("{0}.cs", staticClass.InterfaceName);

            CodeTypeDeclaration targetInterface = new CodeTypeDeclaration(staticClass.InterfaceName);

            if (minimumVisibility.HasFlag(Visibility.Public))
            {
                targetInterface.TypeAttributes = TypeAttributes.Public;
            }
            else
            {
                targetInterface.TypeAttributes = TypeAttributes.NestedAssembly | TypeAttributes.NotPublic;
            }
            targetInterface.IsInterface = true;

            targetInterface.CustomAttributes.Add(new CodeAttributeDeclaration("GeneratedCode", new CodeAttributeArgument(new CodePrimitiveExpression(Assembly.GetAssembly(typeof(Grass)).GetName().Version.ToString())), new CodeAttributeArgument(new CodePrimitiveExpression("ArtisanCode.Grass"))));

            EmitInterfaceMethods(ref targetInterface, staticClass, minimumVisibility);

            emittedNamespace.Types.Add(targetInterface);

            targetUnit.Namespaces.Add(emittedNamespace);

            return new Tuple<string, CodeCompileUnit>(outputFileName, targetUnit);
        }

        public void EmitInterfaceMethods(ref CodeTypeDeclaration targetInterface, ClassDefinition staticClass, Visibility minimumVisibility)
        {
            foreach (var m in staticClass.Methods.Where(x => x.Accessability >= minimumVisibility).OrderBy(x => x.Name))
            {
                CodeMemberMethod method = new CodeMemberMethod();

                method.Name = m.Name;
                method.ReturnType = new CodeTypeReference(m.Info.ReturnType);

                foreach (var p in m.Parameters)
                {
                    method.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(p.Info.ParameterType), p.Name));
                }

                targetInterface.Members.Add(method);
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
