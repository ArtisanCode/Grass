using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace GrassTemplate.Internals
{
    public class MethodSignature
    {
        protected HashSet<string> _requiredNamespaces;

        public Visibility Accessability { get; set; }

        public string ReturnType { get; set; }

        public bool Virtual { get; set; }

        public string Name { get; set; }

        public List<ParameterSignature> Parameters { get; set; }

        public MethodInfo BaseInfo { get; set; }

        public HashSet<string> RequiredNamespaces
        {
            get { return _requiredNamespaces; }
            set { _requiredNamespaces = value; }
        }
                
        public MethodSignature()
        {
            RequiredNamespaces = new HashSet<string>();
            Parameters = new List<ParameterSignature>();
        }

        public MethodSignature(MethodInfo info, bool IsVirtual = true): this()
        {
            BaseInfo = info;
            Accessability = GetMethodVisibility(info);
            ReturnType = TypeHelper.DetermineType(info.ReturnType, ref _requiredNamespaces);
            Name = info.Name;
            Virtual = IsVirtual;

            foreach(var p in info.GetParameters())
            {
                Parameters.Add(new ParameterSignature(p));
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

        public string GetParameterList()
        {
            List<string> output = new List<string>();

            foreach (var p in Parameters)
            {
                output.Add(p.ToParameterDefinition());
            }

            return string.Join(", ", output.ToArray());
        }

        public HashSet<string> GetRequiredNamespaces()
        {
            Parameters.SelectMany(p => p.RequiredNamespaces).ToList().ForEach(n => RequiredNamespaces.Add(n));
            return RequiredNamespaces;            
        }

        public string ToClassDefinition()
        {
            return string.Format( 
                "{0}{1} {2} {3}({4})", 
                Accessability.ToString().ToLower(),
                Virtual?" virtual":"",
                ReturnType,
                Name,
                GetParameterList());
        }

        public string ToInterfaceDefinition()
        {
            return string.Format(
                "{0} {1}({2});",
                ReturnType,
                Name,
                GetParameterList());
        }
    }
}
