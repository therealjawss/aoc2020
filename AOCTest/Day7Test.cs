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
	public class Day7Test
	{
		[Fact]
		public void CanExtractRule()
		{
			List<Bag> bags = new List<Bag>();
			var rulestring = "vibrant aqua bags contain 5 posh plum bags, 5 faded tomato bags, 5 shiny tomato bags, 1 mirrored orange bag.";
			Bag rule = Day07.ParseRule(rulestring,bags);
			rule.hasGold.Should().BeFalse();
			rule.containedBags.Count.Should().Be(4);
		}
		[Fact]
		public void CanParseInput()
		{
			var day = new Day07();
			day.GetInput(file: "test.txt", pattern: "\r\n");
			day.Level1(day.Input).Should().Be("335");
			day.Level2(day.Input).Should().Be("2431");
		}

	}
}
