using AOC2020.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2020.Tools
{
	public class Validator
	{
		public static bool SimpleValidate(Passport passport)
		{
			var validKeys = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
			return passport.Properties.Where(x => validKeys.Contains(x.Key)).Count() == 7 && passport.Properties.Length <= 8;
		}

		public static bool ValidateBYR(string input)
		{
			bool result = MinYearLength(input, out long value);
			result &= InRange(value, 1920, 2002);

			return result;
		}
		public static bool ValidateIYR(string input)
		{
			bool result = MinYearLength(input, out long value);
			result &= InRange(value, 2010, 2020);

			return result;
		}
		public static bool ValidateEYR(string input)
		{
			bool result = MinYearLength(input, out long value);
			result &= InRange(value, 2020, 2030);
			return result;
		}
		private static bool MinYearLength(string input, out long value)
		{
			bool result = input.Length == 4;
			result &= Int64.TryParse(input, out value);
			return result;
		}
		private static bool InRange(long value, int min, int max)
		{
			return value >= min && value <= max;
		}

		public static bool ValidateHeight(string input)
		{
			var pattern = @"^(\d+)(in|cm)$";
			var m = Regex.Match(input, pattern);
			bool result = m.Success;
			if (m.Success)
			{
				result &= Int64.TryParse(m.Groups[1].Value, out long hgt);
				if (result)
				{

					if (m.Groups[2].Value.Equals("cm"))
					{
						result &= InRange(hgt, 150, 193);
					}
					else
					{
						result &= InRange(hgt, 59, 76);
					}
				}

			}
			return result;
		}
	}
}
