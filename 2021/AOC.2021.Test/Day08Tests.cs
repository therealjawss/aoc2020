using FluentAssertions;
using Xunit;

namespace AOC2021.Test
{
    public class Day08Tests
    {
        [Theory]
        [InlineData("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf", 5353)]
        [InlineData("edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc", 9781)]

        public void Should(string input, long expectedOutput)
        {
            var d = new Day8();

         //   d.ProcessOutput(input).Should().Be(expectedOutput);

        }
        [Theory]
        [InlineData("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf")]
        public void CanCreateDecoder(string input)
        {
            var d = new Decoder(input);
           // d.SignalPatterns.Count.Should().Be(10);
        }

        //[Fact]
        //public void DecoderCanReportCompleteness()
        //{
        //    var d = new Decoder("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab");
        //    d.IsDecoded.Should().BeFalse();
        //}
    }
}
