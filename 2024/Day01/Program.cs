// See https://aka.ms/new-console-template for more information
using ChristmasGifts;

Console.WriteLine("Hello, World!");
var d = new Day01();
await d.GetInput();

Console.WriteLine($"Part 1:{d.RunFirst()}");
// Part 1:1320851
//await d.PostFirstAnswer();

Console.WriteLine($"Part 2:{d.RunSecond()}");
//await d.PostSecondAnswer();
public class Day01 : Christmas
{
    public Day01() : base("1", "2024")
    {
    }

    public record Pairs(int first, int second);
    public override string First()
    {
        var pairs = Input.Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).Select(x => new Pairs(x[0], x[1])).ToList();

        var firstList = pairs.Select(x => x.first).OrderBy(x=>x).ToList();
        var secondList = pairs.Select(x => x.second).OrderBy(x=>x).ToList();

        var result = firstList.Zip(secondList, (f, s) => Math.Abs(f - s)).Sum();

        return result.ToString();
    }

    public override string Second()
    {
        var pairs = Input.Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).Select(x => new Pairs(x[0], x[1])).ToList();

        var firstList = pairs.Select(x => x.first).OrderBy(x => x).ToList();
        var secondList = pairs.Select(x => x.second).OrderBy(x => x).ToList();

        var result = firstList.Select(x => secondList.Where(s => s == x).Count() * x).Sum();

        return result.ToString();

    }
}