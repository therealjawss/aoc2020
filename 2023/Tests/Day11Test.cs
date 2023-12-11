using FluentAssertions;
using static Day11;

namespace Tests
{
    public class Day11Test
    {
        Day11 sut;
        public Day11Test()
        {
            sut = new Day11();
        }

        [Fact]
        public void CanFindClearLinesAndGalaxies()
        {
            string[] map = [
                "#.#",
                "...",
                "#.#"
            ];

            sut.FindClearLines(map).Should().BeEquivalentTo([
                new LineSegment(new Point(0, 1), new Point(2, 1)),
                new LineSegment(new Point(1, 0), new Point(1, 2)),
            ]);

            sut.FindGalaxies(map).Should().BeEquivalentTo([
                new Point(0, 0),
                new Point(0, 2),
                new Point(2, 0),
                new Point(2, 2),
            ]);
        }


        [Fact]
        public void CanFindClearLinesAndGalaxiesInDifferentMap()
        {
            string[] map = [
                "#.#",
                "...",
                "###"
            ];

            sut.FindClearLines(map).Should().BeEquivalentTo([
                new LineSegment(new Point(1, 0), new Point(1, 2)),
            ]);

        }

        [Theory]
        [InlineData(0, 0, 0, 2, 2)]
        [InlineData(2, 0, 2, 1, 1)]
        [InlineData(0, 0, 2, 0, 3)]
        [InlineData(0, 0, 2, 1, 4)]
        public void CanFindPointDistances(int p1x, int p1y, int p2x, int p2y, int expected)
        {
            string[] map = [
                "#.#",
                "...",
                "###"
            ];

            var distance = sut.FindMinDistance(map, new Point(p1x, p1y), new Point(p2x, p2y));
            distance.Should().Be(expected);
        }

        [Theory]
        [InlineData(0, 0, 3, 2, 8)]
        public void CanFindMinDistances(int p1x, int p1y, int p2x, int p2y, int expected)
        {
            string[] map = [
                "#..#",
                "....",
                "....",
                "#.##"
            ];

            var distance = sut.FindMinDistance(map, new Point(p1x, p1y), new Point(p2x, p2y));
            distance.Should().Be(expected);
        }

        [Theory]
        [InlineData(0, 3, 8, 7, 15)]
        [InlineData(2, 0, 6, 9, 17)]
        [InlineData(9, 0, 9, 4, 5)]
        [InlineData(0, 3, 2, 0, 6)]
        public void CanFindPointDistancesTestInput(int p1x, int p1y, int p2x, int p2y, int expected)
        {
            string[] map = [
"...#......",
".......#..",
"#.........",
"..........",
"......#...",
".#........",
".........#",
"..........",
".......#..",
"#...#....."
            ];

            var distance = sut.FindMinDistance(map, new Point(p1x, p1y), new Point(p2x, p2y));
            distance.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(Intersecting))]
        public void CanGetIntersection(LineSegment one, LineSegment two)
        {
            var intersection = one.DoIntersect(two);
            intersection.Should().Be(true);
        }

        public static IEnumerable<object[]> Intersecting =>
            new List<object[]>
            {
                new object[]
                {
                    new LineSegment(new Point(0,3), new Point(6,9)),
                    new LineSegment(new Point(0,8), new Point(9,8)),
                },
                new object[]
                {
                    new LineSegment(new Point(0,3), new Point(6,9)),

                    new LineSegment(new Point(3,0), new Point(3,9)),
                },
                new object[]
                {
                    new LineSegment(new Point(0,3), new Point(6,9)),
                    new LineSegment(new Point(0,5), new Point(9,5)),
                },
            };

        [Theory]
        [MemberData(nameof(NotIntersecting))]
        public void CanGetNonIntersection(LineSegment one, LineSegment two)
        {
            var intersection = one.DoIntersect(two);
            intersection.Should().Be(false);
        }

        public static IEnumerable<object[]> NotIntersecting =>
            new List<object[]>
            {
                new object[]
                {
                    new LineSegment(new Point(0,3), new Point(6,9)),
                    new LineSegment(new Point(0,2), new Point(9,2)),
                },
                new object[]
                {
                    new LineSegment(new Point(0,3), new Point(6,9)),
                    new LineSegment(new Point(7,0), new Point(7,9)),
                },

            };
    }
}
