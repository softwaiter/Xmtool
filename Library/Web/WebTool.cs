using System.Collections.Concurrent;
using System.Net.Http;

namespace CodeM.Common.Tools.Web
{
    public class WebTool
    {
        private static WebTool sWTool = new WebTool();

        private WebTool()
        { 
        }

        internal static WebTool New()
        {
            return sWTool;
        }

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

        public HttpSecurity Security()
        {
            return HttpSecurity.New();
        }
    }
}
