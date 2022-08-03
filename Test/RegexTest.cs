using CodeM.Common.Tools;
using Xunit;
using Xunit.Abstractions;

namespace Test
{
    public class RegexTest
    {
        private ITestOutputHelper output;

        public RegexTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        /// 判断13812345678是否为手机号，应为True
        /// </summary>
        [Fact]
        public void MobileTest()
        {
            bool ret = Xmtool.Regex().IsMobile("13812345678");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断13612345678是否为手机号，应为True
        /// </summary>
        [Fact]
        public void MobileTest2()
        {
            bool ret = Xmtool.Regex().IsMobile("13612345678");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断23612345678是否为手机号，应为False
        /// </summary>
        [Fact]
        public void MobileTest3()
        {
            bool ret = Xmtool.Regex().IsMobile("23612345678");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断1361234567是否为手机号，应为False
        /// </summary>
        [Fact]
        public void MobileTest4()
        {
            bool ret = Xmtool.Regex().IsMobile("1361234567");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断abc是否为手机号，应为False
        /// </summary>
        [Fact]
        public void MobileTest5()
        {
            bool ret = Xmtool.Regex().IsMobile("abc");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断空是否为手机号，应为False
        /// </summary>
        [Fact]
        public void MobileTest6()
        {
            bool ret = Xmtool.Regex().IsMobile("");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断“010-80001234”是否为固定电话，应为True
        /// </summary>
        [Fact]
        public void TelephoneTest()
        {
            bool ret = Xmtool.Regex().IsTelephone("010-80001234");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断“0312-6928579”是否为固定电话，应为True
        /// </summary>
        [Fact]
        public void TelephoneTest2()
        {
            bool ret = Xmtool.Regex().IsTelephone("0312-6928579");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断“010-2387950”是否为手机号，应为False
        /// </summary>
        [Fact]
        public void TelephoneTest3()
        {
            bool ret = Xmtool.Regex().IsTelephone("010-2387950");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断“0312-85850278”是否为手机号，应为False
        /// </summary>
        [Fact]
        public void TelephoneTest4()
        {
            bool ret = Xmtool.Regex().IsTelephone("0312-85850278");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断abc是否为手机号，应为False
        /// </summary>
        [Fact]
        public void TelephoneTest5()
        {
            bool ret = Xmtool.Regex().IsTelephone("abc");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断空是否为手机号，应为False
        /// </summary>
        [Fact]
        public void TelephoneTest6()
        {
            bool ret = Xmtool.Regex().IsTelephone("");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断abc@126.com是否有效邮箱，应为True
        /// </summary>
        [Fact]
        public void EmailTest()
        {
            bool ret = Xmtool.Regex().IsEmail("abc@126.com");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断abc@163.com是否有效邮箱，应为True
        /// </summary>
        [Fact]
        public void EmailTest2()
        {
            bool ret = Xmtool.Regex().IsEmail("abc@163.com");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断1100@qq.com是否有效邮箱，应为True
        /// </summary>
        [Fact]
        public void EmailTest3()
        {
            bool ret = Xmtool.Regex().IsEmail("1100@qq.com");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断abc@126.是否有效邮箱，应为False
        /// </summary>
        [Fact]
        public void EmailTest4()
        {
            bool ret = Xmtool.Regex().IsEmail("abc@126.");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断abc@126是否有效邮箱，应为False
        /// </summary>
        [Fact]
        public void EmailTest5()
        {
            bool ret = Xmtool.Regex().IsEmail("abc@126");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断abc@是否有效邮箱，应为False
        /// </summary>
        [Fact]
        public void EmailTest6()
        {
            bool ret = Xmtool.Regex().IsEmail("abc@");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断abc是否有效邮箱，应为False
        /// </summary>
        [Fact]
        public void EmailTest7()
        {
            bool ret = Xmtool.Regex().IsEmail("abc");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断空是否有效邮箱，应为False
        /// </summary>
        [Fact]
        public void EmailTest8()
        {
            bool ret = Xmtool.Regex().IsEmail("");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断http://www.baidu.com是否有效网址，应为True
        /// </summary>
        [Fact]
        public void UrlTest()
        {
            bool ret = Xmtool.Regex().IsUrl("http://www.baidu.com");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断HTTP://WWW.QQ.COM是否有效网址，应为True
        /// </summary>
        [Fact]
        public void UrlTest2()
        {
            bool ret = Xmtool.Regex().IsUrl("HTTP://WWW.QQ.COM");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断https://news.sina.com.cn是否有效网址，应为True
        /// </summary>
        [Fact]
        public void UrlTest3()
        {
            bool ret = Xmtool.Regex().IsUrl("https://news.sina.com.cn");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断http://gjj.beijing.gov.cn/是否有效网址，应为True
        /// </summary>
        [Fact]
        public void UrlTest4()
        {
            bool ret = Xmtool.Regex().IsUrl("http://gjj.beijing.gov.cn");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断www.baidu.com是否有效网址，应为False
        /// </summary>
        [Fact]
        public void UrlTest5()
        {
            bool ret = Xmtool.Regex().IsUrl("www.baidu.com");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断www.baidu是否有效网址，应为False
        /// </summary>
        [Fact]
        public void UrlTest6()
        {
            bool ret = Xmtool.Regex().IsUrl("www.baidu");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断空是否有效网址，应为False
        /// </summary>
        [Fact]
        public void UrlTest7()
        {
            bool ret = Xmtool.Regex().IsUrl("");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断192.168.1.1是否有效IP，应为True
        /// </summary>
        [Fact]
        public void IPTest()
        {
            bool ret = Xmtool.Regex().IsIP("192.168.1.1");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断255.255.255.255是否有效IP，应为True
        /// </summary>
        [Fact]
        public void IPTest2()
        {
            bool ret = Xmtool.Regex().IsIP("255.255.255.255");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断255.255.255.256是否有效IP，应为False
        /// </summary>
        [Fact]
        public void IPTest3()
        {
            bool ret = Xmtool.Regex().IsIP("255.255.255.256");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断255.255.255.-1是否有效IP，应为False
        /// </summary>
        [Fact]
        public void IPTest4()
        {
            bool ret = Xmtool.Regex().IsIP("255.255.255.-1");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断255.255.255是否有效IP，应为False
        /// </summary>
        [Fact]
        public void IPTest5()
        {
            bool ret = Xmtool.Regex().IsIP("255.255.255");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断abc.abc.abc.abc是否有效IP，应为False
        /// </summary>
        [Fact]
        public void IPTest6()
        {
            bool ret = Xmtool.Regex().IsIP("abc.abc.abc.abc");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断0是否为数值，应为True
        /// </summary>
        [Fact]
        public void NumberTest()
        {
            bool ret = Xmtool.Regex().IsNumber("0");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断Int最大值是否为数值，应为True
        /// </summary>
        [Fact]
        public void NumberTest2()
        {
            bool ret = Xmtool.Regex().IsNumber(int.MaxValue.ToString());
            Assert.True(ret);
        }

        /// <summary>
        /// 判断Int最小值是否为数值，应为True
        /// </summary>
        [Fact]
        public void NumberTest3()
        {
            bool ret = Xmtool.Regex().IsNumber(int.MinValue.ToString());
            Assert.True(ret);
        }

        /// <summary>
        /// 判断double最大值是否为数值，应为True
        /// </summary>
        [Fact]
        public void NumberTest4()
        {
            bool ret = Xmtool.Regex().IsNumber(double.MaxValue.ToString("0"));
            Assert.True(ret);
        }

        /// <summary>
        /// 判断double最小值是否为数值，应为True
        /// </summary>
        [Fact]
        public void NumberTest5()
        {
            bool ret = Xmtool.Regex().IsNumber(double.MinValue.ToString("0"));
            Assert.True(ret);
        }

        /// <summary>
        /// 判断abc最小值是否为数值，应为True
        /// </summary>
        [Fact]
        public void NumberTest6()
        {
            bool ret = Xmtool.Regex().IsNumber("abc");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断空最小值是否为数值，应为True
        /// </summary>
        [Fact]
        public void NumberTest7()
        {
            bool ret = Xmtool.Regex().IsNumber("");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断0是否为整数，应为True
        /// </summary>
        [Fact]
        public void IntTest()
        {
            bool ret = Xmtool.Regex().IsInteger("0");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断15是否为整数，应为True
        /// </summary>
        [Fact]
        public void IntTest2()
        {
            bool ret = Xmtool.Regex().IsInteger("15");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断-18是否为整数，应为True
        /// </summary>
        [Fact]
        public void IntTest3()
        {
            bool ret = Xmtool.Regex().IsInteger("-18");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断0.7是否为整数，应为False
        /// </summary>
        [Fact]
        public void IntTest4()
        {
            bool ret = Xmtool.Regex().IsInteger("0.7");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断-9.6是否为整数，应为False
        /// </summary>
        [Fact]
        public void IntTest5()
        {
            bool ret = Xmtool.Regex().IsInteger("-9.6");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断abc是否为整数，应为False
        /// </summary>
        [Fact]
        public void IntTest6()
        {
            bool ret = Xmtool.Regex().IsInteger("abc");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断空是否为整数，应为False
        /// </summary>
        [Fact]
        public void IntTest7()
        {
            bool ret = Xmtool.Regex().IsInteger("");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断0是否为正整数，应为False
        /// </summary>
        [Fact]
        public void PositiveIntTest()
        {
            bool ret = Xmtool.Regex().IsPositiveInteger("0");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断2是否为正整数，应为True
        /// </summary>
        [Fact]
        public void PositiveIntTest2()
        {
            bool ret = Xmtool.Regex().IsPositiveInteger("2");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断2.8是否为正整数，应为False
        /// </summary>
        [Fact]
        public void PositiveIntTest3()
        {
            bool ret = Xmtool.Regex().IsPositiveInteger("2.8");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断-2.8是否为正整数，应为False
        /// </summary>
        [Fact]
        public void PositiveIntTest4()
        {
            bool ret = Xmtool.Regex().IsPositiveInteger("-2.8");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断-100是否为正整数，应为False
        /// </summary>
        [Fact]
        public void PositiveIntTest5()
        {
            bool ret = Xmtool.Regex().IsPositiveInteger("-100");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断abc是否为正整数，应为False
        /// </summary>
        [Fact]
        public void PositiveIntTest6()
        {
            bool ret = Xmtool.Regex().IsPositiveInteger("abc");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断空是否为正整数，应为False
        /// </summary>
        [Fact]
        public void PositiveIntTest7()
        {
            bool ret = Xmtool.Regex().IsPositiveInteger("");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断0是否为自然数，应为True
        /// </summary>
        [Fact]
        public void NaturalIntTest()
        {
            bool ret = Xmtool.Regex().IsNaturalInteger("0");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断10是否为自然数，应为True
        /// </summary>
        [Fact]
        public void NaturalIntTest2()
        {
            bool ret = Xmtool.Regex().IsNaturalInteger("10");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断2.5是否为自然数，应为False
        /// </summary>
        [Fact]
        public void NaturalIntTest3()
        {
            bool ret = Xmtool.Regex().IsNaturalInteger("2.5");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断-4是否为自然数，应为False
        /// </summary>
        [Fact]
        public void NaturalIntTest4()
        {
            bool ret = Xmtool.Regex().IsNaturalInteger("-4");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断abc是否为自然数，应为False
        /// </summary>
        [Fact]
        public void NaturalIntTest5()
        {
            bool ret = Xmtool.Regex().IsNaturalInteger("abc");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断空是否为自然数，应为False
        /// </summary>
        [Fact]
        public void NaturalIntTest6()
        {
            bool ret = Xmtool.Regex().IsNaturalInteger("");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断1.5是否为小数，应为True
        /// </summary>
        [Fact]
        public void DecimalTest()
        {
            bool ret = Xmtool.Regex().IsDecimal("1.5");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断0.2是否为小数，应为True
        /// </summary>
        [Fact]
        public void DecimalTest2()
        {
            bool ret = Xmtool.Regex().IsDecimal("0.2");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断-5.8是否为小数，应为True
        /// </summary>
        [Fact]
        public void DecimalTest3()
        {
            bool ret = Xmtool.Regex().IsDecimal("-5.8");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断-0.9是否为小数，应为True
        /// </summary>
        [Fact]
        public void DecimalTest4()
        {
            bool ret = Xmtool.Regex().IsDecimal("-0.9");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断0是否为小数，应为False
        /// </summary>
        [Fact]
        public void DecimalTest5()
        {
            bool ret = Xmtool.Regex().IsDecimal("0");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断100是否为小数，应为False
        /// </summary>
        [Fact]
        public void DecimalTest6()
        {
            bool ret = Xmtool.Regex().IsDecimal("100");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断-9是否为小数，应为False
        /// </summary>
        [Fact]
        public void DecimalTest7()
        {
            bool ret = Xmtool.Regex().IsDecimal("-9");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断abc是否为小数，应为False
        /// </summary>
        [Fact]
        public void DecimalTest8()
        {
            bool ret = Xmtool.Regex().IsDecimal("abc");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断空是否为小数，应为False
        /// </summary>
        [Fact]
        public void DecimalTest9()
        {
            bool ret = Xmtool.Regex().IsDecimal("");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断“Hello”是否为英文， 应为True
        /// </summary>
        [Fact]
        public void EnglishTest1()
        {
            bool ret = Xmtool.Regex().IsEnglish("Hello");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断“Abc123”是否为英文，应为False
        /// </summary>
        [Fact]
        public void EnglishTest2()
        {
            bool ret = Xmtool.Regex().IsEnglish("Abc123");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断“中国China”是否为英文，应为False
        /// </summary>
        [Fact]
        public void EnglishTest3()
        {
            bool ret = Xmtool.Regex().IsEnglish("中国China");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断空是否为英文，应为False
        /// </summary>
        [Fact]
        public void EnglishTest4()
        {
            bool ret = Xmtool.Regex().IsEnglish("");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断“China”是否为小写英文，应为False
        /// </summary>
        [Fact]
        public void LowercaseEnglishTest1()
        {
            bool ret = Xmtool.Regex().IsLowercaseEnglish("China");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断“hello”是否为小写英文，应为True
        /// </summary>
        [Fact]
        public void LowercaseEnglishTest2()
        {
            bool ret = Xmtool.Regex().IsLowercaseEnglish("hello");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断空是否为小写英文，应为False
        /// </summary>
        [Fact]
        public void LowercaseEnglishTest3()
        {
            bool ret = Xmtool.Regex().IsLowercaseEnglish("");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断“China”是否为大写英文，应为False
        /// </summary>
        [Fact]
        public void CapitalEnglishTest1()
        {
            bool ret = Xmtool.Regex().IsCapitalEnglish("China");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断“ABC”是否为大写英文，应为True
        /// </summary>
        [Fact]
        public void CapitalEnglishTest2()
        {
            bool ret = Xmtool.Regex().IsCapitalEnglish("ABC");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断空是否为大写英文，应为False
        /// </summary>
        [Fact]
        public void CapitalEnglishTest3()
        {
            bool ret = Xmtool.Regex().IsCapitalEnglish("");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断“中国”是否为中文，应为True
        /// </summary>
        [Fact]
        public void ChineseTest1()
        {
            bool ret = Xmtool.Regex().IsChinese("中国");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断“中国Good”是否为中文，应为False
        /// </summary>
        [Fact]
        public void ChineseTest2()
        {
            bool ret = Xmtool.Regex().IsChinese("中国Good");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断空是否为中文，应为False
        /// </summary>
        [Fact]
        public void ChineseTest3()
        {
            bool ret = Xmtool.Regex().IsChinese("");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断“Abc123”是否为数字或英文，应为True
        /// </summary>
        [Fact]
        public void MixCharTest1()
        {
            bool ret = Xmtool.Regex().IsMixChar("Abc123");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断“Abc”是否为数字或英文，应为True
        /// </summary>
        [Fact]
        public void MixCharTest2()
        {
            bool ret = Xmtool.Regex().IsMixChar("Abc");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断“123”是否为数字或英文，应为True
        /// </summary>
        [Fact]
        public void MixCharTest3()
        {
            bool ret = Xmtool.Regex().IsMixChar("123");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断“中国Abc123”是否为数字或英文，应为False
        /// </summary>
        [Fact]
        public void MixCharTest4()
        {
            bool ret = Xmtool.Regex().IsMixChar("中国Abc123");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断空是否为数字或英文，应为False
        /// </summary>
        [Fact]
        public void MixCharTest5()
        {
            bool ret = Xmtool.Regex().IsMixChar("");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断“一二三Abc123”是否为中文、数字或英文，应为True
        /// </summary>
        [Fact]
        public void MixChar2Test1()
        {
            bool ret = Xmtool.Regex().IsMixChar2("一二三Abc123");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断“Abc123”是否为中文、数字或英文，应为True
        /// </summary>
        [Fact]
        public void MixChar2Test2()
        {
            bool ret = Xmtool.Regex().IsMixChar2("Abc123");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断“Abc”是否为中文、数字或英文，应为True
        /// </summary>
        [Fact]
        public void MixChar2Test3()
        {
            bool ret = Xmtool.Regex().IsMixChar2("Abc");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断“123”是否为中文、数字或英文，应为True
        /// </summary>
        [Fact]
        public void MixChar2Test4()
        {
            bool ret = Xmtool.Regex().IsMixChar2("123");
            Assert.True(ret);
        }

        /// <summary>
        /// 判断“一二三Abc123~”是否为数字或英文，应为False
        /// </summary>
        [Fact]
        public void MixChar2Test5()
        {
            bool ret = Xmtool.Regex().IsMixChar("一二三Abc123~");
            Assert.False(ret);
        }

        /// <summary>
        /// 判断空是否为数字或英文，应为False
        /// </summary>
        [Fact]
        public void MixChar2Test6()
        {
            bool ret = Xmtool.Regex().IsMixChar2("");
            Assert.False(ret);
        }

    }
}
