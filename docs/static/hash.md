# 散列算法

散列算法在软件开发中经常被用来校验数据完整性，也会用来对密码等数据进行不可逆加密进行保护；本工具包提供了MD5、SHA1、SHA256、SHA384、SHA512等常用散列算法。

[MD5散列算法](#hash-md5)

[SHA1散列算法](#hash-sha1)

[SHA256散列算法](#hash-sha256)

[SHA384散列算法](#hash-sha384)

[SHA512散列算法](#hash-sha512)

#### <a id="hash-md5">1. MD5散列算法</a>

###### public string MD5(string text, string encoding = "utf-8")

说明：对传入的字符串进行MD5散列计算，并返回结算结果；编码格式默认为UTF8，可进行指定。

```c#
string md5Str = Xmtool.Hash().MD5("admin@123");
// TODO
```

#### <a id="hash-sha1">2. SHA1散列算法</a>

###### public string SHA1(string text, string encoding = "utf-8")

说明：对传入的字符串进行SHA1散列计算，并返回计算结果；编码格式默认为UTF8，可进行指定。

```c#
string sha1Str = Xmtool.Hash().SHA1("admin@123");
// TODO
```

#### <a id="hash-sha256">3. SHA256散列算法</a>

###### public string SHA256(string text, string encoding = "utf-8")

说明：对传入的字符串进行SHA256散列计算，并返回计算结果；编码格式默认为UTF8，可进行指定。

```c#
string sha256Str = Xmtool.Hash().SHA256("admin@123");
// TODO
```

#### <a id="hash-sha384">4. SHA384散列算法</a>

###### public string SHA384(string text, string encoding = "utf-8")

说明：对传入的字符串进行SHA384散列计算，并返回计算结果；编码格式默认为UTF8，可进行指定。

```c#
string sha384Str = Xmtool.Hash().SHA384("admin@123");
// TODO
```

#### <a id="hash-sha512">5. SHA512散列算法</a>

###### public string SHA512(string text, string encoding = "utf-8")

说明：对传入的字符串进行SHA512散列计算，并返回计算结果；编码格式默认为UTF8，可进行指定。

```c#
string sha512Str = Xmtool.Hash().SHA512("admin@123");
// TODO
```

