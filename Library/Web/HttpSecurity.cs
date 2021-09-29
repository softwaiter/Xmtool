using Ganss.XSS;

namespace CodeM.Common.Tools.Web
{
    public class HttpSecurity
    {
        private HtmlSanitizer mSanitizer = new HtmlSanitizer();

        public string Xss(string str)
        {
            return mSanitizer.Sanitize(str);
        }
    }
}
