# netcoreTools
.netcore常用工具类集合




## Install

### 依赖安装
#### Package Manager
```shell
Install-Package CodeM.Common.Tools -Version 1.1.1
```

#### .NET CLI
```shell
dotnet add package CodeM.Common.Tools --version 1.1.1
```

#### PackageReference
```xml
<PackageReference Include="CodeM.Common.Tools" Version="1.1.1" />
```

#### Paket CLI
```shell
paket add CodeM.Common.Tools --version 1.1.1
```



## CryptoUtils
### 加密相关类




##### Base64编码方法
string Base64Encode(string text, string encoding = "utf-8")

参数：
text:待编码的字符串
encoding:指定字符编码格式，默认utf-8

返回：经过Base64编码后的字符串




##### Base64解码方法
string Base64Decode(string base64Text, string encoding = "utf-8")

参数：
base64Text:待解码的Base64编码的字符串
encoding:指定字符编码格式，默认utf-8

返回：经过Base64解码后的字符串




##### AES加密方法
string AESEncode(string text, string key, string encoding = "utf-8")

参数：

text:待加密的字符串

key:加密字符串

encoding:指定字符编码格式，默认utf-8

返回：加密后的字符串




##### AES解密方法
string AESDecode(string aesText, string key, string encoding = "utf-8")

参数：

aesText:待解密的AES加密字符串

key:加密字符串

encoding:指定字符编码格式，默认utf-8

返回：解密后的字符串




## HashUtils
### Hash算法类



##### MD5哈希计算
string MD5(string text, string encoding = "utf-8")

参数：

text:待哈希计算的字符串

encoding:指定字符编码格式，默认utf-8

返回：经过MD5哈希计算的字符串




##### SHA1哈希计算<br/>
string SHA1(string text, string encoding = "utf-8")

参数：

text:待哈希计算的字符串

encoding:指定字符编码格式，默认utf-8

返回：经过SHA1哈希计算的字符串




##### SHA256计算
string SHA256(string text, string encoding = "utf-8")

参数：

text:待哈希计算的字符串

encoding:指定字符编码格式，默认utf-8

返回：经过SHA256哈希计算的字符串




##### SHA384计算
string SHA384(string text, string encoding = "utf-8")

参数：

text:待哈希计算的字符串

encoding:指定字符编码格式，默认utf-8

返回：经过SHA384哈希计算的字符串




##### SHA512计算
string SHA512(string text, string encoding = "utf-8")

参数：

text:待哈希计算的字符串

encoding:指定字符编码格式，默认utf-8

返回：经过SHA512哈希计算的字符串




## XmlUtils
### Xml文件遍历类



##### 节点遍历方法
void Iterate(string file, XmlNodeInfoGetter callback = null)

参数：
file:待遍历的XML文件绝对路径
callback:回调函数，可在回调函数中进行节点信息解析和收集




##### XmlNodeInfoGetter回调方法定义
bool XmlNodeInfoGetter(XmlNodeInfo nodeInfo);

参数：
nodeInfo:当前遍历到的节点对象

返回：返回true，程序继续向下遍历；否则，停止遍历。




##### XmlNodeInfo对象
属性：
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

方法:
bool HasAttributes()

返回：返回当前节点是否包括属性信息


string GetAttribute(int index)
参数：
index:指定属性的索引位置
返回：返回指定索引位置的属性的内容

string GetAttribute(string name)
参数：
name:指定属性的名称
返回：返回指定名称的属性的内容

string GetAttribute(string localName, string namespaceURI)
参数：
localName:指定属性的本地名称
namespaceURI:指定属性的命名空间URI
返回：返回指定名称和命名空间的属性的内容



## Json2Dynamic

### Json动态对象转换器



##### 添加Json配置文件

public JsonConfigParser AddJsonFile(string path)

参数：

path:Json配置文件全路径

返回：返回Json配置文件解析器本身，可链式调用，增加多个配置文件，配置文件中有相同项，后添加的优先。



##### 将添加的Json配置文件内容解析为动态对象

public dynamic Parse(string jsonStr=null)

参数：

jsonStr:除了可以通过添加配置文件转换动态对象，还可以直接传递一个Json字符串来转换动态对象；可选。

返回：将Json配置文件的内容转换为DynamicObject对象并返回。



## RegexUtils

### 常用正则表达式匹配方法



##### 判断手机号

bool IsMobile(string value)

参数：

value:要判断的字符串内容。

返回：是有效手机号返回true，否则返回false。



##### 判断电子邮箱

bool IsEmail(string value)

参数：

value:要判断的字符串内容。

返回：是有效电子邮箱返回true，否则返回false。



##### 判断网址

bool IsUrl(string value)

参数：

value:要判断的字符串内容。

返回：是有效网址返回true，否则返回false。



##### 判断IPv4地址

bool IsIP(string value)

参数：

value:要判断的字符串内容。

返回：是有效IPv4地址返回true，否则返回false。



##### 判断数值

bool IsNumber(string value)

参数：

value:要判断的字符串内容。

返回：是有效数值返回true，否则返回false。



##### 判断整数

bool IsInteger(string value)

参数：

value:要判断的字符串内容。

返回：是有效整数返回true，否则返回false。



##### 判断正整数

bool IsPositiveInteger(string value)

参数：

value:要判断的字符串内容。

返回：是有效正整数返回true，否则返回false。



##### 判断自然数

bool IsNaturalInteger(string value)

参数：

value:要判断的字符串内容。

返回：是有效自然数返回true，否则返回false。



##### 判断小数

bool IsDecimal(string value)

参数：

value:要判断的字符串内容。

返回：是有效小数返回true，否则返回false。

