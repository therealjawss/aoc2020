using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2020.Days
{

	public class Day23 : Christmas
	{
		public override int Day => 23;
		public static void Run()
		{
			var d = new Day23();
			d.GetInput();
			Console.WriteLine(d.Level1(d.Input));
			Console.WriteLine(d.Level2(d.Input));

		}

		Queue<int>[] P;
		public override string Level1(string[] input)
		{
			//var cups = new Queue<int>(input[0].ToCharArray().Select(x => int.Parse(x.ToString())));
			//var buffer = new Queue<int>(

			var cups = input[0];
			int current = 0;
			for (int i = 0; i < 100; i++)
			{
				string hand = "";
				for (int j = 1; j <= 3; j++)
				{
					hand += cups[(current + j) % cups.Length];
				}
				var next = (current + 4) % cups.Length;
				var d = int.Parse(cups[current].ToString());
				var destination = (d - 1) == 0 ? 9 : d - 1;
				while (hand.Contains(destination.ToString()))
				{
					destination = destination - 1 == 0 ? 9 : destination - 1;
				}
				var buffer = cups + cups;
				var destIndex = buffer.IndexOf(destination.ToString()) + 1;
				var start = destIndex;
				var end = buffer.LastIndexOf(cups[0]);
				var HtoD = buffer.IndexOf(hand) + hand.Length;
				var bet = destIndex;
				cups = buffer[HtoD..bet] + hand + buffer[start..end] + cups[0];
			}
			var b = cups + cups;
			var id = cups.IndexOf("1")+1;
			var to = b.LastIndexOf("1");
			var answer = b[id..to];
			return answer;
		}






	}
}