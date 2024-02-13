using Ganss.Xss;

namespace CodeM.Common.Tools.Web
{
    public class HttpSecurity
    {
        private static HttpSecurity sHS = new HttpSecurity();
        public static HttpSecurity New()
        {
            return sHS;
        }

        private HtmlSanitizer mSanitizer = new HtmlSanitizer();

        public string Xss(string str)
        {
            return mSanitizer.Sanitize(str);
        }
    }
}
