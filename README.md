# netcoreTools
.netcore常用工具类集合<br/><br/><br/>

## CryptoUtils
---
### 加密相关类<br/><br/>


##### Base64编码方法<br/>
string Base64Encode(string text, string encoding = "utf-8")<br/>

参数： <br/>
text:待编码的字符串<br/>
encoding:指定字符编码格式，默认utf-8<br/>

返回：经过Base64编码后的字符串<br/><br/><br/>


##### Base64解码方法<br/>
string Base64Decode(string base64Text, string encoding = "utf-8")<br/>

参数：<br/>
base64Text:待解码的Base64编码的字符串<br/>
encoding:指定字符编码格式，默认utf-8<br/>

返回：经过Base64解码后的字符串<br/><br/><br/>


##### AES加密方法<br/>
string AESEncode(string text, string key, string encoding = "utf-8")<br/>

参数：<br/>
text:待加密的字符串<br/>
key:加密字符串<br/>
encoding:指定字符编码格式，默认utf-8<br/>

返回：加密后的字符串<br/><br/><br/>


##### AES解密方法<br/>
string AESDecode(string aesText, string key, string encoding = "utf-8")<br/>

参数：<br/>
aesText:待解密的AES加密字符串<br/>
key:加密字符串<br/>
encoding:指定字符编码格式，默认utf-8<br/>

返回：解密后的字符串<br/><br/><br/>


## HashUtils
---
### 重用Hash算法类<br/><br/>

##### MD5哈希计算<br/>
string MD5(string text, string encoding = "utf-8")<br/>

参数：<br/>
text:待哈希计算的字符串<br/>
encoding:指定字符编码格式，默认utf-8<br/>

返回：经过MD5哈希计算的字符串<br/><br/><br/>


##### SHA1哈希计算<br/>
string SHA1(string text, string encoding = "utf-8")<br/>

参数：<br/>
text:待哈希计算的字符串<br/>
encoding:指定字符编码格式，默认utf-8<br/>

返回：经过SHA1哈希计算的字符串<br/><br/><br/>


##### SHA256计算<br/>
string SHA256(string text, string encoding = "utf-8")<br/>

参数：<br/>
text:待哈希计算的字符串<br/>
encoding:指定字符编码格式，默认utf-8<br/>

返回：经过SHA256哈希计算的字符串<br/><br/><br/>


##### SHA384计算<br/>
string SHA384(string text, string encoding = "utf-8")<br/>

参数：<br/>
text:待哈希计算的字符串<br/>
encoding:指定字符编码格式，默认utf-8<br/>

返回：经过SHA384哈希计算的字符串<br/><br/><br/>


##### SHA512计算<br/>
string SHA512(string text, string encoding = "utf-8")<br/>

参数：<br/>
text:待哈希计算的字符串<br/>
encoding:指定字符编码格式，默认utf-8<br/>

返回：经过SHA512哈希计算的字符串<br/><br/><br/>


## XmlUtils
---
### Xml文件遍历类<br/><br/>

##### 节点遍历方法<br/>
void Iterate(string file, XmlNodeInfoGetter callback = null)<br/>

参数：<br/>
file:待遍历的XML文件绝对路径<br/>
callback:回调函数，可在回调函数中进行节点信息解析和收集<br/><br/><br/>


##### XmlNodeInfoGetter回调方法定义<br/>
bool XmlNodeInfoGetter(XmlNodeInfo nodeInfo);<br/>

参数：<br/>
nodeInfo:当前遍历到的节点对象<br/>

返回：返回true，程序继续向下遍历；否则，停止遍历。<br/><br/><br/>


##### XmlNodeInfo对象<br/>
属性：<br/>
Path:返回当前节点路径，如：/root/person<br/>
LocalName:返回当前节点的本地名称<br/>
FullName:返回当前节点的全名称，包括命名空间和本地名称<br/>
NamespaceURI:返回当前节点的命名空间URI<br/>
IsNode:返回当前节点是否元素节点<br/>
IsEndNode:遍历过程会两次经过一个节点，一次开始，一次结束，该属性返回是否为结束一次的遍历<br/>
IsTextNode:返回当前节点是否为文本节点<br/>
IsCDATANode:返回当前节点是否为CDATA节点<br/>
Text:返回当前节点的文本内容<br/>
CData:返回CDATA节点的内容<br/>
AttributeCount:返回节点属性的数量<br/>
Line:返回当前节点在文件中的行数<br/><br/>

方法:<br/>
bool HasAttributes()<br/>

返回：返回当前节点是否包括属性信息<br/><br/>


string GetAttribute(int index)<br/>
参数：<br/>
index:指定属性的索引位置<br/>
返回：返回指定索引位置的属性的内容<br/><br/>

string GetAttribute(string name)<br/>
参数：<br/>
name:指定属性的名称<br/>
返回：返回指定名称的属性的内容<br/><br/>

string GetAttribute(string localName, string namespaceURI)<br/>
参数：<br/>
localName:指定属性的本地名称<br/>
namespaceURI:指定属性的命名空间URI<br/>
返回：返回指定名称和命名空间的属性的内容<br/><br/>
