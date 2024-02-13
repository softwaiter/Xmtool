using System.IO;

namespace CodeM.Common.Tools
{
    public enum OssProvider
    {
        Unset,
        Qiniu,
        Alibaba,
        Tencent
    }

    public interface IOssProvider
    {
        public IOssProvider Config(params string[] args);

        public IOssProvider SetUseHttps(bool useHttps);

        public IOssProvider SetContentType(string contentType);

        public string UploadFile(string bucketName, string filename, string key = null);

        public string UploadStream(string bucketName, Stream stream, string key = null);
    }
}
