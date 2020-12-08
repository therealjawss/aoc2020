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
	public class Day8Test
	{
		[Fact]
		public void CanParseInput()
		{
			var day = new Day7();
			day.GetInput(file: "test.txt", pattern: "\r\n");
			day.Level1(day.Input).Should().Be("335");
			day.Level2(day.Input).Should().Be("2431");
		}

	}
}
