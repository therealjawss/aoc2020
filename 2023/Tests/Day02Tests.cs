using FluentAssertions;

namespace Tests
{
    public class Day02Tests
    {
        [Theory]
        [InlineData("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 1, 3)]
        [InlineData("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue", 2, 3)]
        [InlineData("Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green", 5, 2)]
        public void CanParseGameNumberAndTurns(string input, int gameNumber, int turns)
        {
            var game = new Game(input);
            game.Number.Should().Be(gameNumber);

            game.Turns.Should().HaveCount(turns);
        }

        [Theory]
        [InlineData(" 3 blue, 4 red", 3, 4, 0)]
        [InlineData(" 1 red, 2 green, 6 blue", 6, 1, 2)]
        public void CanParseTurn(string input, int blue, int red, int green)
        {
            var turn = new Turn(input);
            turn.Blue.Should().Be(blue);
            turn.Red.Should().Be(red);
            turn.Green.Should().Be(green);
        }

        [Theory]
        [InlineData("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", true)]
        [InlineData("Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red", false)]
        public void CanCheckPosibility(string input, bool posibility)
        {
            var game = new Game(input);
            game.IsPossibleWithLoad(new Load(12, 13, 14)).Should().Be(posibility);
        }

        [Theory]
        [InlineData("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 4, 2, 6)]
        [InlineData("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue", 1, 3, 4)]
        public void CanGetMinimumLoad(string input, int red, int green, int blue)
        {
            var game = new Game(input);
            game.GetMinimumLoad().Should().Be(new Load(red, green, blue));
        }

        [Theory]
        [InlineData("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", 48)]
        [InlineData("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue", 12)]

        public void CanGetPower(string input, int power)
        {
            var game = new Game(input);
            game.GetPower().Should().Be(power);
        }
    }
}
