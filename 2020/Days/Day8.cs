using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2020.Days
{
    public class Day8 : Christmas
    {
        public override int Day => 8;

        public static void Run()
        {
            Console.WriteLine("hello");
            var day = new Day8();
            day.GetInput(file: "test.txt", pattern: "\n");
            //	day.GetInput();
            Console.WriteLine(day.Level1(day.Input));
            //day.PostL1Answer();
            Task.Delay(60000);
            Console.WriteLine(day.Level2(day.Input));
            //day.PostL2Answer();
        }

		public override string Level1(string[] input)
		{
			return base.Level1(input);
		}
		public override string Level2(string[] input)
		{
			return base.Level2(input);
		}

	}
}