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
            CharacterCaptchaOption option = new CharacterCaptchaOption();
            option.Length = 4;
            string data = Xmtool.Captcha(CaptchaKind.Character).Config(option).Generate();

            string[] items = data.Split("|");
            Assert.Equal(2, items.Length);
            Assert.Equal(4, items[0].Length);
            Assert.StartsWith("data:image/png;base64,", items[1]);
        }

        [Fact]
        public void Test2()
        {
            string data = Xmtool.Captcha(CaptchaKind.Character).Generate(new CharacterCaptchaData("666666"));

            string[] items = data.Split("|");
            Assert.Equal(2, items.Length);
            Assert.Equal(6, items[0].Length);
            Assert.Equal("666666", items[0]);
            Assert.StartsWith("data:image/png;base64,", items[1]);
        }

        [Fact]
        public void Test3()
        {
            bool result = Xmtool.Captcha(CaptchaKind.Character).Validate("1234", "1234");
            Assert.True(result);
        }

        [Fact]
        public void Test4()
        {
            string resourcePath = Path.Combine(Environment.CurrentDirectory, "images");
            string data = Xmtool.Captcha(CaptchaKind.Sliding).Config(new SlidingCaptchaOption(resourcePath)).Generate();
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
        public void Test5()
        {
            string resourcePath = Path.Combine(Environment.CurrentDirectory, "images");
            string data = Xmtool.Captcha(CaptchaKind.Sliding).Config(new SlidingCaptchaOption(resourcePath))
                .Generate(new SlidingCaptchaData(0, 0));
            string[] items = data.Split("|");
            Assert.Equal(8, items.Length);

            int x, y;
            Assert.True(int.TryParse(items[0], out x));
            Assert.True(int.TryParse(items[1], out y));

            Assert.Equal(0, x);
            Assert.Equal(0, y);
        }

        [Fact]
        public void Test6()
        {
            bool result = Xmtool.Captcha(CaptchaKind.Sliding).Validate(0.5f, 0.48f);
            Assert.True(result);
        }
    }
}
