using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace CodeM.Common.Tools.Json
{
    public class JsonConfigParser
    {
        private List<string> mJsonFiles = new List<string>();

        private JsonConfigParser()
        { 
        }

        internal static JsonConfigParser New()
        {
            return new JsonConfigParser();
        }

        public JsonConfigParser AddJsonFile(string path)
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
            JsonDocumentOptions jdo = new JsonDocumentOptions();
            jdo.AllowTrailingCommas = true;
            jdo.CommentHandling = JsonCommentHandling.Skip;

            DynamicObjectExt result = new DynamicObjectExt();
            foreach (string file in mJsonFiles)
            {
                using (StreamReader sr = new StreamReader(file, Encoding.UTF8))
                {
                    string content = sr.ReadToEnd();
                    JsonDocument doc = JsonDocument.Parse(content, jdo);
                    JsonElement rootEl = doc.RootElement;
                    BindConfigObject(result, rootEl);
                }
            }
            if (!string.IsNullOrEmpty(jsonStr))
            {
                JsonDocument doc = JsonDocument.Parse(jsonStr, jdo);
                JsonElement rootEl = doc.RootElement;
                BindConfigObject(result, rootEl);
            }
            return result;
        }

        private void BindConfigObject(DynamicObjectExt configObj, JsonElement element, string key = null)
        {
            if (configObj == null)
            {
                return;
            }

            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        DynamicObjectExt subConfigObj = new DynamicObjectExt();
                        BindConfigObject(subConfigObj, element);
                        configObj.TrySetValue(key, subConfigObj);
                    }
                    else
                    {
                        JsonElement.ObjectEnumerator oe = element.EnumerateObject();
                        while (oe.MoveNext())
                        {
                            BindConfigObject(configObj, oe.Current.Value, oe.Current.Name);
                        }
                    }
                    break;
                case JsonValueKind.Array:
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        List<object> _list = new List<object>();

                        JsonElement.ArrayEnumerator ae = element.EnumerateArray();
                        while (ae.MoveNext())
                        {
                            switch (ae.Current.ValueKind)
                            {
                                case JsonValueKind.String:
                                    _list.Add(ae.Current.GetString());
                                    break;
                                case JsonValueKind.Number:
                                    long value;
                                    if (ae.Current.TryGetInt64(out value))
                                    {
                                        if (value > Int32.MaxValue)
                                        {
                                            _list.Add(value);
                                        }
                                        else if (value > Int16.MaxValue)
                                        {
                                            _list.Add((Int32)value);
                                        }
                                        else
                                        {
                                            _list.Add((Int16)value);
                                        }
                                    }
                                    else
                                    {
                                        _list.Add(ae.Current.GetDouble());
                                    }
                                    break;
                                case JsonValueKind.True:
                                case JsonValueKind.False:
                                    _list.Add(ae.Current.GetBoolean());
                                    break;
                                case JsonValueKind.Null:
                                    _list.Add(null);
                                    break;
                                case JsonValueKind.Object:
                                case JsonValueKind.Array:
                                    DynamicObjectExt itemObj = new DynamicObjectExt();
                                    BindConfigObject(itemObj, ae.Current);
                                    _list.Add(itemObj);
                                    break;
                            }
                        }

                        configObj.TrySetValue(key, _list);
                    }
                    break;
                case JsonValueKind.String:
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        configObj.TrySetValue(key, element.GetString());
                    }
                    break;
                case JsonValueKind.Number:
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        long value;
                        if (element.TryGetInt64(out value))
                        {
                            if (value > Int32.MaxValue)
                            {
                                configObj.TrySetValue(key, value);
                            }
                            else if (value > Int16.MaxValue)
                            {
                                configObj.TrySetValue(key, (Int32)value);
                            }
                            else
                            {
                                configObj.TrySetValue(key, (Int16)value);
                            }
                        }
                        else
                        {
                            configObj.TrySetValue(key, element.GetDouble());
                        }
                    }
                    break;
                case JsonValueKind.True:
                case JsonValueKind.False:
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        configObj.TrySetValue(key, element.GetBoolean());
                    }
                    break;
                case JsonValueKind.Null:
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        configObj.TrySetValue(key, null);
                    }
                    break;
            }
        }
    }
}
