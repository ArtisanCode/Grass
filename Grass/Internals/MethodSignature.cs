using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GrassTemplate.Internals
{
    public class MethodSignature
    {
        public Visibility Accessability { get; set; }

        public string ReturnType { get; set; }

        public bool Virtual { get; set; }

        public string Name { get; set; }

        public ParameterSignature[] Parameters { get; set; }

        public MethodInfo BaseInfo { get; set; }

        public HashSet<string> RequiredNamespaces { get; set; }

        public MethodSignature(MethodInfo info, bool IsVirtual = true)
        {
            RequiredNamespaces = new HashSet<string>();

            BaseInfo = info;
            Accessability = GetMethodVisibility(info);
            ReturnType = DetermineType(info.ReturnType);
            Name = info.Name;
            Virtual = IsVirtual;
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
        
        public string DetermineType(Type t)
        {
            RequiredNamespaces.Add(t.Namespace);

            if (t.IsGenericType)
            {
                var name = t.Name.Split(new[] { '`' }).First();

                var genericParams = new List<string>();

                foreach (var genericArguement in t.GetGenericArguments())
                {
                    genericParams.Add(DetermineType(genericArguement));
                }

                return string.Format("{0}<{1}>", name, string.Join(", ", genericParams));
            }

            if (t == typeof(void))
            {
                return "void";
            }

            return t.Name;
        }
    }
}
