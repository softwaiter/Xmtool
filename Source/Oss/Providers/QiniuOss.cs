using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;
using System;
using System.IO;

namespace CodeM.Common.Tools.Oss.Providers
{
    public class QiniuOss : IOssProvider
    {
        private bool mConfigured = false;

        private string mAppKey;
        private string mSecretKey;
        private bool mUseHttps;
        private string mProxyDomain;
        private string mContentType;

        public IOssProvider Config(params string[] args)
        {
            if (args.Length < 3)
            {
                throw new ArgumentException("需要3个配置参数：AppKey、SecrectKey、ProxyDomain");
            }

            mAppKey = args[0];  // AppKey
            mSecretKey = args[1];   // SecretKey
            mProxyDomain = args[2]; // 源站域名

            mConfigured = true;

            return this;
        }

        public IOssProvider SetContentType(string contentType)
        {
            mContentType = contentType;
            return this;
        }

        public IOssProvider SetUseHttps(bool useHttps)
        {
            mUseHttps = useHttps;
            return this;
        }

        public string UploadFile(string bucketName, string filename, string key = null)
        {
            if (!mConfigured)
            {
                throw new Exception("请先进行OSS初始化配置。");
            }

            Config config = new Config();
            config.Zone = ZoneHelper.QueryZone(mAppKey, bucketName);
            config.UseHttps = mUseHttps;

            FormUploader target = new FormUploader(config);

            if (string.IsNullOrWhiteSpace(key))
            {
                key = Guid.NewGuid().ToString("N").ToLower();
            }

            Mac mac = new Mac(mAppKey, mSecretKey);
            
            PutPolicy putPolicy = new PutPolicy();
            putPolicy.Scope = bucketName;

            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());

            HttpResult resp = target.UploadFile(filename, key, token, null);
            if (resp.Code == 200)
            {
                if (!mProxyDomain.EndsWith("/"))
                {
                    mProxyDomain += "/";
                }

                return string.Concat(mProxyDomain, key);
            }
            else
            {
                throw new Exception(resp.Text);
            }
        }

        public string UploadStream(string bucketName, Stream stream, string key = null)
        {
            if (!mConfigured)
            {
                throw new Exception("请先进行OSS初始化配置。");
            }

            Config config = new Config();
            config.Zone = ZoneHelper.QueryZone(mAppKey, bucketName);
            config.UseHttps = mUseHttps;

            FormUploader target = new FormUploader(config);

            if (string.IsNullOrWhiteSpace(key))
            {
                key = Guid.NewGuid().ToString("N").ToLower();
            }

            Mac mac = new Mac(mAppKey, mSecretKey);

            PutPolicy putPolicy = new PutPolicy();
            putPolicy.Scope = bucketName;

            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());

            HttpResult resp = target.UploadStream(stream, key, token, null);
            if (resp.Code == 200)
            {
                if (!mProxyDomain.EndsWith("/"))
                {
                    mProxyDomain += "/";
                }

                return string.Concat(mProxyDomain, key);
            }
            else
            {
                throw new Exception(resp.Text);
            }
        }
    }
}
