namespace CodeM.Common.Tools
{
    public class SlidingCaptchaData : CaptchaData
    {
        public SlidingCaptchaData()
        { 
        }

        public SlidingCaptchaData(int x, int y)
        {
            GapX = x;
            GapY = y;
        }

        public int? GapX { get; set; } = null;
        public int? GapY { get; set; } = null;
    }
}
