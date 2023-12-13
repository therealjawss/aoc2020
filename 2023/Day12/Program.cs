using ChristmasGifts;

var d = new Day12();
if (Feature.Local)
    await d.GetInput(file: "test.txt", pattern: Environment.NewLine);
else
    await d.GetInput();
Console.WriteLine($"Part 1:{d.RunFirst()}");
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 
public class Day12 : Christmas
{
    string result = "todo";
    public Day12() : base("12", "2023") { }
    Conditions[] conditions;
    public override void Setup()
    {
        conditions = ParseInput(Input);
    }

    public static Conditions[]  ParseInput(string[] Input)
    {
        return Input.Select(i =>
        {
            var parts = i.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return new Conditions(parts[0].Split('.', StringSplitOptions.RemoveEmptyEntries).ToArray(),
                parts[1].Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new string('#', int.Parse(x))).ToArray());
        }).ToArray();
    }

    public override string First()
    {
        result = conditions.Sum(x => GetArrangements(x.patternGroups, x.sequence)).ToString();
        return result;
    }
    public override string Second()
    {
        return result;
    }

    public static int GetArrangements(string[] patternGroups, string[] sequence)
    {
        var combinations = 1;
        if (patternGroups.Length == sequence.Length)
        {
            for (int i = 0; i < patternGroups.Length; i++)
            {
                combinations = combinations * GetCombinations(patternGroups[i], sequence[i]);
            }

        }

        return 1;
    }

    private static int GetCombinations(string v1, string v2)
    {
        if (v1 == v2)
            return 1;
        else
        {
            for (int i = 0; i < v1.Length; i++)
}               if (v1[i] ==)
        }
    }

    public record Conditions(string[] patternGroups, string[] sequence);
}
