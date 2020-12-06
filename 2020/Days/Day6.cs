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

		public override string Level1(string[] input)
		{
			var toAdd = new HashSet<char>();
			long counter = 0;
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i].Length > 0)
				{
					for (int j = 0; j < input[i].Length; j++)
					{
						toAdd.Add(input[i][j]);
					}
				}
				else
				{
					counter += toAdd.Count;
					toAdd = new HashSet<char>();
				}

			}
			counter += toAdd.Count;
			return counter.ToString();
		}

		public override string Level2(string[] input)
		{
			List<char> answers = new List<char>();
			long counter = 0;
			int peopleCtr = 0;
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i].Length > 0)
				{
					peopleCtr++;
					for (int j = 0; j < input[i].Length; j++)
					{
						answers.Add(input[i][j]);
					}
				}
				else
				{
					counter += processGroupAnswers(answers, peopleCtr);

					answers = new List<char>();
					peopleCtr = 0;

				}

			}
			counter += processGroupAnswers(answers, peopleCtr);

			return counter.ToString();
		}

		private long processGroupAnswers(List<char> answers, int peopleCtr)
		{
			var counter = 0;
			var questions = answers.GroupBy(x => x);

			foreach (var question in questions)
			{
				if (question.Count() == peopleCtr)
				{
					counter++;
				}
			}

			return peopleCtr;
		}



		public override string[] GetInput(string file = null, string pattern = null)
		{
			string buffer = base.ReadBuffer(file);
			if (pattern == null)
			{
				Input = buffer.Split("\n").ToArray();
			}
			else
			{

				Input = buffer.Split(pattern).ToArray();
				//var r = new Regex(pattern);
				// Input = Regex.Matches(buffer, pattern).Cast<Match>().Select(m => m.Value).ToArray();
			}
			return Input;
		}

	}
}
