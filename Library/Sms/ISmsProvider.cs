using System.Threading.Tasks;

namespace CodeM.Common.Tools
{
    public enum SmsProvider
    {
        Unset,
        Alibaba,
        Tencent
    }

    public interface ISmsProvider
    {
        public ISmsProvider Config(params string[] args);

        public bool Send(string templateParam, params string[] phoneNums);

        public bool Send2(string signName, string templateCode, string templateParam, params string[] phoneNums);

        public Task<bool> SendAsync(string templateParam, params string[] phoneNums);

        public Task<bool> Send2Async(string signName, string templateCode, string templateParam, params string[] phoneNums);
    }
}
