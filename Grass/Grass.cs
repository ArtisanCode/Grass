using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Grass
{
    public static class Grass
    {
        public static string Static(string qualifiedAssemblyName, bool partial = true)
        {
            var output = new StringBuilder();
            output.AppendLine("using System;");
            output.AppendLine();

            var className = GetClassName(qualifiedAssemblyName);
            output.AppendFormat("public {0} class {1}Wrapper {2}", (partial?"partial":""), className, Environment.NewLine);
            output.AppendLine("{");

            var methods = GetMethods(qualifiedAssemblyName);
            foreach (var m in methods)
            {
                output.AppendLine(GenerateMethodOutput(m));
            }

            output.AppendLine("}");

            return output.ToString();
        }

        public static string GenerateMethodOutput(MethodInfo m)
        {
            var output = new StringBuilder();

            output.AppendFormat("//{0} {1} {2}", GetMethodVisibility(m), GetReturnType(m), m.Name);

            return output.ToString();
        }

        public static string GetReturnType(MethodInfo m)
        {
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
