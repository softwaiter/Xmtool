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

        public SlidingCaptchaData(int x, int y, int template)
            : this(x, y)
        {
            GapTemplate = template;
        }

        public SlidingCaptchaData(int template)
        {
            GapTemplate = template;
        }

        /// <summary>
        /// 缺口位置横坐标
        /// </summary>
        public int? GapX { get; set; } = null;

        /// <summary>
        /// 缺口位置纵坐标
        /// </summary>
        public int? GapY { get; set; } = null;

        /// <summary>
        /// 缺口模板索引号（目前提供0~4）
        /// </summary>
        public int? GapTemplate { get; set; } = null;
    }
}
