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
	public class Day13Test
	{
		[Fact]
		public void firstCase()
		{
			var busses = new List<(ulong, ulong)>();
			busses.Add((0, 17));
			busses.Add((2, 13));
			busses.Add((3, 19));

			var d = new Day13();

			var result = d.compute(busses);
			result.Should().Be(3417);
		}

		[Theory]
		[InlineData(0, 1, 1, 3, 2)]
		[InlineData(0, 3, 1, 5, 9)]
		[InlineData(0, 5, 1, 7, 20)]
		[InlineData(0,7,1,13, 77)]
		public void twoBusses(ulong ai, ulong a, ulong bi, ulong b, ulong expected) {
			var busses = new List<(ulong, ulong)>();
			busses.Add((ai, a));
			busses.Add((bi, b));

			var d = new Day13();
			var result = d.compute(busses);
			result.Should().Be(expected);
		}

		[Theory]
		[InlineData(0, 1, 1, 3, 2, 5, 8)]
		[InlineData(0, 17, 2, 13, 3, 19, 3417 )]
		public void threeBusses(ulong ai, ulong a, ulong bi, ulong b, ulong ci, ulong c, ulong expected) {
			var busses = new List<(ulong, ulong)>();
			busses.Add((ai, a));
			busses.Add((bi, b));
			busses.Add((ci, c));
			var d = new Day13();
			var result = d.compute(busses);
			result.Should().Be(expected);
		}

		[Theory]
		[InlineData(0,67,1,7,2,59,3,61,754018)]
		[InlineData(0,1789,1,37,2,47,3,1889, 1202161486)]
		public void fourBusses(ulong ai, ulong a, ulong bi, ulong b, ulong ci, ulong c, ulong di, ulong d, ulong expected)
		{
			var busses = new List<(ulong, ulong)>();
			busses.Add((ai, a));
			busses.Add((bi, b));
			busses.Add((ci, c));
			busses.Add((di, d));
			var day= new Day13();
			var result = day.compute(busses);
			result.Should().Be(expected);

		}

	}
}
