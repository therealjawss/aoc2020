using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AOC2020.Days
{
	public class Day9 : Christmas
	{
		public override int Day => 9;

		public static void Run()
		{
			Console.WriteLine("hello");
			var day = new Day9();
			//day.GetInput(file: "test.txt", pattern: "\r\n");
			day.GetInput();
			Console.WriteLine(day.Level1(day.Input));
			day.PostL1Answer();
			Thread.Sleep(60000);
			Console.WriteLine(day.Level2(day.Input));
			day.PostL2Answer();
		}
		List<long> RealInput = new List<long>();
		public override string Level1(string[] input)
		{
			var answer = findAnswer(input);
			return answer.ToString();

		}
		public long findAnswer(string[] input, int pream = 24)
		{
			RealInput.Clear();
			Array.ForEach(input, x => RealInput.Add(Convert.ToInt64(x)));
			int start = 0;
			int end = pream;
			int j = 0;
			int k = 0;

			for (int i = end + 1; i < RealInput.Count; i++)
			{
				for (j = start; j <= end - 1; j++)
				{
					for (k = start + 1; k <= end; k++)
					{
						if (RealInput[i] == RealInput[j] + RealInput[k])
						{
							j = end;
							k = end;
						};
					}
				}
				if (j == k)
				{
					start++;
					end++;
					continue;
				}
				else return RealInput[i];
			}
			return 0;
		}

		public override string Level2(string[] input)
		{

			var a = findAnswer(input);
			for (int i = 0; i < RealInput.Count - 1; i++)
			{
				for (int j = i + 1; j < RealInput.Count; j++)
				{
					var currentRange = RealInput.Skip(i).Take(j - i + 1).ToList();
					var sum = currentRange.Sum();
					if (sum == a)
					{
						return (currentRange.Min() + currentRange.Max()).ToString();
					}
					else if (sum > a)
					{
						break;
					}
				}
			}
			return "none";

		}
	}

}