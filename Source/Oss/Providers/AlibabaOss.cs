using Aliyun.OSS;
using Aliyun.OSS.Common;
using System;
using System.IO;
using System.Net;

namespace CodeM.Common.Tools.Oss.Providers
{
    public class AlibabaOss : IOssProvider
    {
        private bool mConfigured;

        private string mAppKey;
        private string mSecretKey;
        private bool mUseHttps;
        private string mProxyDomain;
        private string mEndPoint;
        private string mContentType;

        public IOssProvider Config(params string[] args)
        {
            if (args.Length < 4)
            {
                throw new ArgumentException("需要4个配置参数：AppKey、SecrectKey、ProxyDomain、Endpoint");
            }

            mAppKey = args[0];  // AppKey
            mSecretKey = args[1];   // SecretKey
            mProxyDomain = args[2]; // 源站域名
            mEndPoint = args[3];    // 访问域名
            return this;
        }

        public IOssProvider SetContentType(string contentType)
        {
            mContentType = contentType;
            return this;
        }

        public string UploadFile(string bucketName, string filename, string key = null)
        {
            if (!mConfigured)
            {
                throw new Exception("请先进行OSS初始化配置。");
            }

            ClientConfiguration config = new ClientConfiguration();
            config.Protocol = mUseHttps ? Protocol.Https : Protocol.Http;
            config.ProxyDomain = mProxyDomain;

            OssClient client = new OssClient(mEndPoint,
                mAppKey, mSecretKey, config);

            if (string.IsNullOrWhiteSpace(key))
            {
                key = Guid.NewGuid().ToString("N").ToLower();
            }

            ObjectMetadata meta = new ObjectMetadata();
            if (!string.IsNullOrWhiteSpace(mContentType))
            {
                meta.ContentType = mContentType;
            }

            PutObjectResult result = client.PutObject(bucketName, key, filename, meta);
            if (result.HttpStatusCode == HttpStatusCode.OK)
            {
                if (!mProxyDomain.EndsWith("/"))
                {
                    mProxyDomain += "/";
                }

                return string.Concat(mProxyDomain, key);
            }
            else
            {
                throw new Exception("上传失败，请重试。");
            }
        }

        public string UploadStream(string bucketName, Stream stream, string key = null)
        {
            if (!mConfigured)
            {
                throw new Exception("请先进行OSS初始化配置。");
            }

            ClientConfiguration config = new ClientConfiguration();
            config.Protocol = mUseHttps ? Protocol.Https : Protocol.Http;
            config.ProxyDomain = mProxyDomain;

            OssClient client = new OssClient(mEndPoint,
                mAppKey, mSecretKey, config);

            if (string.IsNullOrWhiteSpace(key))
            {
                key = Guid.NewGuid().ToString("N").ToLower();
            }

            ObjectMetadata meta = new ObjectMetadata();
            if (!string.IsNullOrWhiteSpace(mContentType))
            {
                meta.ContentType = mContentType;
            }

            PutObjectResult result = client.PutObject(bucketName, key, stream, meta);
            if (result.HttpStatusCode == HttpStatusCode.OK)
            {
                if (!mProxyDomain.EndsWith("/"))
                {
                    mProxyDomain += "/";
                }

                return string.Concat(mProxyDomain, key);
            }
            else
            {
                throw new Exception("上传失败，请重试。");
            }
        }

        public IOssProvider SetUseHttps(bool useHttps)
        {
            mUseHttps = useHttps;
            return this;
        }
    }
}
