using System.Drawing;

namespace CodeM.Common.Tools
{
    public class CharacterCaptchaOption : CaptchaOption
    {
        public CharacterCaptchaOption()
        { 
        }

        public CharacterCaptchaOption(int length, bool onlyNumber,
            int width, int height, Color backColor, Color borderColor)
        {
            Length = length;
            OnlyNumber = onlyNumber;
            Width = width;
            Height = height;
            BackColor = backColor;
            BorderColor = borderColor;
        }

        public int Length { get; set; } = 6;

        public bool OnlyNumber { get; set; } = true;

        public int Width { get; set; } = 300;

        public int Height { get; set; } = 120;

        public Color BackColor { get; set; } = Color.White;

        public Color BorderColor { get; set; } = Color.LightGray;
    }
}
