using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2020.Days
{

    public class Day13 : Christmas
    {
        public override int Day => 13;
        public static void Run()
        {
            var day = new Day13();
            day.GetInput();
            Console.WriteLine(day.Level1(day.Input));
            Console.WriteLine("Answer should be " + day.Level2(day.Input));
        }
        public override string Level1(string[] input)
        {
            var earliest = Convert.ToInt32(input[0]);
            var r = Regex.Matches(input[1], @"(\d+)").Select(x => Convert.ToInt32(x.Groups[1].Value));

            var result = r.Aggregate((soonestBus: 10000, waitTime: 1000), (val, bus) =>
            {
                int time = ((int)(earliest / bus) * bus + bus - earliest);
                return time < val.waitTime ? (bus, time) : val;
            });

            Console.WriteLine($"{result.waitTime} minutes for bus {result.soonestBus}");

            return (result.waitTime * result.soonestBus).ToString();
        }
        public override string Level2(string[] input)
        {
            var r = Regex.Matches(input[1], @"(\d+|x)").Select(x => x.Groups[1].Value).ToList();
            List<(ulong, ulong)> busses = new();
            for (int i = 0; i < r.Count(); i++)
            {
                if (!r[i].Equals("x"))
                {
                    busses.Add(((ulong)i, UInt64.Parse(r[i])));
                }
            }
            var result = compute(busses);
            return result.ToString();
        }

		public ulong compute(List<(ulong, ulong)> busses)
		{
            var i = 0;
            ulong mult = 1;
            var b1 = busses[0];
            ulong first = default, next = default;
            for(i = 0;i<busses.Count-1; i++)
			{
                mult = 1;
				first =  FindFirst(busses, i, mult, b1,b1.Item1);
                next = FindFirst(busses, i, mult, b1, first) - first;
                b1 = ((first), next);
			}
            return first;
		}

		private  ulong FindFirst(List<(ulong, ulong)> busses, int i, ulong mult, (ulong, ulong) b1, ulong start=0)
		{

            while (true)
			{
				if ((b1.Item2 * mult + busses[i + 1].Item1 + start) % busses[i + 1].Item2 == 0)
				{
					break;
				};
                mult++;
			}
			return b1.Item2 * mult  + start;
		}
	}

}