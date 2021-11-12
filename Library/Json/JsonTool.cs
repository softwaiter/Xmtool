namespace CodeM.Common.Tools.Json
{
    public class JsonTool
    {
        private static JsonTool sJTool = new JsonTool();

        private JsonTool()
        { 
        }

        internal static JsonTool New()
        {
            return sJTool;
        }

        public JsonConfigParser ConfigParser()
        {
            return JsonConfigParser.New();
        }
    }
}
