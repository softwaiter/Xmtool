using CodeM.Common.Tools.DynamicObject;
using CodeM.Common.Tools.Json;
using CodeM.Common.Tools.Xml;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CodeM.Common.Tools.Web
{
    public class HttpResponseHeadersExt
    {
        HttpResponseHeaders mSource;
        Dictionary<string, IEnumerable<string>> mData;

        internal HttpResponseHeadersExt(HttpResponseHeaders headers)
        {
            if (headers == null)
                throw new ArgumentNullException(nameof(headers));

            mSource = headers;
        }

        private void Prepare()
        {
            if (mData == null)
            {
                lock (this)
                {
                    if (mData == null)
                    {
                        mData = new Dictionary<string, IEnumerable<string>>();

                        IEnumerator<KeyValuePair<string, IEnumerable<string>>> e = mSource.GetEnumerator();
                        while (e.MoveNext())
                        {
                            mData.Add(e.Current.Key, e.Current.Value);
                        }
                    }
                }
            }
        }

        public string this[string key]
        {
            get
            {
                Prepare();
                if (mData.TryGetValue(key, out var value))
                {
                    return string.Join(",", value);
                }
                return null;
            }
        }

        public string this[int index]
        {
            get
            {
                Prepare();
                if (index >= 0 && index < mData.Keys.Count)
                {
                    string[] keys = new string[mData.Keys.Count];
                    mData.Keys.CopyTo(keys, 0);
                    string key = keys[index];
                    if (mData.TryGetValue(key, out var value))
                    {
                        return string.Join(",", value);
                    }
                }
                return null;
            }
        }

        public bool ContainsKey(string key)
        {
            Prepare();
            return mData.ContainsKey(key);
        }

        public string Get(string key, string defaultValue)
        {
            Prepare();
            if (ContainsKey(key))
            {
                return this[key];
            }
            return defaultValue;
        }

        public string Get(int index, string defaultValue)
        {
            Prepare();
            if (index >= 0 && index < mData.Keys.Count)
            {
                return this[index];
            }
            return defaultValue;
        }

        public int Count
        {
            get
            {
                Prepare();
                return mData.Count;
            }
        }
    }

    public class HttpResponseExt
    {
        private HttpStatusCode mStatusCode;
        private HttpResponseHeadersExt mHeaders;
        private string mContent;

        internal HttpResponseExt(HttpStatusCode statusCode, HttpResponseHeaders headers, string content)
        {
            mStatusCode = statusCode;
            mHeaders = new HttpResponseHeadersExt(headers);
            mContent = content;
        }

        public HttpStatusCode StatusCode
        {
            get
            {
                return mStatusCode;
            }
        }

        public HttpResponseHeadersExt Headers
        {
            get
            {
                return mHeaders;
            }
        }

        public string Content
        {
            get
            {
                return mContent;
            }
        }

        private dynamic mJsonObj = null;
        public dynamic Json
        {
            get
            {
                if (mJsonObj == null)
                {
                    try
                    {
                        mJsonObj = JsonConfigParser.New().Parse(mContent);
                    }
                    catch
                    {
                        throw new Exception("返回结果不是有效的JSON格式。");
                    }
                }
                return mJsonObj;
            }
        }

        private dynamic mXmlObj = null;
        public dynamic Xml
        {
            get
            {
                if (mXmlObj == null)
                {
                    try
                    {
                        mXmlObj = XmlTool.New().DeserializeFromString(mContent);
                    }
                    catch
                    {
                        throw new Exception("返回结果不是有效的XML格式。");
                    }
                }
                return mXmlObj;
            }
        }
    }

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

        public HttpClientExt AddRequestHeaderWithoutValidation(string name, string value)
        {
            InitRequest();
            mRequest.Headers.TryAddWithoutValidation(name, value);
            return this;
        }

        public HttpClientExt SetContent(string content)
        {
            InitRequest();
            mRequest.Content = new StringContent(content, Encoding.UTF8);
            return this;
        }

        public HttpClientExt SetJsonContent(string content)
        {
            InitRequest();
            mRequest.Content = new StringContent(content, Encoding.UTF8, "application/json");
            return this;
        }

        public HttpClientExt SetJsonContent(DynamicObjectExt obj)
        {
            InitRequest();
            mRequest.Content = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");
            return this;
        }

        public HttpClientExt Clear()
        {
            if (mRequest != null)
            {
                mRequest.Headers.Clear();
                mRequest.Content = null;
            }
            return this;
        }

        private async Task<HttpResponseExt> SendJsonAsync(string requestUri)
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

            string content = string.Empty;
            if (resp.Content != null)
            {
                content = await resp.Content.ReadAsStringAsync();
            }
            return new HttpResponseExt(resp.StatusCode, resp.Headers, content);
        }

        public async Task<HttpResponseExt> GetAsync(string requestUri)
        {
            InitRequest();
            mRequest.Method = HttpMethod.Get;
            return await SendJsonAsync(requestUri);
        }

        public HttpResponseExt Get(string requestUri)
        {
            return GetAsync(requestUri).GetAwaiter().GetResult();
        }

        public async Task<HttpResponseExt> PostAsync(string requestUri)
        {
            InitRequest();
            mRequest.Method = HttpMethod.Post;
            return await SendJsonAsync(requestUri);
        }

        public HttpResponseExt Post(string requestUri)
        {
            return PostAsync(requestUri).GetAwaiter().GetResult();
        }

        public async Task<HttpResponseExt> PutAsync(string requestUri)
        {
            InitRequest();
            mRequest.Method = HttpMethod.Put;
            return await SendJsonAsync(requestUri);
        }

        public HttpResponseExt Put(string requestUri)
        {
            return PutAsync(requestUri).GetAwaiter().GetResult();
        }

        public async Task<HttpResponseExt> DeleteAsync(string requestUri)
        {
            InitRequest();
            mRequest.Method = HttpMethod.Delete;
            return await SendJsonAsync(requestUri);
        }

        public HttpResponseExt Delete(string requestUri)
        {
            return DeleteAsync(requestUri).GetAwaiter().GetResult();
        }

        public async Task<HttpResponseExt> PatchAsync(string requestUri)
        {
            InitRequest();
            mRequest.Method = HttpMethod.Patch;
            return await SendJsonAsync(requestUri);
        }

        public HttpResponseExt Patch(string requestUri)
        {
            return PatchAsync(requestUri).GetAwaiter().GetResult();
        }

        public async Task<HttpResponseExt> HeadAsync(string requestUri)
        {
            InitRequest();
            mRequest.Method = HttpMethod.Head;
            return await SendJsonAsync(requestUri);
        }

        public HttpResponseExt Head(string requestUri)
        {
            return HeadAsync(requestUri).GetAwaiter().GetResult();
        }

        public async Task<HttpResponseExt> OptionsAsync(string requestUri)
        {
            InitRequest();
            mRequest.Method = HttpMethod.Options;
            return await SendJsonAsync(requestUri);
        }

        public HttpResponseExt Options(string requestUri)
        {
            return OptionsAsync(requestUri).GetAwaiter().GetResult();
        }
    }
}
