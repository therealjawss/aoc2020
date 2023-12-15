using FluentAssertions;
using static Day05;
using Range = Day05.Range;

namespace Tests
{
    public class Day05Tests
    {
        #region Part2
        [Fact]
        public void MinimumTest()
        {
            var sut = new Day05();
            var seeds = new Seed(1)
            {
                Max = 10
            };
            var maps = new Dictionary<Category, Map>
            {
                {
                    Category.seed,
                    new Map(Category.seed, Category.soil, [
                    new Info(1, 5, 2),
                        new Info(10, 2, 1),
                    ])
                },
                {
                    Category.soil,
                    new Map(Category.soil, Category.fertilizer, [
                    new Info(20, 2, 2),
                        new Info(1, 11, 1),
                    ])
                },
                                {
                    Category.fertilizer,
                    new Map(Category.fertilizer, Category.water, [
                        new Info(20, 2, 2),
                        new Info(1, 11, 1),
                    ])
                },{
                    Category.water,
                    new Map(Category.water, Category.light, [
                        new Info(20, 2, 2),
                        new Info(1, 11, 1),
                    ])
                },{
                    Category.light,
                    new Map(Category.light, Category.temperature, [
                        new Info(20, 2, 2),
                        new Info(1, 11, 1),
                    ])
                },
                {
                    Category.temperature,
                    new Map(Category.temperature, Category.humidity, [
                        new Info(20, 2, 2),
                        new Info(1, 11, 1),
                    ])
                },
                {
                    Category.humidity,
                    new Map(Category.humidity, Category.location, [
                        new Info(20, 2, 2),
                        new Info(1, 11, 1),
                    ])
                }, {
                    Category.location,
                    new Map(Category.humidity, null, [
                    ])
                }
            };
;

            seeds.Travel(maps);
            seeds.Locations.Count().Should().Be(1);
        }

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
        [InlineData(1, 2, 3, 4, -1, -1)]
        [InlineData(1, 3, 2, 4, 2, 3)]
        [InlineData(1, 4, 2, 3, 2, 3)]
        [InlineData(14, 43, 2, 3, -1, -1)]
        public void RangeIntersectionTests(long smin, long smax, long imin, long imax, long emin, long emax)
        {
            var r = new Range(smin, smax);
            var i = new Range(imin, imax);

            r.Intersection(i).min.Should().Be(emin);
            r.Intersection(i).max.Should().Be(emax);

        }
        #endregion
        #region Part 2
   
        #endregion

    }
}
