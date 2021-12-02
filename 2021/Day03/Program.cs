using ChristmasGifts;

var d = new Day3();
await d.GetInput();

Console.WriteLine($"Part 1:{d.First()}");
Console.WriteLine($"Part 2:{d.Second()}");


public class Day3 : Christmas
{
    public Day3() : base("3", "2021")
    {
    }

    public override string First()
    {
        return $"todo {1}";
    }

    public override string Second()
    {
        return $"todo {2}";
    }
   
}
