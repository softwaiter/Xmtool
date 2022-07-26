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

                    int r1 = r.Next(0, 100);
                    int g1 = r.Next(0, 100);
                    int b1 = r.Next(0, 100);
                    Color color1 = Color.FromArgb(r1, g1, b1);
                    int r2 = r.Next(100, 188);
                    int g2 = r.Next(100, 188);
                    int b2 = r.Next(100, 188);
                    Color color2 = Color.FromArgb(r2, g2, b2);
                    Brush brush = new LinearGradientBrush(new PointF(x, y),
                        new PointF(x, y + font.Height), color1, color2);

                    Pen pen = new Pen(brush, 5.5f);
                    pen.DashStyle = DashStyle.DashDotDot;
                    int lineCount = Math.Max(1, 10 / validationData.Length);

                    for (int j = 0; j < lineCount; j++)
                    {
                        int ly = r.Next(0, mOption.Height);
                        int bd = r.Next(-100, 100);
                        g.DrawBezier(pen, 0, ly, mOption.Width / 3, ly + bd, mOption.Width / 3 * 2, ly - bd, mOption.Width, ly);
                    }

                    g.DrawString("" + validationData[i], font, new SolidBrush(Color.Gray), x, y);
                    g.DrawString("" + validationData[i], font, brush, x - 1, y - 1);

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
