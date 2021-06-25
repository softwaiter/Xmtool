using System.Collections.Concurrent;
using System.Net.Http;

namespace CodeM.Common.Tools.Web
{
    public class WebUtils
    {
        private static ConcurrentDictionary<string, HttpClient> sClients = new ConcurrentDictionary<string, HttpClient>();

        public static HttpClientExt Client(string name = "default")
        {
            HttpClient client = sClients.GetOrAdd(name, new HttpClient());
            return new HttpClientExt(client);
        }
    }
}
