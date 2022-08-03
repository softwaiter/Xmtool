using CodeM.Common.Tools;
using Xunit;
using Xunit.Abstractions;

namespace Test
{
    public class CryptoTest
    {
        private ITestOutputHelper output;

        public CryptoTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Base64()
        {
            string source = "wangxiaoming";
            string base64Encode = Xmtool.Crypto().Base64Encode(source);
            output.WriteLine("wangxiaoming base64: " + base64Encode);
            string base64Decode = Xmtool.Crypto().Base64Decode(base64Encode);
            Assert.Equal(base64Decode, source);
        }

        [Fact]
        public void Aes()
        {
            string source = "wangxiaoming";
            string aesEncypted = Xmtool.Crypto().AESEncode(source, "test");
            output.WriteLine("wangxiaoming aes: " + aesEncypted);
            string aesDecrypted = Xmtool.Crypto().AESDecode(aesEncypted, "test");
            Assert.Equal(aesDecrypted, source);
        }
    }
}
