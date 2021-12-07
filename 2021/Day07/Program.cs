using ChristmasGifts;

var d = new Day7();
//await d.GetInput(file: "test.txt", pattern: ",");
await d.GetInput(pattern: ",");
//await d.PostFirstAnswer();
Console.WriteLine($"Part 1:{d.First()}");
Console.WriteLine($"Part 2:{d.Second()}");
//await Task.Delay(5000);
await d.PostSecondAnswer();


public class Day7 : Christmas
{
    string result = "todo";
    public Day7() : base("7", "2021") { }

    public override string First()
    {
        var numbers = Input.Select(x => int.Parse(x)).OrderBy(x => x).ToArray();

        var fuel = 0;
        foreach (var item in numbers)
        {
            fuel += Math.Abs(item - numbers[numbers.Length / 2]);
        }
        return fuel.ToString();
    }
    public override string Second()
    {
        var numbers = Input.Select(x => int.Parse(x)).OrderBy(x => x).ToArray();
  
        var average = numbers.Sum() / numbers.Length;
        var middle = (numbers.Min() + numbers.Max()) / 2;
        var start = average < middle ? average : middle;
        var end = average > middle ? average : middle;

        var minFuel = long.MaxValue;
        for (int i = start; i <= end; i++)
        {
            var fuel = 0;
            foreach (var item in numbers)
            {
                fuel += Enumerable.Range(1, Math.Abs(i - item)).Sum();
            }

            minFuel = fuel < minFuel ? fuel : minFuel;
        }

        return minFuel.ToString();
    }
}
