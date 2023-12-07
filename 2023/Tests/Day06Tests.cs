using FluentAssertions;

namespace Tests
{

    public class Day06Tests
    {
        [Theory]
        [InlineData(7, 9, 4)]
        [InlineData(15, 40, 8)]
        [InlineData(30, 200, 9)]
        public void CanGetMin(long t, long d, long expected)
        {
            Day06.GetLeShit(t, d).Should().Be(expected);
        }
    }
}