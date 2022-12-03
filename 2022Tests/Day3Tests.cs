using static Day3;
using static CharHelpers;

namespace AOC2022Tests
{
    public class Day3Tests
    {
        Day3 sut;
        public Day3Tests()
        {
            sut = new Day3();
            sut.GetInput("test.txt").GetAwaiter().GetResult();
        }


        [Fact]
        public void CanBreakPuzzleComponents()
        {
            var input = "1234567890";
            var ruckSack = sut.ParseRucksack(input);
            ruckSack.Should().BeOfType<RuckSack>();
            ruckSack.FirstCompartment.Should().Be("12345");
            ruckSack.SecondCompartment.Should().Be("67890");
        }

        [Fact]
        public void CanFindCommonItem()
        {
            var ruckSack = new RuckSack("abcde", "efghi");
            ruckSack.CommonItem.Should().Be('e');
        }
        [Theory]
        [InlineData('a', 1)]
        [InlineData('A', 27)]
        public void CanGenCharPriority(char item, int expectedPrio)
        {
            item.Priority().Should().Be(expectedPrio);
        }
    }
}
