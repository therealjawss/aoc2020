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
	public class Day16Test
	{

		[Theory]
		[InlineData("zone: 34-521 or 534-971\n", "nearby tickets: 279,705,188,357,892,488,741,247,572,176,760,306,410,861,507,906,179,501,808,525", 19)]
		public void CanCheckValidAgainstField(string field, string values, int expected) {
			var d = new Day16();
			var rules = d.ParseRules(field).First();
			var ticket = d.ParseOtherTickets(values).First();
			var result = d.ValidForIndices(ticket, rules);
			result.Count.Should().Be(expected);
		}


	}
}
