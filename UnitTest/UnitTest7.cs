using CodeM.Common.Tools;
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
                    <null id='aaa' />
                    <hello>123</hello>
                </test>
                <name><![CDATA[张三]]></name>
                <age>18</age>
                <gender>男</gender>
            </xml>";
            dynamic obj = XmlUtils.SerializeFromString(xml);
            Assert.NotNull(obj);
            Assert.Equal("张三", obj.name.Value);
        }
    }
}
