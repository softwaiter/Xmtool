# Image处理工具

在我们的软件系统中，经常需要对图片进行各种各样的处理；例如最常见的头像缩放，需要将用户上传的图片缩放成系统需要的最佳大小。本工具包主要将常用的图片方法进行汇总，方便开发者使用。目前包中提供了图片裁剪、缩放和生成base64字符串的方法，后续根据需要会持续增加。

[1. 对图片文件进行裁剪](#crop-from-file)

[2. 对图片数据流进行裁剪](#crop-from-stream)

[3. 对Image对象进行裁剪](#crop-from-image)

[4. 对图片文件进行缩放](#scale-from-file)

[5. 对图片数据流进行缩放](#scale-from-stream)

[6. 对Image对象进行缩放](#scale-from-image)

[7. 将图片文件转换成Base64字符串](#base64-from-file)

[8. 将图片数据流转换成Base64字符串](#base64-from-stream)

[9. 将Image对象内容转换成Base64字符串](#base64-from-image)



#### <a id="crop-from-file">1. 对图片文件进行裁剪</a>

###### public SKImage Cropping(string originFile, SKRect cropRegion, int width, int height)

说明：加载指定图片文件，根据指定参数进行裁剪；并返回裁剪后的图片对象。

```c#
ImageTool tool = Xmtool.Image();
SKImage cropImage = tool.Cropping("e:\\picture\\index.png", new SKRect(300, 300, 800, 800), 300, 300);
// TODO
```



#### <a id="crop-from-stream">2. 对图像数据流进行裁剪</a>

说明：对传入的图像数据流进行裁剪；并返回裁剪后的图片对象。

###### public SKImage Cropping(Stream stream, SKRect cropRegion, int width, int height)

```c#
ImageTool tool = Xmtool.Image();
using (FileStream stream = new FileStream("e:\\picture\\index.png", FileMode.Open, FileAccess.Read))
{ 
	SKImage result = tool.Cropping(stream, new SKRect(300, 300, 800, 800), 300, 300);
	// TODO
}
```



#### <a id="crop-from-image">3. 对Image对象进行裁剪</a>

说明：对Image对象进行裁剪；并返回裁剪后的新图片对象。

###### public SKImage Cropping(SKImage image, SKRect cropRegion, int width, int height)

```c#
SKImage image = SKImage.FromEncodedData("e:\\picture\\index.png");

ImageTool tool = Xmtool.Image();
SKImage result = tool.Cropping(image, new SKRect(300, 300, 800, 800), 300, 300);
// TODO
```



#### <a id="scale-from-file">4. 对图片文件进行缩放</a>

###### public SKImage Resize(string originFile, int height, int width, bool keepRatio, bool getCenter)

说明：将原始图片文件缩放成指定宽高，并返回生成后的图片对象。

```c#
ImageTool tool = Xmtool.Image();
Image result = tool.Resize("c:\\avatar.png", 200, 200, true, true);
// TODO
```



#### <a id="scale-from-stream">5. 对图片数据流进行缩放</a>

###### public SKImage Resize(Stream stream, int height, int width, bool keepRatio, bool getCenter)

说明：将图片数据流缩放成指定宽高，并返回生成后的图片对象。

```c#
ImageTool tool = Xmtool.Image();
using (FileStream stream = new FileStream("c:\\avatar.png", FileMode.Open, FileAccess.Read))
{ 
	Image result = tool.Resize(stream, 200, 200, true, true);
	// TODO
}
```



#### <a id="scale-from-image">6. 对Image对象进行缩放</a>

说明：

###### public SKImage Resize(SKImage image, int height, int width, bool keepRatio, bool getCenter)

```c#
SKImage image = SKImage.FromEncodedData("c:\\avatar.png");

ImageTool tool = Xmtool.Image();
SKImage result = tool.Resize(image, 200, 200, true, true);
// TODO
```



#### <a id="base64-from-file">7. 将图片文件转换成Base64字符串</a>

###### public string ToBase64(string file)

说明：将指定图片文件内容转换成Base64字符串并返回。

```c#
ImageTool tool = Xmtool.Image();
string base64str = tool.ToBase64("c:\avatar.png");
// TODO
```



#### <a id="base64-from-stream">8. 将图片数据流转换成Base64字符串</a>

###### public string ToBase64(Stream stream)

说明：将图片数据流转换成Base64字符串并返回。

```c#
ImageTool tool = Xmtool.Image();
using (FileStream stream = new FileStream("c:\\avatar.png", FileMode.Open, FileAccess.Read))
{ 
	string base64str = tool.ToBase64(stream);
	// TODO
}
```



#### <a id="base64-from-image">9. 将Image对象内容转换成Base64字符串</a>

###### public string ToBase64(SKImage image)

说明：将Image对象内容转换成Base64字符串并返回。

```c#
SKImage image = SKImage.FromEncodedData("c:\\avatar.png");
ImageTool tool = Xmtool.Image();
string base64str = tool.ToBase64(image);
// TODO
```