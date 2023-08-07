# Web操作类库

Web类库提供了HTTP请求和HTTP安全相关两方面的封装。HTTP请求是基于系统自带的HttpClient对象进行了二次封装，提供了更易用的方法，并且对Json类型更加友好；HTTP安全方面主要针对XSS攻击提供了字符处理方法，后续会根据需要不断添加。

## HTTP请求

使用HTTP请求，首选需要获取HttpClientExt对象：

```c#
HttpClientExt client = Xmtool.Web.Client();
// 或者
HttpClientExt client = Xmtool.Web.Client("microservice-1");
```

根据获取Client时传入的参数不同，系统将返回不同的实例；但对相同的参数会做单例处理，也就是说相同的参数将获取到同一个实例；当不传入任何参数时，每次都会返回一个全新的实例。



根据HTTP常用请求场景类库提供了各种属性和方法：

[Timeout属性](#timeout)

[BaseAddress属性](#baseaddress)

[AddDefaultHeader方法](#adddefaultheader)

[AddRequestHeader方法](#addrequestheader)

[AddRequestHeaderWithoutValidation方法](addrequestheaderwithoutvalidation)

[SetContent方法](#setcontent)

[SetJsonContent方法](#setjsoncontent)

[SetJsonContent方法二](#setjsoncontent2)

[Clear方法](#clear)

[Get方法](#get)

[GetAsync方法](#getasync)

[Post方法](#post)

[PostAsync方法](#postasync)

[Put方法](#put)

[PutAsync方法](#putasync)

[Delete方法](#delete)

[DeleteAsync方法](deleteasync)

[Patch方法](#patch)

[PatchAsync方法](#patchasync)

[Head方法](#head)

[HeadAsync方法](#headasync)

[Options方法](#options)

[OptionsAsync方法](#optionsasync)



#### <a id="timeout">1. Timeout属性</a>

###### 说明：用来设置或获取HTTP请求的超时时间。



#### <a id="baseaddress">2. BaseAddress属性</a>

###### 说明: 用来设置或获取HTTP请求的基本地址，在后续使用相对地址发送请求时会使用该地址进行拼接。



#### <a id="adddefaultheader">3. AddDefaultHeader方法</a>

##### public HttpClientExt AddDefaultHeader(string name, string value)

###### 说明：为HTTP请求设置请求头信息，该头信息会在实例每次发起请求时携带。

###### 参数：

**name**：请求头名称。

**value**：请求头内容。

```c#
HttpClientExt client = Xmtool.Web.Client();
client.AddDefaultHeader("User", "admin");
```



#### <a id="addrequestheader">4. AddRequestHeader方法</a>

##### public HttpClientExt AddRequestHeader(string name, string value)

###### 说明：为HTTP请求设置请求头信息，该头信息仅会在实例下一次发起请求时携带。

###### 参数：

**name**：请求头名称。

**value**：请求头内容。



####  <a id="addrequestheaderwithoutvalidation">5. AddRequestHeaderWithoutValidation方法</a>

##### public HttpClientExt AddRequestHeaderWithoutValidation(string name, string value)

###### 说明：为HTTP请求设置请求头信息，该头信息仅会在实例下一次发起请求时携带；该方法和AddRequestHeader的区别是不会对参数的合法性进行检查。

###### 参数：

**name**：请求头名称。

**value**：请求头内容。



#### <a id="setcontent">6. SetContent方法</a>

##### public HttpClientExt SetContent(string content)

###### 说明：为下一次HTTP请求（主要是提交类请求，如Post）设置提交内容。

###### 参数：

**content**：请求提交内容。

```c#
HttpClientExt client = Xmtool.Web.Client();
client.SetContnent("Good Day");
// TODO
```



#### <a id="setjsoncontent">7. SetJsonContent方法</a>

##### public HttpClientExt SetJsonContent(string content)

###### 说明：为下一次请求设置提交内容，必须是JSON格式。

###### 参数：

**content**：请求提交内容，JSON格式。

```c#
HttpClientExt client = Xmtool.Web.Client();
client.SetJsonContnent("{\"Author\": \"softwaiter\"}");
// TODO
```



#### <a id="setjsoncontent2">8. SetJsonContent方法二</a>

##### public HttpClientExt SetJsonContent([DynamicObjectExt](dynamicobjectext) obj)

###### 说明：通过动态对象为下一次请求设置JSON格式的提交内容。

###### 参数：

**obj**：要提交的动态对象。

```c#
DynamicObjectExt obj = new DynamicObjectExt();
obj.Author = "softwaier";

HttpClientExt client = Xmtool.Web.Client();
client.SetJsonContnent(obj);
// TODO
```



#### <a id="clear">9. Clear方法</a>

##### public HttpClientExt Clear()

###### 说明：清除仅在下一次请求中生效的提交内容和请求头信息。



#### <a id="get">10. Get方法</a>

##### public [HttpResponseExt](#httpresponseext) Get(string requestUri)

###### 说明：以Get方式发起一个HTTP请求。

###### 参数：

**requestUri**：HTTP请求地址，可以是相对地址或绝对地址；如果是相对地址将使用BaseAddress进行拼接。

```c#
HttpClientExt client = Xmtool.Web.Client();
HttpResponseExt rep = client.Get("https://www.baidu.com");
// TODO
```



#### <a id="getasync">11. GetAsync方法</a>

##### public async Task<[HttpResponseExt](#httpresponseext)> GetAsync(string requestUri)

###### 说明：Get方法的异步版本。



#### <a id="post">12. Post方法</a>

##### public [HttpResponseExt](#httpresponseext) Post(string requestUri)

###### 说明：以Post方式发起一个HTTP请求。

###### 参数：

**requestUri**：HTTP请求地址，可以是相对地址或绝对地址；如果是相对地址将使用BaseAddress进行拼接。

```c#
// 新增一条人员信息
HttpClientExt client = Xmtool.Web.Client();
HttpResponseExt rep = client.SetJsonContent("{\"user\": \"admin", \"password\": \"Admin123\"}")
    .Post("https://www.ceshi.com/person");
// TODO
```



#### <a id="postasync">13. PostAsync方法</a>

##### public async Task<[HttpResponseExt](#httpresponseext)> PostAsync(string requestUri)

###### 说明：Post方法的异步版本。



#### <a id="put">14. Put方法</a>

##### public [HttpResponseExt](#httpresponseext) Put(string requestUri)

###### 说明：以Put方式发起一个HTTP请求。

###### 参数：

**requestUri**：HTTP请求地址，可以是相对地址或绝对地址；如果是相对地址将使用BaseAddress进行拼接。

```c#
// 修改Id为1的人员住址
HttpClientExt client = Xmtool.Web.Client();
HttpResponseExt rep = client.SetJsonContent("{\"address\": \"BeiJing\"}")
    .Put("https://www.ceshi.com/person/1");
// TODO
```



#### <a id="putasync">15. PutAsync方法</a>

##### public async Task<[HttpResponseExt](#httpresponseext)> PutAsync(string requestUri)

###### 说明：Put方法的异步版本。



#### <a id="delete">16. Delete方法</a>

##### public [HttpResponseExt](#httpresponseext) Delete(string requestUri)

###### 说明：以Delete方式发起一个HTTP请求。

###### 参数：

**requestUri**：HTTP请求地址，可以是相对地址或绝对地址；如果是相对地址将使用BaseAddress进行拼接。

```c#
// 删除Id为1的人员信息
HttpClientExt client = Xmtool.Web.Client();
HttpResponseExt rep = client.Delete("https://www.ceshi.com/person/1");
// TODO
```



#### <a id="deleteasync">17. DeleteAsync方法</a>

##### public async Task<[HttpResponseExt](#httpresponseext)> DeleteAsync(string requestUri)

###### 说明：Delete方法的异步版本。



#### <a id="patch">18. Patch方法</a>

##### public [HttpResponseExt](#httpresponseext) Patch(string requestUri)

###### 说明：以Patch方式发起一个HTTP请求。

###### 参数：

**requestUri**：HTTP请求地址，可以是相对地址或绝对地址；如果是相对地址将使用BaseAddress进行拼接。

```c#
// 修改Id为1的人员信息
HttpClientExt client = Xmtool.Web.Client();
HttpResponseExt rep = client.SetJsonContent("{\"name\":\"wangxm\", \"age\": 18}")
    .Delete("https://www.ceshi.com/person/1");
// TODO
```



#### <a id="patchasync">19. PatchAsync方法</a>

##### public async Task<[HttpResponseExt](#httpresponseext)> PatchAsync(string requestUri)

###### 说明：Patch方法的异步版本。



#### <a id="head">20. Head方法</a>

##### public [HttpResponseExt](#httpresponseext) Head(string requestUri)

###### 说明：以Head方式发起一个HTTP请求。

###### 参数：

**requestUri**：HTTP请求地址，可以是相对地址或绝对地址；如果是相对地址将使用BaseAddress进行拼接。



#### <a id="headasync">21. HeadAsync方法</a>

##### public async Task<[HttpResponseExt](#httpresponseext)> HeadAsync(string requestUri)

###### 说明：Head方法的异步版本。



#### <a id="options">22. Options方法</a>

##### public [HttpResponseExt](#httpresponseext) Options(string requestUri)

###### 说明：以Options方式发起一个HTTP请求。

###### 参数：

**requestUri**：HTTP请求地址，可以是相对地址或绝对地址；如果是相对地址将使用BaseAddress进行拼接。



#### <a id="optionsasync">23. OptionsAsync方法</a>

##### public async Task<[HttpResponseExt](#httpresponseext)> OptionsAsync(string requestUri)

###### 说明：Options方法的异步版本。



## HTTP安全

HTTP安全在类库中目前仅提供了一个应对XSS攻击的处理方法，该方法会对传入参数进行二次，将内容中有风险部分进行处理并返回。

使用时需要首先获取HttpSecurity对象：

```c#
HttpSecurity security = Xmtool.Web.Security();
```

[Xss方法](#xss)



#### <a id="xss">1. Xss方法</a>

##### public string Xss(string str)

###### 说明：对指定字符内容进行Xss风险处理并返回处理结果。

###### 参数：

**str**：需要进行处理的字符串内容。

```c#
string str = "<script>alert(123);</script><div>hello world.</div>";
string str2 = Xmtool.Web.Security().Xss(str);
Console.Writeline(str2);	// 输出<div>hello world.</div>
//TODO 
```



## 附加说明

### <a id="dynamicobjectext">一、DynamicObjectExt对象</a>

###### 说明：DynamicObjectExt动态对象是Xmtool工具库中最具特色的一项功能；通过该对象可以无需事先定义就能像使用实体类一样进行数据对象的赋值和操作；且比系统自带的dynamic对象更加灵活友好。如果你熟悉Javascript的话，它就像Javascript中的变量一样可以任意使用；更加详细的说明请参考[《DynamicObjectExt详细说明》](./?item=0212)。



### <a id="httpresponseext">二、HttpResponseExt对象</a>

HttpResponseExt对象用于接收HTTP请求返回结果，封装了响应码、响应头、响应内容 3 部分内容。

[StatusCode属性](#statuscode)

[Headers属性](#headers)

[Content属性](#content)

[Json属性](#json)

[Xml属性](#xml)



#### <a id="statuscode">1. StatusCode属性</a>

###### 说明：HTTP请求的响应状态码。



#### <a id="headers">2. Headers属性</a>

###### 说明：HTTP请求的响应头信息。

```c#
HttpClientExt client = Xmtool.Web.Client();
HttpResponseExt resp = client.Get("https://www.baidu.com");
if (resp.Headers.ContainsKey("user"))
{
    string user = resp.Headers["user"];
    // TODO
}
```



#### <a id="content">3. Content属性</a>

###### 说明：HTTP请求的响应内容。



#### <a id="Json">4. Json属性</a>

###### 说明：HTTP请求返回的JSON格式的内容；如果返回内容不是合法的JSON格式，将返回异常。

```json
// https://www.ceshi.com/person/1 请求返回内容
{
    "Name": "softwaiter",
    "Age": 18,
    "Address": "BeiJing"
}
```

```c#
HttpClientExt client = Xmtool.Web.Client();
HttpResponseExt resp = client.Get("https://www.ceshi.com/person/1");
if (resp.Json.Name == "softwaiter")
{
    // TODO
}
```



#### <a id="xml">5. Xml属性</a>

###### 说明：HTTP请求返回的XML格式的内容；如果返回内容不是合法的XML格式，将返回异常。

