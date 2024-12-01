using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Day18;

namespace Tests
{
    public class Day18Tests
    {

        [Fact]
        public void CanProcessInstructions()
        {
            Instruction[] instructions = [
                new Instruction(Direction.R, 6, "#ff0000"),
                new Instruction(Direction.D, 6, "#ff0000"),
                new Instruction(Direction.L, 6, "#ff0000"),
                new Instruction(Direction.U, 6, "#ff0000"),
                ];

            var result = Calculator.ProcessDataIntelligently(instructions);
            result.Should().Be(49);
        }

        [Fact]
        public void CanProcessLeftInstructions()
        {
            Instruction[] instructions = [
                new Instruction(Direction.L, 3, "#ff0000"),
                new Instruction(Direction.D, 3, "#ff0000"),
                new Instruction(Direction.R, 3, "#ff0000"),
                new Instruction(Direction.U, 3, "#ff0000"),
            ];

            var result = Calculator.ProcessDataIntelligently(instructions);
            result.Should().Be(16);
        }


        [Fact]
        public void CanProcessSmallerInstructions()
        {
            Instruction[] instructions = [
                new Instruction(Direction.R, 4, "#ff0000"),
                new Instruction(Direction.D, 2, "#ff0000"),
                new Instruction(Direction.L,4 , "#ff0000"),
                new Instruction(Direction.U, 2, "#ff0000"),
            ];

            var result = Calculator.ProcessDataIntelligently(instructions);
            result.Should().Be(15);
        }


        [Fact]
        public void CanProcessIrregularInstructionss()
        {
            Instruction[] instructions = [
                new Instruction(Direction.R, 2, "#ff0000"),
                new Instruction(Direction.D, 1, "#ff0000"),
                new Instruction(Direction.R, 1, "#ff0000"),
                new Instruction(Direction.D, 1, "#ff0000"),
                new Instruction(Direction.L, 3, "#ff0000"),
                new Instruction(Direction.U, 2, "#ff0000"),
            ];

            var result = Calculator.ProcessDataIntelligently(instructions);
            result.Should().Be(11);
        }

        [Fact]
        public void CanProcessUInstructionss()
        {
            Instruction[] instructions = [
                new Instruction(Direction.R, 1, "#ff0000"),
                new Instruction(Direction.D, 1, "#ff0000"),
                new Instruction(Direction.R, 2, "#ff0000"),
                new Instruction(Direction.U, 1, "#ff0000"),
                new Instruction(Direction.R, 1, "#ff0000"),
                new Instruction(Direction.D, 3, "#ff0000"),
                new Instruction(Direction.L, 4, "#ff0000"),
                new Instruction(Direction.U, 3, "#ff0000"),
            ];

            var result = Calculator.ProcessDataIntelligently(instructions);
            result.Should().Be(19);
        }

        [Fact]
        public void CanProcessSnakeInstructionss()
        {
            Instruction[] instructions = [
                new Instruction(Direction.R, 3, "#ff0000"),
                new Instruction(Direction.D, 2, "#ff0000"),
                new Instruction(Direction.R, 1, "#ff0000"),
                new Instruction(Direction.D, 2, "#ff0000"),
                new Instruction(Direction.L, 3, "#ff0000"),
                new Instruction(Direction.U, 2, "#ff0000"),
                new Instruction(Direction.L, 1, "#ff0000"),
                new Instruction(Direction.U, 2, "#ff0000"),
            ];

            var result = Calculator.ProcessDataIntelligently(instructions);
            result.Should().Be(21);
        }
        [Fact]
        public void CanProcessSnakeLeftInstructionss()
        {
            Instruction[] instructions = [
                new Instruction(Direction.L, 3, "#ff0000"),
                new Instruction(Direction.D, 2, "#ff0000"),
                new Instruction(Direction.L, 1, "#ff0000"),
                new Instruction(Direction.D, 2, "#ff0000"),
                new Instruction(Direction.R, 3, "#ff0000"),
                new Instruction(Direction.U, 2, "#ff0000"),
                new Instruction(Direction.R, 1, "#ff0000"),
                new Instruction(Direction.U, 2, "#ff0000"),
            ];

            var result = Calculator.ProcessDataIntelligently(instructions);
            result.Should().Be(21);
        }

        [Fact]
        public void CanGoLeft()
        {
            Instruction[] instructions = [
                new Instruction(Direction.L, 1, "#ff0000"),
                new Instruction(Direction.D, 1, "#ff0000"),
                new Instruction(Direction.L, 2, "#ff0000"),
                new Instruction(Direction.U, 1, "#ff0000"),
                new Instruction(Direction.L, 1, "#ff0000"),
                new Instruction(Direction.D, 3, "#ff0000"),
                new Instruction(Direction.R, 4, "#ff0000"),
                new Instruction(Direction.U, 3, "#ff0000"),
            ];

            var result = Calculator.ProcessDataIntelligently(instructions);
            result.Should().Be(19);
        }

