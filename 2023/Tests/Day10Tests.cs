using FluentAssertions;
using System.Security.Cryptography.X509Certificates;
using static Day10;

namespace Tests
{
    public class Day10Tests
    {
        string[] map = [
            ".....",
            ".S-7.",
            ".|.|.",
            ".L-J.",
            "....."
        ];
        private readonly Day10 sut;
        public Day10Tests()
        {
            sut = new Day10();
        }
        [Theory]
        [InlineData("OF-J|F7OL--J||LJOF-", 0)]
        public void CanCountCrosses(string input, int crosses)
        {
            sut.CountCrosses(input).Should().Be(crosses);
        }
        [Theory]
        [InlineData("F7LJ", 0)]
        [InlineData("FJLJ", 1)]
        [InlineData("LJLJL7", 1)]
        [InlineData("F7F7", 0)]
        public void CanCountCorners(string input, int crosses)
        {
            sut.CountCrosses(input).Should().Be(crosses);
        }

        [Theory, MemberData(nameof(possibleMaps))]
        public void CanFindStart(string[] input, Coordinates expected)
        {
            var start = FindStart(input);
            start.Should().Be(expected);
        }

        [Theory, MemberData(nameof(possibleExits))]
        public void CanFindExits(string[] map, Coordinates start, Coordinates[] expectedExits)
        {
            Coordinates[] exits = sut.FindExits(map, start).ToArray();

            exits.Should().BeEquivalentTo(expectedExits);
        }

        public static IEnumerable<object[]> possibleExits =>
            new List<object[]>
            {
                 new object[]
                {
                     new string[]
                   {
"........",
".S-----7",
".|.F-7.|",
".L-J.L-J",
"........"
                   },
                    new Coordinates(1,4),
                    new Coordinates[]
                    {
                        new Coordinates(1,3),
                        new Coordinates(1,5)
                    } },
            //    new object[]
            //    {
            //         new string[]
            //       {
            //".....",
            //".|||.",
            //".LJ|.",
            //".7J|.",
            //"....."
            //       },
            //        new Coordinates(2,2),
            //        new Coordinates[]
            //        {
            //            new Coordinates(2,1),
            //            new Coordinates(1,2)
            //        } }
            //    },
            //    new object[]
            //    {
            //         new string[]
            //       {
            //".....",
            //".S-7.",
            //".|.|.",
            //".L-J.",
            //"....."
            //       },
            //        new Coordinates(1,1),
            //        new Coordinates[]
            //        {
            //            new Coordinates(1,2),
            //            new Coordinates(2,1)
            //        }
            //    },
            //      new object[]
            //    {
            //       new string[]
            //       {

            //".....",
            //".S-7.",
            //".|.|.",
            //".L-J.",
            //"....."
            //       },
            //        new Coordinates(3,1),
            //        new Coordinates[]
            //        {
            //            new Coordinates(2,1),
            //            new Coordinates(3,2)
            //        }
            //    }
            };


        public static IEnumerable<object[]> possibleMaps =>
            new List<object[]>
            {
                new object[] {
                    new string[]
                    {
                        ".....",
                        ".S-7.",
                        ".|.|.",
                        ".L-J.",
                        "....."
                    }, new Coordinates(1, 1)
                },
                new object[]
                {
                    new string[]
                    {
                        "..F7.",
                        ".FJ|.",
                        "SJ.L7",
                        "|F--J",
                        "LJ..."
                    }, new Coordinates(2, 0)
                },
            };
    }

}
