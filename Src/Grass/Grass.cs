using GrassTemplate.Internals;
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

        public static void Static(ITextTemplatingEngineHost host, string qualifiedAssemblyName, Visibility minimumVisibility = Visibility.Public, bool partial = true)
        {
            var callContext = CallContext.LogicalGetData("NamespaceHint");
            var ns = callContext == null ? "ArtisanCode.Grass.GeneratedContent" : callContext.ToString();

            var staticClass = new ClassDefinition(qualifiedAssemblyName, minimumVisibility, partial);
            staticClass.PopulateStaticMethods();

            ICodeGen engine = new CodeGen();

            var emittedInterface = engine.EmitInterface(ns, staticClass, minimumVisibility);
            var emittedStaticWrapper = engine.EmitStaticWrapperClass(ns, staticClass, minimumVisibility);

            string templateDirectory = Path.GetDirectoryName(host.TemplateFile);
            string interfaceFilePath = Path.Combine(templateDirectory, emittedInterface.Item1);
            string classWrapperFilePath = Path.Combine(templateDirectory, emittedStaticWrapper.Item1);


            WriteCodefileToDisk(emittedInterface.Item2, interfaceFilePath);
            WriteCodefileToDisk(emittedStaticWrapper.Item2, classWrapperFilePath);

            AddFileToTemplate(host, interfaceFilePath);
            AddFileToTemplate(host, classWrapperFilePath);
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

        public static void Clean(ITextTemplatingEngineHost host)
        {
            var projectItem = LocateTemplateFile(host);
            foreach (ProjectItem i in projectItem.ProjectItems)
            {
                i.Remove();
            }            
        }

        private static void AddFileToTemplate(ITextTemplatingEngineHost host, string outputFilePath)
        {
            var projectItem = LocateTemplateFile(host);
            projectItem.ProjectItems.AddFromFile(outputFilePath);
        }

        private static ProjectItem LocateTemplateFile(ITextTemplatingEngineHost host)
        {
            IServiceProvider hostServiceProvider = (IServiceProvider)host;
            DTE dte = (DTE)hostServiceProvider.GetService(typeof(DTE));

            var projectItem = dte.Solution.FindProjectItem(host.TemplateFile);
            return projectItem;
        }
    }
}
