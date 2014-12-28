using GrassTemplate.Internals;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace GrassTemplate
{
    public static class Grass
    {
        public static string GeneratedCodeTag { get { return String.Format("[GeneratedCode(\"{0}\",\"{1}\")]", "ArtisanCode.Grass", Assembly.GetAssembly(typeof(Grass)).GetName().Version); } }
        
        public static string Static(string qualifiedAssemblyName, Visibility minimumVisibility = Visibility.Public, bool partial = true)
        {
            var output = new StringBuilder();

            var callContext = CallContext.LogicalGetData("NamespaceHint");
            var ns = callContext == null ? "ArtisanCode.Grass.GeneratedContent" : callContext.ToString();

            var staticClass = new ClassDefinition(qualifiedAssemblyName, minimumVisibility, partial);
            staticClass.PopulateStaticMethods();

            var namespaces = staticClass.GetRequiredNamespaces();
            namespaces.Add("System.CodeDom.Compiler"); // Required for the class generated attribute

            output.AppendLine(GenerateUsingStatements(namespaces));
            output.AppendLine(GenerateInterfaceCode(staticClass, ns));

            return output.ToString();
            //var interfaces = new Dictionary<Visibility, HashSet<string>>();

            //var className = GenerateNames(qualifiedAssemblyName);

            //var methods = GetStaticMethods(qualifiedAssemblyName, minimumVisibility);

            //List<string> methodsOutput = new List<string>();            
            //foreach (var m in methods)
            //{
            //    methodsOutput.Add(GenerateMethodOutput(m, ref namespaces, ref interfaces));
            //}


            //output.AppendLine(GenerateUsingStatements(namespaces));
            //output.AppendLine();

            //output.AppendLine(GenerateInterfaceCode(className, methodsOutput, interfaces, ns));

            //output.AppendLine(GenerateClassCode(className, partial, methodsOutput, ns));

            //return output.ToString();
        }

        public static string GenerateInterfaceCode(ClassDefinition classDef, string ns)
        {
            StringBuilder output = new StringBuilder();
            
            output.AppendFormat("namespace {0}{1}", ns, Environment.NewLine);

            output.AppendLine("{");
            output.AppendLine(Indent(1) + GeneratedCodeTag);
            output.AppendLine(Indent(1) + classDef.GetInterfaceSignature());
            output.AppendLine(Indent(1) + "{");

            foreach (var m in classDef.Methods)
            {
                output.AppendLine(Indent(2) + m.ToInterfaceDefinition());
            }
            output.AppendLine(Indent(1) + "}");
            output.AppendLine("}");

            return output.ToString();
        }

        //public static string GenerateInterfaceCode(string className, List<string> methodsOutput, Dictionary<Visibility, HashSet<string>> interfaces, string ns)
        //{
        //    StringBuilder output = new StringBuilder();

        //    foreach (var i in interfaces)
        //    {
        //        output.AppendFormat("namespace {0}{1}", ns, Environment.NewLine);

        //        output.AppendLine("{");
        //        output.AppendLine(Indent(1) + GeneratedCodeTag);
        //        output.AppendFormat("{0}{1} interface I{2}{3} {4}", Indent(1), i.Key.ToString().ToLower(), className, i.Key == Visibility.Public ? "" : i.Key.ToString(), Environment.NewLine);
        //        output.AppendLine(Indent(1) + "{");
        //        foreach (var m in methodsOutput)
        //        {
        //            output.AppendLine(Indent(2) + m.Replace(i.Key.ToString(),"") + ";");
        //        }
        //        output.AppendLine(Indent(1) + "}");
        //        output.AppendLine("}");
        //    }

        //    return output.ToString();
        //}

        //public static string GenerateClassCode(string className, bool partial, List<string> methodsOutput, string ns)
        //{
        //    StringBuilder output = new StringBuilder();

        //    output.AppendFormat("namespace {0}{1}", ns, Environment.NewLine);
        //    output.AppendLine("{");
        //    output.AppendLine(Indent(1) + GeneratedCodeTag);
        //    output.AppendFormat("{0}public {1} class {2}Wrapper : {3} {4}", Indent(1), (partial ? "partial" : ""), className, "I"+className, Environment.NewLine);
        //    output.AppendLine(Indent(1) + "{");
        //    foreach (var m in methodsOutput)
        //    {
        //        output.AppendLine(Indent(2) + m);
        //    }
        //    output.AppendLine(Indent(1) + "}");
        //    output.AppendLine("}");

        //    return output.ToString();
        //}

        public static string Indent(int level) 
        {
            return new string(' ', level * 4);
        }

        private static string GenerateUsingStatements(HashSet<string> namespaces)
        {
            var output = new StringBuilder();

            var ns = namespaces.All();
            Array.Sort(ns);
            foreach (var x in ns)
            {
                output.AppendFormat("using {0};{1}", x, Environment.NewLine);
            }

            return output.ToString();        
        }

        public static void InitInterface(ref Dictionary<Visibility,HashSet<string>> interfaces, Visibility accessability)
        {
            if(interfaces == null)
            {
                interfaces = new Dictionary<Visibility, HashSet<string>>();
            }

            if(!interfaces.ContainsKey(accessability))
            {
                interfaces.Add(accessability, new HashSet<string>());
            }
        }

        public static string GenerateMethodOutput(MethodInfo m, ref HashSet<string> namespaces, ref Dictionary<Visibility,HashSet<string>> interfaces)
        {
            var output = new StringBuilder();
            var methodSigniture = string.Format("{0} {1}", DetermineType(m.ReturnType, ref namespaces), m.Name);

            if(m.IsPublic)
            {
                InitInterface(ref interfaces, Visibility.Public);

                interfaces[Visibility.Public].Add(methodSigniture + ";");
            }
            else if(m.IsFamilyOrAssembly)
            {
                InitInterface(ref interfaces, Visibility.Internal);

                interfaces[Visibility.Internal].Add(methodSigniture + ";");
            }

            output.AppendFormat("//{0} virtual {1}", GetMethodVisibility(m),methodSigniture);

            return output.ToString();
        }

        public static string DetermineType(Type t, ref HashSet<string> namespaces)
        {
            namespaces.Add(t.Namespace);

            if(t.IsGenericType)
            {
                var name = t.Name.Split(new[] {'`'})[0];

                var genericParams = new List<string>();

                foreach (var genericArguement in t.GetGenericArguments())
                {
                    genericParams.Add(DetermineType(genericArguement, ref namespaces));
                }

                return string.Format("{0}<{1}>", name, string.Join(", ", genericParams.ToArray()));
            }
            
            if(t == typeof(void))
            {
                return "void";
            }

            return t.Name;

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

            if(m.IsFamily)
            {
                return "protected";
            }

            else return "internal";
        }

        public static MethodInfo[] GetStaticMethods(string qualifiedAssemblyName, Visibility minimumVisibility = Visibility.Public)
        {
            var type = Type.GetType(qualifiedAssemblyName);

            var accessor = BindingFlags.Public;

            if (EnumHelper.HasFlag(minimumVisibility, Visibility.Internal) || 
                EnumHelper.HasFlag(minimumVisibility, Visibility.Protected) ||
                EnumHelper.HasFlag(minimumVisibility, Visibility.Private))
            {
                accessor |= BindingFlags.NonPublic;
            }

            var methods = type.GetMethods(accessor | BindingFlags.Static);
            
            return methods;
        }

        public static string GetClassName(string qualifiedAssemblyName)
        {
            var classReference = qualifiedAssemblyName.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[0];
            var reference = classReference.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            return reference[reference.Length - 1];
        }
    }
}
