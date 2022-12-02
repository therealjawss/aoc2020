using FluentAssertions;
using static Day2;

namespace Day2Tests
{
    public class Day2Tests
    {
        Day2 sut;
        public Day2Tests()
        {
            sut = new Day2();
            sut.GetInput("test.txt").GetAwaiter().GetResult();

        }
        public async void CalculateScore() {
        
            
        }
        [Theory]
        [InlineData("A X")]
        public void CanParseInput(string input)
        {
            var result=Day2.CalculateScore(input);
        
        }
        [Theory]
        [InlineData(1, 1, 4)]
        [InlineData(2, 2, 5)]
        [InlineData(3, 3, 6)]
        [InlineData(3, 2, 2)]
        [InlineData(1, 3, 3)]
        [InlineData(2, 1, 1)]
        [InlineData(3, 1, 7)]
        [InlineData(1, 2, 8)]
        [InlineData(2, 3, 9)]
        public void CanDetermineWinner(long opponent, long me, long result)
        {
            Day2.resultOf(opponent, me).Should().Be(result);

        }
    }
}
