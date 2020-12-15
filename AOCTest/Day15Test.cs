using AOC2020.Days;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace AOCTest
{
    public class Day15Test
    {
        [Fact]
        public void Run()
        {
            Day15.Run1();
        }
        [Fact]
        public void Run2()
        {
            Day15.Run2();
        }

        [Theory]
        [InlineData(4, "0")]
        public void GetNthNumber(int turn, string expected)
        {
            var d = new Day15();
            d.Input = new string [] { "0,3,6"};
            d.GetNthNumber(d.Input,30000000,turn).Should().Be(expected);
            

        }
    }
}


