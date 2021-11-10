using CodeM.Common.Tools.Web;
using Xunit;
using Xunit.Abstractions;

namespace UnitTest
{
    public class UnitTest6
    {
        private ITestOutputHelper output;

        public UnitTest6(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Test()
        {
            string str = "<script>alert(123);</script><div>hello world.</div>";
            string str2 =WebTool.New().Security().Xss(str);
            Assert.DoesNotContain("script", str2);
        }

        [Fact]
        public void Test2()
        {
            string str = "<div onclick=\"javascript:alert(123);\">hello world.</div>";
            string str2 = WebTool.New().Security().Xss(str);
            Assert.DoesNotContain("alert", str2);
        }

        [Fact]
        public void Test3()
        {
            string str = "<div>hello world.<br/><img src=\"javascript:alert(123);\"></div>";
            string str2 = WebTool.New().Security().Xss(str);
            Assert.DoesNotContain("alert", str2);
        }

        [Fact]
        public void Test4()
        {
            string str = "<div>hello world.<br/></div><iframe src=\"http://www.baidu.com\"/>";
            string str2 = WebTool.New().Security().Xss(str);
            Assert.DoesNotContain("iframe", str2);
        }
    }
}