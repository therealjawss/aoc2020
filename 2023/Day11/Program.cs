using ChristmasGifts;
using System.Data;
var d = new Day11();
Feature.Local = false;
if (Feature.Local)
    await d.GetInput(file: "test.txt", pattern: Environment.NewLine);
else
    await d.GetInput();
Console.WriteLine($"Part 1:{d.RunFirst()}");
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
await d.PostSecondAnswer(); 
public class Day11 : Christmas
{
    string result = "todo";
    public Day11() : base("11", "2023") { }

    IEnumerable<LineSegment> clear = new List<LineSegment>();
    List<Point> galaxies = new();
    public override string First()
    {
        galaxies = FindGalaxies(Input).ToList();
        clear = FindClearLines(Input).ToList();

        Dictionary<LineSegment, long> distances = new();
        for (int i = 0; i < galaxies.Count(); i++)
        {
            for (int j = i + 1; j < galaxies.Count(); j++)
            {
                var g1 = galaxies.ElementAt(i);
                var g2 = galaxies.ElementAt(j);
                var distance = FindMinDistance(Input, g1, g2);
                //Console.WriteLine($"{g1} {g2}: {distance}");
                distances.Add(new LineSegment(galaxies.ElementAt(i), galaxies.ElementAt(j)), (long)distance);
            }
        }

        result = distances.Sum(x => x.Value).ToString();

        return result;
    }
    public override string Second()
    {

        Dictionary<LineSegment, ulong> distances = new();
        for (int i = 0; i < galaxies.Count(); i++)
        {
            for (int j = i + 1; j < galaxies.Count(); j++)
            {
                var g1 = galaxies.ElementAt(i);
                var g2 = galaxies.ElementAt(j);
                var distance = FindMinDistance(Input, g1, g2, factor:1000000);
                distances.Add(new LineSegment(galaxies.ElementAt(i), galaxies.ElementAt(j)), (ulong)distance);
            }
        }

        result = distances.Sum(x => (decimal)x.Value).ToString();

        return result;
    }

    public IEnumerable<LineSegment> FindClearLines(string[] map)
    {
        var rows = new List<LineSegment>();
        var cols = new List<LineSegment>();
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i].All(x => x == '.'))
                yield return new LineSegment(new Point(i, 0), new Point(i, map[i].Length - 1));
            if (map.All(x => x[i] == '.'))
                yield return new LineSegment(new Point(0, i), new Point(map.Length - 1, i));
        }
    }

    public long FindMinDistance(string[] map, Point point1, Point point2, int factor = 2)
    {
        int deltaX = point2.X - point1.X;
        int deltaY = point2.Y - point1.Y;
        int distance = Math.Abs(deltaX) + Math.Abs(deltaY);

        var line = new LineSegment(point1, point2);

        var i = clear.Where(x =>
        {
            return line.DoIntersect(x);
        }).Count();


        return (long)(distance) + i * (factor - 1);
    }

    public IEnumerable<Point> FindGalaxies(string[] map)
    {
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == '#')
                    yield return new Point(i, j);
            }
        }
    }

    public record Point(int X, int Y);
    public record LineSegment(Point Start, Point End)
    {
        public bool DoIntersect(LineSegment two)
        {
            if(two.Start.X == two.End.X)
            {
                return Start.X < End.X ? Start.X <= two.Start.X && End.X >= two.Start.X
                    : Start.X >= two.Start.X && End.X <= two.Start.X;
            } else if (two.Start.Y == two.End.Y)
            {
                return Start.Y < End.Y ? Start.Y <= two.Start.Y && End.Y >= two.Start.Y
                    : Start.Y >= two.Start.Y && End.Y <= two.Start.Y;
            }
            else
            {
                return false;
            }
        }
    }
}
