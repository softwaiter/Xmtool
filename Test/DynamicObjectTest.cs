using CodeM.Common.Tools;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace Test
{
    public class DynamicObjectTest
    {
        private ITestOutputHelper output;

        public DynamicObjectTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void DynamicObjectIntegerTest()
        {
            dynamic obj = Xmtool.DynamicObject();
            obj.Age = 18;
            Assert.Equal(typeof(int), obj.Age.GetType());
            Assert.Equal(18, obj.Age);
        }

        [Fact]
        public void DynamicObjectBooleanTest()
        {
            dynamic obj = Xmtool.DynamicObject();
            obj.IsChinese = true;
            Assert.Equal(typeof(bool), obj.IsChinese.GetType());
            Assert.Equal(true, obj.IsChinese);
        }

        [Fact]
        public void DynamicObjectStringTest()
        {
            dynamic obj = Xmtool.DynamicObject();
            obj.Name = "wangxm";
            Assert.Equal(typeof(string), obj.Name.GetType());
            Assert.Equal("wangxm", obj.Name);
        }

        [Fact]
        public void DynamicObjectDateTimeTest()
        {
            dynamic obj = Xmtool.DynamicObject();
            obj.Birthday = new DateTime(2000, 7, 29);
            Assert.Equal(typeof(DateTime), obj.Birthday.GetType());
            Assert.Equal(new DateTime(2000, 7, 29), obj.Birthday);
        }

        [Fact]
        public void DynamicObjectHasTest()
        {
            dynamic obj = Xmtool.DynamicObject();
            obj.Name = "wangxm";
            Assert.True(obj.Has("Name"));
            Assert.False(obj.Has("Age"));
        }

        [Fact]
        public void DynamicObjectHasPathTest()
        {
            dynamic obj = Xmtool.DynamicObject();
            obj.Name = "wangxm";
            obj.Son = Xmtool.DynamicObject();
            obj.Son.Name = "wangman";
            obj.Son.Age = 1;
            Assert.True(obj.HasPath("Name"));
            Assert.True(obj.HasPath("Son.Name"));
            Assert.True(obj.HasPath("Son.Age"));
            Assert.False(obj.HasPath("Son.Birthday"));
        }

        [Fact]
        public void DynamicObjectSetValueTest()
        {
            dynamic obj = Xmtool.DynamicObject();
            obj.SetValue("Name", "wangxm");
            obj.SetValue("Son.Name", "wangman");
            Assert.True(obj.HasPath("Name"));
            Assert.False(obj.HasPath("Son.Name"));
            Assert.True(obj.Has("Son.Name"));
        }

        [Fact]
        public void DynamicObjectSetValueByPathTest()
        {
            dynamic obj = Xmtool.DynamicObject();
            obj.SetValueByPath("Name", "wangxm");
            obj.SetValueByPath("Son.Name", "wangman");
            Assert.True(obj.HasPath("Name"));
            Assert.True(obj.HasPath("Son.Name"));
            Assert.False(obj.Has("Son.Name"));
        }

        [Fact]
        public void DynamicObjectRemoveTest()
        {
            dynamic obj = Xmtool.DynamicObject();
            obj.Name = "wangxm";
            obj.Son = Xmtool.DynamicObject();
            obj.Son.Name = "wangman";
            obj.Son.Age = 1;
            Assert.True(obj.HasPath("Name"));
            Assert.True(obj.HasPath("Son.Name"));
            obj.Remove("Son");
            Assert.True(obj.HasPath("Name"));
            Assert.False(obj.HasPath("Son"));
        }

        [Fact]
        public void DynamicObjectRemovePathTest()
        {
            dynamic obj = Xmtool.DynamicObject();
            obj.Name = "wangxm";
            obj.Son = Xmtool.DynamicObject();
            obj.Son.Name = "wangman";
            obj.Son.Age = 1;
            Assert.True(obj.HasPath("Name"));
            Assert.True(obj.HasPath("Son.Name"));
            obj.RemovePath("Son.Name");
            Assert.True(obj.HasPath("Name"));
            Assert.True(obj.HasPath("Son"));
            Assert.False(obj.HasPath("Son.Name"));
        }

        [Fact]
        public void DynamicObjectKeysTest()
        {
            dynamic obj = Xmtool.DynamicObject();
            obj.Id = "001";
            obj.Name = "wangxm";
            Assert.Equal(2, obj.Keys.Count);
        }

        [Fact]
        public void DynamicObjectIndexerPropertyTest()
        {
            dynamic obj = Xmtool.DynamicObject();
            obj.Id = "001";
            obj.Name = "wangxm";
            Assert.Equal("wangxm", obj["Name"]);
            Assert.Null(obj["Age"]);
        }

        [Fact]
        public void DynamicObjectSerializeTest()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            dynamic data = Xmtool.DynamicObject();
            data.Name = "wangxm";
            data.Age = 18;
            string json = JsonSerializer.Serialize<dynamic>(new
            {
                data
            }, options);

            Assert.Contains("Name", json);
        }

        [Fact]
        public void DynamicObjectArrayTest()
        {
            dynamic data = Xmtool.DynamicObject();
            data.Children = new string[2];
            data.Children[0] = "张三";
            data.Children[1] = "李四";

            string jsonStr = data.ToString();
            Assert.Equal("{\"Children\":[\"张三\",\"李四\"]}", jsonStr);
        }

        [Fact]
        public void DynamicObjectListTest()
        {
            dynamic data = Xmtool.DynamicObject();
            data.Children = new List<string>();
            data.Children.Add("张三");
            data.Children.Add("李四");

            Assert.Equal(2, data.Children.Count);

            string jsonStr = data.ToString();
            Assert.Equal("{\"Children\":[\"张三\",\"李四\"]}", jsonStr);
        }

        [Fact]
        public void DynamicObjectCloneTest()
        {
            dynamic data = Xmtool.DynamicObject();
            data.Name = "wangxm";
            data.Children = new List<string>();
            data.Children.Add("wangda");
            data.Children.Add("wanger");

            dynamic data2 = data.Clone();
            
            Assert.Equal(data.Name, data2.Name);
            Assert.Equal(data.Children.Count, data2.Children.Count);

            data2.Children.Add("wangsan");
            //暂不支持深度拷贝
            //Assert.NotEqual(data.Children.Count, data2.Children.Count);
        }

        [Fact]
        public void QuotesTest()
        {
            dynamic data = Xmtool.DynamicObject();
            data.Name = "wangxm";
            data.Age = 18;
            data.Desc = "\"wangxm\" is a man.";
            
            string json = data.ToString();
            Assert.Contains("\\\"", json);

            dynamic data2 = Xmtool.Json.ConfigParser().Parse(json);
            Assert.Equal("wangxm", data2.Name);
            Assert.Equal(18, data2.Age);
            Assert.Equal("\"wangxm\" is a man.", data2.Desc);
        }

        [Fact]
        public void ObjectToXML()
        {
            dynamic obj = Xmtool.DynamicObject();
            obj.Name = "wangxm";
            obj.Age = 18;
            obj.Dog = Xmtool.DynamicObject();
            obj.Dog.Name = "Tom";
            obj.Dog.Kind = "中华田园犬";
            obj.Dog.Toys = Xmtool.DynamicObject();
            obj.Dog.Toys.One = "玩具一";
            obj.Dog.Toys.Two = "玩具二";
            string xml = obj.ToXMLString();
            Assert.NotNull(xml);
            Assert.Contains("<Name>wangxm</Name>", xml);
            Assert.Contains("<Dog Name=\"Tom\"", xml);
            Assert.Contains("<Dog Name=\"Tom\" Kind=\"中华田园犬\">", xml);
            Assert.Contains("<Dog Name=\"Tom\" Kind=\"中华田园犬\"><Toys", xml);
            Assert.Contains("<Dog Name=\"Tom\" Kind=\"中华田园犬\"><Toys One=\"玩具一\"", xml);
            Assert.Contains("<Dog Name=\"Tom\" Kind=\"中华田园犬\"><Toys One=\"玩具一\" Two=\"玩具二\">", xml);
        }

        [Fact]
        public void ObjectToXML2()
        {
            dynamic obj = Xmtool.DynamicObject();
            obj.Name = "wangxm";
            obj.Age = 18;
            obj.Dog = Xmtool.DynamicObject();
            obj.Dog.Name = "Tom";
            obj.Dog.Kind = "中华田园犬";
            obj.Dog.Toys = Xmtool.DynamicObject();
            obj.Dog.Toys.One = "玩具一";
            obj.Dog.Toys.Two = "玩具二";
            obj.Dog.Value = "Hello World!";
            string xml = obj.ToXMLString();
            Assert.NotNull(xml);
            Assert.Contains("<Name>wangxm</Name>", xml);
            Assert.Contains("<Dog Name=\"Tom\"", xml);
            Assert.Contains("<Dog Name=\"Tom\" Kind=\"中华田园犬\">", xml);
            Assert.DoesNotContain("<Dog Name=\"Tom\" Kind=\"中华田园犬\"><Toys", xml);
            Assert.Contains("<Dog Name=\"Tom\" Kind=\"中华田园犬\">Hello World!</Dog>", xml);
        }
    }
}
