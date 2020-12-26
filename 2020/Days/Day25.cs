using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2020.Days
{
	public class Day25 : Christmas
	{
		public override int Day => 25;
		public static void Run()
		{
			var d = new Day25();
			d.GetInput();
			Console.WriteLine(d.Level1(d.Input));
		}
		public override string Level1(string[] input)
		{
			var answer = GetKey();

			return answer.ToString();

		}
		ulong max = 10000000;
		private ulong GetKey()
		{
			ulong PKCard = 5764801; // 
			ulong PKdoor = 17807724;//	
									//ulong PKCard = 363891; // transform(SN,cloop)
									//ulong PKdoor = 335121;// transform(SN, dloop)

			var dLS = FindLoopSize(PKdoor);
			var cLS = FindLoopSize(PKCard);

			if (Transform(cLS, PKdoor) == Transform(dLS, PKCard))
				return Transform(cLS, PKdoor);
			return 0;
		}

		public ulong FindLoopSize(ulong key)
		{
			for (ulong i = 1; i < max; i++)
			{
				if (Transform(i) == key)
				{
					return i;
				}
			}
			return 0;
		}


		Dictionary<(ulong, ulong), ulong> lookup = new();
		Dictionary<ulong, ulong> modLookup = new();
		public ulong Transform(ulong loopSize, ulong SN = 7)
		{
			if (!lookup.ContainsKey((SN, loopSize)))
			{
				ulong result = 1;
				if (!lookup.ContainsKey((SN, loopSize - 1)))
				{
					for (ulong i = 0; i < loopSize - 1; i++)
					{
						result *= SN;
						result = result % 20201227;
					}
					lookup.Add((SN, loopSize - 1), result);
				}
				lookup[(SN, loopSize)] = (lookup[(SN, loopSize - 1)] * SN) % 20201227;
			}

			return lookup[(SN, loopSize)];
		}
	}

}

