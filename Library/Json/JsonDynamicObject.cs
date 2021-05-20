using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Text;

namespace CodeM.Common.Tools.Json
{
    internal class JsonDynamicObject : DynamicObject
    {
        Dictionary<string, object> mValues = new Dictionary<string, object>();

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            mValues[binder.Name] = value;
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            MethodInfo mi = GetType().GetMethod(binder.Name);
            if (mi != null)
            {
                result = mi.Invoke(this, args);
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (mValues.ContainsKey(binder.Name))
            {
                result = mValues[binder.Name];
            }
            else
            {
                result = null;
            }
            return true;
        }
        public bool TrySetValue(string name, object value)
        {
            mValues[name] = value;
            return true;
        }

        public bool TryGetValue(string name, out object result)
        {
            if (mValues.ContainsKey(name))
            {
                result = mValues[name];
            }
            else
            {
                result = null;
            }
            return true;
        }

        public bool SetValueByPath(string path, object value)
        {
            bool bRet = true;
            string[] pathItems = path.Split('.');
            dynamic currObj = this;
            for (int i = 0; i < pathItems.Length; i++)
            {
                string item = pathItems[i];
                if (currObj.Has(item))
                {
                    if (i == pathItems.Length - 1)
                    {
                        bRet = currObj.TrySetValue(item, value);
                        break;
                    }

                    if (!currObj.TryGetValue(item, out currObj))
                    {
                        bRet = false;
                        break;
                    }

                    if (!(currObj is JsonDynamicObject))
                    {
                        bRet = false;
                        break;
                    }
                }
                else
                {
                    bRet = false;
                    break;
                }
            }
            return bRet;
        }

        public bool Has(string key)
        {
            return mValues.ContainsKey(key);
        }

        public bool HasPath(string path)
        {
            bool bRet = true;
            string[] pathItems = path.Split('.');
            dynamic currObj = this;
            for (int i = 0; i < pathItems.Length; i++)
            {
                string item = pathItems[i];
                if (currObj.Has(item))
                { 
                    if (i == pathItems.Length - 1)
                    {
                        break;
                    }

                    if (!currObj.TryGetValue(item, out currObj))
                    {
                        bRet = false;
                        break;
                    }
                    
                    if (!(currObj is JsonDynamicObject))
                    {
                        bRet = false;
                        break;
                    }
                }
                else
                {
                    bRet = false;
                    break;
                }
            }
            return bRet;
        }

        public bool Remove(string key)
        {
            return mValues.Remove(key);
        }

        public bool RemovePath(string path)
        {
            bool bRet = true;
            string[] pathItems = path.Split('.');
            dynamic currObj = this;
            for (int i = 0; i < pathItems.Length; i++)
            {
                string item = pathItems[i];
                if (currObj.Has(item))
                {
                    if (i == pathItems.Length - 1)
                    {
                        currObj.Remove(item);
                        break;
                    }

                    if (!currObj.TryGetValue(item, out currObj))
                    {
                        bRet = false;
                        break;
                    }

                    if (!(currObj is JsonDynamicObject))
                    {
                        bRet = false;
                        break;
                    }
                }
                else
                {
                    bRet = false;
                    break;
                }
            }
            return bRet;
        }

        private string SerializeList(dynamic list)
        {
            StringBuilder sbResult = new StringBuilder();

            sbResult.Append("[");
            for (int i = 0; i < list.Count; i++)
            {
                if (i > 0)
                {
                    sbResult.Append(",");
                }

                sbResult.Append(SerializeValue(list[i]));
            }
            sbResult.Append("]");

            return sbResult.ToString();
        }

        private string SerializeValue(dynamic value)
        {
            StringBuilder sbResult = new StringBuilder();

            if (value != null)
            {
                Type _typ = value.GetType();
                if (_typ == typeof(string))
                {
                    sbResult.Append(string.Concat("\"", value, "\""));
                }
                else if (_typ.IsGenericType &&
                    _typ.GetGenericTypeDefinition() == typeof(List<>))
                {
                    sbResult.Append(SerializeList(value));
                }
                else
                {
                    sbResult.Append(value.ToString());
                }
            }
            else
            {
                sbResult.Append("null");
            }

            return sbResult.ToString();
        }

        public override string ToString()
        {
            StringBuilder sbResult = new StringBuilder();

            sbResult.Append("{");
            
            Dictionary<string, object>.Enumerator e = mValues.GetEnumerator();
            while (e.MoveNext())
            {
                if (sbResult.Length > 2)
                {
                    sbResult.Append(",");
                }

                sbResult.Append(string.Concat("\"", e.Current.Key, "\":"));
                sbResult.Append(SerializeValue(e.Current.Value));
            }

            sbResult.Append("}");

            return sbResult.ToString();
        }

    }
}
