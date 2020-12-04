using AOC2020.Days;
using AOC2020.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2020.Days
{
    public class Day4 : Christmas
    {
        public override int Day => 4;
        public override string Level1(string[] input)
        {
            int ctr = 0;
            foreach (var entry in input)
            {
                var passport = ParsePassport(entry);

                if (Validator.SimpleValidate(passport))
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
    public class Passport
    {
        public KeyValuePair<string, string>[] Properties { get; set; }

        internal bool IsValid()
        {
            return Validator.SimpleValidate(this) && ValidYears() && ValidHeight() && ValidHC() && ValidEC() && ValidPPNo();
        }

        private bool ValidPPNo()
        {
            return ValidPattern("pid", @"^\d{9}$");
        }

        private bool ValidEC()
        {
            var val = Extract("ecl");
            if (val == null) return false;
            return Regex.Match(val, "^(amb|blu|brn|gry|grn|hzl|oth)$").Success;
        }
        private bool ValidPattern(string key, string pattern)
        {
            var val = Extract(key);
            if (val == null) return false;
            return Regex.Match(val, pattern).Success;
        }

        private bool ValidHC()
        {
            var val = Extract("hcl");
            if (val == null) return false;
            var res = val != null && Regex.Match(val, "^#[0-9a-f]{6}$").Success;
            return res;
        }

        private bool ValidHeight()
        {
            var val = Extract("hgt");
            return Validator.ValidateHeight(val);
        }

        private bool ValidYears()
        {
            return ValidBirth() && ValidIssue() && ValidExpr();
        }

        private bool ValidExpr()
        {
            string val = Extract("eyr");
            return Validator.ValidateEYR(val);
        }

        private bool ValidIssue()
        {
            string val = Extract("iyr");
            return Validator.ValidateIYR(val);
        }



        private bool ValidBirth()
        {
            string val = Extract("byr");
            return Validator.ValidateBYR(val);

        }
        private bool InRange(long val, int min, int max)
        {
            return val >= min && val <= max;
        }
        private string Extract(string key)
        {
            return Properties.FirstOrDefault(x => x.Key.Equals(key)).Value;
        }

    }
}
