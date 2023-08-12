# JSON配置文件

JSON配置文件是基于[DynamicObjectExt](./?item=0212)扩展动态对象封装而成的JSON文件解析工具。通过JSON配置文件工具可以方便的将JSON文件解析成动态对象进行操作；并且支持同时解析多个JSON文件，JSON文件中的同名属性会根据加载顺序进行覆盖。

通过JSON配置文件可以将系统中常用到的属性配置、系统设置等文件进行加载解析，在系统中方便的操作；当然，也可以是从其他地方（比如数据库）获取到的合法JSON字符串进行解析。

使用时，首先需要获取JSON配置文件工具对象：

```c#
JsonTool json = Xmtool.Json();
// TODO
```

然后，可以通过其中提供的方法解析JSON文件或JSON字符串：

[AddJsonFile方法](#addjsonfile)

[Parse方法](#parse)



#### <a id="addjsonfile">1. AddJsonFile方法</a>

##### public JsonConfigParser AddJsonFile(string path)

###### 说明：增加要解析的JSON配置文件。

###### 参数：

**path**：要解析的JSON配置文件绝对路径。



#### <a id="parse">2. Parse方法</a>

##### public dynamic Parse(string jsonStr = null)

###### 说明：将通过AddJsonFile方法设置的JSON配置文件，以及参数传入的JSON字符串进行解析，并返回解析后的动态对象。

###### 参数：

**jsonStr**：要解析的JSON字符串内容，可以为空；那么将只解析设置的JSON配置文件内容。

```c#
// c:\json1.json
{
    "User": {
        "Name": "softwaiter",
        "Age": 18
    }
}
```

```c#
dynamic obj = Xmtool.Json().AddJsonFile("c:\json1.json").Parse();
Console.WriteLine(obj.User.Name);	// 输出softwaiter
```

如果是多个JSON配置文件，当文件中存在同名属性时，后面的文件内容将覆盖前面文件内容的属性值。

```c#
// c:\json2.json
{
    "User": {
        "Name": "wangxm"
    }
}
```

```c#
dynamic obj = Xmtool.Json().AddJsonFile("c:\json1.json").AddJsonFile("c:\json2.json").Parse();
Console.WriteLine(obj.User.Name);	// 输出wangxm
```

当然，也可以不设置JSON配置文件，通过输入字符串内容进行解析。

```c#
dynamic obj = Xmtool.Json().Parse("{\"Name\": \"softwaiter\"}");
Console.WriteLine(obj.Name);	// 输出softwaiter
```