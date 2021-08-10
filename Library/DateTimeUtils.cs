using System;

namespace CodeM.Common.Tools
{
    public class DateTimeUtils
    {
        public static long GetUtcTimestamp10()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            return (long)ts.TotalSeconds;
        }

        public static long GetUtcTimestamp13()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            return (long)ts.TotalMilliseconds;
        }

        public static DateTime GetLocalDateTimeFromUtcTimestamp10(long ts)
        {
            DateTime st = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1, 0, 0, 0), TimeZoneInfo.Local);
            return st.AddSeconds(ts);
        }

        public static DateTime GetLocalDateTimeFromUtcTimestamp13(long ts)
        {
            DateTime st = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1, 0, 0, 0), TimeZoneInfo.Local);
            return st.AddMilliseconds(ts);
        }

        public static TimeSpan GetTimeSpanFromString(string timespan)
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

            throw new Exception("不识别的时间范围：" + timespan);
        }
    }
}
