# Image处理工具

在我们的软件系统中，经常需要对图片进行各种各样的处理；例如最常见的头像缩放，需要将用户上传的图片缩放成系统需要的最佳大小。本工具包主要将常用的图片方法进行汇总，方便开发者使用。目前包中只提供了图片缩放和图片生成base64字符串的方法，后续根据需要会持续增加。

[对图片文件进行缩放](#scale-from-file)

[对图片数据流进行缩放](#scale-from-stream)

[将图片文件转换成Base64字符串](#base64-from-file)

[将图片数据流转换成Base64字符串](#base64-from-stream)

[将Image对象内容转换成Base64字符串](#base64-from-image)



#### <a id="scale-from-file">1. 对图片文件进行缩放</a>

###### public Image Resize(string originFile, int height, int width, bool keepRatio, bool getCenter)

说明：将原始图片文件缩放成指定宽高，并返回生成后的图片对象。

```c#
ImageTool tool = Xmtool.Image();
Image result = tool.Resize("c:\avatar.png", 200, 200, true, true);
// TODO
```



#### <a id="scale-from-stream">2. 对图片数据流进行缩放</a>

###### public Image Resize(Stream stream, int height, int width, bool keepRatio, bool getCenter)

说明：将图片数据流缩放成指定宽高，并返回生成后的图片对象。

```c#
ImageTool tool = Xmtool.Image();
using (FileStream stream = new FileStream("c:\avatar.png", FileMode.Open, FileAccess.Read))
{ 
	Image result = tool.Resize(stream, 200, 200, true, true);
	// TODO
}
```



#### <a id="base64-from-file">3. 将图片文件转换成Base64字符串</a>

###### public string ToBase64(string file)

说明：将指定图片文件内容转换成Base64字符串并返回。

```c#
ImageTool tool = Xmtool.Image();
string base64str = tool.ToBase64("c:\avatar.png");
// TODO
```



#### <a id="base64-from-stream">4. 将图片数据流转换成Base64字符串</a>

###### public string ToBase64(Stream stream)

说明：将图片数据流转换成Base64字符串并返回。

```c#
ImageTool tool = Xmtool.Image();
using (FileStream stream = new FileStream("c:\avatar.png", FileMode.Open, FileAccess.Read))
{ 
	string base64str = tool.ToBase64(stream);
	// TODO
}
```



#### <a id="base64-from-image">5. 将Image对象内容转换成Base64字符串</a>

###### public string ToBase64(Image image)

说明：将Image对象内容转换成Base64字符串并返回。

```c#
Image image = Image.FromFile("c:\avatar.png");
ImageTool tool = Xmtool.Image();
string base64str = tool.ToBase64(image);
// TODO
```