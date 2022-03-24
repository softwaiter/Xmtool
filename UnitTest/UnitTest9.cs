using CodeM.Common.Tools;
using Xunit;
using Xunit.Abstractions;

namespace UnitTest
{
    public class UnitTest9
    {
        private ITestOutputHelper output;

        public UnitTest9(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Test()
        {
            Xmtool.Sms(SmsProvider.Alibaba).Config("accessKeyId（替换成自己的）", "accessKeySecret（替换成自己的）",
                "阿里云短信签名（替换成自己的）", "模板编码（替换成自己的）");
            bool ret = Xmtool.Sms(SmsProvider.Alibaba).Send("136********", "参数（替换成自己的，如：{\"code\":\"1234\"}）");
            Assert.True(ret);
        }

        [Fact]
        public void Test2()
        {
            ISmsProvider sms = Xmtool.Sms(SmsProvider.Tencent).Config("appid（替换成自己的）", "secretId（替换成自己的）",
                "secretKey（替换成自己的）", "腾讯云短信签名（替换成自己的）", "模板Id（替换成自己的）");
            bool ret = sms.Send("136********", "参数（替换成自己的，如：1234）");
            Assert.True(ret);
        }
    }
}