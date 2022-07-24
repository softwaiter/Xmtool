namespace CodeM.Common.Tools
{
    public class SlidingCaptchaOption : CaptchaOption
    {
        public SlidingCaptchaOption()
        { 
        }

        public SlidingCaptchaOption(string resource)
        {
            Resource = resource;
        }

        public SlidingCaptchaOption(string resource, float resultError)
            : this(resource)
        {
            ResultError = resultError;
        }

        public string Resource { get; set; }

        public float ResultError { get; set; } = 0.02f;
    }
}
