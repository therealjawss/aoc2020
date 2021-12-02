using AOC2021.Days;

Console.WriteLine("Advent of Code 2021!\n**************");
var input = await File.ReadAllTextAsync("input.txt");

var lines = input.Split("\r\n");
(int h, int d) = lines.Aggregate((0, 0), (current, line) => Day02.Parse(current, line));
Console.WriteLine($"Part 1 {h * d}");

(long h2, long d2, long a) = lines.Aggregate(((long)0, (long)0, (long)0), (current, line)
      => Day02.ParseEnhanced(current, line));
Console.WriteLine($"Part 2:{h2 * d2}");

public class Day02
{
    public static (int h, int d) Parse((int h, int d) point, string input)
    {
        var x = Int32.Parse(input.Split(" ")[1]);
        if (input.StartsWith("forward"))
            return (point.h+x, point.d);
        else if (input.StartsWith("down"))
            return (point.h, point.d+x);
        else if (input.StartsWith("up"))
            return (point.h, point.d-x);

        return (point.h, point.d);
    }

    public static (long h, long d, long a) ParseEnhanced((long h, long d, long a) point, string input)
    {
        var x = Int64.Parse(input.Split(" ")[1]);
        if (input.StartsWith("forward"))
            return (point.h+x, point.d+point.a*x, point.a);
        else if (input.StartsWith("down"))
            return (point.h, point.d, point.a+x);
        else if (input.StartsWith("up"))
            return (point.h, point.d, point.a-x);

        return (point.h, point.d, point.a);
    }
}