using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Day4;

namespace _2022Tests
{
    public class Day4Tests
    {
        private Day4 sut;
        public Day4Tests()
        {
            sut = new Day4();
        }

        [Fact]
        public void CanParseInput()
        {
            var input = "2-4,6-8";

            var pair = sut.ParseInput(input);
            pair.Should().Be(new Pair(new Area(2, 4), new Area(6, 8)));
        }

        [Theory]
        [InlineData(2,4,4,4, false)]
        public void AreaCanTellOverlap(int x, int y, int i, int j, bool expectedResult)
        {
            new Area(x, y).FullyOverlapsWith(new Area(i, j)).Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(2, 4, 4, 4, true)]
        public void AreaCanTellContains(int x, int y, int i, int j, bool expectedResult)
        {
            new Area(x, y).FullyContains(new Area(i, j)).Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(2, 4, 4, 6, true)]
        public void AreaCanTellPartialContainment(int x, int y, int i, int j, bool expectedResult)
        {
            new Area(x, y).PartiallyContains(new Area(i, j)).Should().Be(expectedResult);
        }
    }


}
