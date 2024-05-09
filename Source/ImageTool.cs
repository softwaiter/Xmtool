using SkiaSharp;
using System;
using System.IO;

namespace CodeM.Common.Tools
{
    public class ImageTool
    {
        private static ImageTool sImageTool = new ImageTool();

        private ImageTool()
        { 
        }

        internal static ImageTool New()
        {
            return sImageTool;
        }

        public SKImage Cropping(string originFile, SKRect cropRegion, int width, int height)
        {
            using (FileStream fs = new FileStream(originFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return Cropping(fs, cropRegion, width, height);
            }
        }

        public SKImage Cropping(Stream stream, SKRect cropRegion, int width, int height)
        {
            using (SKImage image = SKImage.FromEncodedData(stream))
            {
                return Cropping(image, cropRegion, width, height);
            }
        }

        public SKImage Cropping(SKImage image, SKRect cropRegion, int width, int height)
        {
            using (SKBitmap bmp = new SKBitmap(width, height))
            {
                SKRect target = new SKRect(0, 0, width, height);

                SKCanvas canvas = new SKCanvas(bmp);
                canvas.DrawImage(image, cropRegion, target);
                return SKImage.FromBitmap(bmp);
            }
        }

        public SKImage Resize(string originFile, int height, int width, bool keepRatio, bool getCenter)
        {
            using (FileStream fs = new FileStream(originFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return Resize(fs, height, width, keepRatio, getCenter);
            }
        }

        public SKImage Resize(Stream stream, int height, int width, bool keepRatio, bool getCenter)
        {
            using (SKImage image = SKImage.FromEncodedData(stream))
            {
                return Resize(image, height, width, keepRatio, getCenter);
            }
        }

        public SKImage Resize(SKImage image, int height, int width, bool keepRatio, bool getCenter)
        {
            int sourceX = 0;
            int sourceY = 0;
            int sourceWidth = image.Width;
            int sourceHeight = image.Height;

            int targetX = 0;
            int targetY = 0;
            int targetWidth = width;
            int targetHeight = height;

            if (keepRatio)
            {
                double widthRatio = (double)targetWidth / (double)sourceWidth;
                double heightRatio = (double)targetHeight / (double)sourceHeight;

                if (widthRatio < heightRatio)
                {
                    targetHeight = (int)(sourceHeight * widthRatio);
                }
                else if (heightRatio < widthRatio)
                {
                    targetWidth = (int)(sourceWidth * heightRatio);
                }

                if (getCenter)
                {
                    if (targetHeight < height)
                    {
                        targetY = (height - targetHeight) / 2;
                    }
                    else if (targetWidth < width)
                    {
                        targetX = (width - targetWidth) / 2;
                    }
                }
            }

            using (SKBitmap bmp = new SKBitmap(width, height))
            {
                SKRect source = new SKRect(sourceX, sourceY, sourceX + sourceWidth, sourceY + sourceHeight);
                SKRect target = new SKRect(targetX, targetY, targetX + targetWidth, targetY + targetHeight);
                SKCanvas canvas = new SKCanvas(bmp);
                canvas.DrawImage(image, source, target);
                return SKImage.FromBitmap(bmp);
            }
        }

        public string ToBase64(string file)
        {
            using (SKImage image = SKImage.FromEncodedData(SKData.Create(file)))
            {
                string data = ToBase64(image);
                return data;
            }
        }

        public string ToBase64(SKImage image)
        {
            using (Stream stream = new MemoryStream())
            {
                image.EncodedData.SaveTo(stream);
                return ToBase64(stream);
            }
        }

        public string ToBase64(Stream stream)
        {
            byte[] buff = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(buff, 0, buff.Length);

            return Convert.ToBase64String(buff);
        }
    }
}
