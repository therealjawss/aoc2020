using ChristmasGifts;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Xml.XPath;
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

    public record Instruction(Direction Direction, long Places, string color)
    {
        public Instruction(Direction direction, long Places) : this(direction, Places, string.Empty) { }
        public Instruction Previous { get; set; }
    }
    public record Direction(long x, long y)
    {
        public static Direction R = new(1, 0);
        public static Direction L = new(-1, 0);
        public static Direction U = new(0, -1);
        public static Direction D = new(0, 1);
        public bool isHorizontal => x != 0;
        public bool isVertical => x != 0;

        public bool isClockWise(Direction next)
        {
            if (next == Direction.R) return this == Direction.U;
            else if (next == Direction.D) return this == Direction.R;
            else if (next == Direction.L) return this == Direction.D;
            else if (next == Direction.U) return this == Direction.L;
            else throw new ArgumentException("Invalid direction");
        }

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
                "R" => Direction.R,
                "L" => Direction.L,
                "U" => Direction.U,
                "D" => Direction.D,
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
            '0' => Direction.R,
            '1' => Direction.D,
            '2' => Direction.L,
            '3' => Direction.U,
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
            long maxX = instructions.Where(i => i.Direction == Direction.R).Sum(i => i.Direction.x * i.Places);
            long maxY = instructions.Where(i => i.Direction == Direction.D).Sum(i => i.Direction.y * i.Places);

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
                    if (first == "%")
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
            var points = Map(instructions);
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
        public static Point[] Map(Instruction[] instructions)
        {
            var points = new List<Point>();
            var ptr = new Point(0, 0);

            var lastDirection = instructions.Last().Direction;
            instructions[0].Previous = instructions.Last();
            instructions.Last().Previous = instructions[^2];
            bool isXPadded = false; // nstructions[0].Direction.x == Direction.L.x;
            bool isYPadded = false;// instructions[0].Direction.y == Direction.U.y;

            (var direction, long places, _) = instructions[0];
            bool clockwise = isClockwise(direction, lastDirection);
            for (int i = 0; i < instructions.Length; i++)
            {
                instructions[(i + 1) % instructions.Length].Previous = instructions[i];
                var nextDirection = instructions[(i + 1) % instructions.Length].Direction;
                (direction, places, _) = instructions[i];

                long baseX = ptr.x + (places * direction.x);
                long baseY = ptr.y + (places * direction.y);
                long nextX = baseX;
                long nextY = baseY;
                bool padX = false;
                bool padY = false;

                if (direction.isHorizontal)
                {
                    isXPadded = direction == Direction.L;
                    padX = direction.isClockWise(nextDirection) && !isXPadded;
                    if (!clockwise) 
                        padX = !padX;
                    long offsetX;
                    if (padX )
                    {
                        offsetX = (padX && direction.x != 0 ? direction.x : 0);
                        nextX += offsetX;
                    }
                    else if (!padX )
                    {
                        nextX -= 1;
                    }
                }
                else
                {
                    isYPadded = direction == Direction.U;
                    padY = (direction.isClockWise(nextDirection) && isYPadded) ;
                    if (!clockwise)
                        padY = !padY;
                    long offsetY;
                    if (padY && !isYPadded)
                    {
                        offsetY = (padY && direction.y != 0 ? direction.y : 0);
                        nextY += offsetY;
                    }
                    else if (!padY&& isYPadded )
                    {
                        nextY -= 1;
                    }
                }

                var nextPoint = new Point(nextX, nextY);

                points.Add(nextPoint);
                ptr = nextPoint;
                lastDirection = direction;


            }

            return points.ToArray();
        }

        private static bool isClockwise(Direction direction, Direction lastDirection)
        {
            if (direction == Direction.R) return lastDirection == Direction.U;
            else if (direction == Direction.D) return lastDirection == Direction.R;
            else if (direction == Direction.L) return lastDirection == Direction.D;
            else if (direction == Direction.U) return lastDirection == Direction.L;
            else throw new ArgumentException("Invalid direction");
           
        }

        public static Point[] FindPoints(Instruction[] instructions)
        {
            var points = new List<Point>();
            var lastDirection = instructions.Last().Direction;

            bool isXpadded = instructions[0].Direction.x == Direction.L.x;
            bool isYpadded = instructions[0].Direction.y == Direction.U.y;

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
                Console.Write($"{paddedNewPoint.x}:{paddedNewPoint.y} ");
            }
            Console.WriteLine();
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
