using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2020.Days
{

    public class Day15 : Christmas
    {
        public override int Day => 15;
        public static void Run()
        {
            Run1();
            Run2();
        }
        public static void Run1()
        {
            var d = new Day15();
            d.GetInput();
            Console.WriteLine(d.Level1(d.Input));
        }
        public static void Run2()
        {
            var d = new Day15();
            d.GetInput();
            Console.WriteLine(d.Level2(d.Input));
        }

        public override string Level1(string[] input)
		{
			return GetNthNumber(input, 2020);
		}
        public override string Level2(string[] input)
        {
            return GetNthNumber(input, 30000000);
        }


        List<int> GetData() => "6,3,15,13,1,0".Split(',').Select(i => int.Parse(i)).ToList();
        public string GetNthNumber(string[] input, int limit, int turn = 0)
		{
			var numbers = input[0].Split(",").Select(x => int.Parse(x)).ToList();
			var ctr = 1;
			long last = numbers[0];

			Dictionary<int, (int, int)> words = new();

            long[] wordList = new long[limit];
			foreach (var number in numbers)
			{
                wordList[number] = 0;
				words.Add(number, (ctr, -1));
				ctr++;
				last = number;
			}
			while (ctr <= limit && ctr <= turn)
			{
				//if (words[last].Item2 == -1)
				//{
				//	last = 0;
				//	AddToWordList(words, last, ctr);
				//}
				//else
				//{
				//	last = words[last].Item1 - words[last].Item2;
				//	AddToWordList(words, last, ctr);
				//}
                if (wordList[last] == 0) {
                    last = 0;
                    wordList[0] = ctr - wordList[0];
                } else
				{
                    last = wordList[last];
                    wordList[last] = ctr - wordList[last];
				}

				ctr++;
			}

			return last.ToString();
		}

		private void AddToWordList(Dictionary<int, (int, int)> words, int last, int ctr)
        {
            if (words.ContainsKey(last))
            {
                var prev = words[last].Item1;
                words[last] = (ctr, prev);
            }
            else
            {
                words.Add(last, (ctr, -1));
            }
        }

      
    }
}