using System.Collections.Concurrent;
using System.Net.Http;

namespace CodeM.Common.Tools.Web
{
    public class WebUtils
    {
        private static ConcurrentDictionary<string, HttpClient> sClients = new ConcurrentDictionary<string, HttpClient>();

        /// <summary>
        /// 使用时，切记不要保存返回对象进行单例化
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static HttpClientExt Client(string name = "default")
        {
            HttpClient client = sClients.GetOrAdd(name, new HttpClient());
            return new HttpClientExt(client);
        }

        private static HttpSecurity sInst;
        private static object sLock = new object();
        public static HttpSecurity Security()
        {
            if (sInst == null)
            {
                lock (sLock)
                {
                    if (sInst == null)
                    {
                        sInst = new HttpSecurity();
                    }
                }
            }
            return sInst;
        }
    }
}
