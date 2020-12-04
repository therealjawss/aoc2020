using AOC2020.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2020.Models
{
    public class Passport
    {
        public KeyValuePair<string, string>[] Properties { get; set; }

        internal bool IsValid()
        {
            return PassportValidator.SimpleValidate(this) &&
                PassportValidator.ValidateBYR(Extract("byr")) &&
                PassportValidator.ValidateIYR(Extract("iyr")) &&
                PassportValidator.ValidateEYR(Extract("eyr")) &&
                PassportValidator.ValidateHeight(Extract("hgt")) &&
                PassportValidator.ValidateHairColor(Extract("hcl")) &&
                PassportValidator.ValidatePattern(Extract("ecl"), "^(amb|blu|brn|gry|grn|hzl|oth)$") &&
                PassportValidator.ValidatePattern(Extract("pid"), @"^\d{9}$");
        }
        private string Extract(string key)
        {
            return Properties.FirstOrDefault(x => x.Key.Equals(key)).Value;
        }

    }
}
