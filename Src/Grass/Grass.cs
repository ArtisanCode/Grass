﻿using GrassTemplate.Internals;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Linq;
using System.CodeDom.Compiler;
using System.IO;
using Microsoft.VisualStudio.TextTemplating;
using EnvDTE;

namespace GrassTemplate
{
    public static class Grass
    {
        public static string GeneratedCodeTag { get { return String.Format("[GeneratedCode(\"{0}\",\"{1}\")]", "ArtisanCode.Grass", Assembly.GetAssembly(typeof(Grass)).GetName().Version); } }

        public static void StaticCodeGen(ITextTemplatingEngineHost host, string qualifiedAssemblyName, Visibility minimumVisibility = Visibility.Public, bool partial = true)
        {
            var callContext = CallContext.LogicalGetData("NamespaceHint");
            var ns = callContext == null ? "ArtisanCode.Grass.GeneratedContent" : callContext.ToString();

            var staticClass = new ClassDefinition(qualifiedAssemblyName, minimumVisibility, partial);
            staticClass.PopulateStaticMethods();

            var engine = new CodeGen();

            var emittedInterface = engine.EmitInterface(ns, staticClass, minimumVisibility);

            string templateDirectory = Path.GetDirectoryName(host.TemplateFile);
            string outputFilePath = Path.Combine(templateDirectory, emittedInterface.Item1);

            WriteCodefileToDisk(emittedInterface.Item2, outputFilePath);

            AddFileToTemplate(host, outputFilePath);
        }

        private static void WriteCodefileToDisk(CodeCompileUnit emittedInterface, string outputFilePath)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            using (StreamWriter sourceWriter = new StreamWriter(outputFilePath))
            {
                provider.GenerateCodeFromCompileUnit(emittedInterface, sourceWriter, options);
                sourceWriter.Flush();
            }
        }

        private static void AddFileToTemplate(ITextTemplatingEngineHost host, string outputFilePath)
        {
            IServiceProvider hostServiceProvider = (IServiceProvider)host;
            DTE dte = (DTE)hostServiceProvider.GetService(typeof(DTE));

            var projectItem = dte.Solution.FindProjectItem(host.TemplateFile);
            projectItem.ProjectItems.AddFromFile(outputFilePath);
        }

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

        public static string Indent(int level) 
        {
            return new string(' ', level * 4);
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
