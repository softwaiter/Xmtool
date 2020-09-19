using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

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

        public bool Has(string key)
        {
            return mValues.ContainsKey(key);
        }

    }
}
