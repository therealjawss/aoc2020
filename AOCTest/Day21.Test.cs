using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AOC2020.Days;
using FluentAssertions;

namespace AOCTest
{
    public class Day21Test
    {
        [Theory]
        //[InlineData("baabbaaaabbaaaababbaababb", true)]
        //[InlineData("aaaabbaaaabbaaa", false)]
        [InlineData("babbbbaabbbbbabbbbbbaabaaabaaa", true)]
        public void conforms(string s, bool expected)
        {
            var d = new Day21();
            d.GetInput();
        }

    }
}
