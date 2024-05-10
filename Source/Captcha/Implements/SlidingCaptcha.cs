using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CodeM.Common.Tools.Captcha.Implements
{
    internal class SlidingCaptcha : ICaptcha
    {
        private class GapTemplate
        {
            public GapTemplate(string holeFile, string sliderFile)
            {
                HoleImage = SKImage.FromEncodedData(holeFile);
                SliderImage = SKImage.FromEncodedData(sliderFile);
            }

            public GapTemplate(SKImage holeImage, SKImage sliderImage)
            {
                HoleImage = holeImage;
                SliderImage = sliderImage;
            }

            public SKImage HoleImage { get; set; }
            public SKImage SliderImage { get; set; }
        }

        private List<SKImage> mBackgrounds = new List<SKImage>();
        private List<GapTemplate> mGapTemplates = new List<GapTemplate>();
        private float mResultError = 0.02f;

        private Random mRandom = new Random((int)DateTime.Now.Ticks % int.MaxValue);

        public SlidingCaptcha()
        {
            LoadTemplates();
        }

        private const int TEMPLATE_COUNT = 5;
        private void LoadTemplates()
        {
            Assembly assembly = GetType().Assembly;

            for (int i = 0; i < TEMPLATE_COUNT; i++)
            {
                SKImage holdImage, sliderImage;

                string holeResource = string.Concat("CodeM.Common.Tools.Captcha.Slider_Templates._", (i + 1), ".hole.png");
                using (Stream holeStream = assembly.GetManifestResourceStream(holeResource))
                {
                    holdImage = SKImage.FromEncodedData(SKData.Create(holeStream));
                }

                string sliderResource = string.Concat("CodeM.Common.Tools.Captcha.Slider_Templates._", (i + 1), ".slider.png");
                using (Stream sliderStream = assembly.GetManifestResourceStream(sliderResource))
                {
                    sliderImage = SKImage.FromEncodedData(SKData.Create(sliderStream));
                }

                mGapTemplates.Add(new GapTemplate(holdImage, sliderImage));
            }
        }

        public CaptchaKind Type
        {
            get
            {
                return CaptchaKind.Sliding;
            }
        }

        private void LoadBackgrounds(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException("滑动验证码背景图片存放路径不存在");
            }

            string[] files = Directory.GetFiles(path, "*.jpg");
            foreach (string file in files)
            {
                mBackgrounds.Add(SKImage.FromEncodedData(file));
            }
        }

        private SKImage GetRandomBackground()
        {
            if (mBackgrounds.Count == 0)
            {
                throw new Exception("没有找到任何背景文件。");
            }

            int index = mRandom.Next(mBackgrounds.Count);
            return SKImage.FromEncodedData(mBackgrounds[index].Encode());
        }

        private GapTemplate GetRandomGagTemplate(CaptchaData data = null)
        {
            if (mGapTemplates.Count == 0)
            {
                throw new Exception("没有找到任何缺口模板资源。");
            }

            int index = mRandom.Next(mGapTemplates.Count);

            if (data != null)
            {
                if (!(data is SlidingCaptchaData))
                {
                    throw new ArgumentException("此处需要SlidingCaptchaData类型的参数。");
                }

                SlidingCaptchaData scd = (SlidingCaptchaData)data;
                if (scd.GapTemplate != null)
                {
                    if (scd.GapTemplate >= 0 &&
                        scd.GapTemplate < mGapTemplates.Count)
                    {
                        index = scd.GapTemplate.Value;
                    }
                    else
                    {
                        throw new ArgumentException(string.Concat("GapTemplate只能取值0-", mGapTemplates.Count - 1, "。"));
                    }
                }
            }
            
            return mGapTemplates[index];
        }

        // <summary>
        // 计算凹槽轮廓
        // </summary>
        // <param name = "holeTemplateImage" ></ param >
        // < returns ></ returns >
        private static SKPath CalcHoleShape(SKImage holeTemplateImage)
        {
            SKPath result = new SKPath();

            using (SKBitmap bmpHole = SKBitmap.FromImage(holeTemplateImage))
            {
                for (int y = 0; y < bmpHole.Height; y++)
                {
                    int temp = -1;
                    for (int x = 0; x < bmpHole.Width; x++)
                    {
                        SKColor pixelColor = bmpHole.GetPixel(x, y);

                        if (pixelColor.Alpha != 0)
                        {
                            if (temp == -1)
                            {
                                temp = x;
                            }
                        }
                        else
                        {
                            if (temp != -1)
                            {
                                result.AddRect(new SKRect(temp, y, x - 1, y + 1));
                                temp = -1;
                            }
                        }
                    }
                }
            }

            return result;
        }

        public ICaptcha Config(CaptchaOption option)
        {
            if (!(option is SlidingCaptchaOption))
            {
                throw new ArgumentException("此处需要SlidingCaptchaOption类型的参数。");
            }

            SlidingCaptchaOption sco = (SlidingCaptchaOption)option;

            if (string.IsNullOrWhiteSpace(sco.BackgroundDir))
            {
                throw new ArgumentNullException("参数BackgroundDir不能为空。");
            }

            LoadBackgrounds(sco.BackgroundDir);

            mResultError = sco.ResultError;

            return this;
        }

        public CaptchaResult Generate(CaptchaData data = null)
        {
            using SKImage backgroundImage = GetRandomBackground();
            GapTemplate gapTemplate = GetRandomGagTemplate(data);

            // 凹槽位置
            int randomX = mRandom.Next(gapTemplate.HoleImage.Width + 5, backgroundImage.Width - gapTemplate.HoleImage.Width - 10);
            int randomY = mRandom.Next(5, backgroundImage.Height - gapTemplate.HoleImage.Height - 5);

            if (data != null)
            {
                if (!(data is SlidingCaptchaData))
                {
                    throw new ArgumentException("此处需要SlidingCaptchaData类型的参数。");
                }

                SlidingCaptchaData scd = (SlidingCaptchaData)data;
                randomX = scd.GapX ?? randomX;
                randomY = scd.GapY ?? randomY;
            }

            float percent = (float)Math.Round((decimal)randomX / (decimal)backgroundImage.Width, 2);

            StringBuilder sbResult = new StringBuilder();
            sbResult.Append(backgroundImage.Width);
            sbResult.Append("|");
            sbResult.Append(backgroundImage.Height);

            //// 生成带凹槽背景
            SKBitmap bmpBackground = new SKBitmap(backgroundImage.Width, backgroundImage.Height);
            SKCanvas cBackground = new SKCanvas(bmpBackground);
            cBackground.DrawImage(backgroundImage, 0, 0);
            cBackground.DrawImage(gapTemplate.HoleImage, randomX, randomY);

            //// 增加迷惑性凹槽
            int randomY2;
            if (randomY > 1.5 * gapTemplate.HoleImage.Height)
            {
                randomY2 = (randomY - gapTemplate.HoleImage.Height) -
                    mRandom.Next(5, Math.Max(5, randomY - 2 * gapTemplate.HoleImage.Height));
            }
            else
            {
                randomY2 = (randomY + gapTemplate.HoleImage.Height) +
                    mRandom.Next(5, Math.Max(5, backgroundImage.Height - randomY - 2 * gapTemplate.HoleImage.Height - 5));
            }
            cBackground.DrawImage(gapTemplate.HoleImage, randomX, randomY2);  

            sbResult.Append("|data:image/png;base64,");
            sbResult.Append(ImageTool.New().ToBase64(bmpBackground.Encode(SKEncodedImageFormat.Png, 100).AsStream()));

            cBackground.Dispose();
            bmpBackground.Dispose();

            //// 根据透明度计算凹槽图轮廓形状(形状由不透明区域形成)
            SKPath holeShape = CalcHoleShape(gapTemplate.HoleImage);

            SKBitmap bmpHoleMatting = new SKBitmap(gapTemplate.SliderImage.Width, gapTemplate.SliderImage.Height);
            SKCanvas cHoleMatting = new SKCanvas(bmpHoleMatting);
            SKBitmap bmpSliderBar = new SKBitmap(gapTemplate.SliderImage.Width, backgroundImage.Height);
            SKCanvas cSliderBar = new SKCanvas(bmpSliderBar);

            sbResult.Append("|");
            sbResult.Append(bmpSliderBar.Width);
            sbResult.Append("|");
            sbResult.Append(bmpSliderBar.Height);

            // 生成凹槽抠图
            cHoleMatting.ClipPath(holeShape, SKClipOperation.Intersect, true);
            cHoleMatting.DrawImage(backgroundImage, -randomX, -randomY);

            // 叠加拖块模板
            cHoleMatting.DrawImage(gapTemplate.SliderImage, 0, 0);

            // 绘制拖块条
            cSliderBar.DrawBitmap(bmpHoleMatting, 0, randomY);

            sbResult.Append("|data:image/png;base64,");
            sbResult.Append(ImageTool.New().ToBase64(bmpSliderBar.Encode(SKEncodedImageFormat.Png, 100).AsStream()));

            cHoleMatting.Dispose();
            bmpHoleMatting.Dispose();

            cSliderBar.Dispose();
            bmpSliderBar.Dispose();

            return new CaptchaResult(percent.ToString(), sbResult.ToString());
        }

        public bool Validate(object source, object input)
        {
            float validation;
            if (source is float)
            {
                validation = (float)source;
            }
            else if (source is string)
            {
                if (!float.TryParse(source + "", out validation))
                {
                    throw new ArgumentException("source必须是浮点数。");
                }
            }
            else
            {
                throw new ArgumentException("source必须是浮点数。");
            }

            float user;
            if (input is float)
            {
                user = (float)input;
            }
            else if (input is string)
            {
                if (!float.TryParse(input + "", out user))
                {
                    throw new ArgumentException("input必须是浮点数。");
                }
            }
            else
            {
                throw new ArgumentException("input必须是浮点数。");
            }

            int digits = Math.Max(0, mResultError.ToString().Length - mResultError.ToString().IndexOf(".") - 1);
            float error = (float)Math.Round(Math.Abs(user - validation), digits);
            return error <= mResultError;
        }
    }
}
