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
            Console.WriteLine(day.Level2(day.Input));
        }
        public override string Level1(string[] input)
        {
            var earliest = Convert.ToInt32(input[0]);
            var r = Regex.Matches(input[1], @"(\d+)").Select(x => Convert.ToInt32(x.Groups[1].Value));

            var result = r.Aggregate((soonestBus: 10000, waitTime: 1000), (val, bus) =>
            {
                int time = ((earliest / bus) * bus + bus - earliest);
                return time < val.waitTime ? (bus, time) : val;
            });

            Console.WriteLine($"{result.waitTime} minutes for bus {result.soonestBus}");

            return (result.waitTime * result.soonestBus).ToString();
        }
        public override string Level2(string[] input)
        {
            var r = Regex.Matches(input[1], @"(\d+|x)").Select(x => x.Groups[1].Value).ToList();
            List<Bus> busses = new();
            for (int i = 0; i < r.Count(); i++)
            {
                if (!r[i].Equals("x"))
                {
                    busses.Add(new Bus(i,Int64.Parse(r[i])));
                }
            }
            long startTime = 0;
            startTime = FindTime(busses, startTime);
            Console.WriteLine(startTime);
            return base.Level2(input);
        }

         long FindTime(Bus bus1, Bus bus2, long startTime)
		{
            startTime += bus1.number;
            var mult = startTime / bus1.number;
            if (bus2.offset == bus2.number - startTime % bus2.number) return startTime;
            else return FindTime(bus1, bus2, startTime+bus1.number);
		}
        Dictionary<(long, long), long> mods = new();
        long FindTime(List<Bus> busses, long starttime)
        {
            starttime += busses[0].number;

            var b = busses.Where(b => b.offset > 0).ToList();
            if (b.All(b => passes(starttime, b)))
                return starttime;
            else return FindTime(busses, starttime);
		}
       
		private bool passes(long starttime, Bus b)
		{ 
            if (!mods.ContainsKey((starttime, b.number)))
			{
                mods.Add((starttime, b.number), starttime % b.number);
			}
            return b.offset == b.number - mods[(starttime, b.number)];
		}

		record Bus(long offset, long number);
    }
}