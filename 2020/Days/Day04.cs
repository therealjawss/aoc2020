using AOC2020.Days;
using AOC2020.Models;
using AOC2020.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2020.Days
{
    public class Day04 : Christmas
    {
        public override int Day => 4;
        public override string Level1(string[] input)
        {
            int ctr = 0;
            foreach (var entry in input)
            {
                var passport = ParsePassport(entry);

                if (PassportValidator.SimpleValidate(passport))
                {
                    ctr++;
                }


            }
            return ctr.ToString(); ;
        }
        public override string Level2(string[] input)
        {
            int ctr = 0;
            foreach (var entry in input)
            {
                var passport = ParsePassport(entry);
                if (passport.IsValid())
                {
                    ctr++;
                }


            }
            return ctr.ToString(); ;
        }

        private Passport ParsePassport(string entry)
        {
            var pattern = @"(\w+):(#?\w+)";
            var keys = Regex.Matches(entry, pattern).Cast<Match>();
            var p = new Passport();
            p.Properties = keys.Select(key => new KeyValuePair<string, string>(key.Groups[1].Value, key.Groups[2].Value)).ToArray();
            return p;
        }

    }
}
