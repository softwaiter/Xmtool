namespace CodeM.Common.Tools
{
    public class CharacterCaptchaData : CaptchaData
    {
        public CharacterCaptchaData()
        { 
        }

        public CharacterCaptchaData(string code)
        {
            Code = code;
        }

        public string Code { get; set; } = null;
    }
}
