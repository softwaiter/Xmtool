using CodeM.Common.Tools.Config;
using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace UnitTest
{
    public class UnitTest2
    {
        private ITestOutputHelper output;

        public UnitTest2(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void LoadOneConfig()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "appsettings.json");
            JsonConfigParser parser = new JsonConfigParser();
            dynamic configObj = parser.AddJsonFile(path).Parse();
            Assert.True(configObj.Has("User"));
        }

        [Fact]
        public void LoadTowConfig()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "appsettings.json");
            string path2 = Path.Combine(Environment.CurrentDirectory, "appsettings.Development.json");
            JsonConfigParser parser = new JsonConfigParser();
            dynamic configObj = parser.AddJsonFile(path).AddJsonFile(path2).Parse();
            Assert.Equal(configObj.Test, "This is a example.");
        }
    }
}
