namespace CodeM.Common.Tools
{
    public class CaptchaResult
    {
        public CaptchaResult()
        { 
        }

        public CaptchaResult(string validationData, string displayData)
        {
            ValidationData = validationData;
            DisplayData = displayData;
        }

        public string ValidationData { get; set; }
        public string DisplayData { get; set; }
    }
}
