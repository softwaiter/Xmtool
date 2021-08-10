# netcoreTools
.netcore常用工具类集合




## Install

### 依赖安装
#### Package Manager
```shell
Install-Package CodeM.Common.Tools -Version 1.1.9
```

#### .NET CLI
```shell
dotnet add package CodeM.Common.Tools --version 1.1.9
```

#### PackageReference
```xml
<PackageReference Include="CodeM.Common.Tools" Version="1.1.9" />
```

#### Paket CLI
```shell
paket add CodeM.Common.Tools --version 1.1.9
```



## CryptoUtils
### 加密相关类




##### Base64编码方法
string Base64Encode(string text, string encoding = "utf-8")

###### 参数：
text:待编码的字符串
encoding:指定字符编码格式，默认utf-8

###### 返回：

经过Base64编码后的字符串




##### Base64解码方法
string Base64Decode(string base64Text, string encoding = "utf-8")

###### 参数：
base64Text:待解码的Base64编码的字符串
encoding:指定字符编码格式，默认utf-8

###### 返回：

经过Base64解码后的字符串




##### AES加密方法
string AESEncode(string text, string key, string encoding = "utf-8")

###### 参数：

text:待加密的字符串

key:加密字符串

encoding:指定字符编码格式，默认utf-8

###### 返回：

加密后的字符串




##### AES解密方法
string AESDecode(string aesText, string key, string encoding = "utf-8")

###### 参数：

aesText:待解密的AES加密字符串

key:加密字符串

encoding:指定字符编码格式，默认utf-8

###### 返回：

解密后的字符串




## HashUtils
### Hash算法类



##### MD5哈希计算
string MD5(string text, string encoding = "utf-8")

###### 参数：

text:待哈希计算的字符串

encoding:指定字符编码格式，默认utf-8

###### 返回：

经过MD5哈希计算的字符串




##### SHA1哈希计算<br/>
string SHA1(string text, string encoding = "utf-8")

###### 参数：

text:待哈希计算的字符串

encoding:指定字符编码格式，默认utf-8

###### 返回：

经过SHA1哈希计算的字符串




##### SHA256计算
string SHA256(string text, string encoding = "utf-8")

###### 参数：

text:待哈希计算的字符串

encoding:指定字符编码格式，默认utf-8

###### 返回：

经过SHA256哈希计算的字符串




##### SHA384计算
string SHA384(string text, string encoding = "utf-8")

###### 参数：

text:待哈希计算的字符串

encoding:指定字符编码格式，默认utf-8

###### 返回：

经过SHA384哈希计算的字符串




##### SHA512计算
string SHA512(string text, string encoding = "utf-8")

###### 参数：

text:待哈希计算的字符串

encoding:指定字符编码格式，默认utf-8

###### 返回：

经过SHA512哈希计算的字符串




## XmlUtils
### Xml文件遍历类



##### 节点遍历方法
void Iterate(string file, XmlNodeInfoGetter callback = null)

###### 参数：
file:待遍历的XML文件绝对路径
callback:回调函数，可在回调函数中进行节点信息解析和收集




##### XmlNodeInfoGetter回调方法定义
bool XmlNodeInfoGetter(XmlNodeInfo nodeInfo);

###### 参数：
nodeInfo:当前遍历到的节点对象

###### 返回：

返回true，程序继续向下遍历；否则，停止遍历。




##### XmlNodeInfo对象
##### 属性：

Path:返回当前节点路径，如：/root/person
LocalName:返回当前节点的本地名称
FullName:返回当前节点的全名称，包括命名空间和本地名称
NamespaceURI:返回当前节点的命名空间URI
IsNode:返回当前节点是否元素节点
IsEndNode:遍历过程会两次经过一个节点，一次开始，一次结束，该属性返回是否为结束一次的遍历
IsTextNode:返回当前节点是否为文本节点
IsCDATANode:返回当前节点是否为CDATA节点
Text:返回当前节点的文本内容
CData:返回CDATA节点的内容
AttributeCount:返回节点属性的数量
Line:返回当前节点在文件中的行数

##### 方法:
bool HasAttributes()

###### 返回：

返回当前节点是否包括属性信息



string GetAttribute(int index)
###### 参数：
index:指定属性的索引位置

###### 返回：

返回指定索引位置的属性的内容



string GetAttribute(string name)
###### 参数：
name:指定属性的名称

###### 返回：

返回指定名称的属性的内容



string GetAttribute(string localName, string namespaceURI)

###### 参数：
localName:指定属性的本地名称
namespaceURI:指定属性的命名空间URI

###### 返回：

返回指定名称和命名空间的属性的内容



## Json2Dynamic

### Json动态对象转换器



##### 添加Json配置文件

public JsonConfigParser AddJsonFile(string path)

###### 参数：

path:Json配置文件全路径

###### 返回：

返回Json配置文件解析器本身，可链式调用，增加多个配置文件，配置文件中有相同项，后添加的优先。



##### 将添加的Json配置文件内容解析为动态对象

public dynamic Parse(string jsonStr=null)

###### 参数：

jsonStr:除了可以通过添加配置文件转换动态对象，还可以直接传递一个Json字符串来转换动态对象；可选。

###### 返回：

将Json配置文件的内容转换为DynamicObject实例并返回。



##### 创建一个Json动态对象

public dynamic CreateObject()

###### 参数：

无

###### 返回：

返回创建好的JsonDynamicObject实例。



## JsonDynamicObject

### Json动态对象



##### 尝试设置属性内容

public bool TrySetValue(string name, object value)

###### 参数：

name: 要设置的属性名

value: 属性值

###### 返回：

设置成功返回true；否则，返回false。



##### 尝试读取属性内容

public bool TryGetValue(string name, out object result)

