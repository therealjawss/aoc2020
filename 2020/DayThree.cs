namespace AOC
{
    public class DayThree : DayModule
    {
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
            Slope[] slopes =  {
                new Slope(1,1),
                new Slope(3,1),
                new Slope(5,1),
                new Slope(7,1),
                new Slope(1,2)
            };
            double result = 1;
            foreach (var slope in slopes)
            {
                result = result * GetTreesForSlope(input, slope);
            }

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