using FluentAssertions;
using static Day05;
using Range = Day05.Range;

namespace Tests
{
    public class Day05Tests
    {

        [Theory]
        [InlineData(79, 81)]
        [InlineData(55, 57)]
        public void CanCheckInfo(long source, long expected)
        {
            var info = new Info(52, 50, 48);
            info.GetDestination(source).Should().Be(expected);
        }

        [Theory]
        [InlineData(49)]
        [InlineData(99)]
        public void ReturnsNegative(long source)
        {
            var info = new Info(52, 50, 48);
            info.GetDestination(source).Should().BeLessThan(0);
        }

        //[Theory]
        //[InlineData(1, 0, 1)]
        //[InlineData(1, 9, 10)]
        //[InlineData(11, 9, 20)]
        //[InlineData(11, 99, 110)]
        //[InlineData(11, 999, 1010)]
        //[InlineData(9, 9, 18)]
        //[InlineData(121, 19, 140)]
        //public void CanAdd(long addend, string augend, string expected)
        //{
        //    StringOperations.Add(addend, augend).Should().Be(expected);
        //}

        //[Theory]
        //[InlineData(1, 0, 1)]
        //[InlineData(9, 1, 8)]
        //[InlineData(10, 1, 9)]
        //[InlineData(12, 1, 11)]
        //[InlineData(100, 99, 1)]
        //[InlineData(153, 39, 114)]
        //[InlineData(10, 9, 1)]
        //[InlineData(150, 9, 141)]
        //[InlineData(99, 100, - )]
        //[InlineData(98, 99, - )]
        //[InlineData(100, 1, 99)]
        //public void CanSubtract(string minuend, string subtrahend, string expected)
        //{
        //    StringOperations.Subtract(minuend, subtrahend).Should().Be(expected);
        //}
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(98, 50)]
        [InlineData(53, 55)]
        public void Map_CanMap(long seed, long soil)
        {
            var map = new Map(Category.seed, Category.soil, [
                new Info(50, 98, 2),
                new Info(52, 50, 48),
            ]);

            map.GetDestination(seed).Should().Be(soil);
        }

        [Theory]
        [InlineData(1, 2, 3, 4, -1 , -1 )]
        [InlineData(1, 3, 2, 4, 2, 3)]
        [InlineData(1, 4, 2, 3, 2, 3)]
        [InlineData(14, 43, 2, 3, -1 , -1 )]
        public void RangeIntersectionTests(long smin, long smax, long imin, long imax, long emin, long emax)
        {
            var r = new Range(smin, smax);
            var i = new Range(imin, imax);

            r.Intersection(i).min.Should().Be(emin);
            r.Intersection(i).max.Should().Be(emax);

        }
    }
}
