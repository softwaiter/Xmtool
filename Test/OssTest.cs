using CodeM.Common.Tools;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace Test
{
    public class OssTest
    {
        private ITestOutputHelper output;

        public OssTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void QiniuUploadFile()
        {
            IOssProvider oss = Xmtool.Oss(OssProvider.Qiniu)
                .Config("替换成你自己的AppKey", "替换成你自己的AppSecret", "替换成自己的域名地址");
            string url = oss.UploadFile("替换成你的存储桶名称", "要上传的本地文件路径", "上传到服务器的文件key，如果不传将随机生成");
            Assert.NotEmpty(url);
        }

        [Fact]
        public void QiniuUploadStream()
        {
            IOssProvider oss = Xmtool.Oss(OssProvider.Qiniu)
                .Config("替换成你自己的AppKey", "替换成你自己的AppSecret", "替换成自己的域名地址");
            using (FileStream stream = new FileStream("要上传的本地文件路径", FileMode.Open))
            {
                string url = oss.UploadStream("替换成你的存储桶名称", stream, "上传到服务器的文件key，如果不传将随机生成");
                Assert.NotEmpty(url);
            }
        }

        [Fact]
        public void AlibabaUploadFile()
        {
            IOssProvider oss = Xmtool.Oss(OssProvider.Alibaba)
                .Config("替换成你自己的AppKey", "替换成你自己的AppSecret", "替换成自己的域名地址", "替换成存储桶对应的EndPoint地址");
            string url = oss.UploadFile("替换成你的存储桶名称", "要上传的本地文件路径", "上传到服务器的文件key，如果不传将随机生成");
            Assert.NotEmpty(url);
        }

        [Fact]
        public void AlibabaUploadStream()
        {
            IOssProvider oss = Xmtool.Oss(OssProvider.Alibaba)
                .Config("替换成你自己的AppKey", "替换成你自己的AppSecret", "替换成自己的域名地址", "替换成存储桶对应的EndPoint地址");
            using (FileStream stream = new FileStream("要上传的本地文件路径", FileMode.Open))
            {
                string url = oss.UploadStream("替换成你的存储桶名称", stream, "上传到服务器的文件key，如果不传将随机生成");
                Assert.NotEmpty(url);
            }
        }

        [Fact]
        public void TencentUploadFile()
        {
            IOssProvider oss = Xmtool.Oss(OssProvider.Tencent)
                .Config("替换成你自己的AppId", "替换成你自己的SecretId", "替换成你自己的SecretKey", "替换成自己的域名地址", "替换成存储桶对应的Region简称");
            string url = oss.UploadFile("替换成你的存储桶名称", "要上传的本地文件路径", "上传到服务器的文件key，如果不传将随机生成");
            Assert.NotEmpty(url);
        }

        [Fact]
        public void TencentUploadStream()
        {
            IOssProvider oss = Xmtool.Oss(OssProvider.Tencent)
                .Config("替换成你自己的AppId", "替换成你自己的SecretId", "替换成你自己的SecretKey", "替换成自己的域名地址", "替换成存储桶对应的Region简称");
            using (FileStream stream = new FileStream("要上传的本地文件路径", FileMode.Open))
            {
                string url = oss.UploadStream("替换成你的存储桶名称", stream, "上传到服务器的文件key，如果不传将随机生成");
                Assert.NotEmpty(url);
            }
        }
    }
}
