using CodeM.Common.Tools;
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
            string data = Xmtool.Captcha(CaptchKind.Character).Config(4) .Generate();

            string[] items = data.Split("|");
            Assert.Equal(2, items.Length);
            Assert.Equal(4, items[0].Length);
            Assert.StartsWith("data:image/png;base64,", items[1]);
        }

        [Fact]
        public void Test2()
        {
            bool result = Xmtool.Captcha(CaptchKind.Character).Validate("1234", "1234");
            Assert.True(result);
        }
    }
}
