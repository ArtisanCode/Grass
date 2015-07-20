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
using GrassTemplate.Internals.Generation;

namespace GrassTemplate
{
    public static class Grass
    {
        public static string GeneratedCodeTag { get { return String.Format("[GeneratedCode(\"{0}\",\"{1}\")]", "ArtisanCode.Grass", Assembly.GetAssembly(typeof(Grass)).GetName().Version); } }

        public static void Static(ITextTemplatingEngineHost host, string qualifiedAssemblyName, GrassOptions codeGenOptions = null)
        {
            var options = codeGenOptions ?? new GrassOptions();

            var callContext = CallContext.LogicalGetData("NamespaceHint");
            var ns = callContext == null ? "ArtisanCode.Grass.GeneratedContent" : callContext.ToString();

            var staticClass = new ClassDefinition(qualifiedAssemblyName, ns, options);
            staticClass.PopulateStaticMethods();

            ICodeGen engine = CreateCodeGenEngine(host);

            var emittedInterface = engine.EmitInterface(ns, staticClass, options);
            var emittedStaticWrapper = engine.EmitStaticWrapperClass(ns, staticClass, options);

            string templateDirectory = Path.GetDirectoryName(host.TemplateFile);
            string interfaceFilePath = Path.Combine(templateDirectory, emittedInterface.Filename);
            string classWrapperFilePath = Path.Combine(templateDirectory, emittedStaticWrapper.Filename);


            WriteCodefileToDisk(engine, emittedInterface.CompilationOutput, interfaceFilePath);
            WriteCodefileToDisk(engine, emittedStaticWrapper.CompilationOutput, classWrapperFilePath);

            AddFileToTemplate(host, interfaceFilePath);
            AddFileToTemplate(host, classWrapperFilePath);
        }

        private static ICodeGen CreateCodeGenEngine(ITextTemplatingEngineHost host)
        {
            var projectItem = LocateTemplateFile(host);

            if (projectItem.ContainingProject.FullName.Contains("vbproj"))
            {
                return new VBCodeGen();
            }

            return new CSCodeGen();

        }

        private static void WriteCodefileToDisk(ICodeGen engine, CodeCompileUnit emittedInterface, string outputFilePath)
        {
            CodeDomProvider provider = engine.CreateCodeDomProvider();
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
