using CodeM.Common.Tools.Json;
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
            Json2DynamicParser parser = new Json2DynamicParser();
            dynamic configObj = parser.AddJsonFile(path).Parse();
            Assert.True(configObj.Has("User"));
        }

        [Fact]
        public void LoadTowConfig()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "appsettings.json");
            string path2 = Path.Combine(Environment.CurrentDirectory, "appsettings.Development.json");
            Json2DynamicParser parser = new Json2DynamicParser();
            dynamic configObj = parser.AddJsonFile(path).AddJsonFile(path2).Parse();
            Assert.Equal(configObj.Test, "This is a example.");
        }

        [Fact]
        public void HasPath()
        { 
            string path = Path.Combine(Environment.CurrentDirectory, "appsettings.json");
            Json2DynamicParser parser = new Json2DynamicParser();
            dynamic configObj = parser.AddJsonFile(path).Parse();

            bool ret = configObj.HasPath("User");
            Assert.True(ret);
            ret = configObj.HasPath("User.Name");
            Assert.True(ret);

            ret = configObj.HasPath("NotHave");
            Assert.False(ret);
            ret = configObj.HasPath("User.NotHave");
            Assert.False(ret);
        }

        [Fact]
        public void RemovePath()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "appsettings.json");
            Json2DynamicParser parser = new Json2DynamicParser();
            dynamic configObj = parser.AddJsonFile(path).Parse();

            bool ret = configObj.HasPath("User.Name");
            Assert.True(ret);
            configObj.RemovePath("User.Name");
            ret = configObj.HasPath("User.Name");
            Assert.False(ret);

            ret = configObj.HasPath("User");
            Assert.True(ret);
            configObj.RemovePath("User");
            ret = configObj.HasPath("User");
            Assert.False(ret);
        }

        [Fact]
        public void SetValueByPath()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "appsettings.json");
            Json2DynamicParser parser = new Json2DynamicParser();
            dynamic configObj = parser.AddJsonFile(path).Parse();

            Assert.Equal(configObj.User.Name, "Wangxm");

            configObj.SetValueByPath("User.Name", "Huxy");
            Assert.Equal(configObj.User.Name, "Huxy");

            configObj.SetValueByPath("Company.Name", "MYWS");
            Assert.Equal(configObj.Company.Name, "MYWS");
        }
    }
}
