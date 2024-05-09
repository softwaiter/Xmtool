using CodeM.Common.Tools;
using SkiaSharp;
using System.ComponentModel;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using static System.Net.Mime.MediaTypeNames;

namespace Test
{
    public class ImageTest
    {
        private ITestOutputHelper output;

        public ImageTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void CroppingImageFile()
        {
            SKImage image = Xmtool.Image().Cropping("e:\\picture\\index.png", new SKRect(300, 300, 800, 800), 300, 300);
            Assert.NotNull(image);

            using (FileStream fs = new FileStream("e:\\picture\\index_cropping2.png", FileMode.Create, FileAccess.Write))
            {
                image.Encode().SaveTo(fs);
            }
        }

        [Fact]
        public void ResizeImageFile()
        {
            SKImage image = Xmtool.Image().Resize("e:\\picture\\index.png", 300, 100, false, false);
            Assert.NotNull(image);

            using (FileStream fs = new FileStream("e:\\picture\\index_resize2.png", FileMode.Create, FileAccess.Write))
            {
                image.Encode().SaveTo(fs);
            }
        }

        [Fact]
        [Description("保持纵横比改变图片大小")]
        public void ResizeImageFile2()
        {
            SKImage image = Xmtool.Image().Resize("e:\\picture\\index.png", 300, 100, true, false);
            Assert.NotNull(image);

            using (FileStream fs = new FileStream("e:\\picture\\index_resize3.png", FileMode.Create, FileAccess.Write))
            {
                image.Encode().SaveTo(fs);
            }
        }

        [Fact]
        [Description("保持纵横比改变图片大小并居中绘制")]
        public void ResizeImageFile3()
        {
            using (FileStream stream = new FileStream("e:\\picture\\index.png", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                SKImage origin = SKImage.FromEncodedData(stream);
                SKImage image = Xmtool.Image().Resize(origin, 300, 100, true, true);
                Assert.NotNull(image);

                using (FileStream fs = new FileStream("e:\\picture\\index_resize4.png", FileMode.Create, FileAccess.Write))
                {
                    image.Encode().SaveTo(fs);
                }
            }
        }

        [Fact]
        public void ImageFileToBase64()
        {
            string base64Data = Xmtool.Image().ToBase64("e:\\picture\\Xmtool.png");
            Assert.NotEmpty(base64Data);
        }

        [Fact]
        public void ImageStreamToBase64()
        {
            using (FileStream stream = new FileStream("e:\\picture\\Xmtool.png", FileMode.Open, FileAccess.Read))
            {
                string base64Data = Xmtool.Image().ToBase64(stream);
                Assert.NotEmpty(base64Data);
            }
        }

        [Fact]
        public void ImageObjectToBase64()
        {
            SKImage image = SKImage.FromEncodedData("e:\\picture\\Xmtool.png");
            string base64Data = Xmtool.Image().ToBase64(image);
            Assert.NotEmpty(base64Data);
        }
    }
}
