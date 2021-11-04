using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Text;

namespace CodeM.Common.Tools.Json
{
    public class DynamicObjectExt : DynamicObject
    {
        Dictionary<string, object> mValues = new Dictionary<string, object>();

        public Dictionary<string, object>.KeyCollection Keys
        {
            get
            {
                return mValues.Keys;
            }
        }

        public dynamic this[string key]
        {
            get
            {
                if (mValues.ContainsKey(key))
                {
                    return mValues[key];
                }
                return null;
            }
        }

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
                if ("ToXMLString".Equals(binder.Name) && args.Length == 0)
                {
                    args = new object[] { "" };
                }
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

                if (i == pathItems.Length - 1)
                {
                    bRet = currObj.TrySetValue(item, value);
                    break;
                }

                dynamic subObj = null;
                if (!currObj.TryGetValue(item, out subObj))
                {
                    subObj = new DynamicObjectExt();
                    currObj.TrySetValue(item, subObj);
                    currObj = subObj;
                }
                else
                {
                    if (subObj == null)
                    {
                        subObj = new DynamicObjectExt();
                        currObj.TrySetValue(item, subObj);
                    }
                    currObj = subObj;
                }

                if (!(currObj is DynamicObjectExt))
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
                    
                    if (!(currObj is DynamicObjectExt))
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

                    if (!(currObj is DynamicObjectExt))
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
                else if (_typ == typeof(bool))
                {
                    sbResult.Append(value.ToString().ToLower());
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

        internal string SerializeXMLAttributes()
        {
            StringBuilder sbResult = new StringBuilder();

            Dictionary<string, object>.Enumerator e = mValues.GetEnumerator();
            while (e.MoveNext())
            {
                if (!"Value".Equals(e.Current.Key))
                {
                    if (e.Current.Value != null)
                    {
                        Type _typ = e.Current.Value.GetType();
                        if (_typ == typeof(string) || _typ.IsValueType)
                        {
                            sbResult.Append(" " + e.Current.Key + "=\"" + e.Current.Value + "\"");
                        }
                    }
                    else
                    {
                        sbResult.Append(" " + e.Current.Key + "=\"\"");
                    }
                }
            }

            return sbResult.ToString();
        }

        internal string SerializeXMLNode(bool isRootNode = true)
        {
            StringBuilder sbResult = new StringBuilder();

            Dictionary<string, object>.Enumerator e = mValues.GetEnumerator();
            while (e.MoveNext())
            {
                if (e.Current.Value != null)
                {
                    if (e.Current.Value is DynamicObjectExt)
                    {
                        dynamic objValue = (DynamicObjectExt)e.Current.Value;

                        sbResult.Append("<" + e.Current.Key);
                        sbResult.Append(objValue.SerializeXMLAttributes());
                        sbResult.Append(">");

                        if (objValue.Has("Value"))
                        {
                            sbResult.Append(objValue.Value);
                        }
                        else
                        {
                            sbResult.Append(objValue.SerializeXMLNode(false));
                        }

                        sbResult.Append("</" + e.Current.Key + ">");
                    }
                    else
                    {
                        if (isRootNode)
                        {
                            sbResult.Append("<" + e.Current.Key + ">");
                            sbResult.Append(e.Current.Value.ToString());
                            sbResult.Append("</" + e.Current.Key + ">");
                        }
                    }
                }
                else
                {
                    if (isRootNode)
                    {
                        sbResult.Append("<" + e.Current.Key + " />");
                    }
                }
            }

            return sbResult.ToString();
        }

        public string ToXMLString(string defaultNS = "")
        {
            StringBuilder sbResult = new StringBuilder();

            sbResult.Append("<xml");
            if (!string.IsNullOrWhiteSpace(defaultNS))
            {
                sbResult.Append(" xmlns=\"" + defaultNS + "\"");
            }
            sbResult.Append(">");

            sbResult.Append(SerializeXMLNode());

            sbResult.Append("</xml>");

            return sbResult.ToString();
        }
    }
}
