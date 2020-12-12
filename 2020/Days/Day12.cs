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

			var result = Process(Instructions);

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

            var result = Process2(Instructions);
            return result.ToString();

        }

        coord Ship;
        coord WayPoint;
		private int Process2(List<instruction> instructions)
		{
            Ship = new coord(0, 0, (1, 0));
            WayPoint = new coord(10, 1, (1, 0));

            foreach (var instruction in instructions)
            {
                MoveThings(instruction);
            }

            return Math.Abs(Ship.x) + Math.Abs(Ship.y);
        }
        private void MoveThings(instruction instruction)
		{
			switch (instruction.action)
			{
                case "F":
                    coord dest;
                    for(int i=0; i<instruction.param; i++)
					{
                        Ship = new coord(Ship.x + WayPoint.x, Ship.y + WayPoint.y, Ship.dir); ;
					}
                    break;
                case "N":
                case "S":
                case "W":
                case "E":
                    WayPoint = Move(instruction, WayPoint.x, WayPoint.y, Ship.dir);
                    break;
                case "R":
                    WayPoint = TurnWaypoint(Ship, WayPoint, instruction.param);
                    break;
                case "L":
                    WayPoint = TurnWaypoint(Ship, WayPoint, -instruction.param);
                    break;
                default:
                    break;
			}
		}

		private coord TurnWaypoint(coord ship, coord wayPoint, int param)
		{
            var possible = new List<(int, int)> { (wayPoint.x, wayPoint.y), (wayPoint.y, -wayPoint.x), (-wayPoint.x, -wayPoint.y), (-wayPoint.y, wayPoint.x) };
            var moveBy = (param / 90 + 4) % 4;

            var newCoord = possible[moveBy];
            return new coord(newCoord.Item1, newCoord.Item2, ship.dir);
		}

		private int Process(List<instruction> instructions)
        {
            coord position = new coord(0, 0, (1,0));
            foreach (var instruction in instructions)
            {
                position = Move(instruction, position.x, position.y, position.dir);

            }
            return Math.Abs(position.x) + Math.Abs(position.y);

        }
        private coord Move(instruction instruction, int x, int y, (int, int) dir)
        {
            switch (instruction.action)
            {
                case "F": return new coord(x + dir.Item1 * instruction.param, y + dir.Item2 * instruction.param, dir);
                case "N": return new coord(x, y + instruction.param, dir);
                case "S": return new coord(x, y - instruction.param, dir);
                case "E": return new coord(x + instruction.param, y, dir);
                case "W": return new coord(x - instruction.param, y, dir);
                case "L": return turn(new coord(x, y, dir), -instruction.param);
                case "R": return turn(new coord(x, y, dir), instruction.param);
                default:
                    return new coord(x, y, dir);
            }
        }

        private coord turn(coord coord, int param)
        {
            var directions = new List<Direction>() { Direction.North, Direction.East, Direction.South, Direction.West };
            Direction dir = TupleToDirection(coord.dir);

            var start = directions.IndexOf(dir);
            var moveBy = (param / 90);
            moveBy = (start + moveBy + 4) % 4;
            var newDir = directions[ (moveBy)];
            return new coord(coord.x, coord.y, DirectionToTuple(directions, newDir));
        }

        private static Direction TupleToDirection((int, int) coord)
        {
            return coord switch
            {
                (0, 1) => Direction.North,
                (0, -1) => Direction.South,
                (1, 0) => Direction.East,
                (-1, 0) => Direction.West,
                _ => Direction.None

            };
        }

        private static (int, int) DirectionToTuple(List<Direction> directions, Direction newDir)
        {
            return newDir switch
            {
                Direction.North => (0, 1),
                Direction.South => (0, -1),
                Direction.East => (1, 0),
                Direction.West => (-1, 0),
                Direction.None => (0, 0)
            };
        }

       
        public enum Direction
        {
            North, South, West, East, None
        }
        record coord(int x, int y, (int, int) dir);
        record instruction(string action, int param);
    }
}
