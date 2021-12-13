using ChristmasGifts;
var d = new Day13();
//await d.GetInput(file: "test.txt", pattern: "\r\n\r\n");
await d.GetInput(pattern: "\n\n");
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 1:{d.RunFirst()}");
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 
public class Day13 : Christmas
{
    string result = "todo";
    public Day13() : base("13", "2021") { }
    public override string First()
    {
        var map = Input[0].Split("\n", StringSplitOptions.TrimEntries)
                .Select(x => x.Split(",")
                .Select(num => int.Parse(num))
                .ToArray());
        var pointMap = map.Select(x => new Point(x[0], x[1])).ToHashSet();

        var foldinginstructions = Input[1].Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split("="))
            .Select(x =>
            {
                var end = x[0].ToCharArray()[x[0].Length - 1];
                if (end == 'x') return new Fold(Axis.x, int.Parse(x[1]));
                else return new Fold(Axis.y, int.Parse(x[1]));
            }).ToList();

        var instruction  = foldinginstructions.First();
        pointMap = FoldMap(pointMap, instruction);

        result = pointMap.Count.ToString();
        return result;
    }
    public record Point(int x, int y);
    public enum Axis { x = 0, y =1};
    public record Fold(Axis Axis, int index);
    public override string Second()
    {
        var map = Input[0].Split("\n", StringSplitOptions.TrimEntries)
            .Select(x => x.Split(",")
            .Select(num => int.Parse(num))
            .ToArray());
        var pointMap = map.Select(x => new Point(x[0], x[1])).ToHashSet();

        var foldinginstructions = Input[1].Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split("="))
            .Select(x =>
            {
                var end = x[0].ToCharArray()[x[0].Length - 1];
                if (end == 'x') return new Fold(Axis.x, int.Parse(x[1]));
                else return new Fold(Axis.y, int.Parse(x[1]));
            }).ToList();

        foreach(var instruction in foldinginstructions)
            pointMap = FoldMap(pointMap, instruction);

        var maxX = pointMap.Max(x => x.x)+1;
        var maxY = pointMap.Max(x => x.y)+1;

        for (int i = 0; i<maxY; i++)
        {
            for (int j = 0; j<maxX; j++)
            {
                if (pointMap.Contains(new Point(j, i))) Console.Write("#");
                else Console.Write(".");
            }
            Console.WriteLine();
        }

        result = pointMap.Count.ToString();

        return result;
    }

    public HashSet<Point> FoldMap(HashSet<Point> Map, Fold instruction)
    {
        var groups = Map.GroupBy(x => instruction.Axis == Axis.x ? x.x : x.y)
            .Select(y => y).ToList();
        var points = groups.Where(p => p.Key < instruction.index)
            .SelectMany(x => x)
            .Select(x => new Point(x.x, x.y))
            .ToHashSet();
        var otherpoints = groups.Where(p => p.Key> instruction.index)
            .SelectMany(x => x)
            .Select(x => new Point(x.x, x.y))
            .Select(p => instruction.Axis == Axis.x ? new Point(instruction.index - (p.x - instruction.index), p.y) : new Point(p.x, instruction.index - (p.y-instruction.index)))
            .ToHashSet();
        return points.Union(otherpoints).ToHashSet();
    }
}
