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
			//Console.WriteLine(d.Level2(d.Input));

		}
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
	}
			return answer.ToString();
		}
		Queue<int>[] P;
		Stack<int>[] Game = new Stack<int>[2];
		int GameLen;
		private int hasWinner()
		{
			return P[0].Count == GameLen ? 0 : P[1].Count == GameLen ? 1 : -1;
		}
		private void ParseInput(string[] input)
		{
			var len = input.Length / 2;
			var idx = len + 1;
			var p1 = new Queue<int>(input[1..len].Select(x => int.Parse(x)));
			var p2 = new Queue<int>(input[idx..].Select(x => int.Parse(x)));
			GameLen = (len-1) * 2;
			P = new Queue<int>[] { p1, p2 };
		}



		public override string Level2(string[] input)
		{
			var answer = "";
			return answer;
		}


	}
}