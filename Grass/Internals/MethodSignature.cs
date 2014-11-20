using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GrassTemplate.Internals
{
    public class MethodSignature
    {
        protected HashSet<string> _requiredNamespaces;

        public Visibility Accessability { get; set; }

        public string ReturnType { get; set; }

        public bool Virtual { get; set; }

        public string Name { get; set; }

        public ParameterSignature[] Parameters { get; set; }

        public MethodInfo BaseInfo { get; set; }

        public HashSet<string> RequiredNamespaces
        {
            get { return _requiredNamespaces; }
            set { _requiredNamespaces = value; }
        }
                
        public MethodSignature()
        {
            RequiredNamespaces = new HashSet<string>();
        }

        public MethodSignature(MethodInfo info, bool IsVirtual = true): this()
        {
            BaseInfo = info;
            Accessability = GetMethodVisibility(info);
            ReturnType = TypeHelper.DetermineType(info.ReturnType, ref _requiredNamespaces);
            Name = info.Name;
            Virtual = IsVirtual;

            var parameterList = new List<ParameterSignature>();
            foreach(var p in info.GetParameters())
            {
                parameterList.Add(new ParameterSignature(p));
            }
        }

        public Visibility GetMethodVisibility(MethodInfo m)
        {
            if (m.IsPublic)
            {
                return Visibility.Public;
            }

            if (m.IsPrivate)
            {
                return Visibility.Private;
            }

            if (m.IsFamily)
            {
                return Visibility.Protected;
            }

            else return Visibility.Internal;
        }        
    }
}
