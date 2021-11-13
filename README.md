# Xmtool	.netcore常用工具集合库
收集.netcore开发过程中经常需要用到的工具类，目前收集的相关工具类涉及Json、Xml、正则表达式常用判断、加解密、Http请求、日期时间，数据类型、动态对象等，持续收集中。。。<br/>




## 依赖安装

##### Package Manager
```shell
Install-Package CodeM.Common.Tools -Version 1.2.0
```

##### .NET CLI
```shell
dotnet add package CodeM.Common.Tools --version 1.2.0
```

##### PackageReference
```xml
<PackageReference Include="CodeM.Common.Tools" Version="1.2.0" />
```

##### Paket CLI
```shell
paket add CodeM.Common.Tools --version 1.2.0
```

<br/>

<br/>

## 使用方法

Xmtool工具集合库通过Xmtool静态类统一对外提供服务，所有的详细工具类都可以通过Xmtool返回对应工具类实例，用户通过实例调用具体工具类的API。<br/>

### Xmtool  API

#### 方法：

<span style="color: #686868;"><b>1. 获取编码加解密工具类</b></span>

[CryptoTool](#crypto-tool) Crypto()<br/>

<span style="color: #686868;"><b>2. 获取日期时间工具类</b></span>

[DateTimeTool](#datetime-tool) DateTime()<br/>

<span style="color: #686868;"><b>3. 获取Hash工具类</b></span>

[HashTool](#hash-tool) Hash()<br/>

<span style="color: #686868;"><b>4. 获取正则表达式工具类</b></span>

[RegexTool](#regex-tool) Regex()<br/>

<span style="color: #686868;"><b>5. 获取类型相关工具类</b></span>

[TypeTool](#type-tool) Type()<br/>

<span style="color: #686868;"><b>6. 获取Xml工具类</b></span>

[XmlTool](#xml-tool) Xml()<br/>

#### 属性：

<span style="color: #686868;"><b>1. 返回Json工具类</b></span>

[JsonTool](#json-tool) Json<br/>

<span style="color: #686868;"><b>2. 返回Web工具类</b></span>

[WebTool](#web-tool) Web<br/>

<br/>


### <span id="crypto-tool">CryptoTool  API</span>


##### 1. 对字符串进行Base64编码并返回结果
string Base64Encode(string text, string encoding = "utf-8")

###### 参数：
text:待编码的字符串
encoding:指定字符编码格式，默认utf-8

###### 返回：

经过Base64编码后的字符串

<br/>


##### 2. 解码Base64字符串并返回结果
string Base64Decode(string base64Text, string encoding = "utf-8")

###### 参数：
base64Text:待解码的Base64编码的字符串
encoding:指定字符编码格式，默认utf-8

###### 返回：

经过Base64解码后的字符串

<br/>


##### 3. AES加密方法
string AESEncode(string text, string key, string encoding = "utf-8")

###### 参数：

text:待加密的字符串

key:加密字符串

encoding:指定字符编码格式，默认utf-8

###### 返回：

加密后的字符串

<br/>


##### 4. AES解密方法
string AESDecode(string aesText, string key, string encoding = "utf-8")

###### 参数：

aesText:待解密的AES加密字符串

key:加密字符串

encoding:指定字符编码格式，默认utf-8

###### 返回：

解密后的字符串<br/>

<br/>

### <span id="datetime-tool">DateTimeTool  API</span>


##### 1. 获取10位UTC时间戳

long GetUtcTimestamp10()

###### 参数：

无

###### 返回：

返回10位当前Utc时间戳。

<br/>

##### 2. 获取13位UTC时间戳

long GetUtcTimestamp13()

###### 参数：

无

###### 返回：

返回10位当前Utc时间戳。

<br/>

##### 3. 根据10位UTC时间戳得到本地日期时间对象

DateTime GetLocalDateTimeFromUtcTimestamp10(long ts)

###### 参数：

ts：10位UTC时间戳

###### 返回：

返回时间戳转换后的本地日期时间对象。

<br/>

##### 4. 根据13位UTC时间戳得到本地日期时间对象

DateTime GetLocalDateTimeFromUtcTimestamp13(long ts)

###### 参数：

ts：13位UTC时间戳

###### 返回：

返回时间戳转换后的本地日期时间对象。

<br/>

##### 5. 检查字符串时间范围格式是否合法

bool CheckStringTimeSpan(string timespan,  bool throwError = true)

###### 参数：

timespan：时间范围字符串，单位支持ms, s, m, h, d；如：1s，代表1秒
throwError：格式不合法时是否抛出错误，默认true，抛出

###### 返回：

合法返回true；否则，返回false。

<br/>

##### 6. 根据时间范围字符串转换为时间段对象

TimeSpan GetTimeSpanFromString(string timespan，bool throwError = true)

###### 参数：

timespan：时间范围字符串，单位支持ms, s, m, h, d；如：1s，代表1秒
throwError：格式不合法时是否抛出错误，默认true，抛出

###### 返回：

返回转换后的时间段对象。<br/>

<br/>

### <span id="hash-tool">HashTool  API</span>


##### 1. MD5哈希计算
string MD5(string text, string encoding = "utf-8")

###### 参数：

text:待哈希计算的字符串

encoding:指定字符编码格式，默认utf-8

###### 返回：

经过MD5哈希计算的字符串

<br/>


##### 2. SHA1哈希计算
string SHA1(string text, string encoding = "utf-8")

###### 参数：

text:待哈希计算的字符串

encoding:指定字符编码格式，默认utf-8

###### 返回：

经过SHA1哈希计算的字符串

<br/>


##### 3. SHA256计算
string SHA256(string text, string encoding = "utf-8")

###### 参数：

text:待哈希计算的字符串

encoding:指定字符编码格式，默认utf-8

###### 返回：

经过SHA256哈希计算的字符串

<br/>


##### 4. SHA384计算
string SHA384(string text, string encoding = "utf-8")

###### 参数：

text:待哈希计算的字符串

encoding:指定字符编码格式，默认utf-8

###### 返回：

经过SHA384哈希计算的字符串

<br/>


##### 5. SHA512计算
string SHA512(string text, string encoding = "utf-8")

###### 参数：

text:待哈希计算的字符串

encoding:指定字符编码格式，默认utf-8

###### 返回：

经过SHA512哈希计算的字符串<br/>

<br/>

### <span id="regex-tool">RegexTool  API</span>

##### 1. 判断是否手机号

bool IsMobile(string value)

###### 参数：

value:要判断的字符串内容。

###### 返回：

是有效手机号返回true，否则返回false。

<br/>

##### 2. 判断是否电子邮箱

bool IsEmail(string value)

###### 参数：

value:要判断的字符串内容。

###### 返回：

是有效电子邮箱返回true，否则返回false。

<br/>

##### 3. 判断是否网址

bool IsUrl(string value)

###### 参数：

value:要判断的字符串内容。

###### 返回：

是有效网址返回true，否则返回false。

<br/>

##### 4. 判断是否IPv4地址

bool IsIP(string value)

###### 参数：

value:要判断的字符串内容。

###### 返回：

是有效IPv4地址返回true，否则返回false。

<br/>

##### 5. 判断是否数值

bool IsNumber(string value)

###### 参数：

value:要判断的字符串内容。

###### 返回：

是有效数值返回true，否则返回false。

<br/>

##### 6. 判断是否整数

bool IsInteger(string value)

###### 参数：

value:要判断的字符串内容。

###### 返回：

是有效整数返回true，否则返回false。

<br/>

##### 7. 判断是否正整数

bool IsPositiveInteger(string value)

###### 参数：

value:要判断的字符串内容。

###### 返回：

是有效正整数返回true，否则返回false。

<br/>

##### 8. 判断是否自然数

bool IsNaturalInteger(string value)

###### 参数：

value:要判断的字符串内容。

###### 返回：

是有效自然数返回true，否则返回false。

<br/>

##### 9. 判断是否小数

bool IsDecimal(string value)

###### 参数：

value:要判断的字符串内容。

###### 返回：

是有效小数返回true，否则返回false。<br/>

<br/>

### <span id="type-tool">TypeTool  API</span>


##### 1. 判断指定对象是否为List类型

bool IsList(object obj)

###### 参数：

obj: 待判断类型的对象。

###### 返回：

是返回true；否则，返回false。<br/>

<br/>


### <span id="xml-tool">XmlTool  API</span>


##### 1. Xml节点遍历方法（从文件加载内容）
void Iterate(string file, XmlNodeInfoGetter callback = null)

###### 参数：
file:待遍历的XML文件绝对路径
callback:回调函数，可在回调函数中进行节点信息解析和收集

<br/>


##### 2. Xml节点遍历方法（从字符串加载内容）
void IterateFromString(string content, [XmlNodeInfoGetter](#xmlnodeinfogetter) callback = null)

###### 参数：
content:待遍历的XML文件内容
callback:回调函数，可在回调函数中进行节点信息解析和收集

<br/>


##### <span id="xmlnodeinfogetter">3. XmlNodeInfoGetter回调方法定义</span>
bool XmlNodeInfoGetter([XmlNodeInfo](#xmlnodeinfo) nodeInfo);

###### 参数：
nodeInfo:当前遍历到的节点对象

###### 返回：

返回true，程序继续向下遍历；否则，停止遍历。

<br/>


##### <span id="xmlnodeinfo">4. XmlNodeInfo对象</span>
##### 属性：

Path:返回当前节点路径，如：/root/person
LocalName:返回当前节点的本地名称
FullName:返回当前节点的全名称，包括命名空间和本地名称
NamespaceURI:返回当前节点的命名空间URI
IsRoot:是否根节点
IsNode:返回当前节点是否元素节点
IsEndNode:遍历过程会两次经过一个节点，一次开始，一次结束，该属性返回是否为结束一次的遍历
IsTextNode:返回当前节点是否为文本节点
IsCDATANode:返回当前节点是否为CDATA节点
Level:节点级别
Text:返回当前节点的文本内容
CData:返回CDATA节点的内容
AttributeCount:返回节点属性的数量
Line:返回当前节点在文件中的行数

##### 方法:
bool HasAttributes()

###### 返回：

返回当前节点是否包括属性信息

<br/>

string GetAttribute(int index)
###### 参数：
index:指定属性的索引位置

###### 返回：

返回指定索引位置的属性的内容

<br/>

string GetAttribute(string name)
###### 参数：
name:指定属性的名称

###### 返回：

返回指定名称的属性的内容

<br/>

string GetAttribute(string localName, string namespaceURI)

###### 参数：
localName:指定属性的本地名称
namespaceURI:指定属性的命名空间URI

###### 返回：

返回指定名称和命名空间的属性的内容

<br/>


##### 5. 反序列化动态对象的方法（从文件加载内容）
dynamic Deserialize(string file, bool includeRoot = false)

###### 参数:
file:待反序列化的XML文件绝对路径
inculdeRoot:是否包含根节点

###### 返回：

返回序列化之后的动态对象。

<br/>


##### 6. 反序列化动态对象的方法（从字符串加载内容）
dynamic DeserializeFromString(string xml, bool includeRoot = false)

###### 参数:
xml:待反序列化的XML内容
inculdeRoot:是否包含根节点

###### 返回：

返回序列化之后的动态对象。<br/>

<br/>

### <span id="json-tool">JsonTool  API</span>

##### 1. 获取Json配置文件解析器实例

[JsonConfigParser](#jsonconfigparser) ConfigParser()<br/>

<br/>

### <span id="jsonconfigparser">JsonConfigParser  API</span>

##### 1. 添加要解析的Json配置文件

JsonConfigParser AddJsonFile(string path)

###### 参数：

path:Json配置文件全路径

###### 返回：

返回Json配置文件解析器本身，可链式调用，增加多个配置文件，配置文件中有相同项，后添加的优先。

<br/>

##### 2. 将添加的Json配置文件内容解析为动态对象

dynamic Parse(string jsonStr=null)

###### 参数：

jsonStr:除了可以通过添加配置文件转换动态对象，还可以直接传递一个Json字符串来转换动态对象；可选。

###### 返回：

将Json配置文件的内容转换为[DynamicObjectExt](#dynamicobjectext)实例并返回。<br/>

<br/>

### <span id="dynamicobjectext">DynamicObjectExt 动态对象</span>

DynamicObjectExt是对DynamicObject的扩展。使用时，可以直接进行属性赋值，并可通过内置方法读、写、判断和删除等操作。

```c#
dynamic obj = new DynamicObjectExt();
obj.Name = "wangxm";
obj.Age = 18;

if (obj.Has("Age"))
{
    //TODO
}
```

<br/>

#### 方法：

##### 1. 获取对象所有属性

Dictionary<string, object>.KeyCollection Keys

<br/>

##### 2. 以索引器形式通过key获取对应属性内容

dynamic this[string key]

<br/>

##### 3. 尝试设置属性内容

bool TrySetValue(string name, object value)

###### 参数：

name: 要设置的属性名

value: 属性值

###### 返回：

设置成功返回true；否则，返回false。

<br/>

##### 4. 尝试读取属性内容

bool TryGetValue(string name, out object result)

###### 参数：

name: 要读取的属性名

result: 读取成功后数据的内容

###### 返回：

读取成功返回true；否则，返回false。

<br/>

##### 5. 根据路径设置属性内容

bool SetValueByPath(string path, object value)

###### 参数：

path: 属性完整路径，用点连接；如User.Name。

value: 属性值

###### 返回：

设置成功返回true；否则，返回false。

<br/>

##### 6. 判断属性是否存在

bool Has(string key)

###### 参数：

key: 要判断的属性

###### 返回：

属性存在返回true；否则，返回false。

<br/>

##### 7. 判断路径是否存在

bool HasPath(string path)

###### 参数：

path： 要判断的路径，多个由点分隔。

###### 返回：

路径存在返回true；否则，返回false。

<br/>

##### 8. 删除指定属性

bool Remove(string key)

###### 参数：

key: 要删除的属性

###### 返回：

删除成功返回true；否则，返回false。

<br/>

##### 9. 删除指定路径

bool RemovePath(string path)

###### 参数：

path: 要删除的路径，多个由点分隔。

###### 返回：

删除成功返回true；否则，返回false。

<br/>


##### 10. 将对象序列化为Json字符串
string ToString()

###### 参数：
无。

###### 返回：
序列化之后的Json字符串。

<br/>


##### 11. 将对象序列化为XML字符串
string ToXMLString(string defaultNS = "")

###### 参数：
defaultNS:转换时XML的默认命名空间，默认为空。

###### 返回：
序列化之后的XML字符串。<br/>

<br/>

### <span id="web-tool">WebTool  API</span>

##### 1. 获取HttpClientExt对象实例

[HttpClientExt](#httpclientext) Client(string name="default")

###### 参数：

name:实例标识，同一标识不会反复实例化对象。

###### 返回：

返回HttpClientExt对象实例。

<br/>

##### 2. 获取Web安全操作对象

[HttpSecurity](#httpsecurity) Security()

###### 参数：

无。

###### 返回：

返回HttpSecurity对象实例。<br/>

<br/>

### <span id="httpclientext">HttpClientExt  API</span>

#### 方法：

##### 1. 增加默认请求头，对后续所有请求起作用

HttpClientExt AddDefaultHeader(string name, string value)

###### 参数：

name: Header名称

value: Header值

###### 返回：

当前HttpClientExt实例，可链式调用。

<br/>

##### 2. 增加临时性请求头，仅在本次请求起作用

HttpClientExt AddRequestHeader(string name, string value)

###### 参数：

name: Header名称

value: Header值

###### 返回：

当前HttpClientExt实例，可链式调用。

<br/>

##### 3. 设置JSON格式请求体内容

HttpClientExt SetJsonContent(string content)

###### 参数：

content: 字符串格式Json内容

###### 返回：

当前HttpClientExt实例，可链式调用。

<br/>

##### 4. 设置JSON格式请求体内容

HttpClientExt SetJsonContent(dynamic content)

###### 参数：

content: 可转换成Json格式的动态对象，建议使用JsonDynamicObject对象

###### 返回：

当前HttpClientExt实例，可链式调用。

<br/>


##### 5. 提交一个Get请求，并获取请求返回结果

[HttpResponseExt](#httpresponseext) GetJsonAsync(string requestUri)

###### 参数：

requestUri：请求地址

###### 返回：

返回请求结果对象。

<br/>

##### 4. 提交一个Get请求，并获取请求返回结果

[HttpResponseExt](#httpresponseext) GetJson(string requestUri)

###### 参数：

requestUri：请求地址

###### 返回：

返回请求结果对象。

<br/>

##### <font color="red">注：</font>Post、Put、Delete、Head、Options相关请求方法请参照GetJsonAsync、GetJson。

<br/>

#### 属性：

##### 1. 获取或设置超时时间

TimeSpan Timeout

<br/>

##### 2. 获取或设置请求基本地址

Uri BaseAddress

<br/>

<br/>

### <span id="httpresponseext">HttpResponseExt  API</span>

#### 属性：

##### 1. 获取请求的状态码

HttpStatusCode StatusCode

<br/>

##### 2. 获取请求反馈的字符串内容

string Content

<br/>

##### 3. 将请求反馈的字符串按照Json格式解析，并返回解析后的动态对象（如果字符串不是Json格式将抛出异常）

dynamic Json

<br/>

##### 4. 将请求反馈的字符串按照Xml格式解析，并返回解析后的动态对象（如果字符串不是Xml格式将抛出异常）

dynamic Xml

<br/>

<br/>

### <span id="httpsecurity">HttpSecurity  API</span>

##### 1. XSS安全过滤方法

string Xss(string str)

###### 参数：

str：要进行XSS处理的字符串。

###### 返回：

按照规则处理后的字符串。

