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
		public void Level1Test()
		{
			var day = new Day8();
			day.GetInput(file: "test.txt", pattern: "\r\n");

			day.Level1(day.Input).Should().Be("5");
		}

		[Fact]
		public void Level2Test()
		{
			var day = new Day8();
			day.GetInput(file: "test.txt", pattern: "\r\n");
			day.Level2(day.Input).Should().Be("8");
		}

		[Fact]
		public void LoopTest()
		{
			var day = new Day8();

			day.Input = day.GetInput(file: "test.txt", pattern: "\r\n");
			day.ParseOperations();
			long acc = 0;
			day.ItLooped(day.Instructions, ref acc).Should().BeFalse();
		}
		[Theory]
		[InlineData(0, Operation.jmp, Operation.nop, 3)]
		[InlineData(5, Operation.acc, Operation.acc, 48)]
		[InlineData(8, Operation.nop, Operation.jmp, 155)]

		public void ChangeTest(int index, Operation operation, Operation changed, int argument)
		{
			var day = new Day8();
			day.Input = day.GetInput(file: "test.txt", pattern: "\r\n");
			day.ParseOperations();
			day.ChangeIt(day.Instructions, index);
			day.Instructions[index].operation.Should().Be(changed);
			day.Instructions[index].argument.Should().Be(argument);
			day.ChangeIt(day.Instructions, index);
			day.Instructions[index].operation.Should().Be(operation);
			day.Instructions[index].argument.Should().Be(argument);
		}

	}
}
