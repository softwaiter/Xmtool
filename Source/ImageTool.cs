using System;
using System.Drawing;
using System.Drawing.Imaging;
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

        public Image Resize(string originFile, int height, int width, bool keepRatio, bool getCenter)
        {
            using (FileStream fs = new FileStream(originFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return Resize(fs, height, width, keepRatio, getCenter);
            }
        }

        public Image Resize(Stream stream, int height, int width, bool keepRatio, bool getCenter)
        {
            int destWidth = width;
            int destHeight = height;
            Image fullsizeImage = Image.FromStream(stream);

            if (keepRatio || getCenter)
            {
                double scaleRatio = (double)fullsizeImage.Width / (double)destWidth;
                if (getCenter)
                {
                    int bmpY = (int)((fullsizeImage.Height - (destHeight * scaleRatio)) / 2);
                    Rectangle section = new Rectangle(new Point(0, bmpY), new Size(fullsizeImage.Width, (int)(height * scaleRatio)));
                    Bitmap bmp = new Bitmap(fullsizeImage);
                    fullsizeImage.Dispose();

                    using (Bitmap bmp2 = new Bitmap(section.Width, section.Height))
                    {
                        Graphics cutImg = Graphics.FromImage(bmp2);
                        cutImg.DrawImage(bmp, 0, 0, section, GraphicsUnit.Pixel);

                        fullsizeImage = bmp2;
                        bmp.Dispose();
                        cutImg.Dispose();

                        return fullsizeImage.GetThumbnailImage(destWidth, destHeight, null, IntPtr.Zero);
                    }
                }
                else
                {
                    destHeight = (int)(fullsizeImage.Height / scaleRatio);
                }
            }

            return fullsizeImage.GetThumbnailImage(destWidth, destHeight, null, IntPtr.Zero);
        }

        public string ToBase64(string file)
        {
            Image image = Image.FromFile(file);
            string data = ToBase64(image);
            image.Dispose();
            return data;
        }

        public string ToBase64(Stream stream)
        {
            Image image = Image.FromStream(stream);
            string data = ToBase64(image);
            image.Dispose();
            return data;
        }

        public string ToBase64(Image image)
        {
            using (Stream destStream = new MemoryStream())
            {
                image.Save(destStream, ImageFormat.Png);

                byte[] buff = new byte[destStream.Length];
                destStream.Position = 0;
                destStream.Read(buff, 0, buff.Length);
                destStream.Close();

                return Convert.ToBase64String(buff);
            }
        }
    }
}
