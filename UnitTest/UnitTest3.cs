using CodeM.Common.Tools;
using Xunit;
using Xunit.Abstractions;

namespace UnitTest
{
    public class UnitTest3
    {
        private ITestOutputHelper output;

        public UnitTest3(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// 判断13812345678是否为手机号，应为True
        /// </summary>
        [Fact]
        public void MobileTest()
        {
            bool ret = RegexUtils.IsMobile("13812345678");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断13612345678是否为手机号，应为True
        /// </summary>
        [Fact]
        public void MobileTest2()
        {
            bool ret = RegexUtils.IsMobile("13612345678");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断23612345678是否为手机号，应为False
        /// </summary>
        [Fact]
        public void MobileTest3()
        {
            bool ret = RegexUtils.IsMobile("23612345678");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断1361234567是否为手机号，应为False
        /// </summary>
        [Fact]
        public void MobileTest4()
        {
            bool ret = RegexUtils.IsMobile("1361234567");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断abc是否为手机号，应为False
        /// </summary>
        [Fact]
        public void MobileTest5()
        {
            bool ret = RegexUtils.IsMobile("abc");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断空是否为手机号，应为False
        /// </summary>
        [Fact]
        public void MobileTest6()
        {
            bool ret = RegexUtils.IsMobile("");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断abc@126.com是否有效邮箱，应为True
        /// </summary>
        [Fact]
        public void EmailTest()
        {
            bool ret = RegexUtils.IsEmail("abc@126.com");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断abc@163.com是否有效邮箱，应为True
        /// </summary>
        [Fact]
        public void EmailTest2()
        {
            bool ret = RegexUtils.IsEmail("abc@163.com");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断1100@qq.com是否有效邮箱，应为True
        /// </summary>
        [Fact]
        public void EmailTest3()
        {
            bool ret = RegexUtils.IsEmail("1100@qq.com");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断abc@126.是否有效邮箱，应为False
        /// </summary>
        [Fact]
        public void EmailTest4()
        {
            bool ret = RegexUtils.IsEmail("abc@126.");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断abc@126是否有效邮箱，应为False
        /// </summary>
        [Fact]
        public void EmailTest5()
        {
            bool ret = RegexUtils.IsEmail("abc@126");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断abc@是否有效邮箱，应为False
        /// </summary>
        [Fact]
        public void EmailTest6()
        {
            bool ret = RegexUtils.IsEmail("abc@");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断abc是否有效邮箱，应为False
        /// </summary>
        [Fact]
        public void EmailTest7()
        {
            bool ret = RegexUtils.IsEmail("abc");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断空是否有效邮箱，应为False
        /// </summary>
        [Fact]
        public void EmailTest8()
        {
            bool ret = RegexUtils.IsEmail("");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断http://www.baidu.com是否有效网址，应为True
        /// </summary>
        [Fact]
        public void UrlTest()
        {
            bool ret = RegexUtils.IsUrl("http://www.baidu.com");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断HTTP://WWW.QQ.COM是否有效网址，应为True
        /// </summary>
        [Fact]
        public void UrlTest2()
        {
            bool ret = RegexUtils.IsUrl("HTTP://WWW.QQ.COM");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断https://news.sina.com.cn是否有效网址，应为True
        /// </summary>
        [Fact]
        public void UrlTest3()
        {
            bool ret = RegexUtils.IsUrl("https://news.sina.com.cn");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断http://gjj.beijing.gov.cn/是否有效网址，应为True
        /// </summary>
        [Fact]
        public void UrlTest4()
        {
            bool ret = RegexUtils.IsUrl("http://gjj.beijing.gov.cn");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断www.baidu.com是否有效网址，应为False
        /// </summary>
        [Fact]
        public void UrlTest5()
        {
            bool ret = RegexUtils.IsUrl("www.baidu.com");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断www.baidu是否有效网址，应为False
        /// </summary>
        [Fact]
        public void UrlTest6()
        {
            bool ret = RegexUtils.IsUrl("www.baidu");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断空是否有效网址，应为False
        /// </summary>
        [Fact]
        public void UrlTest7()
        {
            bool ret = RegexUtils.IsUrl("");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断192.168.1.1是否有效IP，应为True
        /// </summary>
        [Fact]
        public void IPTest()
        {
            bool ret = RegexUtils.IsIP("192.168.1.1");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断255.255.255.255是否有效IP，应为True
        /// </summary>
        [Fact]
        public void IPTest2()
        {
            bool ret = RegexUtils.IsIP("255.255.255.255");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断255.255.255.256是否有效IP，应为False
        /// </summary>
        [Fact]
        public void IPTest3()
        {
            bool ret = RegexUtils.IsIP("255.255.255.256");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断255.255.255.-1是否有效IP，应为False
        /// </summary>
        [Fact]
        public void IPTest4()
        {
            bool ret = RegexUtils.IsIP("255.255.255.-1");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断255.255.255是否有效IP，应为False
        /// </summary>
        [Fact]
        public void IPTest5()
        {
            bool ret = RegexUtils.IsIP("255.255.255");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断abc.abc.abc.abc是否有效IP，应为False
        /// </summary>
        [Fact]
        public void IPTest6()
        {
            bool ret = RegexUtils.IsIP("abc.abc.abc.abc");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断0是否为数值，应为True
        /// </summary>
        [Fact]
        public void NumberTest()
        {
            bool ret = RegexUtils.IsNumber("0");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断Int最大值是否为数值，应为True
        /// </summary>
        [Fact]
        public void NumberTest2()
        {
            bool ret = RegexUtils.IsNumber(int.MaxValue.ToString());
            Assert.True(ret);
        }

        /// <summary>
        /// 判断Int最小值是否为数值，应为True
        /// </summary>
        [Fact]
        public void NumberTest3()
        {
            bool ret = RegexUtils.IsNumber(int.MinValue.ToString());
            Assert.True(ret);
        }

        /// <summary>
        /// 判断double最大值是否为数值，应为True
        /// </summary>
        [Fact]
        public void NumberTest4()
        {
            bool ret = RegexUtils.IsNumber(double.MaxValue.ToString("0"));
            Assert.True(ret);
        }

        /// <summary>
        /// 判断double最小值是否为数值，应为True
        /// </summary>
        [Fact]
        public void NumberTest5()
        {
            bool ret = RegexUtils.IsNumber(double.MinValue.ToString("0"));
            Assert.True(ret);
        }

        /// <summary>
        /// 判断abc最小值是否为数值，应为True
        /// </summary>
        [Fact]
        public void NumberTest6()
        {
            bool ret = RegexUtils.IsNumber("abc");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断空最小值是否为数值，应为True
        /// </summary>
        [Fact]
        public void NumberTest7()
        {
            bool ret = RegexUtils.IsNumber("");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断0是否为整数，应为True
        /// </summary>
        [Fact]
        public void IntTest()
        {
            bool ret = RegexUtils.IsInteger("0");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断15是否为整数，应为True
        /// </summary>
        [Fact]
        public void IntTest2()
        {
            bool ret = RegexUtils.IsInteger("15");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断-18是否为整数，应为True
        /// </summary>
        [Fact]
        public void IntTest3()
        {
            bool ret = RegexUtils.IsInteger("-18");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断0.7是否为整数，应为False
        /// </summary>
        [Fact]
        public void IntTest4()
        {
            bool ret = RegexUtils.IsInteger("0.7");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断-9.6是否为整数，应为False
        /// </summary>
        [Fact]
        public void IntTest5()
        {
            bool ret = RegexUtils.IsInteger("-9.6");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断abc是否为整数，应为False
        /// </summary>
        [Fact]
        public void IntTest6()
        {
            bool ret = RegexUtils.IsInteger("abc");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断空是否为整数，应为False
        /// </summary>
        [Fact]
        public void IntTest7()
        {
            bool ret = RegexUtils.IsInteger("");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断0是否为正整数，应为False
        /// </summary>
        [Fact]
        public void PositiveIntTest()
        {
            bool ret = RegexUtils.IsPositiveInteger("0");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断2是否为正整数，应为True
        /// </summary>
        [Fact]
        public void PositiveIntTest2()
        {
            bool ret = RegexUtils.IsPositiveInteger("2");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断2.8是否为正整数，应为False
        /// </summary>
        [Fact]
        public void PositiveIntTest3()
        {
            bool ret = RegexUtils.IsPositiveInteger("2.8");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断-2.8是否为正整数，应为False
        /// </summary>
        [Fact]
        public void PositiveIntTest4()
        {
            bool ret = RegexUtils.IsPositiveInteger("-2.8");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断-100是否为正整数，应为False
        /// </summary>
        [Fact]
        public void PositiveIntTest5()
        {
            bool ret = RegexUtils.IsPositiveInteger("-100");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断abc是否为正整数，应为False
        /// </summary>
        [Fact]
        public void PositiveIntTest6()
        {
            bool ret = RegexUtils.IsPositiveInteger("abc");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断空是否为正整数，应为False
        /// </summary>
        [Fact]
        public void PositiveIntTest7()
        {
            bool ret = RegexUtils.IsPositiveInteger("");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断0是否为自然数，应为True
        /// </summary>
        [Fact]
        public void NaturalIntTest()
        {
            bool ret = RegexUtils.IsNaturalInteger("0");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断10是否为自然数，应为True
        /// </summary>
        [Fact]
        public void NaturalIntTest2()
        {
            bool ret = RegexUtils.IsNaturalInteger("10");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断2.5是否为自然数，应为False
        /// </summary>
        [Fact]
        public void NaturalIntTest3()
        {
            bool ret = RegexUtils.IsNaturalInteger("2.5");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断-4是否为自然数，应为False
        /// </summary>
        [Fact]
        public void NaturalIntTest4()
        {
            bool ret = RegexUtils.IsNaturalInteger("-4");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断abc是否为自然数，应为False
        /// </summary>
        [Fact]
        public void NaturalIntTest5()
        {
            bool ret = RegexUtils.IsNaturalInteger("abc");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断空是否为自然数，应为False
        /// </summary>
        [Fact]
        public void NaturalIntTest6()
        {
            bool ret = RegexUtils.IsNaturalInteger("");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断1.5是否为小数，应为True
        /// </summary>
        [Fact]
        public void DecimalTest()
        {
            bool ret = RegexUtils.IsDecimal("1.5");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断0.2是否为小数，应为True
        /// </summary>
        [Fact]
        public void DecimalTest2()
        {
            bool ret = RegexUtils.IsDecimal("0.2");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断-5.8是否为小数，应为True
        /// </summary>
        [Fact]
        public void DecimalTest3()
        {
            bool ret = RegexUtils.IsDecimal("-5.8");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断-0.9是否为小数，应为True
        /// </summary>
        [Fact]
        public void DecimalTest4()
        {
            bool ret = RegexUtils.IsDecimal("-0.9");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断0是否为小数，应为False
        /// </summary>
        [Fact]
        public void DecimalTest5()
        {
            bool ret = RegexUtils.IsDecimal("0");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断100是否为小数，应为False
        /// </summary>
        [Fact]
        public void DecimalTest6()
        {
            bool ret = RegexUtils.IsDecimal("100");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断-9是否为小数，应为False
        /// </summary>
        [Fact]
        public void DecimalTest7()
        {
            bool ret = RegexUtils.IsDecimal("-9");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断abc是否为小数，应为False
        /// </summary>
        [Fact]
        public void DecimalTest8()
        {
            bool ret = RegexUtils.IsDecimal("abc");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断空是否为小数，应为False
        /// </summary>
        [Fact]
        public void DecimalTest9()
        {
            bool ret = RegexUtils.IsDecimal("");
            Assert.False(ret);
        }

    }
}
