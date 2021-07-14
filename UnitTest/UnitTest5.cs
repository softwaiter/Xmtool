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
    }
}
