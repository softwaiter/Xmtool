using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CodeM.Common.Tools.DynamicObject
{

    [Serializable]
    public class DynamicObjectExt : IDynamicMetaObjectProvider, IDictionary<string, object>, ICloneable
    {
        public object this[string key]
        {
            get
            {
                return GetValue(key);
            }
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
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        public object GetValue(string name)
        {
            if (mValues.ContainsKey(name))
            {
                return mValues[name];
            }
            else if ("Keys".Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return mValues.Keys;
            }
            else if ("Values".Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return mValues.Values;
            }
            else if ("Count".Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return mValues.Count;
            }
            else if ("IsReadOnly".Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return null;
        }

        public object GetValueByPath(string path)
        {
            object result = null;
            string[] pathItems = path.Split('.');
            dynamic currObj = this;
            for (int i = 0; i < pathItems.Length; i++)
            {
                string item = pathItems[i];
                if (currObj.TryGetValue(item, out result))
                {
                    currObj = result;
                }
                else
                {
                    break;
                }
            }
            return result;
        }

        public object SetValue(string name, object value)
        {
            mValues[name] = value;
            return value;
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

        private string SerializeArray(dynamic list)
        {
            StringBuilder sbResult = new StringBuilder();

            sbResult.Append("[");
            for (int i = 0; i < list.Length; i++)
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
                    sbResult.Append(string.Concat("\"", value.Replace("\"", "\\\""), "\""));
                }
                else if (_typ.IsArray)
                {
                    sbResult.Append(SerializeArray(value));
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
                        else if (_typ.IsArray)
                        {
                            StringBuilder sbValue = new StringBuilder();
                            dynamic arrObj = e.Current.Value;
                            for (int i = 0; i < arrObj.Length; i++)
                            {
                                if (sbValue.Length > 0)
                                {
                                    sbValue.Append(",");
                                }
                                sbValue.Append(arrObj[i]);
                            }
                            sbResult.Append(" " + e.Current.Key + "=\"" + sbValue + "\"");
                        }
                        else if (_typ.IsGenericType &&
                            _typ.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            StringBuilder sbValue = new StringBuilder();
                            dynamic listObj = e.Current.Value;
                            for (int i = 0; i < listObj.Count; i++)
                            {
                                if (sbValue.Length > 0)
                                {
                                    sbValue.Append(",");
                                }
                                sbValue.Append(listObj[i]);
                            }
                            sbResult.Append(" " + e.Current.Key + "=\"" + sbValue + "\"");
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
                            Type _typ = e.Current.Value.GetType();
                            if (_typ.IsArray)
                            {
                                sbResult.Append("<" + e.Current.Key + ">");
                                dynamic arrObj = e.Current.Value;
                                for (int i = 0; i < arrObj.Length; i++)
                                {
                                    sbResult.Append("<value>");
                                    sbResult.Append(arrObj[i]);
                                    sbResult.Append("</value>");
                                }
                                sbResult.Append("</" + e.Current.Key + ">");
                            }
                            else if (_typ.IsGenericType &&
                                _typ.GetGenericTypeDefinition() == typeof(List<>))
                            {
                                sbResult.Append("<" + e.Current.Key + ">");
                                dynamic listObj = e.Current.Value;
                                for (int i = 0; i < listObj.Count; i++)
                                {
                                    sbResult.Append("<item>");
                                    sbResult.Append(listObj[i]);
                                    sbResult.Append("</item>");
                                }
                                sbResult.Append("</" + e.Current.Key + ">");
                            }
                            else
                            {
                                sbResult.Append("<" + e.Current.Key + ">");
                                sbResult.Append(e.Current.Value.ToString());
                                sbResult.Append("</" + e.Current.Key + ">");
                            }
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

        #region IDictionary<string, object>
        private Dictionary<string, object> mValues = new Dictionary<string, object>();

        public void Add(string key, object value)
        {
            SetValue(key, value);
        }

        public bool ContainsKey(string key)
        {
            return mValues.ContainsKey(key);
        }

        public void Add(KeyValuePair<string, object> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            mValues.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            if (mValues.ContainsKey(item.Key))
            {
                return mValues[item.Key] == item.Value;
            }
            return false;
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            mValues.ToArray().CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            if (Contains(item))
            {
                return Remove(item.Key);
            }
            return false;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return mValues.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return mValues.GetEnumerator();
        }

        ICollection<string> IDictionary<string, object>.Keys => mValues.Keys;

        public ICollection<object> Values => mValues.Values;

        public int Count => mValues.Count;

        public bool IsReadOnly => false;

        object IDictionary<string, object>.this[string key] { get => GetValue(key); set => SetValue(key, value); }
        #endregion

        #region IDynamicMetaObjectProvider
        public DynamicMetaObject GetMetaObject(Expression parameter)
        {
            return new DynamicObjectExtMetaObject(parameter, BindingRestrictions.Empty, this);
        }
        #endregion

        #region ICloneable
        public object Clone()
        {
            DynamicObjectExt cloneObj = new DynamicObjectExt();
            Dictionary<string, object>.Enumerator e = mValues.GetEnumerator();
            while (e.MoveNext())
            {
                cloneObj.SetValue(e.Current.Key, e.Current.Value);
            }
            return cloneObj;
        }
        #endregion
    }
}
