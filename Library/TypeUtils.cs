using System;
using System.Collections.Generic;

namespace CodeM.Common.Tools
{
    public class TypeUtils
    {
        public static bool IsList(object obj)
        {
            Type _typ = obj.GetType();
            return _typ.IsGenericType && _typ.GetGenericTypeDefinition() == typeof(List<>);
        }
    }
}
