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
        [Fact]
        public void CanSolve2()
        {
            var result = sut.RunSecond();
            result.Should().Be("12");
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
        public void CanGetScore(long opponent, long me, long result)
        {
            Day2.resultOf(opponent, me).Should().Be(result);

        }


        // Rock     A   X   lose
        // Paper    B   Y   draw
        // Scissors C   Z   win
        [Theory]
        [InlineData("A X", 3)]
        [InlineData("A Y", 4)]
        [InlineData("A Z", 8)]
        [InlineData("B X", 1)]
        [InlineData("B Y", 5)]
        [InlineData("B Z", 9)]
        [InlineData("C X", 2)]
        [InlineData("C Y", 6)]
        [InlineData("C Z", 7)]
        public void CanTranslateStrategy(string input, int score)
        {
           
        }

        [Theory]
        [InlineData(1, 1, 3)]
        [InlineData(2, 2, 5)]
        [InlineData(3, 3, 7)]

        [InlineData(2, 1, 1)]
        [InlineData(3, 1, 2)]

        [InlineData(2, 3, 9)]
        [InlineData(1, 3, 8)]
        [InlineData(1, 2, 4)]
        [InlineData(3, 2, 6)]
        public void CanExecuteStrategy(int opponent, int me, int score)
        {
            Day2.strategyOf(opponent, me).Should().Be(score);
        }
    }
}