        [Fact]
        public void CanGoLeftComplex()
        {
            Instruction[] instructions = [
                new Instruction(Direction.L, 2, "#ff0000"),
                new Instruction(Direction.D, 2, "#ff0000"),
                new Instruction(Direction.R, 1, "#ff0000"),
                new Instruction(Direction.D, 2, "#ff0000"),
                new Instruction(Direction.L, 3, "#ff0000"),
                new Instruction(Direction.U, 1, "#ff0000"),
                new Instruction(Direction.L, 1, "#ff0000"),
                new Instruction(Direction.U, 1, "#ff0000"),
                new Instruction(Direction.L, 1, "#ff0000"),
                new Instruction(Direction.D, 4, "#ff0000"),
                new Instruction(Direction.R, 8, "#ff0000"),
                new Instruction(Direction.U, 2, "#ff0000"),
                new Instruction(Direction.R, 1, "#ff0000"),
                new Instruction(Direction.U, 2, "#ff0000"),
                new Instruction(Direction.L, 2, "#ff0000"),
                new Instruction(Direction.D, 1, "#ff0000"),
                new Instruction(Direction.L, 1, "#ff0000"),
                new Instruction(Direction.U, 3, "#ff0000"),
            ];

            var result = Calculator.ProcessDataIntelligently(instructions);
            result.Should().Be(50);
        }
        [Fact]
        public void CanReverseU()
        {
            Instruction[] instructions = [
                new Instruction(Direction.R, 8, "#ff0000"),
                new Instruction(Direction.D, 4, "#ff0000"),
                new Instruction(Direction.L, 3, "#ff0000"),
                new Instruction(Direction.U, 1, "#ff0000"),
                new Instruction(Direction.R, 1, "#ff0000"),
                new Instruction(Direction.U, 2, "#ff0000"),
                new Instruction(Direction.L, 4, "#ff0000"),
                new Instruction(Direction.D, 2, "#ff0000"),
                new Instruction(Direction.R, 1, "#ff0000"),
                new Instruction(Direction.D, 1, "#ff0000"),
                new Instruction(Direction.L, 3, "#ff0000"),
                new Instruction(Direction.U, 4, "#ff0000"),
            ];

            var result = Calculator.ProcessDataIntelligently(instructions);
            result.Should().Be(40);
        }
         [Fact]
 public void Can()
 {
     Instruction[] instructions = new[]
     {

         new Instruction(Direction.L, 2, "#ff0000"),
         new Instruction(Direction.D, 2, "#ff0000"),
         new Instruction(Direction.L, 5, "#ff0000"),
         new Instruction(Direction.U, 10, "#ff0000"),
         new Instruction(Direction.R, 2, "#ff0000"),
         new Instruction(Direction.D, 2, "#ff0000"),
         new Instruction(Direction.R,8 , "#ff0000"),
         new Instruction(Direction.D, 8, "#ff0000"),
         new Instruction(Direction.L, 3, "#ff0000"),
         new Instruction(Direction.U, 2, "#ff0000"),
     };

     var result = Calculator.ProcessDataIntelligently(instructions);
     result.Should().Be(103);
 }
        //[Fact]
        //public void CanCalculateAreaOfSquare()
        //{
        //    Point[] points = [new Point(2, 0), new Point(2,2), new Point(0,2), new Point(0,0)];
        //    Calculator.CalculatePolygonArea(points).Should().Be(9);
        //}


        //[Fact]
        //public void CanCalculateAreaOfBiggerSquare()
        //{
        //    Point[] points = [new Point(4, 0), new Point(4, 2), new Point(0, 2), new Point(0, 0)];
        //    Calculator.CalculatePolygonArea(points).Should().Be(15);
        //}

        //[Fact]
        //public void CanCalculateAreaOfIrregular()
        //{
        //    Point[] points = [new Point(2, 0), new Point(2, 1), new Point(3,1), new Point(3, 2), new Point(0, 2), new Point(0,0)];
        //    Calculator.CalculatePolygonArea(points).Should().Be(11);
        //}
        //[Fact]
        //public void CanCalculateAreaOfAnotherIrregular()
        //{
        //    Point[] points = [
        //        new Point(2, 0), 
        //        new Point(2, 1), 
        //        new Point(3, 1), 
        //        new Point(3, 3), 
        //        new Point(0, 3) , 
        //        new Point(0, 0)];
        //    Calculator.CalculatePolygonArea(points).Should().Be(15);
        //}
        //[Fact]
        //public void CanCalculateAreaOfUShaped()
        //{
        //    Point[] points = [
        //        new Point(1, 0),
        //        new Point(1, 1),
        //        new Point(3, 1),
        //        new Point(3, 0),
        //        new Point(4, 0),
        //        new Point(4, 3),
        //        new Point(0, 3),
        //        new Point(0, 0)

        //    ];
        //    Calculator.CalculatePolygonArea(points).Should().Be(19) ;
        //}
        //[Fact]
        //public void CanCalculateAreaOfClosedUShaped()
        //{
        //    Point[] points = [
        //        new Point(1, 0),
        //        new Point(1, 1),
        //        new Point(3, 1),
        //        new Point(3, 0),
        //        new Point(4, 0),
        //        new Point(4, 2),
        //        new Point(0, 2),
        //        new Point(0, 0)

        //    ];
        //    Calculator.CalculatePolygonArea(points).Should().Be(14);
        //}
        //[Fact]
        //public void CanCalculateAreaOfSnakeShaped()
        //{
        //    Point[] points = [
        //        new Point(4, 0),
        //        new Point(4, 2),
        //        new Point(6, 2),
        //        new Point(6, 4),
        //        new Point(1, 4),
        //        new Point(1, 2),
        //        new Point(0, 2),
        //        new Point(0, 0)

        //    ];
        //    Calculator.CalculatePolygonArea(points).Should().Be(29);
        //}
    }

}
