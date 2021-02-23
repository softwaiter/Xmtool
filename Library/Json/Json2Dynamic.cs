using Swifter.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeM.Common.Tools.Json
{
    public class Json2Dynamic
    {
        private List<string> mJsonFiles = new List<string>();
        public dynamic CreateObject()
        {
            return new JsonDynamicObject();
        }

        public Json2Dynamic AddJsonFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("文件未找到", path);
            }
            mJsonFiles.Add(path);
            return this;
        }

        public dynamic Parse(string jsonStr = null)
        {
            JsonDynamicObject result = new JsonDynamicObject();
            foreach (string file in mJsonFiles)
            {
                using (StreamReader sr = new StreamReader(file, Encoding.UTF8))
                {
                    dynamic jsonObj = JsonFormatter.DeserializeObject<dynamic>(sr.ReadToEnd());
                    BindConfigObject(result, jsonObj);
                }
            }
            if (!string.IsNullOrEmpty(jsonStr))
            {
                dynamic jsonObj = JsonFormatter.DeserializeObject<dynamic>(jsonStr);
                BindConfigObject(result, jsonObj);
            }
            return result;
        }

        private void BindConfigObject(JsonDynamicObject configObj, dynamic jsonObj, string key = null)
        {
            if (configObj == null)
            {
                return;
            }

            if (jsonObj == null)
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    configObj.TrySetValue(key, null);
                }
                return;
            }

            Type _typ = jsonObj.GetType();
            if (_typ == typeof(Dictionary<string, object>))
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    JsonDynamicObject subConfigObj = new JsonDynamicObject();
                    BindConfigObject(subConfigObj, jsonObj);
                    configObj.TrySetValue(key, subConfigObj);
                }
                else
                {
                    foreach (string jsonKey in jsonObj.Keys)
                    {
                        dynamic value = jsonObj[jsonKey];
                        BindConfigObject(configObj, value, jsonKey);
                    }
                }
            }
            else if (_typ == typeof(List<object>))
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    List<object> _list = new List<object>();
                    foreach (dynamic item in jsonObj)
                    {
                        Type _itemTyp = item.GetType();
                        if (_itemTyp == typeof(Dictionary<string, object>) ||
                            _itemTyp == typeof(List<object>))
                        {
                            JsonDynamicObject itemObj = new JsonDynamicObject();
                            BindConfigObject(itemObj, item);
                            _list.Add(itemObj);
                        }
                        else
                        {
                            _list.Add(item);
                        }
                    }
                    configObj.TrySetValue(key, _list);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    configObj.TrySetValue(key, jsonObj);
                }
            }
        }
    }
}
