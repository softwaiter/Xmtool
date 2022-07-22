using CodeM.Common.Tools;
using System;
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
            string data = Xmtool.Captcha(CaptchKind.Character).Config(4).Generate();

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

        [Fact]
        public void Test3()
        {
            string resourcePath = Path.Combine(Environment.CurrentDirectory, "images");
            string data = Xmtool.Captcha(CaptchKind.Sliding).Config(resourcePath).Generate();
            string[] items = data.Split("|");
            Assert.Equal(8, items.Length);

            int x, y;
            Assert.True(int.TryParse(items[0], out x));
            Assert.True(int.TryParse(items[1], out y));

            int width, height;
            Assert.True(int.TryParse(items[2], out width));
            Assert.True(int.TryParse(items[3], out height));

            Assert.True(x < width);
            Assert.True(y < height);

            Assert.StartsWith("data:image/png;base64,", items[4]);
            Assert.StartsWith("data:image/png;base64,", items[7]);
        }

        [Fact]
        public void Test4()
        {
            bool result = Xmtool.Captcha(CaptchKind.Sliding).Validate(0.5f, 0.48f);
            Assert.True(result);
        }
    }
}
