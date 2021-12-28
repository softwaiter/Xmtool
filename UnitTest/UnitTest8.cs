using CodeM.Common.Tools;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace UnitTest
{
    public class UnitTest8
    {
        private ITestOutputHelper output;

        public UnitTest8(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Test()
        {
            string a = Xmtool.Captcha().Random(4);
            string b = Xmtool.Captcha().Random(4);
            string c = Xmtool.Captcha().Random(4);

            string d = Xmtool.Captcha().RandomOnlyNumber(4);
            string e = Xmtool.Captcha().RandomOnlyNumber(4);
            string f = Xmtool.Captcha().RandomOnlyNumber(4);

            string code;
            MemoryStream stream;
            Xmtool.Captcha().Build(100, 45, out stream, out code, true, 4);

            Assert.Equal(4, code.Length);
            Assert.NotNull(stream);
            Assert.True(stream.Length > 0);
        }
    }
}
