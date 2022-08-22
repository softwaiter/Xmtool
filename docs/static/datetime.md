# 日期时间

DateTimeTool是Xmtool中用来提供日期时间相关方法的工具类，使用时可通过Xmtool下的`DateTime`方法获得。

```c#
DateTimeTool dtt = Xmtool.DateTime();
```

目前该工具类下主要提供了时间戳、字符串时间范围相关的方法，具体如下：

[获取10位UTC时间戳](#m1)

[获取13位UTC时间戳](#m2)

[根据10位UTC时间戳逆向获得UTC日期时间](#m3)

[根据13位UTC时间戳逆向获得UTC日期时间](m4)

[根据10位UTC时间戳逆向获得本地日期时间](m5)

[根据13位UTC时间戳逆向获得本地日期时间](#m6)

[将字符串表示的时间范围转换为TimSpan时间段](#m7)

[检查字符串表示的时间范围格式是否合法](#m8)



#### <a id="m1">1. 获取10位UTC时间戳</a>

###### public long GetUtcTimestamp10()

```c#
// 返回当前UTC时间的10位时间戳
long ts = Xmtool.DateTime().GetUtcTimestamp10();
```



#### <a id="m2">2. 获取13位UTC时间戳</a>

###### public long GetUtcTimestamp13()

```c#
// 返回当前UTC时间的13位时间戳
long ts = Xmtool.DateTime().GetUtcTimestamp13();
```



#### <a id="m3">3. 根据10位UTC时间戳逆向获得UTC日期时间</a>

###### public DateTime GetUtcDateTimeFromUtcTimestamp10(long ts)

```c#
long ts = 1660737444;
// 将传入的10位UTC时间戳ts转换为对应的UTC日期时间
DateTime dt = Xmtool.DateTime().GetUtcDateTimeFromUtcTimestamp10(ts);
```



#### <a id="m4">4. 根据13位UTC时间戳逆向获得UTC日期时间</a>

###### public DateTime GetUtcDateTimeFromUtcTimestamp13(long ts)

```c#
long ts = 1660737467850;
// 将传入的13位UTC时间戳ts转换为对应的UTC日期时间
DateTime dt = Xmtool.DateTime().GetUtcDateTimeFromUtcTimestamp13(ts);
```



#### <a id="m5">5. 根据10位UTC时间戳逆向获得本地日期时间</a>

###### public DateTime GetLocalDateTimeFromUtcTimestamp10(long ts)

```c#
long ts = 1660737444;
// 将传入的10位UTC时间戳ts转换为对应的本地化日期时间
DateTime dt = Xmtool.DateTime().GetLocalDateTimeFromUtcTimestamp10(ts);
```



#### <a id="m6">6. 根据13位UTC时间戳逆向获得本地日期时间</a>

###### public DateTime GetLocalDateTimeFromUtcTimestamp13(long ts)

```c#
long ts = 1660737467850;
// 将传入的13位UTC时间戳ts转换为对应的本地化日期时间
DateTime dt = Xmtool.DateTime().GetLocalDateTimeFromUtcTimestamp13(ts);
```



#### <a id="m7">7. 将字符串表示的时间范围转换为TimSpan时间段</a>

###### public TimeSpan? GetTimeSpanFromString(string timespan, bool throwError = true)

###### 参数说明：

timespan: 时间段的字符表示，支持ms（毫秒）、s（秒）、m（分钟）、h（小时）、d（天）；例如：60s表示60秒钟。

throwError: 转换出错时是否抛出，默认true，直接抛出错误。

```c#
string time = "20m";
// 将传入的字符串表示时间范围转换为TimeSpan时间段，通常用于解析配置文件中的超时时间等
TimeSpan? ts = Xmtool.DateTime().GetTimeSpanFromString(time);
```



#### <a id="m8">8. 检查字符串表示的时间范围格式是否合法</a>

###### public bool CheckStringTimeSpan(string timespan, bool throwError = true)

###### 参数说明：

timespan: 时间段的字符表示，支持ms（毫秒）、s（秒）、m（分钟）、h（小时）、d（天）；例如：60s表示60秒钟。

throwError: 转换出错时是否抛出，默认true，直接抛出错误。

```c#
string time = "20m";
// 判断传入的字符串表示时间范围是否为合法的时间段格式，配合GetTimeSpanFromString方法使用，提前处理错误
if (Xmtool.DateTime().CheckStringTimeSpan(time, false))
{
	TimeSpan? ts = Xmtool.DateTime().GetTimeSpanFromString(time);
}
else
{
    Console.WriteLine("输入的字符串时间范围无法识别，请检查格式是否合法。");
}
```

