using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AOC2020.Days
{
    public class Day9 : Christmas
    {
        public override int Day => 9;

        public static void Run()
        {
            Console.WriteLine("hello");
            var day = new Day9();
            day.GetInput(file: "test.txt", pattern: "\r\n");
            //  day.GetInput();
            Console.WriteLine(day.Level1(day.Input));
            //  day.PostL1Answer();
            Console.WriteLine(day.Level2(day.Input));
            //day.PostL2Answer();
        }
        List<ulong> realInput = new List<ulong>();
        public override string Level1(string[] input)
        {
            var answer = findAnswer(input);
            return answer.ToString();

        }
        public ulong findAnswer(string[] input, int pream = 24)
        {
            realInput.Clear();
            Array.ForEach(input, x => realInput.Add(Convert.ToUInt64(x)));
            int start = 0;
            int end = pream;
            int j = 0;
            int k = 0;

            for (int i = end + 1; i < realInput.Count; i++)
            {
                for (j = start; j <= end - 1; j++)
                {
                    for (k = start + 1; k <= end; k++)
                    {
                        if (k == j) continue;
                        if (realInput[i] == realInput[j] + realInput[k])
                        {
                            j = end;
                            k = end;
                        };
                    }
                }
                if (j == k)
                {
                    start++;
                    end++;
                    continue;
                }
                else return realInput[i];
            }
            return 0;
        }

        public override string Level2(string[] input)
        {

            var a = findAnswer(input);
            for (int i = 0; i < realInput.Count - 2; i++)
            {
                var tempsum = realInput[i];
                var s = realInput[i];
                var l = realInput[i];
                for (int j = i+1; j < realInput.Count - 1; j++)
                {
                    if (i == j) continue;
                    tempsum += realInput[j];
                    s = realInput[j] < s ? realInput[j] : s;
                    l = realInput[j] > l ? realInput[j] : l;
                    if (tempsum == a)
                    {
                        return (s + l).ToString();
                    } else if (tempsum > a)
					{
                        break;
					}
                }
            }
            return "";

        }
    }

}