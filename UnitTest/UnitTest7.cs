using CodeM.Common.Tools;
using CodeM.Common.Tools.Json;
using CodeM.Common.Tools.Xml;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace UnitTest
{
    public class UnitTest7
    {
        private ITestOutputHelper output;

        public UnitTest7(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void CheckListType()
        {
            List<string> lst = new List<string>();
            Assert.True(TypeUtils.IsList(lst));
        }

        [Fact]
        public void XmlRootNode()
        {
            string xml = @"<xml>
                <name><![CDATA[张三]]></name>
                <age>18</age>
                <gender>男</gender>
            </xml>";

            bool xmlIsRoot = false;
            XmlUtils.IterateFromString(xml, (XmlNodeInfo node) =>
            {
                if (node.Path == "/xml")
                {
                    xmlIsRoot = node.IsRoot;
                }
                return true;
            });
            Assert.True(xmlIsRoot);
        }

        [Fact]
        public void XmlNodeLevel()
        {
            string xml = @"<xml>
                <name><![CDATA[张三]]></name>
                <age>18</age>
                <gender>男</gender>
            </xml>";

            int nameLevel = 0;
            XmlUtils.IterateFromString(xml, (XmlNodeInfo node) =>
            {
                if (node.Path == "/xml/name")
                {
                    nameLevel = node.Level;
                }
                return true;
            });
            Assert.Equal(2, nameLevel);
        }

        [Fact]
        public void XmlSerialize()
        {
            string xml = @"<xml>
                <test id='demo'>
                    <hello id='aaa' />
                    <world>123</world>
                </test>
                <name><![CDATA[张三]]></name>
                <age>18</age>
                <gender>男</gender>
            </xml>";
            dynamic obj = XmlUtils.SerializeFromString(xml);
            Assert.NotNull(obj);
            Assert.Equal("张三", obj.name.Value);
            Assert.Equal("aaa", obj.test.hello.id);
        }

        [Fact]
        public void JsonToXML()
        {
            dynamic obj = new Json2Dynamic().CreateObject();
            obj.Name = "wangxm";
            obj.Age = 18;
            obj.Dog = new Json2Dynamic().CreateObject();
            obj.Dog.Name = "Tom";
            obj.Dog.Kind = "中华田园犬";
            obj.Dog.Toys = new Json2Dynamic().CreateObject();
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
        public void JsonToXML2()
        {
            dynamic obj = new Json2Dynamic().CreateObject();
            obj.Name = "wangxm";
            obj.Age = 18;
            obj.Dog = new Json2Dynamic().CreateObject();
            obj.Dog.Name = "Tom";
            obj.Dog.Kind = "中华田园犬";
            obj.Dog.Toys = new Json2Dynamic().CreateObject();
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
