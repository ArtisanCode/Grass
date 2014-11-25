using System;
using System.Collections.Generic;
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

        public ParameterSignature()
        {
            RequiredNamespaces = new HashSet<string>();
        }

        public ParameterSignature(ParameterInfo info) : this()
        {
            BaseInfo = info;
            Name = info.Name;
            Type = TypeHelper.DetermineType(info.ParameterType, ref _requiredNamespaces);
        }

        public string ToParameterDefinition()
        {
            return string.Format("{0} {1}", Type, Name);
        }
    }
}
