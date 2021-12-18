using FluentAssertions;
using Xunit;
using static Day18;

namespace AOC2021.Test
{
    public class Day18Tests
    {

        [Fact]
        public void CanParseBasic()
        {
            var d = new Day18();
            d.ParseSnailfishNumber("[1,2]").Should().Be(new SnailfishNumber(1, 2));
        }

        [Fact]
        public void CanParseEmbedded()
        {
            var d = new Day18();
            var inner = new SnailfishNumber(1, 2);
            var outer = new SnailfishNumber(inner, 3);
            d.ParseSnailfishNumber("[[1,2],3]").Should().Be(outer);
        }
        [Fact]
        public void CanParseEmbeddedWithNumberFirst()
        {
            var d = new Day18();
            var inner = new SnailfishNumber(8,7);
            var outer = new SnailfishNumber(9,inner);
            d.ParseSnailfishNumber("[9,[8,7]]").Should().Be(outer);
        }
        [Fact]
        public void CanParseTwoEmbeddeds()
        {
            var d = new Day18();
            var inner = new SnailfishNumber(1, 9);
            var another = new SnailfishNumber(8, 5);
            var outer = new SnailfishNumber(inner, another);
            d.ParseSnailfishNumber("[[1,9],[8,5]]").Should().Be(outer);
        }
        
    }
}
