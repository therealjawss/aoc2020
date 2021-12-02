using ChristmasGifts;

var d = new Day2();
await d.GetInput();

Console.WriteLine($"Part 1:{d.First()}");
Console.WriteLine($"Part 2:{d.Second()}");


public class Day2 : Christmas
{
    public Day2() : base("2","2021")
    {
    }

    public override string First()
    {
        (int h, int d) = Input.Aggregate((0, 0), (current, line) => Parse(current, line));
        return (h*d).ToString();
    }

    public override string Second()
    {
        (long h, long d, long a) = Input.Aggregate(((long)0, (long)0, (long)0), (current, line)
      => ParseEnhanced(current, line));

        return $"{h * d}";
    }
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
