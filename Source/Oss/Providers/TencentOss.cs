using COSXML;
using COSXML.Auth;
using COSXML.Model.Object;
using System;
using System.IO;

namespace CodeM.Common.Tools.Oss.Providers
{
    public class TencentOss : IOssProvider
    {
        private bool mConfigured;

        private string mAppId;
        private string mSecretId;
        private string mSecretKey;
        private bool mUseHttps;
        private string mProxyDomain;
        private string mRegion;
        private string mContentType;

        public IOssProvider Config(params string[] args)
        {
            if (args.Length < 5)
            {
                throw new ArgumentException("需要5个配置参数：AppId, SecretId、SecrectKey、ProxyDomain, Region");
            }

            mAppId = args[0];
            mSecretId = args[1];    // SecretId
            mSecretKey = args[2];   // SecretKey
            mProxyDomain = args[3]; // 源站域名
            mRegion = args[4];  // COS地域

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

            CosXmlConfig cosXmlConfig = new CosXmlConfig.Builder()
                .IsHttps(mUseHttps)
                .SetAppid(mAppId)
                .SetRegion(mRegion)
                .SetProxyDomain(mProxyDomain)
                .Build();
            QCloudCredentialProvider credential = new DefaultQCloudCredentialProvider(
                mSecretId, mSecretKey, 600);    // 600秒有效时长
            CosXml cosXml = new CosXmlServer(cosXmlConfig, credential);

            if (string.IsNullOrWhiteSpace(key))
            {
                key = Guid.NewGuid().ToString("N").ToLower();
            }

            PostObjectRequest request = new PostObjectRequest(bucketName, key, filename);
            if (!string.IsNullOrWhiteSpace(mContentType))
            {
                request.SetContentType(mContentType);
            }
            request.IsHttps = mUseHttps;

            PostObjectResult result = cosXml.PostObject(request);
            if (result.IsSuccessful())
            {
                if (!mProxyDomain.EndsWith("/"))
                {
                    mProxyDomain += "/";
                }

                return string.Concat(mProxyDomain, key);
            }
            else
            {
                throw new Exception(result.httpMessage);
            }
        }

        public string UploadStream(string bucketName, Stream stream, string key = null)
        {
            if (!mConfigured)
            {
                throw new Exception("请先进行OSS初始化配置。");
            }

            CosXmlConfig cosXmlConfig = new CosXmlConfig.Builder()
                .IsHttps(mUseHttps)
                .SetAppid(mAppId)
                .SetRegion(mRegion)
                .SetProxyDomain(mProxyDomain)
                .Build();
            QCloudCredentialProvider credential = new DefaultQCloudCredentialProvider(
                mSecretId, mSecretKey, 600);    // 600秒有效时长
            CosXml cosXml = new CosXmlServer(cosXmlConfig, credential);

            if (string.IsNullOrWhiteSpace(key))
            {
                key = Guid.NewGuid().ToString("N").ToLower();
            }

            PutObjectRequest request = new PutObjectRequest(bucketName, key, stream);
            if (!string.IsNullOrWhiteSpace(mContentType))
            {
                request.SetRequestHeader("Content-Type", mContentType);
            }
            request.IsHttps = mUseHttps;

            PutObjectResult result = cosXml.PutObject(request);
            if (result.IsSuccessful())
            {
                if (!mProxyDomain.EndsWith("/"))
                {
                    mProxyDomain += "/";
                }

                return string.Concat(mProxyDomain, key);
            }
            else
            {
                throw new Exception(result.httpMessage);
            }
        }

        public IOssProvider SetUseHttps(bool useHttps)
        {
            mUseHttps = useHttps;
            return this;
        }
    }
}
