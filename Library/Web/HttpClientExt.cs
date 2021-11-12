using CodeM.Common.Tools.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CodeM.Common.Tools.Web
{
    public class HttpClientExt
    {
        private HttpClient mClient;

        private HttpRequestMessage mRequest = null;

        internal HttpClientExt(HttpClient client)
        {
            mClient = client;
        }

        private void InitRequest()
        {
            if (mRequest == null)
            {
                mRequest = new HttpRequestMessage();
            }
        }

        public TimeSpan Timeout
        {
            get
            {
                return mClient.Timeout;
            }
            set
            {
                mClient.Timeout = value;
            }
        }

        public Uri BaseAddress
        {
            get
            {
                return mClient.BaseAddress;
            }
            set
            {
                mClient.BaseAddress = value;
            }
        }

        public HttpClientExt AddDefaultHeader(string name, string value)
        {
            mClient.DefaultRequestHeaders.Add(name, value);
            return this;
        }

        public HttpClientExt AddRequestHeader(string name, string value)
        {
            InitRequest();
            mRequest.Headers.Add(name, value);
            return this;
        }

        public HttpClientExt SetJsonContent(string content)
        {
            InitRequest();
            mRequest.Content = new StringContent(content, Encoding.UTF8, "application/json");
            return this;
        }

        public HttpClientExt SetJsonContent(dynamic obj)
        {
            InitRequest();
            mRequest.Content = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");
            return this;
        }

        private async Task<dynamic> SendJsonAsync(string requestUri)
        {
            InitRequest();
            if (mClient.BaseAddress == null)
            {
                mRequest.RequestUri = new Uri(requestUri);
            }
            else
            {
                mRequest.RequestUri = new Uri(mClient.BaseAddress, requestUri);
            }

            HttpResponseMessage resp = await mClient.SendAsync(mRequest);
            
            mRequest.Headers.Clear();
            mRequest.Content = null;

            if (resp.IsSuccessStatusCode)
            {
                string result = await resp.Content.ReadAsStringAsync();
                try
                {
                    return JsonConfigParser.New().Parse(result);
                }
                catch
                {
                    throw new Exception("请求结果不是合法JSON格式。");
                }
            }
            return null;
        }

        public async Task<dynamic> GetJsonAsync(string requestUri)
        {
            InitRequest();
            mRequest.Method = HttpMethod.Get;
            return await SendJsonAsync(requestUri);
        }

        public dynamic GetJson(string requestUri)
        {
            return GetJsonAsync(requestUri).GetAwaiter().GetResult();
        }

        public async Task<dynamic> PostJsonAsync(string requestUri)
        {
            InitRequest();
            mRequest.Method = HttpMethod.Post;
            return await SendJsonAsync(requestUri);
        }

        public dynamic PostJson(string requestUri)
        {
            return PostJsonAsync(requestUri).GetAwaiter().GetResult();
        }

        public async Task<dynamic> PutJsonAsync(string requestUri)
        {
            InitRequest();
            mRequest.Method = HttpMethod.Put;
            return await SendJsonAsync(requestUri);
        }

        public dynamic PutJson(string requestUri)
        {
            return PutJsonAsync(requestUri).GetAwaiter().GetResult();
        }

        public async Task<dynamic> DeleteJsonAsync(string requestUri)
        {
            InitRequest();
            mRequest.Method = HttpMethod.Delete;
            return await SendJsonAsync(requestUri);
        }

        public dynamic DeleteJson(string requestUri)
        {
            return DeleteJsonAsync(requestUri).GetAwaiter().GetResult();
        }

        public async Task<dynamic> PatchJsonAsync(string requestUri)
        {
            InitRequest();
            mRequest.Method = HttpMethod.Patch;
            return await SendJsonAsync(requestUri);
        }

        public dynamic PatchJson(string requestUri)
        {
            return PatchJsonAsync(requestUri).GetAwaiter().GetResult();
        }

        public async Task<dynamic> HeadJsonAsync(string requestUri)
        {
            InitRequest();
            mRequest.Method = HttpMethod.Head;
            return await SendJsonAsync(requestUri);
        }

        public dynamic HeadJson(string requestUri)
        {
            return HeadJsonAsync(requestUri).GetAwaiter().GetResult();
        }

        public async Task<dynamic> OptionsJsonAsync(string requestUri)
        {
            InitRequest();
            mRequest.Method = HttpMethod.Options;
            return await SendJsonAsync(requestUri);
        }

        public dynamic OptionsJson(string requestUri)
        {
            return OptionsJsonAsync(requestUri).GetAwaiter().GetResult();
        }
    }
}
