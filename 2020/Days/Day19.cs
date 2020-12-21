using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace AOC2020.Days
{

    public class Day19 : Christmas
    {
        public override int Day => 19;

        public static void Run()
        {
            var day = new Day19();
            day.GetInput();
            Run2();

            //Console.WriteLine(day.Level1(day.Input));
            // day.PostL1Answer();
            //Console.WriteLine("Answer should be " + day.Level2(day.Input));
            //day.PostL2Answer();
        }
        public static void Run1()
        {
            var day = new Day19();
            day.GetInput();
            Console.WriteLine(day.Level1(day.Input));
        }
        public static void Run2()
        {
            var day = new Day19();
            day.GetInput();
            Console.WriteLine(day.Level2(day.Input));
        }

        public override string Level1(string[] input)
        {
            ParseInput(input);
            var count = LinesToTest.Where(x => Conforms(x)).Count();
            return count.ToString();
        }

        public bool Conforms(string x)
        {
            bool result = true;
            var rule = Rules[0];
            int i = 0;
            result = Expect(rule, x, ref i) && i == x.Length;

            return result;
        }

        private bool Expect(List<List<int>> rule, string v, ref int i)
        {
            bool result = false;
            foreach (var r in rule)
            {
                var buffer = i;
                result |= Expect(r, v, ref i);
                if (result)
                    return true;
                i = buffer;
            }
            return result;
        }

        private bool Expect(List<int> r, string v, ref int i)
        {
            bool result = true;
            if (i == v.Length) return true;
            for (int i1 = 0; i1 < r.Count; i1++)
            {
                int item = r[i1];
                var buffer = i;
                result &= Expect(item, v, ref i);
                if (i >= v.Length - 1 && result && i1 == 0)
                {
                    if (v.Length == i)
                    {
                        i = buffer;
                        return false;
                    }
                }
                if (!result)
                {
                    i = buffer;
                    return false;
                }
            }
            return result;
        }

        private bool Expect(int item, string v, ref int i)
        {
            if (literals.ContainsKey(item))
            {
                if (i >= v.Length)
                {
                    i++;
                    return true;
                }
                if (literals[item] == v[i])
                {
                    i++;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return Expect(Rules[item], v, ref i);
            }
        }

        public List<string> LinesToTest;
        public void ParseInput(string[] input)
        {
            //get the rules
            var rules = input.Where(x => Regex.Match(x, @"(\d+):").Success).ToList();
            ParseRules(rules);
            //get the input
            LinesToTest = input.Where(x => Regex.Match(x, @"^[ab]+").Success).ToList();
        }

        private void ParseRules(List<string> rules)
        {
            foreach (var rule in rules)
            {
                var idx = int.Parse(rule.Extract(@"^(\d+):"));
                var abpattern = @"""([ab])""";
                var ab = rule.Extract(abpattern);
                if (ab.Length > 0)
                {
                    literals.Add(idx, ab[0]);
                }
                else
                {
                    var idxpattern = @"( (\d+))+";
                    var combinations = Regex.Matches(rule, idxpattern).Cast<Match>();
                    var clist = new List<List<int>>();
                    foreach (var c in combinations)
                    {
                        var plist = c.Value.Trim().Split(" ").Select(x => int.Parse(x)).ToList();
                        clist.Add(plist);
                    }
                    Rules.Add(idx, clist);
                }
            }
        }
        Dictionary<int, char> literals = new();
        Dictionary<int, List<List<int>>> Rules = new();

        public override string Level2(string[] input)
        {
            ParseInput(input);
            var count = LinesToTest.Where(x => Conforms(x)).Count();
            return count.ToString();
        }
    }
    public static class Regextensions
    {
        public static string Extract(this string input, string pattern)
        {
            return Regex.Match(input, pattern).Groups[1].Value;
        }

        //public static List<string> Extract(this string input, string pattern) {
        //    return Regex.Matches(input, pattern).Cast<Match>().Select(x => x.Groups[1].Captures.Select(y => y.Value)).ToList();
        //}

    }
}