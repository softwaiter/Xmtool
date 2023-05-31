# 类型判断

根据实际应用中的功能需求，封装了TypeTool类库，其中包含了对类型的判断、克隆等操作；后续会根据使用过程中的需求随时添加。

想使用类型相关方法时，我们首先需要通过如下方法取得TypeTool对象实例。

```c#
TypeTool tt = Xmtool.Type();
// TODO
```

有了TypeTool实例之后，就可以调用实例中各种方法了；具体方法如下：

[判断是否简单类型](#is-simple)

[判断是否List类型](#is-list)

[判断对象是否可复制](#is-cloneable)

[克隆列表对象](#clone-list)

<br/>

<br/>

#### <a id="is-simple">1. 判断是否简单类型</a>

##### public bool IsSimpleType(object obj)

###### 说明：判断指定对象内容是否为简单类型（值类型或字符串类型）。

```c#
String str = "Hello World";

TypeTool tt = Xmtool.Type();
if (tt.IsSimpleType(str))
{
	// TODO    
}
else 
{
    // TODO
}
```

#### <a id="is-list">2. 判断是否List类型</a>

##### public bool IsList(object obj)

###### 说明：判断指定对象是否为泛型列表类型。

```c#
List<String> persons = new List<String>();

TypeTool tt = Xmtool.Type();
if (tt.IsList(persons))
{
    // TODO
}
```

#### <a id="is-cloneable">3. 判断对象是否可复制</a>

##### public bool IsCloneable(object obj)

###### 说明：判断对象是否可复制（即是否实现了ICloneable接口）

```c#
string str = "Hello World.";
    
TypeTool tt = Xmtool.Type();
if (tt.IsCloneable(str))
{
    // TODO
}
```

#### <a id="clone-list">4. 克隆列表对象</a>

##### public IList CloneList(IList list, bool includeContent = true)

###### 说明：复制指定列表对象。

###### 参数

list：待复制的列表对象。

includeContent ：是否复制列表对象中的内容，默认为true；否则只复制列表对象本身。

```c#
List<String> persons = new List<String>();
persons.Add("张三");
persons.Add("李四");

TypeTool tt = Xmtool.Type();
IList cloneObj = tt.CloneList(persons);
// TODO
```