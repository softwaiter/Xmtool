using CodeM.Common.Tools.Security;
using CodeM.Common.Tools.Xml;
using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace UnitTest
{
    public class UnitTest1
    {
        private ITestOutputHelper output;

        public UnitTest1(ITestOutputHelper output) {
            this.output = output;
        }

        [Fact]
        public void Md5()
        {
            string md5 = HashUtils.MD5("wangxiaoming");
            output.WriteLine("wangxiaoming md5: " + md5);
            Assert.Equal(32, md5.Length);
        }

        [Fact]
        public void Sha1() {
            string sha1 = HashUtils.SHA1("wangxiaoming");
            output.WriteLine("wangxiaoming sha1: " + sha1);
            Assert.Equal(40, sha1.Length);
        }

        [Fact]
        public void Sha256() {
            string sha256 = HashUtils.SHA256("wangxiaoming");
            output.WriteLine("wangxiaoming sha256: " + sha256);
            Assert.Equal(64, sha256.Length);
        }

        [Fact]
        public void Base64() {
            string source = "wangxiaoming";
            string base64Encode = CryptoUtils.Base64Encode(source);
            output.WriteLine("wangxiaoming base64: " + base64Encode);
            string base64Decode = CryptoUtils.Base64Decode(base64Encode);
            Assert.Equal(base64Decode, source);
        }

        [Fact]
        public void Aes() {
            string source = "wangxiaoming";
            string aesEncypted = CryptoUtils.AESEncode(source, "test");
            output.WriteLine("wangxiaoming aes: " + aesEncypted);
            string aesDecrypted = CryptoUtils.AESDecode(aesEncypted, "test");
            Assert.Equal(aesDecrypted, source);
        }

        [Fact]
        public void XmlInterate() {
            string aaaArg = string.Empty;

            string path = Path.Combine(Environment.CurrentDirectory, "ioc.xml");
            bool isObj = false;
            XmlUtils.Iterate(path, (XmlNodeInfo node) =>
            {
                if (!node.IsEndNode)
                {
                    if (node.Path == "/objects/object")
                    {
                        isObj = node.GetAttribute("id") == "aaa";
                    }
                    else if (node.Path == "/objects/object/constructor-arg/@text")
                    {
                        if (isObj)
                        {
                            aaaArg = node.Text;
                        }
                    }

                    output.WriteLine(node.FullPath);
                }
                return true;
            });

            Assert.Equal("\"wangxm\"", aaaArg);
        }

        [Fact]
        public void XmlIterateFromString() {
            int age = 0;

            string xml = @"<xml>
                <name>ÕÅÈý</name>
                <age>18</age>
                <gender>ÄÐ</gender>
            </xml>";
            XmlUtils.IterateFromString(xml, (XmlNodeInfo node) =>
            {
                if (!node.IsEndNode)
                {
                    if (node.Path == "/xml/age/@text")
                    {
                        age = int.Parse(node.Text);
                    }
                }
                return true;
            });

            Assert.Equal(18, age);
        }
    }
}
