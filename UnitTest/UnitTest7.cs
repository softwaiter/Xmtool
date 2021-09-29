using CodeM.Common.Tools;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace UnitTest
{
    public class UnitTest7
    {
        private ITestOutputHelper output;

        public UnitTest7(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Test()
        {
            List<string> lst = new List<string>();
            Assert.True(TypeUtils.IsList(lst));
        }
    }
}
