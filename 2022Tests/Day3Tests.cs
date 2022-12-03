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
        
        [Fact]
        public void CanBreakIntoGroups()
        {
            var input = new string[] { "abc", "cde", "cfg", "hij", "hkl", "hmn" };

            var groups = sut.GetElfGroups(input);
            groups.First().Should().Be(new ElfGroup("abc", "cde", "cfg"));
            groups[1].Should().Be(new ElfGroup("hij", "hkl", "hmn"));
        }

        [Fact]
        public void CanFindCommonItemInGroup()
        {
            var ruckSack = new ElfGroup("abcde", "efghi", "ejklm");
            ruckSack.CommonItem.Should().Be('e');
        }
    }
}
