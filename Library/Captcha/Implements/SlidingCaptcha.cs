using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeM.Common.Tools.Captcha.Implements
{
    internal class SlidingCaptcha : ICaptcha
    {
        private class GapTemplate
        {
            public GapTemplate(string holeFile, string sliderFile)
            {
                HoleImage = Image.Load<Rgba32>(holeFile);
                SliderImage = Image.Load<Rgba32>(sliderFile);
            }

            public Image<Rgba32> HoleImage { get; set; }
            public Image<Rgba32> SliderImage { get; set; }
        }

        private List<Image<Rgba32>> mBackgrounds = new List<Image<Rgba32>>();
        private List<GapTemplate> mGapTemplates = new List<GapTemplate>();
        private float mResultError = 0.02f;

        private Random mRandom = new Random((int)DateTime.Now.Ticks % int.MaxValue);

        public CaptchaKind Type
        {
            get
            {
                return CaptchaKind.Sliding;
            }
        }

        private void LoadResources(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException(path);
            }

            string backgroundPath = System.IO.Path.Combine(path, "backgrounds");
            if (!Directory.Exists(backgroundPath))
            {
                throw new DirectoryNotFoundException(backgroundPath, new Exception("背景图片存放路径未找到。"));
            }

            string templatePath = System.IO.Path.Combine(path, "templates");
            if (!Directory.Exists(templatePath))
            {
                throw new DirectoryNotFoundException(templatePath, new Exception("缺口图片存放路径未找到。"));
            }

            string[] files = Directory.GetFiles(backgroundPath, "*.jpg");
            foreach (string file in files)
            {
                mBackgrounds.Add(Image.Load<Rgba32>(file));
            }

            string[] dirs = Directory.GetDirectories(templatePath);
            foreach (string dir in dirs)
            {
                string holeFile = System.IO.Path.Combine(dir, "hole.png");
                string sliderFile = System.IO.Path.Combine(dir, "slider.png");
                if (File.Exists(holeFile) && File.Exists(sliderFile))
                { 
                    mGapTemplates.Add(new GapTemplate(holeFile, sliderFile));
                }
            }
        }

        private Image<Rgba32> GetRandomBackground()
        {
            if (mBackgrounds.Count == 0)
            {
                throw new Exception("");
            }

            int index = mRandom.Next(mBackgrounds.Count);
            return mBackgrounds[index];
        }

        private GapTemplate GetRandomGagTemplate()
        {
            if (mGapTemplates.Count == 0)
            {
                throw new Exception("");
            }

            int index = mRandom.Next(mGapTemplates.Count);
            return mGapTemplates[index];
        }

        /// <summary>
        /// 计算凹槽轮廓
        /// 原理： 一行一行扫描，每行不透明小方块连接形成数个小长方形（RectangularPolygon）。
        ///       多个RectangularPolygon形成ComplexPolygon，ComplexPolygon则代表图形的轮廓
        /// </summary>
        /// <param name="holeTemplateImage"></param>
        /// <returns></returns>
        private static ComplexPolygon CalcHoleShape(Image<Rgba32> holeTemplateImage)
        {
            int temp = 0;
            var pathList = new List<IPath>();
            holeTemplateImage.ProcessPixelRows(accessor =>
            {
                for (int y = 0; y < holeTemplateImage.Height; y++)
                {
                    var rowSpan = accessor.GetRowSpan(y);
                    for (int x = 0; x < rowSpan.Length; x++)
                    {
                        ref Rgba32 pixel = ref rowSpan[x];
                        if (pixel.A != 0)
                        {
                            if (temp == 0)
                            {
                                temp = x;
                            }
                        }
                        else
                        {
                            if (temp != 0)
                            {
                                pathList.Add(new RectangularPolygon(temp, y, x - temp, 1));
                                temp = 0;
                            }
                        }
                    }
                }
            });

            return new ComplexPolygon(new PathCollection(pathList));
        }

        public ICaptcha Config(CaptchaOption option)
        {
            if (!(option is SlidingCaptchaOption))
            {
                throw new ArgumentException("此处需要SlidingCaptchaOption类型的参数。");
            }

            SlidingCaptchaOption sco = (SlidingCaptchaOption)option;

            if (string.IsNullOrWhiteSpace(sco.Resource))
            {
                throw new ArgumentNullException("Resource不能为空。");
            }

            LoadResources(sco.Resource);

            mResultError = sco.ResultError;

            return this;
        }

        public CaptchaResult Generate(CaptchaData data = null)
        {
            Image<Rgba32> backgroundImage = GetRandomBackground();
            GapTemplate gapTemplate = GetRandomGagTemplate();

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
                randomX = scd.GapX ?? scd.GapX.Value;
                randomY = scd.GapY ?? scd.GapY.Value;
            }

            float percent = (float)Math.Round((decimal)randomX / (decimal)backgroundImage.Width);

            StringBuilder sbResult = new StringBuilder();
            sbResult.Append(backgroundImage.Width);
            sbResult.Append("|");
            sbResult.Append(backgroundImage.Height);

            // 生成背景
            backgroundImage.Mutate(x => x.DrawImage(gapTemplate.HoleImage, new Point(randomX, randomY), 1));
            sbResult.Append("|");
            sbResult.Append(backgroundImage.ToBase64String(SixLabors.ImageSharp.Formats.Png.PngFormat.Instance));

            // 根据透明度计算凹槽图轮廓形状(形状由不透明区域形成)
            ComplexPolygon holeShape = CalcHoleShape(gapTemplate.HoleImage);

            using Image holeMattingImage = new Image<Rgba32>(gapTemplate.SliderImage.Width, gapTemplate.SliderImage.Height);
            using Image sliderBarImage = new Image<Rgba32>(gapTemplate.SliderImage.Width, backgroundImage.Height);

            sbResult.Append("|");
            sbResult.Append(sliderBarImage.Width);
            sbResult.Append("|");
            sbResult.Append(sliderBarImage.Height);

            // 生成凹槽抠图
            holeMattingImage.Mutate(x =>
            {
                x.Clip(holeShape, p => p.DrawImage(backgroundImage, new Point(-randomX, -randomY), 1));
            });
            // 叠加拖块模板
            holeMattingImage.Mutate(x => x.DrawImage(gapTemplate.SliderImage, new Point(0, 0), 1));
            // 绘制拖块条
            sliderBarImage.Mutate(x => x.DrawImage(holeMattingImage, new Point(0, randomY), 1));
            sbResult.Append("|");
            sbResult.Append(sliderBarImage.ToBase64String(SixLabors.ImageSharp.Formats.Png.PngFormat.Instance));

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
