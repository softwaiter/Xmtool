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
        /// 本对象不支持并发，在多线程中请谨慎使用
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public HttpClientExt Client(string name = "default")
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
