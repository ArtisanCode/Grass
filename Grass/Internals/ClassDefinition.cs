﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GrassTemplate.Internals
{
    public class ClassDefinition
    {
        public string ClassName { get; set; }

        public string InterfaceName { get; set; }

        public string QualifiedAssemblyName { get; set; }

        public Visibility MinimumVisibility { get; set; }

        List<MethodSignature> Methods { get; set; }

        public bool IsPartial { get; set; }

        public ClassDefinition(string qualifiedAssemblyName, Visibility minimumVisibility = Visibility.Public, bool isPartial = true)
        {
            QualifiedAssemblyName = qualifiedAssemblyName;
            MinimumVisibility = minimumVisibility;
            IsPartial = isPartial;
            GenerateNames();
        }

        public string GetClassSignature(string accessability = "public")
        {
            return string.Format("{0}{1} class {2}Wrapper : {3}", accessability, (IsPartial ? " partial" : ""), ClassName, InterfaceName);
        }
        
        public string GetInterfaceSignature(string accessability = "public")
        {
            return string.Format("{0} interface {1}", accessability, InterfaceName);
        }

        public void GenerateNames()
        {
            var classReference = QualifiedAssemblyName.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[0];
            var reference = classReference.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);

            ClassName = reference[reference.Length - 1];
            InterfaceName = "I" + ClassName;
        }
        
        public void PopulateStaticMethods()
        {
            Methods = new List<MethodSignature>();

            var type = Type.GetType(QualifiedAssemblyName);

            var accessor = BindingFlags.Public;

            if (EnumHelper.HasFlag(MinimumVisibility, Visibility.Internal) ||
                EnumHelper.HasFlag(MinimumVisibility, Visibility.Protected) ||
                EnumHelper.HasFlag(MinimumVisibility, Visibility.Private))
            {
                accessor |= BindingFlags.NonPublic;
            }

            var methods = type.GetMethods(accessor | BindingFlags.Static);
            
            foreach (var info in methods)
            {
                Methods.Add(new MethodSignature(info));
            }
        }
    }
}