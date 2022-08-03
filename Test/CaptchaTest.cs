using CodeM.Common.Tools;
using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace Test
{
    public class CaptchaTest
    {
        private ITestOutputHelper output;

        public CaptchaTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Test()
        {
            CharacterCaptchaOption option = new CharacterCaptchaOption();
            option.Length = 4;
            CaptchaResult result = Xmtool.Captcha(CaptchaKind.Character).Config(option).Generate();
            Assert.Equal(4, result.ValidationData.Length);
            Assert.StartsWith("data:image/png;base64,", result.DisplayData);
        }

        [Fact]
        public void Test2()
        {
            CaptchaResult result = Xmtool.Captcha(CaptchaKind.Character).Generate(new CharacterCaptchaData("666666"));
            Assert.Equal(6, result.ValidationData.Length);
            Assert.Equal("666666", result.ValidationData);
            Assert.StartsWith("data:image/png;base64,", result.DisplayData);
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
            string resourcePath = Path.Combine(Environment.CurrentDirectory, "resources\\slider_backgrounds");
            CaptchaResult result = Xmtool.Captcha(CaptchaKind.Sliding).Config(new SlidingCaptchaOption(resourcePath)).Generate();
            
            string[] items = result.DisplayData.Split("|");
            Assert.Equal(6, items.Length);

            int width, height;
            Assert.True(int.TryParse(items[0], out width));
            Assert.True(int.TryParse(items[1], out height));

            int sliderWidth, sliderHeight;
            Assert.True(int.TryParse(items[3], out sliderWidth));
            Assert.True(int.TryParse(items[4], out sliderHeight));

            Assert.True(sliderWidth < width);
            Assert.Equal(sliderHeight, height);

            Assert.StartsWith("data:image/png;base64,", items[2]);
            Assert.StartsWith("data:image/png;base64,", items[5]);
        }

        [Fact]
        public void Test5()
        {
            string resourcePath = Path.Combine(Environment.CurrentDirectory, "resources\\slider_backgrounds");
            CaptchaResult result = Xmtool.Captcha(CaptchaKind.Sliding).Config(new SlidingCaptchaOption(resourcePath))
                .Generate(new SlidingCaptchaData(0, 0));

            Assert.Equal("0", result.ValidationData);
        }

        [Fact]
        public void Test6()
        {
            bool result = Xmtool.Captcha(CaptchaKind.Sliding).Validate(0.5f, 0.48f);
            Assert.True(result);
            
            bool result2 = Xmtool.Captcha(CaptchaKind.Sliding).Validate("0.5", "0.48");
            Assert.True(result2);
        }
    }
}
