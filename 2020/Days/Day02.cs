using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2020.Days
{

    public class Day02 : Christmas
    {
        public override int Day => 2;
		public override string Level1(string[] input)
        {
            int ctr = 0;
            foreach (var line in input)
            {
                var entry = GetEntry(line);
                var occurences = entry.password.ToCharArray().Count(x => x == entry.target);
                if (occurences >= entry.min && occurences <= entry.max)
                {
                    ctr++;
                }
            }

            return ctr.ToString();
        }

        public override string Level2(string[] input)
        {

            int ctr = 0;
            foreach (var line in input)
            {
                var entry = GetEntry(line);
                if (IsValid(entry))
                {
                    ctr++;
                }
            }

            return ctr.ToString();
        }

        public static bool IsValid(Entry entry)
        {
            var isValid = (entry.min <= entry.password.Length ? entry.password[entry.min - 1] == entry.target : false) ^
                                (entry.max <= entry.password.Length ? entry.password[entry.max - 1] == entry.target : false);
            return isValid;
        }

        private static Entry GetEntry(string entry)
        {
            var pattern = @"(\d+)-(\d+)\s(\w):\s(\w*)";
            var r = new Regex(pattern, RegexOptions.IgnoreCase);
            Match m = r.Match(entry);
            if (m.Success)
            {
                return new Entry(Convert.ToInt32(m.Groups[1].Value), Convert.ToInt32(m.Groups[2].Value), Convert.ToChar(m.Groups[3].Value), m.Groups[4].Value);
            }

            return null;
        }


    }
    public record Entry(int min, int max, char target, string password);
}