using System;
using System.Dynamic;
using System.Linq.Expressions;

namespace CodeM.Common.Tools.Json
{
    [Serializable]
    internal class DynamicObjectExtMetaObject : DynamicMetaObject
    {
        DynamicObjectExt mValue;

        public DynamicObjectExtMetaObject(Expression expression, BindingRestrictions restrictions, object value) :
            base(expression, restrictions, value)
        {
            mValue = (DynamicObjectExt)value;
        }

        public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
        {
            string methodName = "SetValue";
            BindingRestrictions restrictions = BindingRestrictions.GetTypeRestriction(Expression, LimitType);
            Expression[] args = new Expression[2];
            args[0] = Expression.Constant(binder.Name);
            args[1] = Expression.Convert(value.Expression, typeof(object));
            Expression self = Expression.Convert(Expression, LimitType);
            Expression methodCall = Expression.Call(self, typeof(DynamicObjectExt ).GetMethod(methodName), args);
            DynamicMetaObject dmoSetValue = new DynamicMetaObject(methodCall, restrictions);
            return dmoSetValue;
        }

        public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
        {
            string methodName = "GetValue";
            BindingRestrictions restrictions = BindingRestrictions.GetTypeRestriction(Expression, LimitType);
            Expression[] args = new Expression[1];
            args[0] = Expression.Constant(binder.Name);
            Expression self = Expression.Convert(Expression, LimitType);
            Expression methodCall = Expression.Call(self, typeof(DynamicObjectExt).GetMethod(methodName), args);
            DynamicMetaObject dmoGetValue = new DynamicMetaObject(methodCall, restrictions);
            return dmoGetValue;
        }
    }
}
