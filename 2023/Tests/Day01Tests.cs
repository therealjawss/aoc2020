using FluentAssertions;
using System.Runtime.CompilerServices;

namespace Tests
{
    public class Day01Tests
    {
        [Theory]
        [InlineData("1a1", 11)]
        [InlineData("11", 11)]
        [InlineData("a11", 11)]
        [InlineData("7pqrstsixteen", 76)]
        public void CanGetNumberFromString(string text, int expected)
        {
            var number = text.GetNumbersFromString();

            number.Should().Be(expected);
        }

        [Theory]
        [InlineData("a1", 1)]
        [InlineData("ab21", 2)]
        [InlineData("azerob21", 0)]
        [InlineData("aoneb21", 1)]
        [InlineData("athreeoneb21", 3)]
        public void TextExtensionCanGetFirstNumber(string text, int firstNumber)
        {
            text.FirstNumber().Should().Be(firstNumber);
        }


        [Theory]
        [InlineData("a1", 1)]
        [InlineData("a1c2", 2)]
        [InlineData("a1c2three", 3)]
        public void TextExtensionCanGetLastNumber(string text, int lastNumber)
        {
            text.LastNumber().Should().Be(lastNumber);
        }
    }

   
}