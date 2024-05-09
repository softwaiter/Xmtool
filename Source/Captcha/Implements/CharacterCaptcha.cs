using SkiaSharp;
using System;
using System.Drawing;
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

            SKBitmap bmp = new SKBitmap(mOption.Width, mOption.Height);

            SKCanvas canvas = new SKCanvas(bmp);
                
            canvas.Clear(new SKColor(mOption.BackColor.R, mOption.BackColor.G, mOption.BackColor.B, mOption.BackColor.A));

            SKPaint borderPaint = new SKPaint();
            borderPaint.IsAntialias = true;
            borderPaint.Color = new SKColor(mOption.BorderColor.R, mOption.BorderColor.G, mOption.BorderColor.B, mOption.BorderColor.A);
            borderPaint.IsStroke = true;
            borderPaint.StrokeWidth = 1;
            canvas.DrawRect(new SKRect(0, 0, mOption.Width - 1, mOption.Height - 1), borderPaint);

            float fontSize = mOption.Height / 2.9f;
            SKFont font = new SKFont(SKTypeface.FromFamilyName(SKTypeface.Default.FamilyName,
                SKFontStyleWeight.Bold, SKFontStyleWidth.Normal, SKFontStyleSlant.Italic));
            font.LinearMetrics = true;
            font.Edging = SKFontEdging.Antialias;
            font.Hinting = SKFontHinting.Full;
            font.Size = fontSize;

            SKPaint solidPaint = new SKPaint(font);
            solidPaint.Color = new SKColor(Color.Gray.R, Color.Gray.G, Color.Gray.B, Color.Gray.A);

            int cellWidth = mOption.Width / validationData.Length;
            int charWidth = (int)solidPaint.MeasureText("8");
            int charHeight = (int)Math.Floor(font.Metrics.CapHeight);

            float offsetX = 0;
            Random r = new Random();
            for (int i = 0; i < validationData.Length; i++)
            {
                float x = r.Next((int)offsetX, (int)Math.Max((i + 1) * cellWidth - charWidth, 0));
                float y = r.Next(charHeight, mOption.Height - charHeight);

                byte r1 = (byte)r.Next(0, 255);
                byte g1 = (byte)r.Next(0, 255);
                byte b1 = (byte)r.Next(0, 255);
                SKColor color1 = new SKColor(r1, g1, b1, 255);
                byte r2 = (byte)r.Next(0, 255);
                byte g2 = (byte)r.Next(0, 255);
                byte b2 = (byte)r.Next(0, 255);
                SKColor color2 = new SKColor(r2, g2, b2, 255);

                SKPaint gradientPaint = new SKPaint();
                gradientPaint.IsAntialias = true;
                gradientPaint.Shader = SKShader.CreateLinearGradient(new SKPoint(x, y),
                    new SKPoint(x, y + font.Metrics.CapHeight),
                    [color1, color2],
                    SKShaderTileMode.Mirror);

                for (int j = 0; j < 20; j++)
                {
                    int x3 = r.Next(0, mOption.Width);
                    int y3 = r.Next(0, mOption.Height);
                    int radiusX = r.Next(0, 10);
                    int radiusY = r.Next(0, 10);
                    canvas.DrawOval(new SKRect(x3, y3, x3 + radiusX, y3 + radiusY), gradientPaint);

                    int x4 = r.Next(0, mOption.Width);
                    int y4 = r.Next(0, mOption.Height);
                    int radius2 = r.Next(0, 10);
                    canvas.DrawRect(new SKRect(x4, y4, x4 + radius2, y4 + radius2), gradientPaint);

                    int x5 = r.Next(0, mOption.Width);
                    int y5 = r.Next(0, mOption.Height);
                    int radius3 = r.Next(1, 10);
                    float startAngle = r.Next(0, 360);
                    float sweepAngle = r.Next(0, 360);
                    canvas.DrawArc(new SKRect(x5, y5, x5 + radius3, y5 + radius3), startAngle, sweepAngle, true, gradientPaint);
                }

                canvas.DrawText("" + validationData[i], x, y, font, solidPaint);
                canvas.DrawText("" + validationData[i], x - 1, y - 1, font, gradientPaint);

                for (int k = 0; k < 3; k++)
                {
                    int x11 = r.Next(0, mOption.Width);
                    int y11 = r.Next(0, mOption.Height);
                    int x12 = r.Next(0, mOption.Width);
                    int y12 = r.Next(0, mOption.Height);
                    int width = r.Next(0, 2);

                    SKPaint line1Paint = new SKPaint();
                    line1Paint.IsAntialias = true;
                    line1Paint.Color = color1;
                    line1Paint.IsStroke = true;
                    line1Paint.StrokeWidth = width;
                    canvas.DrawLine(new SKPoint(x11, y11), new SKPoint(x12, y12), line1Paint);

                    int x21 = r.Next(0, mOption.Width);
                    int y21 = r.Next(0, mOption.Height);
                    int x22 = r.Next(0, mOption.Width);
                    int y22 = r.Next(0, mOption.Height);
                    int width2 = r.Next(0, 2);

                    SKPaint line2Paint = new SKPaint();
                    line2Paint.IsAntialias = true;
                    line2Paint.Color = color2;
                    line2Paint.IsStroke = true;
                    line2Paint.StrokeWidth = width2;
                    canvas.DrawLine(new SKPoint(x21, y21), new SKPoint(x22, y22), line2Paint);
                }

                offsetX = x + charWidth;
            }

            Stream stream = bmp.Encode(SKEncodedImageFormat.Png, 100).AsStream();
            canvas.Dispose();
            bmp.Dispose();

            StringBuilder sbData = new StringBuilder();
            sbData.Append("data:image/png;base64,");

            byte[] pngBytes = new byte[stream.Length];
            stream.Read(pngBytes, 0, pngBytes.Length);
            stream.Dispose();

            string pngData = Convert.ToBase64String(pngBytes);
            sbData.Append(pngData);

            return new CaptchaResult(validationData, sbData.ToString());
        }

        public bool Validate(object source, object input)
        {
            return source != null && input != null
                && string.Equals(source, input);
        }
    }
}
