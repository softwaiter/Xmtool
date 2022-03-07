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
            Xmtool.Sms(SmsProvider.Alibaba).Config("accessKeyId（替换成自己的）", "accessKeySecret（替换成自己的）", "阿里云短信测试", "SMS_154950909");
            bool ret = Xmtool.Sms(SmsProvider.Alibaba).Send("{\"code\":\"1234\"}", "136********");
            Assert.True(ret);
        }
    }
}