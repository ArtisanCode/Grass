using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Grass
{
    public static class Grass
    {
        public static string GeneratedCodeTag { get { return String.Format("[GeneratedCode(\"{0}\",\"{1}\")]", "ArtisanCode.Grass", Assembly.GetAssembly(typeof(Grass)).GetName().Version); } }
        public static string Static(string qualifiedAssemblyName, bool partial = true)
        {
            var output = new StringBuilder();
            var namespaces = new HashSet<string>() { "System.CodeDom.Compiler" };

            var className = GetClassName(qualifiedAssemblyName);
            var methods = GetMethods(qualifiedAssemblyName);

            var methodsOutput = methods.Select(x=>GenerateMethodOutput(x, ref namespaces)).ToList();

            output.AppendLine(GenerateUsingStatements(namespaces));
            output.AppendLine();

            output.AppendLine(GeneratedCodeTag);
            output.AppendFormat("public {0} class {1}Wrapper {2}", (partial?"partial":""), className, Environment.NewLine);
            output.AppendLine("{");
            foreach (var m in methodsOutput)
            {
                output.AppendLine(m);
            }
            output.AppendLine("}");

            return output.ToString();
        }

        private static string GenerateUsingStatements(HashSet<string> namespaces)
        {
            var output = new StringBuilder();

            foreach (var x in namespaces.OrderBy(x=>x))
            {
                output.AppendFormat("using {0};{1}", x, Environment.NewLine);
            }

            return output.ToString();
        }

        public static string GenerateMethodOutput(MethodInfo m, ref HashSet<string> namespaces)
        {
            var output = new StringBuilder();

            output.AppendFormat("//{0} {1} {2}", GetMethodVisibility(m), GetReturnType(m, ref namespaces), m.Name);

            return output.ToString();
        }

        public static string GetReturnType(MethodInfo m, ref HashSet<string> namespaces)
        {
            namespaces.Add(m.ReturnType.Namespace);

            if(m.ReturnType == typeof(void))
            {
                return "void";
            }

            return m.ReturnType.Name;

        }

        public static string GetMethodVisibility(MethodInfo m)
        {
            if(m.IsPublic)
            {
                return "public";
            }

            if (m.IsPrivate)
            {
                return "private";
            }

            else return "internal";
        }

        public static List<MethodInfo> GetMethods(string qualifiedAssemblyName)
        {
            var type = Type.GetType(qualifiedAssemblyName);

            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static);

            return methods.OrderBy(x => x.Name).ToList();
        }

        public static string GetClassName(string qualifiedAssemblyName)
        {
            return qualifiedAssemblyName.Split(new[] {"."}, StringSplitOptions.RemoveEmptyEntries).Last();
        }
    }
}
