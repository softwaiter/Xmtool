# 发送邮件

发送邮件常用来作为发送系统通知的一种方式，广泛的在各种后台管理中使用；虽然.NetCore框架提供了发送邮件的组件，但使用起来还是有一点复杂度，为了使用起来更简单、更友好，对发送邮件的功能进行了二次封装。

首先，需要获得一个邮件发送对象；在获取该对象时，必须传递最基本的SMTP邮件服务器地址、SMTP端口、是否启用SSL，以及发送邮件使用的账户名和密码。

[获取邮件发送对象](#create-mailtool)

获取到邮件发送对象之后，可以直接调用下面的任意一个方法进行邮件发送操作，简单方便。

[发送普通邮件-完全版](#send-all)

[发送普通邮件-简化版](#send-simple)

[发送网页邮件-完全版](#sendhtml-all)

[发送网页邮件-简化版](#sendhtml-simple)



#### <a id="create-mailtool">1. 获取邮件发送对象</a>

###### public static MailTool Mail(string host, int port, string account, string password, bool enableSsl = false)

```c#
MailTool mail = Xmtool.Mail("smtp.126.com", 25, "test", "test@123");
// TODO
```

#### <a id="send-all">2. 发送普通邮件-完全版</a>

######         public void Send(string subject, string body, string bodyEncoding, string from, string fromName, string to, string replyTo, string cc, string bcc, params string[] attachments)

```c#
MailTool mail = Xmtool.Mail("smtp.126.com", 25, "test", "test@123");
mail.Send("邮件标题", "邮件内容", "utf-8(内容编码格式)", "收件人看到的发件人邮箱地址", 
          "收件人看到的发件人名称", "收件人邮箱地址", "收件人回复邮件的接收地址",
          "抄送地址，多个用;分隔", "秘密抄送地址，多个用;分隔", "附件文件地址，可以多个");
```

#### <a id="send-simple">3. 发送普通邮件-简化版</a>

###### public void Send(string subject, string body, string from, string to, params string[] attachments)

```c#
MailTool mail = Xmtool.Mail("smtp.126.com", 25, "test", "test@123");
mail.Send("邮件标题", "邮件内容", "收件人看到的发件人邮箱地址", "收件人邮箱地址", "附件文件地址");
```

#### <a id="sendhtml-all">4. 发送网页邮件-完全版</a>

###### public void SendHtml(string subject, string body, string bodyEncoding, string from, string fromName, string to, string replyTo, string cc, string bcc, params string[] attachments)

```c#
MailTool mail = Xmtool.Mail("smtp.126.com", 25, "test", "test@123");
mail.Send("邮件标题", "网页源码内容", "utf-8(内容编码格式)", "收件人看到的发件人邮箱地址", 
          "收件人看到的发件人名称", "收件人邮箱地址", "收件人回复邮件的接收地址",
          "抄送地址，多个用;分隔", "秘密抄送地址，多个用;分隔", "附件文件地址，可以多个");
```

#### <a id="sendhtml-simple">5. 发送网页邮件-简化版</a>

###### public void SendHtml(string subject, string body, string from, string to, params string[] attachments)

```c#
MailTool mail = Xmtool.Mail("smtp.126.com", 25, "test", "test@123");
mail.Send("邮件标题", "网页源码内容", "收件人看到的发件人邮箱地址", "收件人邮箱地址", "附件文件地址");
```



<b>*另外，同时提供了SendAsync、SendHtmlAsync等功能相同的异步方法，使用时可根据需要选择。</b>

