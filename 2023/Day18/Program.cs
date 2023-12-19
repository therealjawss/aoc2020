using ChristmasGifts;
using System.Text.RegularExpressions;
var d = new Day18();
Feature.Local = false;
if (Feature.Local)
    await d.GetInput(file: "test.txt", pattern: Environment.NewLine);
else
    await d.GetInput();
Console.WriteLine($"Part 1:{d.RunFirst()}");
//await d.PostFirstAnswer();
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer();
public class Day18 : Christmas
{
    string result = "todo";
    public Day18() : base("18", "2023") { }
    public Instruction[] Instructions { get; set; }
    public override string First()
    {
        Instructions = RawInput.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(x => ParseInstruction(x.Trim())).ToArray();
        var calculator = new Calculator();
        var longway = calculator.ProcessData(Instructions);
        Console.WriteLine(longway);
        var total = Calculator.ProcessDataIntelligently(Instructions);
        return total.ToString();
    }


    public override string Second()
    {
        var instructions = Instructions.Select(ParseCorrectInstruction).ToArray();
        var calculator = new Calculator();
        var total = Calculator.ProcessDataIntelligently(instructions);

        //var subtotal = instructions.Sum(i => i.Places);
        //total = total + subtotal;
        return total.ToString();
    }

    public record Instruction(Direction Direction, long Places, string color);
    public record Direction(long x, long y)
    {
        public static Direction Right = new(1, 0);
        public static Direction Left = new(-1, 0);
        public static Direction Up = new(0, -1);
        public static Direction Down = new(0, 1);
    }

    public Instruction ParseInstruction(string input)
    {
        Regex regex = new Regex(@"(\w) (\d+) \((#[0-9a-fA-F]{6})\)");
        Match match = regex.Match(input);
        if (match.Success)
        {
            string direction = match.Groups[1].Value;
            long places = long.Parse(match.Groups[2].Value);
            string color = match.Groups[3].Value;
            Direction parsedDirection = direction switch
            {
                "R" => Direction.Right,
                "L" => Direction.Left,
                "U" => Direction.Up,
                "D" => Direction.Down,
                _ => throw new ArgumentException("Invalid direction"),
            };
            return new Instruction(parsedDirection, places, color);
        }
        else
        {
            throw new ArgumentException("Invalid input format");
        }
    }


    public Instruction ParseCorrectInstruction(Instruction instruction)
    {
        var hex = instruction.color[1..6];
        long decimalValue = long.Parse(hex, System.Globalization.NumberStyles.HexNumber);

        var direction = instruction.color[6] switch
        {
            '0' => Direction.Right,
            '1' => Direction.Down,
            '2' => Direction.Left,
            '3' => Direction.Up,
            _ => throw new ArgumentException("Invalid direction"),
        };
        return new Instruction(direction, decimalValue, $"#{hex}{direction}");
    }

    public record Point(long x, long y);

    public class Calculator
    {
        public string[] grid;
        public static double CalculatePolygonArea(Point[] points)
        {
            long area = 0;

            for (int i = 0; i < points.Length; i++)
            {
                int j = (i + 1) % points.Length;
                Console.WriteLine($"<<{area} += [({points[i].x}*{points[j].y}):({(points[i].x * points[j].y)} )] - [({points[j].x}*{points[i].y}):({(points[j].x * points[i].y)})]>>");

                area += (points[i].x * points[j].y) - (points[j].x * points[i].y);
            }
            Console.WriteLine($"area {area} => {area / 2}");
            area /= 2;
            area = Math.Abs(area);

            return area;
        }
        public int ProcessData(Instruction[] instructions)
        {
            long maxX = instructions.Where(i => i.Direction == Direction.Right).Sum(i => i.Direction.x * i.Places);
            long maxY = instructions.Where(i => i.Direction == Direction.Down).Sum(i => i.Direction.y * i.Places);

            grid = Enumerable.Range(0, (int)maxX * 2 + 1).Select(x => new string('.', (int)maxY * 2 + 1)).ToArray();

            var first = "%";
            var x = maxX;
            var y = maxY;
            foreach (var instruction in instructions)
            {
                for (int i = 0; i < instruction.Places; i++)
                {
                    x += instruction.Direction.x;
                    y += instruction.Direction.y;
                    grid[y] = grid[y][..(int)x] + first + grid[y][((int)x + 1)..];
                    if(first == "%")
                        first = "#";

                }
            }

            var leastX = grid.Where(line => line.Contains("#")).Min(x => x.IndexOf("#"));

            var temp = grid.Where(line => line.Contains('#')).Select(line => line[leastX..]).ToList();


            temp.ForEach(Console.WriteLine);
            var before = new string('.', temp[0].Length);
            var after = temp[1];
            int total = 0;
            for (int i = 0; i < temp.Count; i++)
            {
                after = (i + 1 == temp.Count) ? new string('.', temp[i].Length) : temp[i + 1];
                total += countFill(temp[i], before, after);
                before = temp[i];
            }

            return total;

        }

