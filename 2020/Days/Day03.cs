using System.Collections.Generic;
using System.Linq;

namespace AOC2020.Days
{
    public class Day03 : Christmas
    {
        public override int Day => 3;
        public override string Level1(string[] input)
        {
            int i = 0;
            int ctr = 0;
            for (int j = 0; j < input.Length; j++)
            {
                var line = input[j].ToCharArray();
                if (line[i] == '#')
                {
                    ctr++;
                }
                i = (i + 3) % line.Length;
            }

            return ctr.ToString();
        }

        public override string Level2(string[] input)
        {
            List<Slope> slopes = new List<Slope>
            {
                new Slope(1, 1),
                new Slope(3, 1),
                new Slope(5, 1),
                new Slope(7, 1),
                new Slope(1, 2)
            };

            //thanks for the tip on Aggregate Anders :D
            ulong result = slopes.Aggregate((ulong)1, (val, next) => val * (ulong)GetTreesForSlope(input, next));

            return result.ToString();
        }

        public int GetTreesForSlope(string[] input, Slope slope)
        {
            int i = 0;
            int ctr = 0;

            for (int j = 0; j < input.Length; j += slope.down)
            {
                var line = input[j].ToCharArray();
                if (line[i] == '#')
                {
                    ctr++;
                }
                i = (i + slope.right) % line.Length;
            }

            return ctr;
        }
    }

    public record Slope(int right, int down);
}