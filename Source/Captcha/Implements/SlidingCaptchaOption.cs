namespace CodeM.Common.Tools
{
    public class SlidingCaptchaOption : CaptchaOption
    {
        public SlidingCaptchaOption()
        { 
        }

        public SlidingCaptchaOption(string backgroundDir)
        {
            BackgroundDir = backgroundDir;
        }

        public SlidingCaptchaOption(string backgroundDir, float resultError)
            : this(backgroundDir)
        {
            ResultError = resultError;
        }

        public string BackgroundDir { get; set; }

        public float ResultError { get; set; } = 0.02f;
    }
}
