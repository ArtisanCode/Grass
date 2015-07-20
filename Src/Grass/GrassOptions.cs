using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrassTemplate
{
    public class GrassOptions
    {
        public bool UseDynamic { get; set; }

        public bool GeneratePartialClass { get; set; }

        public Visibility MinimumVisibility { get; set; }

        public GrassOptions()
        {
            UseDynamic = true;
            GeneratePartialClass = true;
            MinimumVisibility = Visibility.Public;
        }
    }
}
