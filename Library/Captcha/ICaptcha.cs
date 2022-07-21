namespace CodeM.Common.Tools
{
    public enum CaptchKind
    {
        Character,
        Sliding
    }

    public interface ICaptcha
    {
        public ICaptcha Config(params object[] args);

        public string Generate(string initData = null);

        public bool Validate(string source, string input);
    }
}
