using System;

namespace AOC2020.Days
{

    public class Day15 : Christmas
    {
        public override int Day => 15;
        public static void Run(){
            Run1();
            //Run2();
        }
        public static void Run1() {
            var d = new Day15();
            d.GetInput();
            Console.WriteLine(d.Level1(d.Input));
        }
        public static void Run2() {
            var d = new Day15();
            d.GetInput();
            Console.WriteLine(d.Level1(d.Input));
        }

		public override string Level1(string[] input)
        {
            return "";
        }

        public override string Level2(string[] input)
        {
            return "";
        }

    }
}