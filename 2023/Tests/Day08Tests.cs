using FluentAssertions;
using static Day08;

namespace Tests
{
    public class Day08Tests
    {
        [Fact]
        public void CanGetNextLoopingInstruction()
        {
            var instruction = "LLR";
            var instructions = new Instructions(instruction);
            instructions.Next().Should().Be(0);
            instructions.Next().Should().Be(0);
            instructions.Next().Should().Be(1);
                             
            instructions.Next().Should().Be(0);
            instructions.Next().Should().Be(0);
            instructions.Next().Should().Be(1);

        }
    }
}
