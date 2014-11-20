using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GrassTemplate.Internals
{
    public class ParameterSignature
    {
        protected HashSet<string> _requiredNamespaces;

        public string Type { get; set; }

        public string Name { get; set; }

        public ParameterInfo BaseInfo { get; set; }

        public HashSet<string> RequiredNamespaces {
            get { return _requiredNamespaces; }
            set { _requiredNamespaces = value; }
        }

        public ParameterSignature(ParameterInfo info)
        {
           RequiredNamespaces = new HashSet<string>();

            BaseInfo = info;
            Type = TypeHelper.DetermineType(info.ParameterType, ref _requiredNamespaces);
            Name = info.Name;
        }

        
    }
}
