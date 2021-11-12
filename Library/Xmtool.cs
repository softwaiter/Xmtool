using CodeM.Common.Tools.Json;
using CodeM.Common.Tools.Web;
using CodeM.Common.Tools.Xml;

namespace CodeM.Common.Tools
{
    public class Xmtool
    {
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
    }
}
