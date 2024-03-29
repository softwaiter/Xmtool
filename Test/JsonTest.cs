﻿using CodeM.Common.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace Test
{
    public class JsonTest
    {
        private ITestOutputHelper output;

        public JsonTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void LoadOneConfig()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "resources\\appsettings.json");
            dynamic configObj = Xmtool.Json.ConfigParser().AddJsonFile(path).Parse();
            Assert.True(configObj.Has("User"));
        }

        [Fact]
        public void LoadTowConfig()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "resources\\appsettings.json");
            string path2 = Path.Combine(Environment.CurrentDirectory, "resources\\appsettings.Development.json");
            dynamic configObj = Xmtool.Json.ConfigParser().AddJsonFile(path).AddJsonFile(path2).Parse();
            Assert.Equal(configObj.Test, "This is a example.");
        }

        [Fact]
        public void HasPath()
        { 
            string path = Path.Combine(Environment.CurrentDirectory, "resources\\appsettings.json");
            dynamic configObj = Xmtool.Json.ConfigParser().AddJsonFile(path).Parse();

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
            string path = Path.Combine(Environment.CurrentDirectory, "resources\\appsettings.json");
            dynamic configObj = Xmtool.Json.ConfigParser().AddJsonFile(path).Parse();

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
            string path = Path.Combine(Environment.CurrentDirectory, "resources\\appsettings.json");
            dynamic configObj = Xmtool.Json.ConfigParser().AddJsonFile(path).Parse();

            Assert.Equal(configObj.User.Name, "Wangxm");

            configObj.SetValueByPath("User.Name", "Huxy");
            Assert.Equal(configObj.User.Name, "Huxy");

            configObj.SetValueByPath("Company.Name", "MYWS");
            Assert.Equal(configObj.Company.Name, "MYWS");
        }

        [Fact]
        public void GetValueByPath()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "resources\\appsettings.json");
            dynamic configObj = Xmtool.Json.ConfigParser().AddJsonFile(path).Parse();

            object name = configObj.GetValueByPath("User.Name");
            Assert.Equal("Wangxm", name);

            object addr = configObj.GetValueByPath("User.Address");
            Assert.Null(addr);
        }
    }
}