###### 参数：

name: 要读取的属性名

result: 读取成功后数据的内容

###### 返回：

读取成功返回true；否则，返回false。



##### 根据路径设置属性内容

public bool SetValueByPath(string path, object value)

###### 参数：

path: 属性完整路径，用点连接；如User.Name。

value: 属性值

###### 返回：

设置成功返回true；否则，返回false。



##### 判断属性是否存在

public bool Has(string key)

###### 参数：

key: 要判断的属性

###### 返回：

属性存在返回true；否则，返回false。



##### 判断路径是否存在

public bool HasPath(string path)

###### 参数：

path： 要判断的路径，多个由点分隔。

###### 返回：

路径存在返回true；否则，返回false。



##### 删除指定属性

public bool Remove(string key)

###### 参数：

key: 要删除的属性

###### 返回：

删除成功返回true；否则，返回false。



##### 删除指定路径

public bool RemovePath(string path)

###### 参数：

path: 要删除的路径，多个由点分隔。

###### 返回：

删除成功返回true；否则，返回false。



## RegexUtils

### 常用正则表达式匹配方法



##### 判断手机号

bool IsMobile(string value)

###### 参数：

value:要判断的字符串内容。

###### 返回：

是有效手机号返回true，否则返回false。



##### 判断电子邮箱

bool IsEmail(string value)

###### 参数：

value:要判断的字符串内容。

###### 返回：

是有效电子邮箱返回true，否则返回false。



##### 判断网址

bool IsUrl(string value)

###### 参数：

value:要判断的字符串内容。

###### 返回：

是有效网址返回true，否则返回false。



##### 判断IPv4地址

bool IsIP(string value)

###### 参数：

value:要判断的字符串内容。

###### 返回：

是有效IPv4地址返回true，否则返回false。



##### 判断数值

bool IsNumber(string value)

###### 参数：

value:要判断的字符串内容。

###### 返回：

是有效数值返回true，否则返回false。



##### 判断整数

bool IsInteger(string value)

###### 参数：

value:要判断的字符串内容。

###### 返回：

是有效整数返回true，否则返回false。



##### 判断正整数

bool IsPositiveInteger(string value)

###### 参数：

value:要判断的字符串内容。

###### 返回：

是有效正整数返回true，否则返回false。



##### 判断自然数

bool IsNaturalInteger(string value)

###### 参数：

value:要判断的字符串内容。

###### 返回：

是有效自然数返回true，否则返回false。



##### 判断小数

bool IsDecimal(string value)

###### 参数：

value:要判断的字符串内容。

###### 返回：

是有效小数返回true，否则返回false。



## WebUtils

### Web常用操作库



##### 获取HttpClientExt对象实例

HttpClientExt Client(string name="default")

###### 参数：

name:实例标识，同一标识不会反复实例化对象。

###### 返回：

返回HttpClientExt对象实例。



## HttpClientExt

### HttpClientExt请求对象



##### 获取或设置超时时间

TimeSpan Timeout



##### 获取或设置请求基本地址

Uri BaseAddress



##### 增加默认请求头，对后续所有请求起作用

HttpClientExt AddDefaultHeader(string name, string value)

###### 参数：

name: Header名称

value: Header值

###### 返回：

当前HttpClientExt实例，可链式调用。




##### 增加临时性请求头，仅在本次请求起作用
HttpClientExt AddRequestHeader(string name, string value)

###### 参数：

name: Header名称

value: Header值

###### 返回：

当前HttpClientExt实例，可链式调用。



##### 设置JSON格式请求体内容
HttpClientExt SetJsonContent(string content)

###### 参数：
content: 字符串格式Json内容

###### 返回：
当前HttpClientExt实例，可链式调用。



##### 设置JSON格式请求体内容
HttpClientExt SetJsonContent(dynamic content)
###### 参数：
content: 可转换成Json格式的动态对象，建议使用JsonDynamicObject对象

###### 返回：
当前HttpClientExt实例，可链式调用。




##### 提交一个Get请求，并获取Json格式返回结果
dynamic GetJsonAsync(string requestUri)
###### 参数：
requestUri：请求地址
###### 返回：
返回结果转换成的动态对象，JsonDynamicObject类型；结果为非法Json格式，返回null



##### 提交一个Get请求，并获取Json格式返回结果
dynamic GetJson(string requestUri)
###### 参数：
requestUri：请求地址
###### 返回：
返回结果转换成的动态对象，JsonDynamicObject类型；结果为非法Json格式，返回null



##### <font color="red">注：</font>Post、Put、Delete、Head、Options相关请求方法请参照GetJsonAsync、GetJson。



## DateTimeUtils

### 日期时间常用方法库



##### 获取10位UTC时间戳

long GetUtcTimestamp10()

###### 参数：

无

###### 返回：

返回10位当前Utc时间戳。



##### 获取13位UTC时间戳

long GetUtcTimestamp13()

###### 参数：

无

###### 返回：

返回10位当前Utc时间戳。



##### 根据10位UTC时间戳得到本地日期时间对象

DateTime GetLocalDateTimeFromUtcTimestamp10(long ts)

###### 参数：

ts：10位UTC时间戳

###### 返回：

返回时间戳转换后的本地日期时间对象。



##### 根据13位UTC时间戳得到本地日期时间对象

DateTime GetLocalDateTimeFromUtcTimestamp13(long ts)

###### 参数：

ts：13位UTC时间戳

###### 返回：

返回时间戳转换后的本地日期时间对象。



##### 根据时间范围字符串转换为时间段对象
TimeSpan GetTimeSpanFromString(string timespan)
###### 参数：
timespan：时间范围字符串，单位支持ms, s, m, h, d；如：1s，代表1秒
###### 返回：
返回转换后的时间段对象。