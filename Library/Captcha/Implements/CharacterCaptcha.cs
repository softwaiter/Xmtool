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
        private int _length = 6;
        private bool _onlyNumber = true;
        private int _width = 300;
        private int _height = 125;
        private Color _backColor = Color.White;
        private Color _borderColor = Color.LightGray;

        /// <summary>
        /// 可顺序设置字符长度、是否只包含数字、宽度、高度、背景色、边框色6个参数；传入null即不设置，使用默认值
        /// </summary>
        /// <param name="args">length(默认6)、onlynumber(默认true)、width(默认300)、height(默认125)，backcolor(默认Color.White), bordercolor(默认Color.LightGray)</param>
        /// <returns></returns>
        public ICaptcha Config(params object[] args)
        {
            if (args.Length > 4)
            {
                throw new ArgumentException("最多可配置6个参数：length, onlynumber, width, height，backcolor, bordercolor。");
            }

            if (args.Length > 0 && args[0] != null)
            {
                if (args[0] is int)
                {
                    _length = (int)args[0];
                }
                else
                {
                    throw new ArgumentException("length必须是整型。");
                }
            }

            if (args.Length > 1 && args[1] != null)
            {
                if (args[1] is bool)
                {
                    _onlyNumber = (bool)args[1];
                }
                else
                {
                    throw new ArgumentException("onlynumber必须是布尔型。");
                }
            }

            if (args.Length > 2 && args[2] != null)
            {
                if (args[2] is int)
                {
                    _width = (int)args[2];
                }
                else
                {
                    throw new ArgumentException("width必须是整型。");
                }
            }

            if (args.Length > 3 && args[3] != null)
            {
                if (args[3] is int)
                {
                    _height = (int)args[3];
                }
                else
                {
                    throw new ArgumentException("height必须是整型。");
                }
            }

            if (args.Length > 4 && args[4] != null)
            {
                if (args[4] is Color)
                {
                    _backColor = (Color)args[4];
                }
                else
                {
                    throw new ArgumentException("backcolor必须是Color类型。");
                }
            }

            if (args.Length > 5 && args[5] != null)
            {
                if (args[5] is Color)
                {
                    _backColor = (Color)args[5];
                }
                else
                {
                    throw new ArgumentException("bordercolor必须是Color类型。");
                }
            }

            return this;
        }

        public string Generate(string initData = null)
        {
            StringBuilder sbResult = new StringBuilder();

            string code = initData;
            if (string.IsNullOrWhiteSpace(code))
            {
                code = RandomTool.New().RandomCaptcha(_length, _onlyNumber);
            }
            sbResult.Append(code).Append("|");

            using (MemoryStream stream = new MemoryStream())
            {
                Bitmap bmp = new Bitmap(_width, _height);

                Graphics g = Graphics.FromImage(bmp);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

                g.Clear(_backColor);
                Pen penBorder = new Pen(_borderColor, 1);
                g.DrawRectangle(penBorder, 0, 0, _width - 1, _height - 1);

                float fontSize = _height / 2.9f;
                Font font = new Font(SystemFonts.DefaultFont.FontFamily,
                    fontSize, FontStyle.Bold | FontStyle.Italic);
                SizeF charSize = g.MeasureString("G", font);
                int charWidth = _width / code.Length;
                int charHeight = (int)Math.Ceiling(charSize.Height);

                float offsetX = 0;
                Random r = new Random();
                for (int i = 0; i < code.Length; i++)
                {
                    float x = r.Next((int)offsetX, (int)Math.Max((i + 1) * charWidth - charSize.Width, 0));
                    float y = r.Next(0, _height - charHeight);

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
                    int lineCount = Math.Max(1, 10 / code.Length);

                    for (int j = 0; j < lineCount; j++)
                    {
                        int ly = r.Next(0, _height);
                        int bd = r.Next(-100, 100);
                        g.DrawBezier(pen, 0, ly, _width / 3, ly + bd, _width / 3 * 2, ly - bd, _width, ly);
                    }

                    g.DrawString("" + code[i], font, new SolidBrush(Color.Gray), x, y);
                    g.DrawString("" + code[i], font, brush, x - 1, y - 1);

                    offsetX = x + charSize.Width;
                }

                bmp.Save(stream, ImageFormat.Png);

                g.Dispose();
                bmp.Dispose();

                sbResult.Append("data:image/png;base64,");

                byte[] pngBytes = stream.ToArray();
                string pngData = Convert.ToBase64String(pngBytes);
                sbResult.Append(pngData);
            }

            return sbResult.ToString();
        }

        public bool Validate(string source, string input)
        {
            return string.Equals(source, input);
        }
    }
}
