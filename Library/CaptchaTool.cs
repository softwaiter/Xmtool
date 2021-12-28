using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;

namespace CodeM.Common.Tools
{
    public class CaptchaTool
    {
        private static char[] sCaptchaChars = {'2', '3', '4', '5', '6', '7', '8', '9',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N',
            'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b',
            'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'm', 'n', 'p', 'q', 'r',
            's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};
        private static Color sBackColor = Color.FromArgb(238, 238, 238);
        private static Color sBorderColor = Color.LightGray;

        internal static CaptchaTool New()
        {
            return new CaptchaTool();
        }

        private CaptchaTool()
        { 
        }

        public string Random(int len = 6)
        {
            string result = string.Empty;
            int step = (int)DateTime.Now.Ticks % int.MaxValue;
            Random r = new Random(step);
            for (int i = 0; i < len; i++)
            {
                int index = r.Next(0, int.MaxValue) % sCaptchaChars.Length;
                result += sCaptchaChars[index];
            }
            return result;
        }

        public string RandomOnlyNumber(int len = 6)
        {
            string result = string.Empty;
            int step = (int)DateTime.Now.Ticks % int.MaxValue;
            Random r = new Random(step);
            for (int i = 0; i < len; i++)
            {
                int num = r.Next(0, int.MaxValue) % 10;
                result += num;
            }
            return result;
        }

        public void Build(int width, int height, string code, out MemoryStream stream)
        {
            stream = new MemoryStream();

            Bitmap bmp = new Bitmap(width, height);

            Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            g.Clear(sBackColor);
            Pen penBorder = new Pen(sBorderColor, 1);
            g.DrawRectangle(penBorder, 0, 0, width - 1, height - 1);

            float fontSize = height / 2.9f;
            Font font = new Font(SystemFonts.DefaultFont.FontFamily,
                fontSize, FontStyle.Bold | FontStyle.Italic);
            SizeF charSize = g.MeasureString("G", font);
            int charWidth = width / code.Length;
            int charHeight = (int)Math.Ceiling(charSize.Height);

            float offsetX = 0;
            Random r = new Random();
            for (int i = 0; i < code.Length; i++)
            {
                float x = r.Next((int)offsetX, (int)(Math.Max((i + 1) * charWidth - charSize.Width, 0)));
                float y = r.Next(0, (int)(height - charHeight));

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

                Pen pen = new Pen(brush, 1);
                pen.DashStyle = DashStyle.DashDotDot;
                int lineCount = Math.Max(1, 10 / code.Length);
                for (int j = 0; j < lineCount; j++)
                {
                    int ly = r.Next(0, height);
                    int ly2 = r.Next(0, height);
                    g.DrawLine(pen, 0, ly, width, ly);
                    g.DrawLine(pen, 0, ly2, width, ly2);
                    g.DrawLine(pen, 0, ly, width, ly2);
                }

                g.DrawString("" + code[i], font, new SolidBrush(Color.Gray), x, y);
                g.DrawString("" + code[i], font, brush, x - 1, y - 1);

                offsetX = x + charSize.Width;
            }

            bmp.Save(stream, ImageFormat.Png);

            g.Dispose();
            bmp.Dispose();
        }

        public void Build(int width, int height, out MemoryStream stream,
            out string code, bool onlyNumber, int codeLength = 6)
        {
            if (onlyNumber)
            {
                code = RandomOnlyNumber(codeLength);
            }
            else
            {
                code = Random(codeLength);
            }
            Build(width, height, code, out stream);
        }
    }
}
