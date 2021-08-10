using CodeM.Common.Tools;
using System;
using Xunit;
using Xunit.Abstractions;

namespace UnitTest
{
    public class UnitTest5
    {
        private ITestOutputHelper output;

        public UnitTest5(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Test()
        {
            DateTime dtNow = DateTime.Now;
            long ts = DateTimeUtils.GetUtcTimestamp10();
            Assert.True((ts + "").Length == 10);

            DateTime dt = DateTimeUtils.GetLocalDateTimeFromUtcTimestamp10(ts);
            Assert.Equal(dt.Year, dtNow.Year);
            Assert.Equal(dt.Month, dtNow.Month);
            Assert.Equal(dt.Day, dtNow.Day);
            Assert.Equal(dt.Hour, dtNow.Hour);
            Assert.Equal(dt.Minute, dtNow.Minute);
            Assert.Equal(dt.Second, dtNow.Second);
        }

        [Fact]
        public void Test2()
        {
            DateTime dtNow = DateTime.Now;
            long ts = DateTimeUtils.GetUtcTimestamp13();
            Assert.True((ts + "").Length == 13);

            DateTime dt = DateTimeUtils.GetLocalDateTimeFromUtcTimestamp13(ts);
            Assert.Equal(dt.Year, dtNow.Year);
            Assert.Equal(dt.Month, dtNow.Month);
            Assert.Equal(dt.Day, dtNow.Day);
            Assert.Equal(dt.Hour, dtNow.Hour);
            Assert.Equal(dt.Minute, dtNow.Minute);
            Assert.Equal(dt.Second, dtNow.Second);
        }

        [Fact]
        public void Test3()
        {
            string timespan = "1000ms";
            TimeSpan? ts = DateTimeUtils.GetTimeSpanFromString(timespan);
            Assert.NotNull(ts);
            Assert.True(ts.Value.TotalSeconds == 1);
        }

        [Fact]
        public void Test4()
        {
            string timespan = "1m";
            TimeSpan? ts = DateTimeUtils.GetTimeSpanFromString(timespan);
            Assert.NotNull(ts);
            Assert.True(ts.Value.TotalSeconds == 60);
        }

        [Fact]
        public void Test5()
        {
            string timespan = "1h";
            TimeSpan? ts = DateTimeUtils.GetTimeSpanFromString(timespan);
            Assert.NotNull(ts);
            Assert.True(ts.Value.TotalSeconds == 60 * 60);
        }

        [Fact]
        public void Test6()
        {
            string timespan = "15";
            TimeSpan? ts = DateTimeUtils.GetTimeSpanFromString(timespan);
            Assert.NotNull(ts);
            Assert.True(ts.Value.TotalSeconds == 15);
        }

        [Fact]
        public void Test7()
        {
            bool bRet = DateTimeUtils.CheckStringTimeSpan("1s");
            Assert.True(bRet);
        }

        [Fact]
        public void Test8()
        {
            try
            {
                bool bRet = DateTimeUtils.CheckStringTimeSpan("abc");
            }
            catch (Exception exp)
            {
                Assert.Contains("错误的时间段格式", exp.Message);
            }
        }

        [Fact]
        public void Test9()
        {
            bool bRet = DateTimeUtils.CheckStringTimeSpan("abc", false);
            Assert.False(bRet);
        }
    }
}
