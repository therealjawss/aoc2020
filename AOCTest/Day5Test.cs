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
	public class Day5Test
	{

		[Fact] 
		public void TestColumns()
		{
			var day = new Day5();
			var result = day.FindId("FBFBBFFRLR");
			result.Should().Be(357);
		}
	}
}
