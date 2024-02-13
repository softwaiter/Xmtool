using CodeM.Common.Tools.Captcha;
using CodeM.Common.Tools.DynamicObject;
using CodeM.Common.Tools.Json;
using CodeM.Common.Tools.Oss;
using CodeM.Common.Tools.Sms;
using CodeM.Common.Tools.Web;
using CodeM.Common.Tools.Xml;

namespace CodeM.Common.Tools
{
    public class Xmtool
    {
        public static DynamicObjectExt DynamicObject()
        {
            return new DynamicObjectExt();
        }

        public static JsonTool Json
        {
            get
            {
                return JsonTool.New();
            }
        }

        public static WebTool Web
        {
            get
            {
                return WebTool.New();
            }
        }

        public static DateTimeTool DateTime()
        {
            return DateTimeTool.New();
        }

        public static CryptoTool Crypto()
        {
            return CryptoTool.New();
        }

        public static HashTool Hash()
        {
            return HashTool.New();
        }

        public static RegexTool Regex()
        {
            return RegexTool.New();
        }

        public static TypeTool Type()
        {
            return TypeTool.New();
        }

        public static XmlTool Xml()
        {
            return XmlTool.New();
        }

        public static MailTool Mail(string host, int port, string account, string password, bool enableSsl = false)
        {
            return MailTool.New(host, port, enableSsl, account, password);
        }

        public static RandomTool Random()
        {
            return RandomTool.New();
        }

        public static ICaptcha Captcha(CaptchaKind kind)
        {
            return CaptchaTool.New(kind);
        }

        public static ISmsProvider Sms(SmsProvider provider)
        {
            return SmsTool.New(provider);
        }

        public static IOssProvider Oss(OssProvider provider)
        {
            return OssTool.New(provider);
        }

        public static ImageTool Image()
        {
            return ImageTool.New();
        }
    }
}
