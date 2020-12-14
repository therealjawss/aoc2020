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
	public class Day14Test
	{
		[Fact]
		public void Example1()
		{
			string[] s =
			{
				"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X",
				"mem[8] = 11",
				"mem[7] = 101",
				"mem[8] = 0"
			};
			var c = new Computer(s);
			c.Run();


		}
		[Fact]
		public void CanConvertToBinary()
		{
			var result = ((long)11).ToBinaryString();
			result.Length.Should().Be(36);
			result.Should().Be("000000000000000000000000000000001011");
			Convert.ToInt32(result, 2).Should().Be(11);
		}

		[Fact]
		public void ApplyMask()
		{
			var s = "000000000000000000000000000000001011";
			var mask = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X";
			var result = s.ApplyMask(mask);
			result.Should().Be("000000000000000000000000000001001001");
		}
	}
}
