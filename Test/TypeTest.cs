using CodeM.Common.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Test
{
    public class TypeTest
    {
        private ITestOutputHelper output;

        public TypeTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// 判断字符串是否为简单类型，应为True
        /// </summary>
        [Fact]
        public void StringIsSimpleType()
        {
            string name = "张三";
            bool ret = Xmtool.Type().IsSimpleType(name);
            Assert.True(ret);
        }

        /// <summary>
        /// 判断16位整型是否为简单类型，应为True
        /// </summary>
        [Fact]
        public void Int16IsSimpleType()
        {
            Int16 age = 18;
            bool ret = Xmtool.Type().IsSimpleType(age);
            Assert.True(ret);
        }

        /// <summary>
        /// 判断32位整型是否为简单类型，应为True
        /// </summary>
        [Fact]
        public void Int32IsSimpleType()
        {
            int age = 18;
            bool ret = Xmtool.Type().IsSimpleType(age);
            Assert.True(ret);
        }

        /// <summary>
        /// 判断64位整型是否为简单类型，应为True
        /// </summary>
        [Fact]
        public void Int64IsSimpleType()
        {
            long age = 18;
            bool ret = Xmtool.Type().IsSimpleType(age);
            Assert.True(ret);
        }

        /// <summary>
        /// 判断浮点型是否为简单类型，应为True
        /// </summary>
        [Fact]
        public void FloatIsSimpleType()
        {
            float deposit = 99999999.99f;
            bool ret = Xmtool.Type().IsSimpleType(deposit);
            Assert.True(ret);
        }

        /// <summary>
        /// 判断Double型是否为简单类型，应为True
        /// </summary>
        [Fact]
        public void DoubleIsSimpleType()
        {
            double deposit = 99999999.99;
            bool ret = Xmtool.Type().IsSimpleType(deposit);
            Assert.True(ret);
        }

        /// <summary>
        /// 判断布尔型是否为简单类型，应为True
        /// </summary>
        [Fact]
        public void BooleanIsSimpleType()
        {
            bool isChinese = true;
            bool ret = Xmtool.Type().IsSimpleType(isChinese);
            Assert.True(ret);
        }

        /// <summary>
        /// 判断日期时间型是否为简单类型，应为True
        /// </summary>
        [Fact]
        public void DateTimeIsSimpleType()
        {
            DateTime birthday = DateTime.Now;
            bool ret = Xmtool.Type().IsSimpleType(birthday);
            Assert.True(ret);
        }

        /// <summary>
        /// 判断TimeSpan型是否为简单类型，应为True
        /// </summary>
        [Fact]
        public void TimeSpanIsSimpleType()
        {
            TimeSpan ts = new TimeSpan(1, 1, 1);
            bool ret = Xmtool.Type().IsSimpleType(ts);
            Assert.True(ret);
        }

        /// <summary>
        /// 判断Object型是否为简单类型，应为False
        /// </summary>
        [Fact]
        public void ObjectIsNotSimpleType()
        {
            Object obj = new object();
            bool ret = Xmtool.Type().IsSimpleType(obj);
            Assert.False(ret);
        }

        /// <summary>
        /// 判断List型是否为简单类型，应为False
        /// </summary>
        [Fact]
        public void ListIsNotSimpleType()
        {
            List<string> persons = new List<string>();
            persons.Add("张三");
            persons.Add("李四");

            bool ret = Xmtool.Type().IsSimpleType(persons);
            Assert.False(ret);
        }

        /// <summary>
        /// 判断Array型是否为简单类型，应为False
        /// </summary>
        [Fact]
        public void ArrayIsNotSimpleType()
        {
            string[] persons = new string[] { "张三", "李四" };
            bool ret = Xmtool.Type().IsSimpleType(persons);
            Assert.False(ret);
        }

        /// <summary>
        /// 判断List<object>是否为列表类型，应为True
        /// </summary>
        [Fact]
        public void IsList()
        {
            List<object> list = new List<object>();
            bool ret = Xmtool.Type().IsList(list);
            Assert.True(ret);
        }

        /// <summary>
        /// 判断List<string>是否为列表类型，应为True
        /// </summary>
        [Fact]
        public void IsList2()
        {
            List<string> list = new List<string>();
            bool ret = Xmtool.Type().IsList(list);
            Assert.True(ret);
        }

        /// <summary>
        /// 判断List<int>是否为列表类型，应为True
        /// </summary>
        [Fact]
        public void IsList3()
        {
            List<int> list = new List<int>();
            bool ret = Xmtool.Type().IsList(list);
            Assert.True(ret);
        }

        /// <summary>
        /// 判断List<DateTime>是否为列表类型，应为True
        /// </summary>
        [Fact]
        public void IsList4()
        {
            List<DateTime> list = new List<DateTime>();
            bool ret = Xmtool.Type().IsList(list);
            Assert.True(ret);
        }

        /// <summary>
        /// 判断object是否为列表类型，应为False
        /// </summary>
        [Fact]
        public void IsList5()
        {
            object obj = new object();
            bool ret = Xmtool.Type().IsList(obj);
            Assert.False(ret);
        }

        /// <summary>
        /// 判断string是否为列表类型，应为False
        /// </summary>
        [Fact]
        public void IsList6()
        {
            string name = "张三";
            bool ret = Xmtool.Type().IsList(name);
            Assert.False(ret);
        }

        /// <summary>
        /// 对指定List对象进行克隆操作，应成功
        /// </summary>
        [Fact]
        public void CloneList()
        {
            List<string> persons = new List<string>();
            persons.Add("张三");
            persons.Add("李四");

            IList clone = Xmtool.Type().CloneList(persons);
            Assert.NotNull(clone);
            Assert.Equal(2, clone.Count);
            Assert.Equal("张三", clone[0]);
            Assert.Equal("李四", clone[1]);

            IList clone2 = Xmtool.Type().CloneList(persons, false);
            Assert.NotNull(clone2);
            Assert.Equal(0, clone2.Count);
        }
    }
}
