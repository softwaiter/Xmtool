using CodeM.Common.Tools.Web;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CodeM.Common.Tools.Sms.Providers
{
    internal class TencentSms : ISmsProvider
    {
        private string mAppId;
        private string mSecretId;
        private string mSecretKey;
        private string mSignName;
        private string mTemplateCode;

        /// <summary>
        /// 初始化参数（secrectId、secrectKey、signName、templateCode、appId）
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public ISmsProvider Config(params string[] args)
        {
            if (args.Length < 5)
            {
                throw new ArgumentException("需要5个配置参数：secrectId、secrectKey、signName、templateCode, appId");
            }

            mSecretId = args[0];
            mSecretKey = args[1];
            mSignName = args[2];
            mTemplateCode = args[3];
            mAppId = args[4];
            return this;
        }

        private object GenPostBody(string signName, string templateCode, string templateParam, params string[] phoneNums)
        {
            dynamic result = new Json.DynamicObjectExt();
            result.SmsSdkAppId = mAppId;
            result.SignName = signName;
            result.TemplateId = templateCode;
            result.PhoneNumberSet = phoneNums;
            result.TemplateParamSet = templateParam.Split("|");
            return result;
        }
        private static byte[] HmacSHA256(byte[] key, byte[] msg)
        {
            using (HMACSHA256 mac = new HMACSHA256(key))
            {
                return mac.ComputeHash(msg);
            }
        }

        private string GenAuthorization(object postData, long timestamp)
        {
            // ************* 步骤 1：拼接规范请求串 *************
            string host = "sms.tencentcloudapi.com";
            string httpRequestMethod = "POST";
            string canonicalUri = "/";
            string canonicalQueryString = "";
            string canonicalHeaders = "content-type:application/json; charset=utf-8\n" + "host:" + host + "\n";
            string signedHeaders = "content-type;host";
            string hashedRequestPayload = HashTool.New().SHA256(postData.ToString());
            string canonicalRequest = httpRequestMethod + "\n" + canonicalUri + "\n" + canonicalQueryString + "\n"
                    + canonicalHeaders + "\n" + signedHeaders + "\n" + hashedRequestPayload;

            // ************* 步骤 2：拼接待签名字符串 *************
            DateTime dt = DateTimeTool.New().GetUtcDateTimeFromUtcTimestamp10(timestamp);
            string datestr = dt.ToString("yyyy-MM-dd");
            string service = "sms";
            string algorithm = "TC3-HMAC-SHA256";
            string credentialScope = datestr + "/" + service + "/" + "tc3_request";
            string hashedCanonicalRequest = HashTool.New().SHA256(canonicalRequest);
            string stringToSign = algorithm + "\n" + timestamp + "\n" + credentialScope + "\n" + hashedCanonicalRequest;

            // ************* 步骤 3：计算签名 *************
            byte[] tc3SecretKey = Encoding.UTF8.GetBytes("TC3" + mSecretKey);
            byte[] secretDate = HmacSHA256(tc3SecretKey, Encoding.UTF8.GetBytes(datestr));
            byte[] secretService = HmacSHA256(secretDate, Encoding.UTF8.GetBytes(service));
            byte[] secretSigning = HmacSHA256(secretService, Encoding.UTF8.GetBytes("tc3_request"));
            byte[] signatureBytes = HmacSHA256(secretSigning, Encoding.UTF8.GetBytes(stringToSign));
            string signature = BitConverter.ToString(signatureBytes).Replace("-", "").ToLower();

            // ************* 步骤 4：拼接 Authorization *************
            string authorization = algorithm + " "
                + "Credential=" + mSecretId + "/" + credentialScope + ", "
                + "SignedHeaders=" + signedHeaders + ", "
                + "Signature=" + signature;

            return authorization;
        }

        private static readonly string sUrl = "https://sms.tencentcloudapi.com";

        private HttpClientExt GenHttpClient(string signName, string templateCode, string templateParam, params string[] phoneNums)
        {
            object _postBody = GenPostBody(signName, templateCode, templateParam, phoneNums);
            long timestamp = DateTimeTool.New().GetUtcTimestamp10();
            string authorization = GenAuthorization(_postBody, timestamp);
            return WebTool.New().Client()
                .AddRequestHeader("X-TC-Action", "SendSms")
                .AddRequestHeader("X-TC-Region", "ap-guangzhou")
                .AddRequestHeader("X-TC-Timestamp", "" + timestamp)
                .AddRequestHeader("X-TC-Version", "2021-01-11")
                .AddRequestHeader("X-TC-Language", "zh-CN")
                .AddRequestHeaderWithoutValidation("Authorization", authorization)
                .AddRequestHeader("Host", "sms.tencentcloudapi.com")
                .SetJsonContent(_postBody);
        }

        private bool ParseResult(HttpResponseExt res)
        {
            if (res.Json != null && res.Json.HasPath("Response.SendStatusSet"))
            {
                dynamic statusSet = res.Json.Response.SendStatusSet;
                if (statusSet != null)
                {
                    Type _typ = statusSet.GetType();
                    if ((_typ.IsGenericType &&
                        _typ.GetGenericTypeDefinition() == typeof(List<>)) ||
                        (_typ.IsArray && statusSet.Length > 0))
                    {
                        dynamic statusObj = statusSet[0];
                        if (statusObj != null && statusObj.Has("Code"))
                        {
                            return "Ok".Equals(statusObj.Code);
                        }
                    }
                }
            }
            return false;
        }

        public bool Send(string templateParam, string[] phoneNums)
        {
            HttpClientExt client = GenHttpClient(mSignName, mTemplateCode, templateParam, phoneNums);
            HttpResponseExt res = client.PostJson(sUrl);
            return ParseResult(res);
        }

        public bool Send2(string signName, string templateCode, string templateParam, params string[] phoneNums)
        {
            HttpClientExt client = GenHttpClient(signName, templateCode, templateParam, phoneNums);
            HttpResponseExt res = client.PostJson(sUrl);
            return ParseResult(res);
        }

        public async Task<bool> SendAsync(string templateParam, params string[] phoneNums)
        {
            HttpClientExt client = GenHttpClient(mSignName, mTemplateCode, templateParam, phoneNums);
            HttpResponseExt res = await client.PostJsonAsync(sUrl);
            return ParseResult(res);
        }

        public async Task<bool> Send2Async(string signName, string templateCode, string templateParam, params string[] phoneNums)
        {
            HttpClientExt client = GenHttpClient(signName, templateCode, templateParam, phoneNums);
            HttpResponseExt res = await client.PostJsonAsync(sUrl);
            return ParseResult(res);
        }
    }
}
