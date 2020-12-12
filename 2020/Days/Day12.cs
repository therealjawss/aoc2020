using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AOC2020.Days
{
    public class Day12 : Christmas
    {
        public override int Day => 12;
        public static void Run()
        {
            var day = new Day12();
            Console.WriteLine($"hello day {day.Day}");
            //day.GetInput(file: "test.txt", pattern: "\r\n");
            day.GetInput();
            Console.WriteLine(day.Level1(day.Input));
            //day.PostL1Answer();
            //	Thread.Sleep(60000);
            Console.WriteLine(day.Level2(day.Input));
            //day.PostL2Answer();
        }
        List<instruction> Instructions = new();
        public override string Level1(string[] input)
		{
			ParseInstructions(input);

            coord position = new coord(0, 0, (1,0));

            position = Instructions.Aggregate(position, (val, instruction)=>Move(instruction, val.x, val.y, val.dir));
      
            var result = Math.Abs(position.x) + Math.Abs(position.y);

			return result.ToString();
		}

		private void ParseInstructions(string[] input)
		{
            Instructions.Clear();
			var pattern = @"(\w)(\d+)";
			for (int i = 0; i < input.Length; i++)
			{
				var entry = new Regex(pattern).Match(input[i]);
				Instructions.Add(new instruction(entry.Groups[1].Value, Convert.ToInt32(entry.Groups[2].Value)));
			}
		}

		public override string Level2(string[] input)
        {
            ParseInstructions(input);
            coord Ship = new coord(0, 0, (1, 0));
            (int,int) WayPoint = (10, 1);

            (Ship, WayPoint) =  Instructions.Aggregate((Ship, WayPoint),
                                    (val, instruction) => instruction.action switch {
                                        ("F") => (new coord(val.Ship.x + val.WayPoint.Item1*instruction.param, val.Ship.y + val.WayPoint.Item2 * instruction.param, val.Ship.dir), val.WayPoint),
                                        ("N") or ("S") or ("W") or ("E") => (val.Ship, MoveWaypoint(instruction,val.WayPoint)), 
                                        ("R") => (val.Ship, turnWaypoint(val.WayPoint, instruction.param)),
                                        ("L")=> (val.Ship, turnWaypoint(val.WayPoint, -instruction.param)),
                                        _=> (val.Ship, val.WayPoint)
                                });

                
            var result = Math.Abs(Ship.x) + Math.Abs(Ship.y);

            return result.ToString();

        }

        private (int, int) MoveWaypoint(instruction instruction, (int, int) wp) {

            return instruction.action switch
            {
                ("N") => (wp.Item1, wp.Item2 + instruction.param),
                ("S") => (wp.Item2, wp.Item2 - instruction.param),
                ("W") => (wp.Item1 - instruction.param, wp.Item2),
                ("E") => (wp.Item1+instruction.param, wp.Item2),
                _=> (wp.Item1, wp.Item2)
            };
        }

        private coord Move(instruction instruction, int x, int y, (int, int) dir)
        {
			return instruction.action switch
			{
				"F" => new coord(x + dir.Item1 * instruction.param, y + dir.Item2 * instruction.param, dir),
				"N" => new coord(x, y + instruction.param, dir),
				"S" => new coord(x, y - instruction.param, dir),
				"E" => new coord(x + instruction.param, y, dir),
				"W" => new coord(x - instruction.param, y, dir),
				"L" => turn(new coord(x, y, dir), -instruction.param),
				"R" => turn(new coord(x, y, dir), instruction.param),
				_ => new coord(x, y, dir),
			};
		}

        private coord turn(coord coord, int param)
        {
			return param switch
			{
				90 or -270 => new coord(coord.x, coord.y, (coord.dir.Item2, -coord.dir.Item1)),
				-90 or 270 => new coord(coord.x, coord.y, (-coord.dir.Item2, coord.dir.Item1)),
				_ => new coord(coord.x, coord.y, (-coord.dir.Item1, -coord.dir.Item2)),
			};
		}
        private (int,int) turnWaypoint((int,int) coord, int param)
        {
			return param switch
			{
				90 or -270 => (coord.Item2, -coord.Item1),
				-90 or 270 => (-coord.Item2, coord.Item1),
				_ => (-coord.Item1, -coord.Item2),
			};
		}

        record coord(int x, int y, (int, int) dir);
        record instruction(string action, int param);
    }
}
