using System;

namespace AOC
{

    public class DayOne : DayModule
    {
        public override string Level1(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 1; j < input.Length - 1; j++)
                {
                    if (i == j) continue;

                    var first = Int32.Parse(input[i]);
                    var second = Int32.Parse(input[j]);
                    if (first + second == 2020)
                    {
                        return (first * second).ToString();
                    }
                }
            }
            return "";
        }

        public override string Level2(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 1; j < input.Length - 1; j++)
                {
                    for (int k = 1; k < input.Length - 1; k++)
                    {
                        if (i == j || j == k || i == k) continue;

                        var first = Int32.Parse(input[i]);
                        var second = Int32.Parse(input[j]);
                        var third = Int32.Parse(input[k]);
                        if (first + second + third == 2020)
                        {
                            return (first * second * third).ToString();
                        }
                    }
                }
            }
            return "";
        }

    }
}