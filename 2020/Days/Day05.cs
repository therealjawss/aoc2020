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
	public class Day05 : Christmas
	{
		public override int Day => 5;

		public override string Level1(string[] input)
		{
			int highest = 0;
			for (int i = 0; i < input.Length; i++)
			{
				var id = FindId(input[i]);
				highest = (id > highest) ? id : highest;
			}

			return highest.ToString();
		}

		List<int> seatQueue = new List<int>();
		public override string Level2(string[] input)
		{
			var list = new Queue<int>();
			for (int i = 0; i < input.Length; i++)
			{
				AddToList(FindId(input[i]));
			}
			for (int i = 0; i < seatQueue.Count; i++)
			{
				if (seatQueue[i + 1] - seatQueue[i] == 2) return (seatQueue[i] + 1).ToString();
			}

			return base.Level2(input);
		}

		private void AddToList(int seatNumber)
		{
			Insert(seatNumber, 0, seatQueue.Count);
		}

		private void Insert(int seatNumber, int startindex, int endIndex)
		{
			if (startindex != endIndex && seatNumber > seatQueue[startindex])
			{
				Insert(seatNumber, startindex + 1, endIndex);
			}
			else
			{
				seatQueue.Insert(startindex, seatNumber);
				return;
			}
		}

		public int FindId(string input)
		{
			var row = input.Substring(0, 7);
			var column = input.Substring(7, 3);

			var result = Traverse(row, 0, 127, 'F') * 8 + Traverse(column, 0, 7, 'L');
			return result;
		}

		public int Traverse(string inst, int bottom, int top, char lowerCode)
		{
			if (bottom == top)
			{
				return bottom;
			}

			var midpoint = (int)((top - bottom) / 2) + bottom;

			if (inst[0] == lowerCode)
			{
				return Traverse(inst.Substring(1), bottom, midpoint, lowerCode);
			}
			else
			{
				return Traverse(inst.Substring(1), midpoint + 1, top, lowerCode);
			}

		}

	}
}
