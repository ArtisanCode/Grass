using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrassTemplate.Internals.Generation
{
    public class CSCodeGen : CodeGen
    {
        public override string FileExtension { get { return "cs"; } }

        public override CodeDomProvider CreateCodeDomProvider()
        {
            return CodeDomProvider.CreateProvider("CSharp");
        }
    }
}
