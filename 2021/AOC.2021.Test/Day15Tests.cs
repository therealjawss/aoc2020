using FluentAssertions;
using Xunit;

namespace AOC2021.Test
{
    public class Day15Tests
    {
        int[][] riskmap = new int[][] {
               new int[] { 1, 1, 6 },
               new int[] { 1, 3, 8 },
               new int[] { 2, 1, 3 }
            };

        int[][] smallRiskMap = new int[][] {
               new int[] { 1, 1 },
               new int[] { 1, 3 }
            };

        [Theory]
        [InlineData(1, 1, 4)]
        [InlineData(2, 2, 7)]
        public void CanGetLesserRisk(int i, int j, int expected)
        {
        
            var d = new Day15(riskmap);
            var risk = d.GetMinimumRiskUntil(i,j);
            risk.Should().Be(expected);
        }

        [Fact]
        public void CanMakeFullMap()
        {
            var d = new Day15(smallRiskMap);
            int[][] fullmap = d.GenerateFullMap();
            fullmap.Length.Should().Be(smallRiskMap.Length * 5);
            fullmap[0].Length.Should().Be(smallRiskMap.Length * 5);
        }
    }
}
