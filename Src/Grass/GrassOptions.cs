using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrassTemplate
{
    public class GrassOptions
    {
        public bool GeneratePartialClass { get; set; }

        public bool GenerateVirtualMethods { get; set; }

        public Visibility MinimumVisibility { get; set; }

        public bool UseDynamic { get; set; }

        public GrassOptions()
        {
            UseDynamic = true;
            GeneratePartialClass = true;
            GenerateVirtualMethods = true;
            MinimumVisibility = Visibility.Public;
        }
    }
}
