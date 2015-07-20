using System;
using System.Collections.Generic;
using System.Text;

namespace GrassTemplate.Internals
{
    public class TypeHelper
    {
        public static string DetermineType(Type t, ref HashSet<string> namespaces, GrassOptions options)
        {
            namespaces.Add(t.Namespace);

            if (t.IsGenericType)
            {
                var name = t.Name.Split(new[] { '`' })[0];

                var genericParams = new List<string>();

                foreach (var genericArguement in t.GetGenericArguments())
                {
                    genericParams.Add(DetermineType(genericArguement, ref namespaces, options));
                }

                return string.Format("{0}<{1}>", name, string.Join(", ", genericParams.ToArray()));
            }

            if (t == typeof(void))
            {
                return "void";
            }

            if(t.Name.Equals("object", StringComparison.OrdinalIgnoreCase) && options.UseDynamic)
            {
                return "dynamic";
            }

            return t.Name.Replace("&",""); // Fix ref type issues
        }
    }
}
