# 随机值

基于整个工具包的精简设计原则，对于.NetCore框架已经提供且使用方便的功能，本工具包不会进行二次封装；因此，在随机值工具类中，没有提供获取随机数值的方法，目前只提供了一个生成随机验证码的方法，后续根据需要会持续增加。

[生成验证码](#gen-captcha)

#### <a id="gen-captcha">1. 生成验证码</a>

###### public string RandomCaptcha(int len, bool onlyNumber = false)

说明：生成指定规格的验证码并返回。

```c#
// 生成4位纯数字验证码
string numCaptcha = Xmtool.Random().RandomCaptcha(4, true);
// 生成6位字母和数字混合的验证码
string captcha = Xmtool.Random().RandomCaptcha(6);
```

