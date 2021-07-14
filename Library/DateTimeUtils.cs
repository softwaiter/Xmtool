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
    }
}
