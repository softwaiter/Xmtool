# 加密解密

加密解密是开发过程中多多少少必然会遇到的需求，Xmtool封装了最常用的Base64和AES对称加密方法；严格来讲，Base64不算加密解密，为了管理和使用方便，故放在加密解密模块。

[Base64编码](#crypto-base64-1)

[Base64解码](#crypto-base64-2)

[AES加密](#crypto-aes-1)

[AES解密](#crypto-aes-2)

#### <a id="crypto-base64-1">1. Base64编码</a>

###### public string Base64Encode(string text, string encoding = "utf-8") 

说明：对传入字符串进行Base64编码并返回，默认字符串编码格式为UTF8。

```c#
string base64Str = Xmtool.Crypto().Base64Encode("https://github.com/softwaiter");
// TODO
```

#### <a id="crypto-base64-2">2. Base64解码</a>

###### public string Base64Decode(string base64Text, string encoding = "utf-8")

说明：对经过Base64编码的字符串进行解码，返回解码后的明文内容，默认解码编码格式为UTF8。

```c#
string url = Xmtool.Crypto().Base64Decode("aHR0cHM6Ly9naXRodWIuY29tL3NvZnR3YWl0ZXI=");
// TODO
```

#### <a id="crypto-aes-1">3. AES加密</a>

###### public string AESEncode(string text, string key, string encoding = "utf-8")

说明：AES对称加密方法，key为加密的盐值，encoding为加密字符串的编码格式，默认为UTF8。

```c#
string password = "admin@123";
string encryptedPass = Xmtool.Crypto().AESEncode(password, "salt123");
// TODO
```

#### <a id="crypto-aes-2">4. AES解密</a>

###### public string AESDecode(string aesText, string key, string encoding = "utf-8")

说明：AES解密方法，解密时需使用和加密时相同的key和encoding编码格式。

```c#
string password = Xmtool.Crypto().AESDecode("vi3G7kR7r5GaLglLOGGtzw==", "salt123");
// TODO
```

