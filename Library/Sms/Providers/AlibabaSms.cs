using CodeM.Common.Tools.Web;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CodeM.Common.Tools.Sms.Providers
{
    internal class AlibabaSms : ISmsProvider
    {
        private string mAccessKeyId;
        private string mAccessKeySecret;
        private string mSignName;
        private string mTemplateCode;

        /// <summary>
        /// 初始化参数（accessKeyId、accessKeySecrect、signName、templateCode）
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public ISmsProvider Config(params string[] args)
        {
            if (args.Length < 4)
            {
                throw new ArgumentException("需要4个配置参数：accessKeyId、accessKeySecrect、signName、templateCode");
            }

            mAccessKeyId = args[0];
            mAccessKeySecret = args[1];
            mSignName = args[2];
            mTemplateCode = args[3];
            return this;
        }

        private string UrlEncode(string str)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in str)
            {
                string encodeC = HttpUtility.UrlEncode(c.ToString());
                if (encodeC.Length > 1)
                {
                    builder.Append(encodeC.ToUpper());
                }
                else
                {
                    builder.Append(c);
                }
            }
            return builder.ToString();
        }

        private string SpecialUrlEncode(string value)
        {
            return UrlEncode(value)
                .Replace("+", "%20")
                .Replace("*", "%2A")
                .Replace("%7E", "~");
        }

        private string GenSign(string strToSign)
        {
            using (var hmac = new HMACSHA1(Encoding.UTF8.GetBytes(mAccessKeySecret + "&")))
            {
                var hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(strToSign));
                return Convert.ToBase64String(hashValue);
            }
        }

        private string GenQueryParams(string signName, string templateCode, string templateParam, params string[] phoneNums)
        {
            Dictionary<string, string> _params = new Dictionary<string, string>();
            _params.Add("AccessKeyId", mAccessKeyId);
            _params.Add("Format", "JSON");
            _params.Add("RegionId", "cn-hangzhou");
            _params.Add("SignatureMethod", "HMAC-SHA1");
            _params.Add("SignatureNonce", Guid.NewGuid().ToString("N"));
            _params.Add("SignatureVersion", "1.0");
            _params.Add("Timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"));
            _params.Add("Version", "2017-05-25");
            _params.Add("Action", "SendSms");
            _params.Add("SignName", signName);
            _params.Add("PhoneNumbers", String.Join(",", phoneNums));
            _params.Add("TemplateCode", templateCode);
            _params.Add("TemplateParam", templateParam);

            List<string> _keyList = new List<string>();
            _keyList.Add("AccessKeyId");
            _keyList.Add("Format");
            _keyList.Add("RegionId");
            _keyList.Add("SignatureMethod");
            _keyList.Add("SignatureNonce");
            _keyList.Add("SignatureVersion");
            _keyList.Add("Timestamp");
            _keyList.Add("Version");
            _keyList.Add("Action");
            _keyList.Add("SignName");
            _keyList.Add("PhoneNumbers");
            _keyList.Add("TemplateCode");
            _keyList.Add("TemplateParam");

            StringBuilder sbResult = new StringBuilder();
            _keyList.Sort(string.CompareOrdinal);
            foreach (string key in _keyList)
            {
                sbResult
                    .Append("&")
                    .Append(SpecialUrlEncode(key))
                    .Append("=")
                    .Append(SpecialUrlEncode(_params[key]));
            }
            string sortedQueryString = sbResult.ToString().Substring(1);

            StringBuilder stringToSign = new StringBuilder();
            stringToSign.Append("GET").Append("&");
            stringToSign.Append(SpecialUrlEncode("/")).Append("&");
            stringToSign.Append(SpecialUrlEncode(sortedQueryString));

            string sign = GenSign(stringToSign.ToString());
            sbResult.Insert(0, string.Concat("Signature=", SpecialUrlEncode(sign)));

            return sbResult.ToString();
        }

        private string GenRquestUri(string _queryParams)
        {
            string url = "http://dysmsapi.aliyuncs.com/?" + _queryParams;
            return url;
        }

        public bool Send(string templateParam, params string[] phoneNums)
        {
            string _queryParams = GenQueryParams(mSignName, mTemplateCode, templateParam, phoneNums);
            string url = GenRquestUri(_queryParams);
            HttpResponseExt res = WebTool.New().Client().GetJson(url);
            return res.Json.Code == "OK";
        }

        public bool Send2(string signName, string templateCode, string templateParam, params string[] phoneNums)
        {
            string _queryParams = GenQueryParams(signName, templateCode, templateParam, phoneNums);
            string url = GenRquestUri(_queryParams);
            HttpResponseExt res = WebTool.New().Client().GetJson(url);
            return res.Json.Code == "OK";
        }

        public async Task<bool> SendAsync(string phoneNum, params string[] templateParams)
        {
            string _queryParams = GenQueryParams(mSignName, mTemplateCode, phoneNum, templateParams);
            string url = GenRquestUri(_queryParams);
            HttpResponseExt res = await WebTool.New().Client().GetJsonAsync(url);
            return res.Json.Code == "OK";
        }

        public async Task<bool> Send2Async(string signName, string templateCode, string phoneNum, params string[] templateParams)
        {
            string _queryParams = GenQueryParams(signName, templateCode, phoneNum, templateParams);
            string url = GenRquestUri(_queryParams);
            HttpResponseExt res = await WebTool.New().Client().GetJsonAsync(url);
            return res.Json.Code == "OK";
        }
    }
}
