using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AOC2020.Days
{
    public class Day10 : Christmas
    {
        public override int Day => 10;

        public static void Run()
        {
            Console.WriteLine("hello day 10");
            var day = new Day10();
            //day.GetInput(file: "test.txt", pattern: "\r\n");
            day.GetInput();
            //Console.WriteLine(day.Level1(day.Input));
            //day.PostL1Answer();
            //    Thread.Sleep(60000);
            Console.WriteLine(day.Level2(day.Input));
            //day.PostL2Answer();
        }
        public override string Level1(string[] input)
        {
            var n = AsNumbers.OrderBy(x => x).ToArray();
            int ones = 0;
            int threes = 0;
            int current = 0;
            foreach (var item in n)
            {
                if (current + 1 == item)
                {
                    current += 1;
                    ones++;
                }
                else if (current + 2 == item)
                {
                    current += 2;

                }
                else if (current + 3 == item)
                {
                    current += 3;
                    threes++;
                }
            }
            return (ones * ++threes).ToString();
        }
        private List<long> asNumbers = new();
        public List<long> AsNumbers
        {
            get
            {
                if (asNumbers.Count == 0)
                {
                    Array.ForEach(Input, x => asNumbers.Add(Convert.ToInt64(x)));

                }
                return asNumbers;
            }
        }
        public List<long> GetInputAsNumbers()
        {
            Array.ForEach(Input, x => AsNumbers.Add(Convert.ToInt64(x)));
            return AsNumbers;
        }
        public override string Level2(string[] input)
        {

            AsNumbers.Add(0);
            var n = AsNumbers.OrderBy(x => x).ToList();
            var last = n[n.Count - 1] + 3;
            n.Add(last);
            Array.ForEach(n.ToArray(), Console.WriteLine);
            var graph = new AdjacencyGraph<long, TaggedEdge<long,int>>();
            Array.ForEach(n.ToArray(), x => graph.AddVertex(x));
            Array.ForEach(n.ToArray(), x =>
                Array.ForEach(n.Where(y => y - x <= 3 && y - x > 0).ToArray(), y => graph.AddEdge(createEdge(x, y))));
            var result = CountPathsToEnd(n, 0);
            return result.ToString();
        }

		private TaggedEdge<long, int> createEdge(long x, long y)
		{
            return new TaggedEdge<long, int>(x, y, (int)(y-x));
		}
        Dictionary<int, long> found = new();
		private long CountPathsToEnd(List<long> list, int currentIndex)
        {
            if (list[currentIndex] == list[list.Count - 1]) return 1;
            var next = list.Where(x => x - list[currentIndex] <= 3 && x - list[currentIndex] > 0).ToList();
            if (next.Count == 0) return 0;
            long paths = 0;
            foreach (var item in next)
            {
                var idx = list.IndexOf(item);
                if (found.ContainsKey(idx))
				{
                    paths += found[idx];
				}else
				{
                    var p =  CountPathsToEnd(list, list.IndexOf(item));
                    found.Add(idx, p);
                    paths += p;

				}
            }
            return paths;
        }
    }
}
