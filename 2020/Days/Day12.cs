using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AOC2020.Days
{
	public class Day12: Christmas
	{
		public override int Day => 12;
		public static void Run()
		{
			var day = new Day10();
			Console.WriteLine($"hello day {day.Day}");
			//day.GetInput(file: "test.txt", pattern: "\r\n");
			day.GetInput();
			Console.WriteLine(day.Level1(day.Input));
			//day.PostL1Answer();
			Thread.Sleep(60000);
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
