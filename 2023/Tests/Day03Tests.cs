﻿using FluentAssertions;
using System.Security.Principal;

namespace Tests
{
    public class Day03Tests
    {
        [Fact]
        public void CanCheckAdjacentOnMinimalGridWithNoSymbols()
        {
            var grid = new string[] {
                "1.",
                ".."
            };

            grid.GetNumbers().Should().BeEquivalentTo(new int[] { });
        }
        [Fact]
        public void CanCheckAdjacentOnMinimalGrid()
        {
            var grid = new string[] {
                "1*",
                ".."
            };

            grid.GetNumbers().Should().BeEquivalentTo(new int[] { 1 });
        }

        [Fact]
        public void CanCheckAdjacentOnMinimalGridWithSymbolBelowNumber()
        {
            var grid = new string[] {
                "1.",
                "*."
            };

            grid.GetNumbers().Should().BeEquivalentTo(new int[] { 1 });
        }

        [Fact]
        public void CanCheckAdjacentOnMinimalGridWithSymbolBeforeNumber()
        {
            var grid = new string[] {
                "*1",
                ".."
            };

            grid.GetNumbers().Should().BeEquivalentTo(new int[] { 1 });
        }
        [Fact]
        public void CanCheckDoubleDigitInBiggerGrid()
        {
            var grid = new string[]
            {
                "12..",
                "..$.",
            };
            grid.GetNumbers().Should().BeEquivalentTo(new int[] { 12 });
        }

        [Fact]
        public void CanCheckDoubleDigitInBiggerGridWithSymbolAbove()
        {
            var grid = new string[]
            {
                "+...",
                ".12.",
                "....",
            };
            grid.GetNumbers().Should().BeEquivalentTo(new int[] { 12 });
        }

        [Fact]
        public void CanCheckMoreThan1DoubleDigitInBiggerGridWithSymbolBefore()
        {
            var grid = new string[]
            {
                "+...25..",
                ".12...16",
                ".....+..",
            };
            grid.GetNumbers().Should().BeEquivalentTo(new int[] { 25, 12, 16 });
        }

        [Fact]
        public void CanExtractGears()
        {
            var grid = new string[]
           {
                "+...25..",
                "......*.",
                ".....3..",
           };
            var dict = new Dictionary<(int, int), List<long>>();
            var result = grid.GetNumbers(dict).ToList();

            dict[(1,6)].Should().Contain(25);
            dict[(1,6)].Should().Contain(3);
        }


        [Fact]
        public void CanExtractGearsBeside()
        {
            var grid = new string[]
           {
                "+.3*25..",
                "........",
                ".....3..",
           };
            var dict = new Dictionary<(int, int), List<long>>();
            var result = grid.GetNumbers(dict).ToList();

            dict[(0, 3)].Should().Contain(3);
        }
    }
}
