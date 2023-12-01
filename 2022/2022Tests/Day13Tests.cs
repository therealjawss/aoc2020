using static Day13;

namespace _2022Tests
{
    public class Day13Tests
    {
        [Theory]
        [InlineData("[1,1,3,1,1]", "[1,1,5,1,1]", true)]
        [InlineData("[1,1,3,1,1]", "[1,1,2,1,1]", false)]
        [InlineData("[[]]", "[]", false)]
        public void CanCompareSimpleLists(string left, string right, bool expected)
        {
            var sut = new Pair(new Packet(left), new Packet(right));
            sut.InOrder().Should().Be(expected);
        }

        [Theory]
        [InlineData("[1,2,3]", "1,2,3")]
        [InlineData("[1,[2],3]", "1,[2],3")]
        [InlineData("[[],3]", "[],3")]
        [InlineData("[[1],[2,3,4]]", "[1],[2,3,4]")]
        [InlineData("[1,[2,[3,[4,[5,6,7]]]],8,9]", "1,[2,[3,[4,[5,6,7]]]],8,9")]
        public void CanGetInside(string outside, string expected)
        {
            var inside = outside.GetInsides();
            inside.Should().Be(expected);   

        }
    }
}
