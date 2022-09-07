# 正则表达式

在日常的软件开发过程中，每个人都会或多或少的遇到各种各样需要校验数据格式的需求，有些格式是和具体业务逻辑相关；而有些格式是业界标准，在任何系统和功能中都一致通用的。Xmtool将大家最常用的格式检查正则表达式进行了整理汇总，主要提供如下方法：

### 常用类

[是否合法手机号码](#comm1)

[是否合法固定电话](#comm2)

[是否合法邮箱地址](#comm3)

[是否合法URL链接地址](#comm4)

[是否合法IP地址](#comm5)

[是否合法身份证号码](#comm6)

### 字符类

[是否英文字符](#chr1)

[是否小写英文字符](#chr2)

[是否大写英文字符](#chr3)

[是否中文字符](#chr4)

[是否中文或英文字符](#chr5)

[是否中文和英文字符](#chr6)

[是否英文或数字](#chr7)

[是否英文和数字](#chr8)

[是否中文或英文或数字](#chr9)

[是否中文和英文和数字](#chr10)

[是否有效账户名](#chr11)

### 数字类

[是否合法数值](#num1)

[是否合法整数](#num2)

[是否合法正整数](#num3)

[是否合法自然数](#num4)

[是否合法浮点数](#num5)

[是否特定精度的浮点数](#num6)

<br/>

### 一、常用类

#### <a id="comm1">1. 是否合法手机号码</a>

###### public bool IsMobile(string value)

说明：判断传入字符串 `value` 是否为1开头的11位数字。

```c#
// 判断13012345678是否为合法手机号
bool ret = Xmtool.Regex().IsMobile("13012345678");
```



#### <a id="comm2">2. 是否合法固定电话</a>

###### public bool IsTelephone(string value)

说明：判断传入字符串 `value` 是否为合法固定电话，010-12345678或0312-1234567。

```c#
// 判断010-12345678是否为合法固定电话
bool ret = Xmtool.Regex().IsTelephone("010-12345678");
```



#### <a id="comm3">3. 是否合法邮箱地址</a>

###### public bool IsEmail(string value)

说明：判断传入字符串 `value` 是否为合法邮箱地址格式，如xxxx@xx.com等。

```c#
// 判断test@xmtool.com是否为合法邮箱地址
bool ret = Xmtool.Regex().IsEmail("test@xmtool.com");
```



#### <a id="comm4">4. 是否合法URL链接地址</a>

###### public bool IsUrl(string vaule)

说明：采用宽松检查格式，http://、https://、ftp://等作为前缀的字符串都属于合法的URL链接地址。

```c#
// 判断http://www.baidu.com是否为合法URL地址
bool ret = Xmtool.Regex().IsUrl("http://www.baidu.com");
```



#### <a id="comm5">5. 是否合法IP地址</a>

###### public bool IsIP(string value)

说明：该方法检测不支持IPv6，只针对IPv4地址格式进行检测；如192.168.1.1。

```c#
// 判断192.168.1.1是否为有效IPv4地址
bool ret = Xmtool.Regex().IsIP("192.168.1.1");
```



#### <a id="comm6">6. 是否合法身份证号码</a>

###### public bool IsIDCard(string value)

说明：针对中国公民身份证号码进行检测，18位符合规则的号码。

```c#
// 判断身份证 1xxxxxxxxxxxxxxxxxx 是否为合法身份证号码
bool ret = Xmtool.Regex().IsIDCard("1xxxxxxxxxxxxxxxxxx");
```

### 二、字符类

#### <a id="chr1">1. 是否英文字符</a>

###### public bool IsEnglish(string value)

说明：判断传入的 `value` 是否全部由英文字符组成。

```c#
// 判断 Hello 是否英文字符
bool ret = Xmtool.Regex().IsEnglish("Hello");
```



#### <a id="chr2">2. 是否小写英文字符</a>

###### public bool IsLowercaseEnglish(string value)

说明：判断传入的 `value` 是否全部由小写英文字符组成。

```c#
// 判断 hello 是否小写英文字符
bool ret = Xmtool.Regex().IsLowercaseEnglish("hello");
```



#### <a id="chr3">3. 是否大写英文字符</a>

###### public bool IsCapitalEnglish(string value)

说明：判断传入的 `value` 是否全部由大写英文字符组成。

```c#
// 判断 HELLO 是否大写英文字符
bool ret = Xmtool.Regex().IsCapitalEnglish("HELLO");
```



#### <a id="chr4">4. 是否中文字符</a>

###### public bool IsChinese(string value)

说明：判断传入的 `value` 是否全部由中文汉字组成。

```c#
// 判断 中国人民万岁 是否中文字符
bool ret = Xmtool.Regex().IsChinese("中国人民万岁");
```



#### <a id="chr5">5. 是否中文或英文字符</a>

###### public bool IsChineseOrEnglish(string value)

说明：判断传入的 `value` 是否全部由中文汉字<b>或者</b>英文字符组成。

```c#
// 判断 Hello你好 是否中文或英文字符
bool ret = Xmtool.Regex().IsChineseOrEnglish("Hello你好");
```



#### <a id="chr6">6. 是否中文和英文字符</a>

###### public bool IsChineseAndEnglish(string value)

说明：判断传入的 `value` 是否全部由中文汉字<b>或者</b>英文字符组成；且<b>同时包含</b>中文汉字和英文字符。

```c#
// 判断 Hello你好 是否中文和英文字符
bool ret = Xmtool.Regex().IsChineseAndEnglish("Hello你好");
```



#### <a id="chr7">7. 是否英文或数字</a>

###### public bool IsEnglishOrNumber(string value)

说明：判断传入的 `value` 是否全部由英文字符<b>或者</b>数字组成。

```c#
// 判断 Hello123 是否英文或数字
bool ret = Xmtool.Regex().IsEnglishOrNumber("Hello123");
```



#### <a id="chr8">8. 是否英文和数字</a>

###### public bool IsEnglishAndNumber(string value)

说明：判断传入的 `value` 是否全部由英文字符<b>或者</b>数字组成；且<b>同时包含</b>英文字符和数字。

```c#
// 判断 Hello123 是否英文字符和数字
bool ret = Xmtool.Regex().IsEnglishAndNumber("Hello123");
```



#### <a id="chr9">9. 是否中文或英文或数字</a>

###### public bool IsChineseOrEnglishOrNumber(string value)

说明：判断传入的 `value` 是否全部由中文汉字<b>或者</b>英文字符<b>或者</b>数字组成。

```c#
// 判断 Hello123 是否中文或英文或数字
bool ret = Xmtool.Regex().IsChineseOrEnglishOrNumber("Hello123");
```



#### <a id="chr10">10. 是否中文和英文和数字</a>

###### public bool IsChineseAndEnglishAndNumber(string value)

说明：判断传入的 `value` 是否全部由中文汉字<b>或者</b>英文字符<b>或者</b>数字组成；且<b>同时包含</b>中文汉字、英文字符和数字。

```c#
// 判断 Hello你好123 是否中文和英文和数字
bool ret = Xmtool.Regex().IsChineseAndEnglishAndNumber("Hello你好123");
```



#### <a id="chr11">11. 是否有效账户名</a>

###### public bool IsAccount(string value)

说明：判断传入的 `value` 是否有效的账户名，规则为以英文字符开头，只能包含英文字符、数字或者下划线。

```c#
// 判断 softwaiter 是否有效账户名
bool ret = Xmtool.Regex().IsAccount("softwaiter");
```

### 三、数字类

#### <a id="num1">1. 是否合法数值</a>

###### public bool IsNumber(string value)

说明：判断传入的 `value` 是否为数值，包括任意形式的数值：0、整数、小数等。

```c#
// 判断 123 是否合法数值
bool ret = Xmtool.Regex().IsNumber("123");
```



#### <a id="num2">2. 是否合法整数</a>

###### public bool IsInteger(string value)

说明：判断传入的 `value` 是否为整数，包括负整数、0、正整数。

```c#
// 判断 123 是否合法整数
bool ret = Xmtool.Regex().IsInteger("123");
```



#### <a id="num3">3. 是否合法正整数</a>

###### public bool IsPositiveInteger(string value)

说明：判断传入的 `value` 是否为正整数，即所有大于0的整数。

```c#
// 判断 123 是否合法正整数
bool ret = Xmtool.Regex().IsPositiveInteger("123");
```



#### <a id="num4">4. 是否合法自然数</a>

###### public bool IsNaturalInteger(string value)

说明：判断传入的 `value` 是否为自然数，即所有大于等于0的整数。

```c#
// 判断 123 是否合法自然数
bool ret = Xmtool.Regex().IsNaturalInteger("123");
```



#### <a id="num5">5. 是否合法浮点数</a>

###### public bool IsDecimal(string value)

说明：判断传入的 `value` 是否为浮点数，即包含小数部分的数值。

```c#
// 判断 0.123 是否合法浮点数
bool ret = Xmtool.Regex().IsDecimal("0.123");
```



#### <a id="num6">6. 是否特定精度的浮点数</a>

###### public bool IsDecimal(string value, int precision)

说明：判断传入的 `value` 是否为指定精度的浮点数，即包含的小数部分的位数为 `precision` 指定值。

```c#
// 判断 0.58 是否合法2位精度的浮点数
bool ret = Xmtool.Regex().IsDecimal("0.58", 2);
```
