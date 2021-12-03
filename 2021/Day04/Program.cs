using ChristmasGifts;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var d = new Day4();
await d.GetInput();
Console.WriteLine($"Part 1:{d.First()}");
//await d.PostFirstAnswer();

Console.WriteLine($"Part 2:{d.Second()}");
//await d.PostSecondAnswer();


public class Day4 : Christmas
{
    public Day4() : base("4", "2021")
    {

    }

    public override string First()
    {
        return $"Todo";
    }

    public override string Second()
    {
        return $"Todo";
    }
}