using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;

namespace GrassTemplate.Internals
{
    public class CodeGenerationResult
    {
        public string Filename { get; set; }

        public CodeCompileUnit CompilationOutput { get; set; }

        public CodeGenerationResult(string filename, CodeCompileUnit compilationOutput)
        {
            this.Filename = filename;
            this.CompilationOutput = compilationOutput;
        }
    }
}
