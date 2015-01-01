using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrassTemplate.Internals
{
    public class VBCodeGen: CodeGen
    {
        public override string FileExtension { get { return "vb"; } }
        public override CodeDomProvider CreateCodeDomProvider()
        {
            return CodeDomProvider.CreateProvider("VisualBasic");
        }

    }
}
