using ChristmasGifts;
var d = new Day09();
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
public class Day09 : Christmas
{
    string result = "todo";
    public Day09() : base("9", "2023") { }
    public override string First()
    {
        List<int> extrapolated = Extrapolate(inReverse: false);
        result = extrapolated.Sum().ToString();
        return result;
    }

    public override string Second()
    {
        var extrapolated = Extrapolate(inReverse: true);
        result = extrapolated.Sum().ToString();
        return result;
    }


    private List<int> Extrapolate(bool inReverse = false)
    {
        List<int> extrapolated = new();
        foreach (var line in Input)
        {

            var input = line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x));
            var differences = new List<int>();
            var sequence = new Stack<int>();
            var numbers = input.ToList();

            do
            {
                differences = numbers.Zip(numbers.Skip(1), (prev, curr) => curr - prev).ToList();
                sequence.Push(inReverse ? differences.First() : differences.Last());
                numbers = differences;

            } while (differences.Any(x => x != 0));

            var e = new Stack<int>();
            e.Push(0);
            foreach (var item in sequence)
            {
                e.Push(item + (inReverse ? -1 : 1) * e.Peek());
            }
            var toAdd = inReverse ? input.First() - e.Peek() : input.Last() + e.Peek();
            extrapolated.Add(toAdd);
        }

        return extrapolated;
    }

}
