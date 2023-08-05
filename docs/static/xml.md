# XML操作

Xml类库提供了以回调方法的方式遍历XML内容的功能，使得Xml读取更灵活快捷，且能随时停止遍历过程；同时提供了将Xml内容反序列化为对象的能力，通过该方法可以将输入参数方便的转换成对象，更易于使用和控制。

当要使用这些功能时，需先获取XmlTool类库对象：

```c#
XmlTool xml = Xmtool.Xml();
```

针对遍历和反序列化功能根据输入源不同，XmlTool中为每个功能各自提供了 2 个方法。

<b>Xml遍历方法：</b>

[Iterate](#iterate)	// 从文件加载Xml内容

[IterateFromString](#iterate-from-string)	// 从字符串加载Xml内容

<b>Xml反序列化方法：</b>

[Deserialize](#deserialize)	// 从文件加载Xml内容

[DeserializeFromString](#deserialize-from-string)	// 从字符串加载Xml内容



#### <a id="#iterate">1. Iterate</a>

##### public void Iterate(string file, <a id="xml-nodeinfo-getter">XmlNodeInfoGetter</a> callback = null)

###### 说明：从指定文件加载Xml内容并进行遍历，用户通过回调函数获取遍历信息

###### 参数：

**file**: Xml文件全路径。

**callback**：<a id="xml-nodeinfo-getter">XmlNodeInfoGetter</a>类型回调函数，用于Xml节点和属性的读取。

```xml
<!--c:\demo.xml-->
<xml>
    <item name="Karl">It's a dog.</item>
    <item name="Tom">It's a cat.</item>
</xml>
```

```c#
private string mTomContent = "";

private bool XmlReader(XmlNodeInfo node)
{
    if (!node.IsEndNode)
    {
        if (node.Path == "/xml/item")	// 当前节点是否/xml/item节点
        {
            if (node.GetAttribute("name") == "Tom")	// 当前节点name属性是否为Tom
            {
                mTomContent = node.Text;	// 获取Tom的说明文本
                return false;	// 退出遍历
            }
        }
    }
    return true;
}

public void GetTomContent(string file)
{
	XmlTool xml = Xmtool.Xml();
    xml.Iterate(file, XmlReader)
}

ReadXml("c:\demo.xml");
Console.WriteLine(mTomContent)
```

#### <a id="#iterate-from-string">2. IterateFromString</a>

##### public void Iterate(string content, <a id="xml-nodeinfo-getter">XmlNodeInfoGetter</a> callback = null)

###### 说明：从指定字符串加载Xml内容并进行遍历，用户通过回调函数获取遍历信息

###### 参数：

**content**: Xml格式的字符串内容。

**callback**：<a id="xml-nodeinfo-getter">XmlNodeInfoGetter</a>类型回调函数，用于Xml节点和属性的读取。

```c#
string xmlData = @"<xml>
                	<name>张三</name>
                	<age>18</age>
                	<gender>男</gender>
            	</xml>";

XmlTool xml = Xmtool.Xml();
xml.IterateFromString(xmlData, (XmlNodeInfo node) => 
{
	if (!node.IsEndNode)
    {
        if (node.Path = "/xml/age")
        {
            Console.WriteLine("张三的年龄为：" + node.Text);
            return false;
        }
    }
    return true;
})
```

#### <a id="#deserialize">3. Deserialize</a>

##### public dynamic Deserialize(string file, bool includeRoot = false)

###### 说明：从指定文件加载Xml内容并进行解析，将解析内容反序列化为动态对象；暂不支持包含同层级同名称节点的Xml内容反序列化。

###### 参数：

**file**: Xml文件全路径。

**includeRoot**：反序列化时是否包含根节点。

```xml
<!--c:\demo.xml-->
<person>
    <name>张三</name>
    <age>18</age>
</person>
```

```c#
XmlTool xml = Xmtool.Xml();
dynamic person = xml.Deserialize("c:\demo.xml", false);
Console.WriteLine("张三的年龄为：" + person.name.Value);
```

#### <a id="#deserialize">4. DeserializeFromString</a>

##### public dynamic DeserializeFromString(string xml, bool includeRoot = false)

###### 说明：从字符串内容加载Xml并进行解析，将解析内容反序列化为动态对象；暂不支持包含同层级同名称节点的Xml内容反序列化。

###### 参数：

**content**: Xml格式的字符串内容。

**includeRoot**：反序列化时是否包含根节点。

```c#
string xmlData = @"<xml>
					<person age="18" gender="男">张三</person>
            	</xml>";

XmlTool xml = Xmtool.Xml();
dynamic person = xml.DeserializeFromString(xmlData);
Console.WriteLine("张三的年龄为：" + person.person.age);
```





## 附件说明

#### <a id="#deserialize-from-string">4. DeserializeFromString</a>



## 附加说明

#### [XmlNodeInfoGetter回调函数](#xml-nodeinfo-getter)

##### public delegate bool XmlNodeInfoGetter(XmlNodeInfo nodeInfo);

#### <a id="xml-nodeinfo-getter">XmlNodeInfoGetter回调函数</a>

##### public delegate bool XmlNodeInfoGetter(XmlNodeInfo nodeInfo);

###### 说明：Xml遍历回调函数；Xml遍历到每个节点就会回调该方法一次，参数是遍历到的当前节点。

###### 参数：

**nodeInfo**：当前节点信息。

<table cellborder="0" cellspacing="0" style="width: 100%;">
	<tr>
		<td><b>名称</b></td>
		<td width="80"><b>类型</b></td>
		<td><b>说明</b></td>
	</tr>
    <tr>
        <td>Path</td>
        <td>属性</td>
        <td>当前节点路径，同名路劲不区分；如：/xml/item</td>
    </tr>
    <tr>
        <td>FullPath</td>
        <td>属性</td>
        <td>当前节点路径，区分同名路径；如：/xml/item[1]</td>
    </tr>
    <tr>
        <td>LocalName</td>
        <td>属性</td>
        <td>节点去掉前缀的名称；如：\<w:item name="softwaiter"/>，该节点LocalName为item。</td>
    </tr>
    <tr>
        <td>FullName</td>
        <td>属性</td>
        <td>节点全名称；如：\<w:item name="softwaiter"/>，该节点FullName为w:item。</td>
    </tr>
    <tr>
        <td>NamespaceURI</td>
        <td>属性</td>
        <td>当前节点名称前缀对应的命名空间地址；没有前缀为空。</td>
    </tr>
    <tr>
        <td>IsRoot</td>
        <td>属性</td>
        <td>当前节点是否根节点。</td>
    </tr>
    <tr>
        <td>IsNode</td>
        <td>属性</td>
        <td>当前是否节点，起始节点或结束节点。</td>
    </tr>
    <tr>
        <td>IsEndNode</td>
        <td>属性</td>
        <td>当前节点是否为结束节点；如：\</item></td>
    </tr>
    <tr>
        <td>IsEmptyNode</td>
        <td>属性</td>
        <td>当前节点是否一个空节点；如：\<item/></td>
    </tr>
    <tr>
        <td>IsTextNode</td>
        <td>属性</td>
        <td>当前节点是否一个字符内容的节点。</td>
    </tr>
    <tr>
        <td>IsCDATANode</td>
        <td>属性</td>
        <td>当前节点是否一个CDATA节点；如：\<item><![CDATA[hello]]</item>Text</td>
    </tr>
	<tr>
		<td>Text</td>
		<td>属性</td>
		<td>当前节点的的字符串内容；需IsTextNode为true时有效。</td>
	</tr>
	<tr>
		<td>CData</td>
		<td>属性</td>
		<td>当前节点的CDATA内容；需IsCDATANode为true时有效。</td>
	</tr>
	<tr>
		<td>Level</td>
		<td>属性</td>
		<td>当前节点的深度层级。</td>
	</tr>
	<tr>
		<td>Line</td>
		<td>属性</td>
		<td>当前节点在文件中的所在行数。</td>
	</tr>
	<tr>
		<td>AttributeCount</td>
		<td>属性</td>
		<td>当前节点包含属性的个数。</td>
	</tr>
	<tr>
		<td>HasAttributes()</td>
		<td>方法</td>
		<td>当前节点是否包含属性。</td>
	</tr>
	<tr>
		<td>GetAttribute(int index)</td>
		<td>方法</td>
		<td>返回属性位置为index的属性值。</td>
	</tr>
	<tr>
		<td>GetAttribute(string name)</td>
		<td>方法</td>
		<td>返回属性名称为name的属性值。</td>
	</tr>
	<tr>
		<td>GetAttribute(string name, string namesapceURI)</td>
		<td>方法</td>
		<td>返回属性名为name，且name命名空间为namespaceURI的属性值。</td>
	</tr>
	<tr>
		<td>GetAttributeName(int index)</td>
		<td>方法</td>
		<td>返回属性位置为index的属性名称。</td>
	</tr>
</table>