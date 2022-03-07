using System.Threading.Tasks;

namespace CodeM.Common.Tools
{
    public enum SmsProvider
    {
        Alibaba,
        Tencent
    }

    public interface ISmsProvider
    {
        public void Config(string accessKeyId, string accessKeySecret, string signName, string templateCode);

        public bool Send(string templateParam, params string[] phones);

        public bool Send(string signName, string templateCode, string templateParam, params string[] phones);

        public Task<bool> SendAsync(string templateParam, params string[] phones);

        public Task<bool> SendAsync(string signName, string templateCode, string templateParam, params string[] phones);
    }
}
