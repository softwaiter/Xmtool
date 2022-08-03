using CodeM.Common.Tools;
using CodeM.Common.Tools.Xml;
using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace Test
{
    public class HashTest
    {
        private ITestOutputHelper output;

        public HashTest(ITestOutputHelper output) {
            this.output = output;
        }

        [Fact]
        public void Md5()
        {
            string md5 = Xmtool.Hash().MD5("wangxiaoming");
            output.WriteLine("wangxiaoming md5: " + md5);
            Assert.Equal(32, md5.Length);
        }

        [Fact]
        public void Sha1() {
            string sha1 = Xmtool.Hash().SHA1("wangxiaoming");
            output.WriteLine("wangxiaoming sha1: " + sha1);
            Assert.Equal(40, sha1.Length);
        }

        [Fact]
        public void Sha256() {
            string sha256 = Xmtool.Hash().SHA256("wangxiaoming");
            output.WriteLine("wangxiaoming sha256: " + sha256);
            Assert.Equal(64, sha256.Length);
        }
    }
}
