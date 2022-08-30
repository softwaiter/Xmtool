using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

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

        /// <summary>
        /// 判断一个对象是否简单类型（值类型和字符串）
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool IsSimpleType(object obj)
        {
            Type _typ = obj.GetType();
            return _typ.IsValueType || _typ == typeof(string);
        }

        public bool IsCloneable(object obj)
        {
            return obj is ICloneable;
        }

        public bool IsList(object obj)
        {
            Type _typ = obj.GetType();
            return _typ.IsGenericType && _typ.GetGenericTypeDefinition() == typeof(List<>);
        }

        public IList CloneList(IList list, bool includeContent = true)
        {
            Type geneType = typeof(List<>);
            Type implType = geneType.MakeGenericType(list.GetType().GetGenericArguments());
            ConstructorInfo ci = implType.GetConstructor(new Type[] { });
            IList cloneObj = ci.Invoke(new Object[] { }) as IList;
            if (includeContent)
            {
                foreach (object item in list)
                {
                    cloneObj.Add(item);
                }
            }
            return cloneObj;
        }
    }
}
