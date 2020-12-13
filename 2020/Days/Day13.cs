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
                int time = ((earliest / bus) * bus + bus - earliest);
                return time < val.waitTime ? (bus, time) : val;
            });

            Console.WriteLine($"{result.waitTime} minutes for bus {result.soonestBus}");

            return (result.waitTime * result.soonestBus).ToString();
        }
        public override string Level2(string[] input)
        {
            var r = Regex.Matches(input[1], @"(\d+|x)").Select(x => x.Groups[1].Value).ToList();
            Dictionary<ulong, ulong> busses = new();
            for (int i = 0; i < r.Count(); i++)
            {
                if (!r[i].Equals("x"))
                {
                    busses.Add((ulong)i, UInt64.Parse(r[i]));
                }
            }

            return "".ToString();
        }

    }

}