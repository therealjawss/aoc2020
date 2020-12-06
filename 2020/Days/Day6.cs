using AOC2020.Days;
using AOC2020.Models;
using AOC2020.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2020.Days
{
	public class Day6 : Christmas
	{
		public override int Day => 6;

		public static void Run()
		{
			Christmas day = new Day6();
			day.GetInput();
			Console.WriteLine(day.Level1(day.Input));
			//day.PostL1Answer();
			Task.Delay(60000);
			Console.WriteLine(day.Level2(day.Input));
			//day.PostL2Answer();

		}
		public override string Level1(string[] input)
		{
			long counter = 0;
			for (int i = 0; i < input.Length; i++)
			{
				var answers = (input[i].Where(c => Char.IsLetter(c))).Distinct().ToList();
				counter += answers.Count;
			}

			return counter.ToString();
		}

		public override string Level2(string[] input)
		{
			long counter = 0;

			for (int i = 0; i < input.Length; i++)
			{
				var answers = (input[i].Where(c => Char.IsLetter(c))).ToList();
				int peopleCtr = input[i].Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).Count();

				counter += processGroupAnswers(answers, peopleCtr);
			}

			return counter.ToString();
		}

		private int processGroupAnswers(List<char> answers, int peopleCtr)
		{
			int counter = 0;
			var result = answers.GroupBy(x => x);
			counter += (result.Where(r => r.Count() == peopleCtr)).Count();

			return counter;
		}

		public override string[] GetInput(string file = null, string pattern = null, Func<string, bool> predicate = null)
		{
			return base.GetInput(file, pattern: "\n\n", x => true); ;
		}

	}
}
