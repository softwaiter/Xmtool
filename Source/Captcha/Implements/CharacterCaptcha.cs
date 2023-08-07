using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Text;

namespace CodeM.Common.Tools.Captcha.Implements
{
    internal class CharacterCaptcha : ICaptcha
    {
        private CharacterCaptchaOption mOption = new CharacterCaptchaOption();

        public CaptchaKind Type
        {
            get
            {
                return CaptchaKind.Character;
            }
        }

        public ICaptcha Config(CaptchaOption option)
        {
            if (!(option is CharacterCaptchaOption))
            {
                throw new ArgumentException("此处需要CharacterCaptchaOption类型的参数。");
            }

            mOption = (CharacterCaptchaOption)option;

            return this;
        }

        public CaptchaResult Generate(CaptchaData data = null)
        {
            string validationData = String.Empty;
            if (data != null)
            {
                if (!(data is CharacterCaptchaData))
                {
                    throw new ArgumentException("此处需要CharacterCaptchaData类型的参数。");
                }
                validationData = ((CharacterCaptchaData)data).Code;
            }
            if (string.IsNullOrWhiteSpace(validationData))
            {
                validationData = RandomTool.New().RandomCaptcha(mOption.Length, mOption.OnlyNumber);
            }

            string displayData = string.Empty;
            using (MemoryStream stream = new MemoryStream())
            {
                Bitmap bmp = new Bitmap(mOption.Width, mOption.Height);

                Graphics g = Graphics.FromImage(bmp);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                g.Clear(mOption.BackColor);
                Pen penBorder = new Pen(mOption.BorderColor, 1);
                g.DrawRectangle(penBorder, 0, 0, mOption.Width - 1, mOption.Height - 1);

                float fontSize = mOption.Height / 2.9f;
                Font font = new Font(SystemFonts.DefaultFont.FontFamily,
                    fontSize, FontStyle.Bold | FontStyle.Italic);
                SizeF charSize = g.MeasureString("G", font);
                int charWidth = mOption.Width / validationData.Length;
                int charHeight = (int)Math.Ceiling(charSize.Height);

                float offsetX = 0;
                Random r = new Random();
                for (int i = 0; i < validationData.Length; i++)
                {
                    float x = r.Next((int)offsetX, (int)Math.Max((i + 1) * charWidth - charSize.Width, 0));
                    float y = r.Next(0, mOption.Height - charHeight);

                    int r1 = r.Next(0, 255);
                    int g1 = r.Next(0, 255);
                    int b1 = r.Next(0, 255);
                    Color color1 = Color.FromArgb(r1, g1, b1);
                    int r2 = r.Next(0, 255);
                    int g2 = r.Next(0, 255);
                    int b2 = r.Next(0, 255);
                    Color color2 = Color.FromArgb(r2, g2, b2);
                    Brush brush = new LinearGradientBrush(new PointF(x, y),
                        new PointF(x, y + font.Height), color1, color2);

                    for (int j = 0; j < 20; j++)
                    {
                        int x3 = r.Next(0, mOption.Width);
                        int y3 = r.Next(0, mOption.Height);
                        int radius = r.Next(0, 10);
                        g.FillEllipse(brush, x3, y3, radius, radius);

                        int x4 = r.Next(0, mOption.Width);
                        int y4 = r.Next(0, mOption.Height);
                        int radius2 = r.Next(0, 10);
                        g.FillRectangle(brush, x4, y4, radius2, radius2);

                        int x5 = r.Next(0, mOption.Width);
                        int y5 = r.Next(0, mOption.Height);
                        int radius3 = r.Next(1, 10);
                        g.FillPolygon(brush, new Point[] { new Point(x5, y5), new Point(x5 - radius3 / 2, y5 + radius3), new Point(x5 + radius3 / 2, y5 + radius3) });
                    }

                    g.DrawString("" + validationData[i], font, new SolidBrush(Color.Gray), x, y);
                    g.DrawString("" + validationData[i], font, brush, x - 1, y - 1);

                    for (int k = 0; k < 3; k++)
                    {
                        int x11 = r.Next(0, mOption.Width);
                        int y11 = r.Next(0, mOption.Height);
                        int x12 = r.Next(0, mOption.Width);
                        int y12 = r.Next(0, mOption.Height);
                        int width = r.Next(0, 2);
                        g.DrawLine(new Pen(color1, width), x11, y11, x12, y12);

                        int x21 = r.Next(0, mOption.Width);
                        int y21 = r.Next(0, mOption.Height);
                        int x22 = r.Next(0, mOption.Width);
                        int y22 = r.Next(0, mOption.Height);
                        int width2 = r.Next(0, 2);
                        g.DrawLine(new Pen(color2, width2), x21, y21, x22, y22);
                    }

                    offsetX = x + charSize.Width;
                }

                bmp.Save(stream, ImageFormat.Png);

                g.Dispose();
                bmp.Dispose();

                StringBuilder sbData = new StringBuilder();
                sbData.Append("data:image/png;base64,");
                byte[] pngBytes = stream.ToArray();
                string pngData = Convert.ToBase64String(pngBytes);
                sbData.Append(pngData);

                displayData = sbData.ToString();
            }

            return new CaptchaResult(validationData, displayData);
        }

        public bool Validate(object source, object input)
        {
            return source != null && input != null
                && string.Equals(source, input);
        }
    }
}
