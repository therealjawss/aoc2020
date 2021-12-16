using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AOC2021.Test
{

    public class Day16Tests
    {
        [Theory]
        [InlineData("D2FE28", "110100101111111000101000")]
        [InlineData("38006F45291200", "00111000000000000110111101000101001010010001001000000000")]
        public void CanConvertToBinary(string hex, string bin)
        {
            var d = new Day16();
            bin = d.ConvertHexToBin(hex);

            bin.Should().Be(bin);

        }
    }
}
