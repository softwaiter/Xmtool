using System;

namespace CodeM.Common.Tools
{
    public class DateTimeTool
    {
        private static DateTimeTool sDTTool = new DateTimeTool();

        private DateTimeTool()
        { 
        }

        internal static DateTimeTool New()
        {
            return sDTTool;
        }

        public long GetUtcTimestamp10()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            return (long)ts.TotalSeconds;
        }

        public long GetUtcTimestamp10(DateTime datetime)
        {
            TimeSpan ts = datetime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0);
            return (long)ts.TotalSeconds;
        }

        public long GetUtcTimestamp13()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            return (long)ts.TotalMilliseconds;
        }

        public long GetUtcTimestamp13(DateTime datetime)
        {
            TimeSpan ts = datetime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0);
            return (long)ts.TotalMilliseconds;
        }

        public DateTime GetUtcDateTimeFromUtcTimestamp10(long ts)
        {
            DateTime st = new DateTime(1970, 1, 1, 0, 0, 0);
            return st.AddSeconds(ts);
        }

        public DateTime GetUtcDateTimeFromUtcTimestamp13(long ts)
        {
            DateTime st = new DateTime(1970, 1, 1, 0, 0, 0);
            return st.AddMilliseconds(ts);
        }

        public DateTime GetLocalDateTimeFromUtcTimestamp10(long ts)
        {
            DateTime st = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1, 0, 0, 0), TimeZoneInfo.Local);
            return st.AddSeconds(ts);
        }

        public DateTime GetLocalDateTimeFromUtcTimestamp13(long ts)
        {
            DateTime st = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1, 0, 0, 0), TimeZoneInfo.Local);
            return st.AddMilliseconds(ts);
        }

        public TimeSpan? GetTimeSpanFromString(string timespan, bool throwError = true)
        {
            if (!string.IsNullOrWhiteSpace(timespan))
            {
                int value;
                string trimedTime = timespan.Trim().ToLower();
                if (trimedTime.EndsWith("ms"))
                {
                    trimedTime = trimedTime.Substring(0, trimedTime.Length - 2);
                    if (int.TryParse(trimedTime, out value))
                    {
                        return TimeSpan.FromMilliseconds(value);
                    }
                }
                else if (trimedTime.EndsWith("s"))
                {
                    trimedTime = trimedTime.Substring(0, trimedTime.Length - 1);
                    if (int.TryParse(trimedTime, out value))
                    {
                        return TimeSpan.FromSeconds(value);
                    }
                }
                else if (trimedTime.EndsWith("m"))
                {
                    trimedTime = trimedTime.Substring(0, trimedTime.Length - 1);
                    if (int.TryParse(trimedTime, out value))
                    {
                        return TimeSpan.FromMinutes(value);
                    }
                }
                else if (trimedTime.EndsWith("h"))
                {
                    trimedTime = trimedTime.Substring(0, trimedTime.Length - 1);
                    if (int.TryParse(trimedTime, out value))
                    {
                        return TimeSpan.FromHours(value);
                    }
                }
                else if (trimedTime.EndsWith("d"))
                {
                    trimedTime = trimedTime.Substring(0, trimedTime.Length - 1);
                    if (int.TryParse(trimedTime, out value))
                    {
                        return TimeSpan.FromDays(value);
                    }
                }
                else
                {
                    if (int.TryParse(trimedTime, out value))
                    {
                        return TimeSpan.FromSeconds(value);
                    }
                }
            }

            if (throwError)
            {
                throw new Exception("不识别的时间范围：" + timespan);
            }
            else
            {
                return null;
            }
        }

        public bool CheckStringTimeSpan(string timespan, bool throwError = true)
        {
            bool result = true;

            if (!string.IsNullOrWhiteSpace(timespan))
            {
                int value;
                string trimedTime = timespan.Trim().ToLower();
                if (trimedTime.EndsWith("ms"))
                {
                    trimedTime = trimedTime.Substring(0, trimedTime.Length - 2);
                    result = int.TryParse(trimedTime, out value);
                }
                else if (trimedTime.EndsWith("s") || trimedTime.EndsWith("m") ||
                    trimedTime.EndsWith("h") || trimedTime.EndsWith("d"))
                {
                    trimedTime = trimedTime.Substring(0, trimedTime.Length - 1);
                    result = int.TryParse(trimedTime, out value);
                }
                else
                {
                    result = int.TryParse(trimedTime, out value);
                }

                if (!result)
                {
                    if (throwError)
                    {
                        throw new Exception(string.Concat("错误的时间段格式，单位仅支持ms、s、m、h、d：", timespan));
                    }
                }
                else
                {
                    if (value < 0)
                    {
                        if (throwError)
                        {
                            throw new Exception(string.Concat("时间段必须大于等于0：", timespan));
                        }
                        else
                        {
                            result = false;
                        }
                    }
                }
            }
            else
            {
                if (throwError)
                {
                    throw new Exception("时间段不能设置空值。");
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }
    }
}
