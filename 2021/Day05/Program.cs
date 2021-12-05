using ChristmasGifts;

var d = new Day5();
await d.GetInput(file: "test.tx", pattern: "\r\n");
//await d.GetInput();
//await d.PostFirstAnswer();
Console.WriteLine($"Part 1:{d.First()}");
Console.WriteLine($"Part 2:{d.Second()}");
//await d.PostSecondAnswer();


public class Day5 : Christmas
{
    public Day5() : base("5", "2021") { }

    public override string First()
    {
        List<Line> lines = MakeLines();

        var vertical = lines.Where(line => line.p1.x == line.p2.x).ToList();
        var horizontal = lines.Where(line => line.p1.y == line.p2.y).ToList();
        var maxX = lines.SelectMany(line => new List<long>() { line.p1.x, line.p2.x }).Max();
        var maxY = lines.SelectMany(line => new List<long>() { line.p1.y, line.p2.y }).Max();

        var points = new int[maxX + 1, maxY + 1];

        foreach (var line in horizontal)
        {
            (var start, var end) = line.p1.x < line.p2.x ? (line.p1, line.p2) : (line.p2, line.p1);

            for (long i = start.x; i <= end.x; i++)
            {
                points[i, start.y]++;
            }
        }

        foreach (var line in vertical)
        {
            (var start, var end) = line.p1.y < line.p2.y
                ? (line.p1, line.p2) : (line.p2, line.p1);

            for (long i = start.y; i <= end.y; i++)
            {
                points[start.x, i]++;
            }
        }

        var result = from int item in points
                     where item > 1
                     select item;

        return $"{result.Count()}";
    }
    public override string Second()
    {
        List<Line> lines = MakeLines();

        var result = lines
            .SelectMany(x => x.GetPoints())
            .GroupBy(x => x)
            .Where(x => x.Count() >1).ToList();

        return $"{result.Count()}";
    }
    public record Point(long x, long y)
    {
        public override string ToString()
        {
            return $"({x},{y})";
        }
        public Point Increment(int xStep, int yStep)
        {
            return new Point(x+xStep, y+yStep);
        }
    }
    public record Line(Point p1, Point p2)
    {
        public override string ToString()
        {
            return $"{p1} -> {p2}";
        }

        public IEnumerable<Point> GetPoints()
        {
            List<Point> points = new List<Point>() { p1 };
            var diffX = p2.x - p1.x;
            var diffY = p2.y - p1.y;
            var xStep = diffX > 0 ? 1 : diffX<0 ? -1 : 0;
            var yStep = diffY > 0 ? 1 : diffY<0 ? -1 : 0;

            Point p = p1;
            do
            {
                p = p.Increment(xStep, yStep);
                points.Add(p);
            } while (p!=p2);

            return points;
        }
    }
    private List<Line> MakeLines()
    {
        return Input.Select(
            x => x.Trim()
                .Split("->")
                .SelectMany(
                    x => x.Trim()
                        .Split(",", StringSplitOptions.TrimEntries)
                        .Select(x => long.Parse(x))
                ).ToArray()

            ).Select(x => new Line(new Point(x[0], x[1]), new Point(x[2], x[3])))
            .ToList();
    }

}
