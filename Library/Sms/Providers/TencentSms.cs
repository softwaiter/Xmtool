using CodeM.Common.Tools.Web;
using System;
using System.Threading.Tasks;

namespace CodeM.Common.Tools.Sms.Providers
{
    internal class TencentSms : ISmsProvider
    {
        private string mAccessKeyId;
        private string mAccessKeySecret;
        private string mSignName;
        private string mTemplateCode;
        private string mAppId;

        public void Config(string accessKeyId, string accessKeySecret, string signName, string templateCode)
        {
            mAccessKeyId = accessKeyId;
            mAccessKeySecret = accessKeySecret;
            mSignName = signName;
            mTemplateCode = templateCode;
            mAppId = "1400642665";
        }

        private object GenPostBody(string signName, string templateCode, string templateParam, params string[] phones)
        {
            dynamic result = new Json.DynamicObjectExt();
            result.Action = "SendSms";
            result.Version = "2021-01-11";
            result.Region = "ap-guangzhou";
            result.SmsSdkAppId = mAppId;
            result.SignName = signName;
            result.TemplateId = templateCode;
            result.PhoneNumberSet = phones;
            result.TemplateParamSet = "";
            return result;
        }

        public bool Send(string templateParam, params string[] phones)
        {
            long timestamp = DateTimeTool.New().GetUtcTimestamp10();

            object _postBody = GenPostBody(mSignName, mTemplateCode, templateParam, phones);
            string url = "http://sms.tencentcloudapi.com";
            HttpResponseExt res = WebTool.New().Client()
                .AddRequestHeader("X-TC-Action", "SendSms")
                .AddRequestHeader("X-TC-Region", "ap-guangzhou")
                .AddRequestHeader("X-TC-Timestamp", "" + timestamp)
                .AddRequestHeader("X-TC-Version", "2021-01-11")
                .AddRequestHeader("Authorization", "hello")
                .SetJsonContent(_postBody)
                .PostJson(url);
            return res.Json.SendStatusSet.Code == "OK";
        }

        public bool Send(string signName, string templateCode, string templateParam, params string[] phones)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendAsync(string templateParam, params string[] phones)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendAsync(string signName, string templateCode, string templateParam, params string[] phones)
        {
            throw new NotImplementedException();
        }
    }
}
