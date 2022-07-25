namespace CodeM.Common.Tools
{
    public enum CaptchaKind
    {
        Character,
        Sliding
    }

    public interface ICaptcha
    {
        public ICaptcha Config(CaptchaOption option);

        public CaptchaResult Generate(CaptchaData data = null);

        public bool Validate(object source, object input);
    }
}
