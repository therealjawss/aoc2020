using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2020.Days
{

	public class Day22 : Christmas
	{
		public override int Day => 22;
		public static void Run()
		{
			var d = new Day22();
			d.GetInput();
			Console.WriteLine(d.Level1(d.Input));
			Console.WriteLine(d.Level2(d.Input));

		}

		Queue<int>[] P;
		public override string Level1(string[] input)
		{
			ParseInput(input);

			do
			{
				var p1 = P[0].Dequeue();
				var p2 = P[1].Dequeue();
				var win = p1 > p2 ? 0 : 1;
				P[win].Enqueue(Math.Max(p1, p2));
				P[win].Enqueue(Math.Min(p1, p2));
			} while (hasWinner() == -1);

			var answer = P[hasWinner()].Reverse().ToList().Aggregate((total: 0, index: 1), (val, next) => ((val.total + next * val.index), val.index + 1)).total;
	
			return answer.ToString();
		}
		private int hasWinner()
		{
			return P[0].Count == 0 ? 1 : P[1].Count == 0 ? 0 : -1;
		}

		public override string Level2(string[] input)
		{
			var answer = "";
			ParseInput(input);
			Dictionary<string, int> rounds = new();
			var win = RecursiveCombat(P, rounds);

			answer = P[win].Reverse().ToList().Aggregate((total: 0, index: 1), (val, next) => ((val.total + next * val.index), val.index + 1)).total.ToString();

			return answer.ToString();
		}

		private int RecursiveCombat(Queue<int>[] P, Dictionary<string, int> rounds)
		{
			var win = hasWinner2(P, rounds);
			while (win==-1) 
			{
				int[] p = new int[2];
				p[0] = P[0].Dequeue();
				p[1] = P[1].Dequeue();

				if (p[0] <= P[0].Count && p[1] <= P[1].Count)
				{
					var p1 = new Queue<int>(P[0].ToArray()[..p[0]]);
					var p2 = new Queue<int>(P[1].ToArray()[..p[1]]);
					win = RecursiveCombat(new Queue<int>[] { p1, p2 },new  Dictionary<string, int>());
				}
				else
				{
					win = p[0] > p[1] ? 0 : 1;
				}

				P[win].Enqueue(p[win]);
				P[win].Enqueue(p[(win + 1) % 2]);
				win = hasWinner2(P, rounds);
			}
			return win;
		}
		int gctr = 0;
 		private int hasWinner2(Queue<int>[] P, Dictionary<string, int> rounds)
		{
			var result = P[0].Count == 0 ? 1 : P[1].Count == 0 ? 0 : -1;
			var play = "p1"+string.Join(",", P[0].Select(x => x.ToString())) +"p2"+ string.Join(",", P[1].Select(x => x.ToString()));
			if (rounds.ContainsKey(play))
				result = 0;
			else
			{
				gctr++;
				rounds.Add(play, gctr);
			}
			return result;
		}
	

		private void ParseInput(string[] input)
		{
			var len = input.Length / 2;
			var idx = len + 1;
			var p1 = new Queue<int>(input[1..len].Select(x => int.Parse(x)));
			var p2 = new Queue<int>(input[idx..].Select(x => int.Parse(x)));
			P = new Queue<int>[] { p1, p2 };
		}



	}
}