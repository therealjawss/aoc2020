using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Day18;

namespace Tests
{
    public class Day18MapTests
    {
        [Fact]
        public void CanMapSimpleClockwise()
        {
            Instruction[] instruction = [
                new Instruction(Direction.R, 1),
                new Instruction(Direction.D, 1),
                new Instruction(Direction.L, 1),
                new Instruction(Direction.U, 1),
            ];

            var result = Calculator.Map(instruction);

            result.Should().BeEquivalentTo(new Point[]
            {
                new Point(0, 0),
                new Point(2, 0),
                new Point(2, 2),
                new Point(0, 2),
            });
        }

        [Fact]
        public void CanMapSimpleCounterClockwise()
        {
            Instruction[] instruction = [
                new Instruction(Direction.L, 1),
                new Instruction(Direction.D, 1),
                new Instruction(Direction.R, 1),
                new Instruction(Direction.U, 1),
            ];

            var result = Calculator.Map(instruction);

            result.Should().BeEquivalentTo(new Point[]
            {
                new Point(0, 0),
                new Point(-2, 0),
                new Point(-2, 2),
                new Point(0, 2),
            });
        }
       
        [Fact]
        public void CanMapSimpleUpwards()
        {
            Instruction[] instruction = [
                new Instruction(Direction.L, 1),
                new Instruction(Direction.U, 1),
                new Instruction(Direction.R, 1),
                new Instruction(Direction.D, 1),
            ];

            var result = Calculator.Map(instruction);

            result.Should().BeEquivalentTo(new Point[]
            {
                new Point(0, 0),
                new Point(-2, 0),
                new Point(-2, -2),
                new Point(0, -2),
            });
        }

        [Fact]
        public void CanMapUShape()
        {
            Instruction[] instruction = [
                new Instruction(Direction.R, 1),
                new Instruction(Direction.D, 1),
                new Instruction(Direction.R, 2),
                new Instruction(Direction.U, 1),
                new Instruction(Direction.R, 1),
                new Instruction(Direction.D, 2),
                new Instruction(Direction.L, 4),
                new Instruction(Direction.U, 2),
            ];

            var result = Calculator.Map(instruction);

            result.Should().BeEquivalentTo(new Point[]
            {
                new Point(2, 0),
                new Point(2, 1),
                new Point(3, 1),
                new Point(3, 0),
                new Point(5, 0),
                new Point(5, 3),
                new Point(0, 3),
                new Point(0, 0),
            });
        }
        [Fact]
        public void CanMapReverseUShape()
        {
            Instruction[] instruction = [
                new Instruction(Direction.L, 1),
                new Instruction(Direction.D, 1),
                new Instruction(Direction.L, 2),
                new Instruction(Direction.U, 1),
                new Instruction(Direction.L, 1),
                new Instruction(Direction.D, 2),
                new Instruction(Direction.R, 4),
                new Instruction(Direction.U, 2),
            ];

            var result = Calculator.Map(instruction);

            result.Should().BeEquivalentTo(new Point[]
            {
                new Point(-2, 0),
                new Point(-2, 1),
                new Point(-3, 1),
                new Point(-3, 0),
                new Point(-5, 0),
                new Point(-5, 3),
                new Point(0, 3),
                new Point(0, 0),
            });
        }
        [Fact]
        public void CanMapUpsideDownUShapee()
        {
            Instruction[] instruction = [
                new Instruction(Direction.R, 4),
                new Instruction(Direction.D, 2),
                new Instruction(Direction.L, 1),
                new Instruction(Direction.U, 1),
                new Instruction(Direction.L, 1),
                new Instruction(Direction.D, 1),
                new Instruction(Direction.L, 1),
                new Instruction(Direction.U, 2),
            ];

            var result = Calculator.Map(instruction);

            result.Should().BeEquivalentTo(new Point[]
            {
                new Point(5, 0),
                new Point(5, 3),
                new Point(3, 3),
                new Point(3, 2),
                new Point(2, 2),
                new Point(2, 3),
                new Point(0, 3),
                new Point(0, 0),
            });
        }
    }
}
