using AOC2020.Days;
using AOC2020.Models;
using AOC2020.Tools;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace AOCTest
{
    public class Day4Test
    {
        [Fact]
        public void CompleteProperties()
        {
            var passport = new Passport {
                Properties = new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("byr", "2002"),
                    new KeyValuePair<string, string>("iyr", "2002"),
                    new KeyValuePair<string, string>("eyr", "2002"),
                    new KeyValuePair<string, string>("hgt", "2002"),
                    new KeyValuePair<string, string>("hcl", "2002"),
                    new KeyValuePair<string, string>("ecl", "2002"),
                    new KeyValuePair<string, string>("pid", "2002"),
                    new KeyValuePair<string, string>("cid", "2002"),
                }
            };

            bool result = PassportValidator.SimpleValidate(passport);

            result.Should().Be(true);
        }
        [Fact]
        public void CompletePropertiesExcludingCID()
        {
            var passport = new Passport
            {
                Properties = new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("byr", "2002"),
                    new KeyValuePair<string, string>("iyr", "2002"),
                    new KeyValuePair<string, string>("eyr", "2002"),
                    new KeyValuePair<string, string>("hgt", "2002"),
                    new KeyValuePair<string, string>("hcl", "2002"),
                    new KeyValuePair<string, string>("ecl", "2002"),
                    new KeyValuePair<string, string>("pid", "2002"),
                }
            };

            bool result = PassportValidator.SimpleValidate(passport);

            result.Should().Be(true);
        }
        [Theory]
        [InlineData("1920", true)]
        [InlineData("1919", false)]
        [InlineData("12345", false)]
        [InlineData("2002", true)]
        [InlineData("2003", false)]
        public void ValidBirthYear(string input, bool expected)
		{
            bool result = PassportValidator.ValidateBYR(input);

            result.Should().Be(expected);
		}

        [Theory]
        [InlineData("1920", false)]
        [InlineData("2010", true)]
        [InlineData("2020", true)]
        [InlineData("2019", true)]
        [InlineData("2021", false)]
        [InlineData("2oeu", false)]
        public void ValidIssueYear(string input, bool expected)
		{
            bool result = PassportValidator.ValidateIYR(input);
            result.Should().Be(expected);
		}
        [Theory]
        [InlineData("1920", false)]
        [InlineData("2020", true)]
        [InlineData("2030", true)]
        [InlineData("2031", false)]
        public void ValidExpirationYear(string input, bool expected)
		{
            bool result = PassportValidator.ValidateEYR(input);
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("60in", true)]
		[InlineData("60", false)]
		[InlineData("60cm", false)]
		[InlineData("cm", false)]
		[InlineData("1incm", false)]
		public void ValidHeight(string input, bool expected)
		{
            bool result = PassportValidator.ValidateHeight(input);
            result.Should().Be(expected);
		}
    }

    
}
