using ChristmasGifts;
using System.Text.RegularExpressions;
var d = new Day03();
//await d.GetInput(file: "test.txt", pattern: Environment.NewLine);
await d.GetInput();
Console.WriteLine($"Part 1:{d.RunFirst()}");
//await d.PostFirstAnswer();
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer();
public class Day03 : Christmas
{
    string result = "todo";
    public Day03() : base("3", "2023") { }
    Dictionary<(int, int), List<long>> potentialGears = new();
    public override string First()
    {
        var numbers = Input.GetNumbers(potentialGears);
        return numbers.Sum().ToString();
    }
    public override string Second()
    {
        var realGears = potentialGears.Where(g => g.Value.Count() == 2).ToList();

        return realGears.Sum(g => g.Value[0] * g.Value[1]).ToString();
    }
}
public static class Day03Extensions
{
    public static IEnumerable<int> GetNumbers(this string[] grid, Dictionary<(int, int), List<long>>? dict = null)
    {
        dict ??= [];
        for (int i = 0; i < grid.Length; i++)
        {
            string? line = grid[i];
            var regex = new Regex(@"\d+");
            var matches = regex.Matches(line);

            foreach (Match match in matches)
            {
                var index = match.Index;
                var length = match.Value.Length;

                bool notYielded = true;

                if (notYielded && index > 0)
                {
                    var idx = index - 1;
                    if (line[idx].IsSymbol())
                    {
                        if (line[idx] == '*')
                            AddGear(dict, i, idx, match.Value);
                        notYielded = false;
                        yield return int.Parse(match.Value);
                    }
                }
                if (notYielded && index + length < line.Length)
                {
                    var idx = index + length;
                    if (line[idx].IsSymbol())
                    {
                        if (line[idx] == '*')
                            AddGear(dict, i, idx, match.Value);
                        notYielded = false;
                        yield return int.Parse(match.Value);
                    }
                }
                if (notYielded && i > 0)
                {
                    var previousLine = grid[i - 1];
                    var start = index == 0 ? 0 : index - 1;
                    int end = GetEndIndexToCheck(index, length, previousLine);
                    var toCheck = previousLine[start..end];

                    if (toCheck.ContainsSymbol())
                    {
                        TryAddGear(dict, i - 1, match.Value, toCheck, start);
                        notYielded = false;
                        yield return int.Parse(match.Value);
                    }
                }
                if (notYielded && i + 1 < grid.Length)
                {
                    var nextLine = grid[i + 1];
                    var start = index == 0 ? 0 : index - 1;
                    var end = GetEndIndexToCheck(index, length, nextLine);
                    var toCheck = nextLine[start..end];

                    if (toCheck.ContainsSymbol())
                    {
                        TryAddGear(dict, i + 1, match.Value, toCheck, start);
                        notYielded = false;
                        yield return int.Parse(match.Value);
                    }
                }
            }
        }
    }

    private static void TryAddGear(Dictionary<(int, int), List<long>> dict, int i, string value, string toCheck, int start)
    {
        var gear = toCheck.IndexesOf('*');
        foreach (var g in gear)
        {
            AddGear(dict, i, start + g, value);
        }

    }
    public static int[] IndexesOf(this string line, char c)
    {
        return line.Select((ch, i) => ch == c ? i : -1).Where(i => i != -1).ToArray();
    }

    private static void AddGear(Dictionary<(int, int), List<long>> dict, int i, int j, string value)
    {

        if (!dict.ContainsKey((i, j)))
        {
            dict.Add((i, j), []);
        }
        dict[(i, j)].Add(int.Parse(value));
    }

    private static int GetEndIndexToCheck(int index, int length, string line)
    {
        return (length + index) == line.Length ? line.Length : length + index + 1;
    }

    public static bool ContainsSymbol(this string line)
    {
        return !line.All(c => c == '.');
    }

    public static bool IsSymbol(this char c)
    {
        return c != '.';
    }

}