        public static double ProcessDataIntelligently(Instruction[] instructions)
        {
            var points = findPoints(instructions);
              PrintPoints(points);
            return CalculatePolygonArea(points);

        }
        public static void PrintPoints(Point[] points)
        {
            var minX = points.Min(p => p.x);
            var maxX = points.Max(p => p.x) + 0 - minX;
            var minY = points.Min(p => p.y);
            var maxY = points.Max(p => p.y) + 0 - minY;

            Console.WriteLine($" min max  {minX}:{maxX} {minY}:{maxY}");
            //    for (int i = 0; i < maxY; i++)
            //    {
            //       for (int j = 0; j < maxX; j++)
            //  {
            //if (points.Any(p => p.x == j + minX && p.y == i + minY))
            //Console.Write("#");
            // else
            ////Console.Write(".");
            //          }
            // Console.WriteLine();
            //         }
        }

        private static Point[] findPoints(Instruction[] instructions)
        {
            var points = new List<Point>();
            var lastDirection = instructions.Last().Direction;

            bool isXpadded = instructions[0].Direction.x == Direction.Left.x;
            bool isYpadded = instructions[0].Direction.y == Direction.Up.y;

            var ptr = new Point(0, 0);
            for (int i = 0; i < instructions.Length; i++)
            {
                var nextDirection = i + 1 == instructions.Length ? instructions[0].Direction : instructions[i + 1].Direction;
                (var direction, long places) = (instructions[i].Direction, instructions[i].Places);
                var newPoint = new Point(
                    ptr.x + places * direction.x,
                    ptr.y + places * direction.y);

                // pad if turn 
                Point padding = new Point(0, 0);
                if (direction.y == 0)
                {
                    if (lastDirection.y != nextDirection.y)
                    {
                        padding = padding with { x = isXpadded ? -1 * direction.x : direction.x };
                        isXpadded = !isXpadded;
                    }
                    else
                    {
                    }

                }
                else if (direction.x == 0)
                {
                    if (lastDirection.x != nextDirection.x)
                    {
                        padding = padding with { y = isYpadded ? -1 * direction.y : direction.y };
                        isYpadded = !isYpadded;

                    }
                    else
                    {

                    }
                }

                var paddedNewPoint = newPoint with
                {
                    x = newPoint.x + (padding.x * (direction.x != 0 ? direction.x : 1)),
                    y = newPoint.y + (padding.y * (direction.y != 0 ? direction.y : 1))
                };
                points.Add(paddedNewPoint);
                lastDirection = direction;
                ptr = paddedNewPoint;
                // Console.Write($"{paddedNewPoint.x}:{paddedNewPoint.y} ");
            }

            return points.ToArray();
        }
        private int countFill(string line, string before, string after)
        {
            bool isEdge((string Value, int Index) value)
            {
                return value.Value.Length == 1 || before[value.Index] == '#' && after[value.Index + value.Value.Length - 1] == '#' ||
                    before[value.Index + value.Value.Length - 1] == '#' && after[value.Index] == '#';
            }

            int total = 0;
            Regex regex = new Regex(@"(#+)");
            var matches = regex.Matches(line).Select(match => (match.Value, match.Index)).ToList();
            bool wasInside = false;
            for (int i = 0; i < matches.Count; i++)
            {
                if (wasInside)
                {
                    total += matches[i].Index - matches[i - 1].Index - matches[i - 1].Value.Length;
                }

                if (isEdge(matches[i]))
                {
                    wasInside = !wasInside;
                }
                total += matches[i].Value.Length;
            }

            return total;
        }

    }
}
