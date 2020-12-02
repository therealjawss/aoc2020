using System;
using System.Linq;

namespace AOC
{

    public class DayTwo : DayModule
    {
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

        private static Entry GetEntry(string entry)
        {
            var buffer = entry.Split(' ');
            var text = buffer[0].Split('-');
            return new Entry(Convert.ToInt32(text[0]), Convert.ToInt32(text[1]), buffer[1][0], buffer[2]);
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
        public static bool IsValid(string entry)
        {
            return IsValid(GetEntry(entry));
        }

        public static bool IsValid(Entry entry)
        {
            var isValid  =  (entry.min <= entry.password.Length ? entry.password[entry.min-1] == entry.target : false) ^
                                (entry.max <= entry.password.Length ? entry.password[entry.max-1] == entry.target : false);
            return isValid;
        }
    }
    public record Entry(int min, int max, char target, string password);
}