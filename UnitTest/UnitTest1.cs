using CodeM.Common.Tools.Security;
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
    }
}
