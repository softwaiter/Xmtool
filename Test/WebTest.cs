using CodeM.Common.Tools;
using CodeM.Common.Tools.DynamicObject;
using CodeM.Common.Tools.Web;
using System;
using System.Net;
using Xunit;
using Xunit.Abstractions;

namespace Test
{
    public class WebTest
    {
        private ITestOutputHelper output;

        public WebTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async void GetTest()
        {
            HttpResponseExt rep = await Xmtool.Web.Client().GetAsync("http://www.baidu.com");
            Assert.Equal(HttpStatusCode.OK, rep.StatusCode);
            try
            {
                dynamic jsonObj = rep.Json;
            }
            catch (Exception exp)
            {
                Assert.Contains("JSON", exp.Message);
            }
        }

        [Fact]
        public async void PostTest()
        {
            dynamic data = new DynamicObjectExt();
            data.channel = 0;
            data.d = 10;
            data.domains = "163.com";
            data.l = 0;
            data.pd = "mail126";
            data.pkid = "QdQXWEQ";
            data.pw = "SHywNfsbpCUH6U/26KQRLYBOa7eMRZp0MqyrF7sfng2hxnRRWoLRNNrPxFF87Wnfz5mbusIBXPLJkN/Ruc2ucA3dYcNQ0k+3DYRphkq6K7xVaJAV2Znw8A25Rl4V/zBmeRvHIvwf0Q4DQJnK+Fbj4+yo3P0bIge7euoPEGrbHUs=";
            data.pwdKeyUp = 0;
            data.rtid = "QQ1MeSNpLaDGwtZG50SlKRzhgz7yAU75";
            data.t = 1624606697553;
            data.tk = "2d47c20c858e673ab49bf00fe7541807";
            data.topURL = "https://www.126.com/";
            data.un = "softwaiter@126.com";

            HttpResponseExt rep = await Xmtool.Web.Client()
                .SetJsonContent(data)
                .PostAsync("https://passport.126.com/dl/zj/mail/gt");
            Assert.Equal(HttpStatusCode.OK, rep.StatusCode);
            Assert.Equal("401", rep.Json.ret);
        }

        [Fact]
        public void DoubleRequestTest()
        {
            HttpResponseExt rep = Xmtool.Web.Client().Get("https://api.juejin.cn/interact_api/v1/pin_tab_lead");
            Assert.Equal(HttpStatusCode.OK, rep.StatusCode);
            rep = Xmtool.Web.Client().Get("http://www.baidu.com");
            Assert.Equal(HttpStatusCode.OK, rep.StatusCode);
            Assert.Contains("<!DOCTYPE html>", rep.Content);
        }

        [Fact]
        public void ScriptTagTest()
        {
            string str = "<script>alert(123);</script><div>hello world.</div>";
            string str2 = Xmtool.Web.Security().Xss(str);
            Assert.DoesNotContain("script", str2);
        }

        [Fact]
        public void OnClickTest()
        {
            string str = "<div onclick=\"javascript:alert(123);\">hello world.</div>";
            string str2 = Xmtool.Web.Security().Xss(str);
            Assert.DoesNotContain("alert", str2);
        }

        [Fact]
        public void ImageSrcTest()
        {
            string str = "<div>hello world.<br/><img src=\"javascript:alert(123);\"></div>";
            string str2 = Xmtool.Web.Security().Xss(str);
            Assert.DoesNotContain("alert", str2);
        }

        [Fact]
        public void IframeSrcTest()
        {
            string str = "<div>hello world.<br/></div><iframe src=\"http://www.baidu.com\"/>";
            string str2 = Xmtool.Web.Security().Xss(str);
            Assert.DoesNotContain("iframe", str2);
        }
    }
}
