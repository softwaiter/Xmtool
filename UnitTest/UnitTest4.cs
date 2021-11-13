﻿using CodeM.Common.Tools;
using CodeM.Common.Tools.Json;
using CodeM.Common.Tools.Web;
using System;
using System.Net;
using Xunit;
using Xunit.Abstractions;

namespace UnitTest
{
    public class UnitTest4
    {
        private ITestOutputHelper output;

        public UnitTest4(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async void GetTest()
        {
            HttpResponseExt rep = await Xmtool.Web.Client().GetJsonAsync("http://www.baidu.com");
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
            data.pw = "SHywNfsbpCUH6U/9lKQRLYBOa7eMRZp0MqyrF7sfng2hxnRRWoLRNNrPxFF84Wnfz5mbusIBXPLJkN/Ruc2ucA3dYcNQ0k+3DYRphkq6K7xVaJAV2Znw8A43Rl4V/zBmeRvHIvwf0Q4DQJnK+Fbj4+yo3P0bIge7euoPEGrbHUs=";
            data.pwdKeyUp = 0;
            data.rtid = "QQ1MeSNpLaDGwtZG50SlKRzhgz7yAU75";
            data.t = 1624606697553;
            data.tk = "2d47c20c858e673ab49bf00fe7541807";
            data.topURL = "https://www.126.com/";
            data.un = "softwaiter@126.com";

            HttpResponseExt rep = await Xmtool.Web.Client()
                .SetJsonContent(data)
                .PostJsonAsync("https://passport.126.com/dl/l");
            Assert.Equal(HttpStatusCode.OK, rep.StatusCode);
            Assert.Equal("401", rep.Json.ret);
        }

        [Fact]
        public void DoubleRequestTest()
        {
            HttpResponseExt rep = Xmtool.Web.Client().GetJson("https://api.juejin.cn/interact_api/v1/pin_tab_lead");
            Assert.Equal(HttpStatusCode.OK, rep.StatusCode);
            rep = Xmtool.Web.Client().GetJson("http://www.baidu.com");
            Assert.Equal(HttpStatusCode.OK, rep.StatusCode);
            Assert.Contains("<!DOCTYPE html>", rep.Content);
        }
    }
}
