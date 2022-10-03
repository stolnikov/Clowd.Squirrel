﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Squirrel.Tests
{
    public class RuntimeTests
    {
        // we are upgrading net6 to a minimum version of 6.0.2 to work
        // around a dotnet SDK bug right now.
        [Theory]
        [InlineData("net6", "net6.0.2-windowsdesktop-x64")]
        [InlineData("net6.0", "net6.0.2-windowsdesktop-x64")]
        [InlineData("net6-x64", "net6.0.2-windowsdesktop-x64")]
        [InlineData("net6-x86", "net6.0.2-windowsdesktop-x86")]
        [InlineData("net3.1", "netcoreapp3.1-windowsdesktop-x64")]
        [InlineData("netcoreapp3.1", "netcoreapp3.1-windowsdesktop-x64")]
        [InlineData("net3.1-x86", "netcoreapp3.1-windowsdesktop-x86")]
        [InlineData("net6.0.2", "net6.0.2-windowsdesktop-x64")]
        [InlineData("net6.0.2-x86", "net6.0.2-windowsdesktop-x86")]
        [InlineData("net6.0.1-x86", "net6.0.1-windowsdesktop-x86")]
        [InlineData("net6.0.0", "net6-windowsdesktop-x64")]
        [InlineData("net6-windowsdesktop", "net6.0.2-windowsdesktop-x64")]
        [InlineData("net6-aspnetcore", "net6.0.2-aspnetcore-x64")]
        public void DotnetParsesValidVersions(string input, string expected)
        {
            var p = Runtimes.DotnetInfo.Parse(input);
            Assert.Equal(expected, p.Id);
        }

        [Theory]
        [InlineData("net3.2")]
        [InlineData("net4.9")]
        [InlineData("net6.0.0.4")]
        public void DotnetParseThrowsInvalidVersion(string input)
        {
            Assert.ThrowsAny<Exception>(() => Runtimes.DotnetInfo.Parse(input));
        }

        [Theory]
        [InlineData("net6", true)]
        [InlineData("net20.0", true)]
        [InlineData("net5.0.14-x86", true)]
        [InlineData("netcoreapp3.1-x86", true)]
        [InlineData("net48", true)]
        [InlineData("netcoreapp4.8", false)]
        [InlineData("net4.8", false)]
        [InlineData("net2.5", false)]
        [InlineData("vcredist110-x64", true)]
        [InlineData("vcredist110-x86", true)]
        [InlineData("vcredist110", true)]
        [InlineData("vcredist143", true)]
        [InlineData("asd", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData("net6-windowsdesktop", true)]
        [InlineData("net6-aspnetcore", true)]
        public void GetRuntimeTests(string input, bool expected)
        {
            var dn = Runtimes.GetRuntimeByName(input);
            Assert.Equal(expected, dn != null);
        }
    }
}
