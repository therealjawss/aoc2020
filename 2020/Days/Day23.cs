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
            var id = cups.IndexOf("1") + 1;
            var to = b.LastIndexOf("1");
            var answer = b[id..to];
            return answer;
        }




        int MAX = 1000000;
        public override string Level2(string[] input)
        {
            var answer = "";
            var cards = input[0].ToCharArray().Select(x => int.Parse(x.ToString())).Concat(MAX == 1000000 ? Enumerable.Range(10, 1000000 - 9) : new int[] { }).ToArray();
            LinkedList<int> lcards = new LinkedList<int>(cards);
            var current = lcards.First;
            var last = lcards.Last;
            Dictionary<int, LinkedListNode<int>> listindex = new Dictionary<int, LinkedListNode<int>>();
            for (int i = 0; i < MAX; i++)
            {
                listindex.Add(current.Value, current);
                current = current.Next;
            }

            current = lcards.First;
            for (int i = 0; i < 10000000; i++)
            {
                LinkedListNode<int> first = current;
                var one = first.GetAfterIn(lcards);
                var two = one.GetAfterIn(lcards);
                var three = two.GetAfterIn(lcards);
                var next = three.GetAfterIn(lcards);

                lcards.Remove(one);
                lcards.Remove(two);
                lcards.Remove(three);

                var hand = new int[] { one.Value, two.Value, three.Value };
                //var hand = cards[1..4];
                var dest = first.Value - 1 == 0 ? MAX : first.Value - 1;

                while (hand.Contains(dest))
                {
                    dest = dest - 1 == 0 ? MAX : dest - 1;
                }
                var d = listindex[dest];
                lcards.AddAfter(d, one);
                lcards.AddAfter(one, two);
                lcards.AddAfter(two, three);
                current = current.GetAfterIn(lcards);

            }
            var o = listindex[1];
            ulong ans = (ulong)o.Next.Value * (ulong)o.Next.Next.Value;
            Console.WriteLine(ans);
            answer = ans.ToString();
            return answer.ToString();

        }



    }
}

public static class ListExtensions
{
    public static int FindIndex(this int[] list, int number)
    {
        return list.Select((n, idx) => (num: n, index: idx)).Where(n => n.num == number).Select(x => x.index).FirstOrDefault();
    }
    public static int LastIndex(this int[] list, int number)
    {
        return list.Select((n, idx) => (num: n, index: idx)).Where(n => n.num == number).Select(x => x.index).LastOrDefault();
    }
    public static LinkedListNode<int> GetAfterIn(this LinkedListNode<int> node, LinkedList<int> list)
    {
        return node.Next == null ? list.First : node.Next;
    }

}


