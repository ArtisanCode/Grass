using System;
using System.Collections.Generic;
using System.Text;

namespace GrassTemplate.Internals
{
    public static class EnumHelper
    {
        /// <summary>
        /// A .NET 2.0 compatible way of checking if an Enum 'HasFlag'
        /// </summary>
        public static bool HasFlag(Enum target, Enum flag)
        {
            // check if from the same type.
            if (target.GetType() != flag.GetType())
            {
                throw new ArgumentException("The checked flag is not from the same type as the checked variable.");
            }

            ulong flagValue = Convert.ToUInt64(flag);
            ulong targetValue = Convert.ToUInt64(target);

            return (targetValue & flagValue) == flagValue;
        }
    }
}
