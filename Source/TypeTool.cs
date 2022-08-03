using System;
using System.Collections.Generic;

namespace CodeM.Common.Tools
{
    public class TypeTool
    {
        private static TypeTool sTTool = new TypeTool();

        private TypeTool()
        { 
        }

        internal static TypeTool New()
        {
            return sTTool;
        }

        public bool IsList(object obj)
        {
            Type _typ = obj.GetType();
            return _typ.IsGenericType && _typ.GetGenericTypeDefinition() == typeof(List<>);
        }
    }
}
