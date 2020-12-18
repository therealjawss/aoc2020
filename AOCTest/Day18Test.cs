﻿using AOC2020.Days;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AOCTest
{
	public class Day18Test
	{
		[Fact]
		public void SimpleExpression()
		{
			var expr = "1 + 3";
			var d = new Day18();
			var result = d.Evaluate(expr);
			result.Should().Be(4);
		}

		[Theory]
		[InlineData("1*3+4", 7)]
		[InlineData("3+4*1", 7)]
		[InlineData("3+4*1+2", 21)]
		[InlineData("1 + 2 * 3 + 4 * 5 + 6", 231)]
		public void MultipleExpression(string expr, int expected) {
			var d = new Day18();
			var result = d.Evaluate(expr);
			result.Should().Be(expected);
		}
		[Theory]
	//	[InlineData("1 + (2 * 4)", 9)]
		[InlineData("(4 * (5 + 6))", 44)]
		[InlineData("1 + (2 * 3) + (4 * (5 + 6))", 51)]
		[InlineData("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 23340)]
		public void WithParentheses(string expr, int expected)
		{
			var d = new Day18();
			var result = d.Evaluate(expr);
			result.Should().Be(expected);
		}

	}
}
