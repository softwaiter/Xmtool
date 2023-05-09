# 发送短信

发送短信是现在软件系统中必不可少的功能；无论是短信验证码登录，还是系统消息通知，以及忘记密码等场景，都需要发送短信来配合完成。市面上有各种各样的短信发送平台，各个平台都提供了相应的SDK工具包或者HTTP接口，使用起来需要熟悉各个平台不同的SDK方法和HTTP接口参数等等，使用起来很不方便；系统运行上线后如果想切换一个短信发送平台，更是困难，相当于重新实现一次短信发送功能。

为了解决以上问题，本类库定义了一套标准方法，将各种短信发送平台基于标准方法进行二次封装；在使用时，无论是选用哪家的短信发送平台，都通过标准方法进行调用，使用方便，扩展维护也异常简单。

当前版本，系统实现了阿里短信和腾讯短信两个短信发送平台的二次封装，后续根据需要会持续更新。

```c#
public enum SmsProvider
{
    Unset,
    Alibaba,	// 阿里短信平台
    Tencent		// 腾讯短信平台
}
```

使用时，需要首先获取一个短信发送对象ISmsProvider；获取对象时，需要指定短信发送平台。

[获取短信发送对象](#create-smstool)

获取短信发送对象后，需要使用Config方法进行配置，这是正式发送短信前必须要进行的工作。

[配置短信发送对象](#config-smstool)

配置完成后，用户便可以调用下面的任意一个方法进行短信发送操作，简单方便。

[发送短信-标准版](#send-standard)

[发送短信-自定义版](#send-cutomized)



#### <a id="create-smstool">1. 获取短信发送对象</a>

###### public static ISmsProvider Sms(SmsProvider provider)

```c#
ISmsProvider sms = Xmtool.Sms(SmsProvider.Alibaba);
// TODO
```

#### <a id="config-smstool">2. 配置短信发送对象</a>

###### public static ISmsProvider Config(params string[] args)

```c#
ISmsProvider sms = Xmtool.Sms(SmsProvider.Alibaba);

// 阿里云短行平台配置方法
sms = sms.Config("accessKeyId（替换成自己的）", "accessKeySecret（替换成自己的）",
                "阿里云短信签名（替换成自己的）", "模板编码（替换成自己的）");
/*
sms = sms.Config("secretId（替换成自己的）", "secretKey（替换成自己的）",
                "腾讯云短信签名（替换成自己的）", "模板Id（替换成自己的）", "appId（替换成自己的）");
*/

// TODO
```

#### <a id="send-standard">3. 发送短信-标准版</a>

###### 说明：标准版默认使用配置时指定的签名和模板进行发送。

###### public bool Send(string templateParam, params string[] phoneNums)

```c#
ISmsProvider sms = Xmtool.Sms(SmsProvider.Alibaba);

// 阿里云短行平台配置方法
sms = sms.Config("accessKeyId（替换成自己的）", "accessKeySecret（替换成自己的）",
                "阿里云短信签名（替换成自己的）", "模板编码（替换成自己的）");
/*
sms = sms.Config("secretId（替换成自己的）", "secretKey（替换成自己的）",
                "腾讯云短信签名（替换成自己的）", "模板Id（替换成自己的）", "appId（替换成自己的）");
*/

sms.Send("参数（替换成自己的，如：{\"code\":\"1234\"}）", "136********");
```

#### <a id="send-customized">4. 发送短信-自定义版</a>

###### 说明：自定义版可以在发送时指定签名和模板。

###### public bool Send2(string signName, string templateCode, string templateParam, params string[] phoneNums)

```c#
ISmsProvider sms = Xmtool.Sms(SmsProvider.Alibaba);

// 阿里云短行平台配置方法
sms = sms.Config("accessKeyId（替换成自己的）", "accessKeySecret（替换成自己的）",
                "阿里云短信签名（替换成自己的）", "模板编码（替换成自己的）");
/*
sms = sms.Config("secretId（替换成自己的）", "secretKey（替换成自己的）",
                "腾讯云短信签名（替换成自己的）", "模板Id（替换成自己的）", "appId（替换成自己的）");
*/

sms.Send("自定义签名", "自定义模板", "参数（替换成自己的，如：1234）", "136********");
```

<b>*另外，同时提供了SendAsync、Send2Async等功能相同的异步方法，使用时可根据需要选择。</b>