using CodeM.Common.Tools;
using Xunit;
using Xunit.Abstractions;

namespace Test
{
    public class SmsTest
    {
        private ITestOutputHelper output;

        public SmsTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Test()
        {
            ISmsProvider sms = Xmtool.Sms(SmsProvider.Alibaba).Config("accessKeyId（替换成自己的）", "accessKeySecret（替换成自己的）",
                "阿里云短信签名（替换成自己的）", "模板编码（替换成自己的）");
            bool ret = sms.Send("参数（替换成自己的，如：{\"code\":\"1234\"}）", "136********");
            Assert.True(ret);
        }

        [Fact]
        public void Test2()
        {
            ISmsProvider sms = Xmtool.Sms(SmsProvider.Tencent).Config("secretId（替换成自己的）", "secretKey（替换成自己的）",
                "腾讯云短信签名（替换成自己的）", "模板Id（替换成自己的）", "appId（替换成自己的）");
            bool ret = sms.Send("参数（替换成自己的，如：1234）", "136********");
            Assert.True(ret);
        }
    }
}