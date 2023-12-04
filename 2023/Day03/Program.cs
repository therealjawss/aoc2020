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

                if (index > 0)
                {
                    var idx = index - 1;
                    if (line[idx].IsSymbol())
                    {
                        if (line[idx] == '*')
                            AddGear(dict, i, idx, match.Value);
                        yield return int.Parse(match.Value);
                        continue;
                    }
                }
                if (index + length < line.Length)
                {
                    var idx = index + length;
                    if (line[idx].IsSymbol())
                    {
                        if (line[idx] == '*')
                            AddGear(dict, i, idx, match.Value);
                        yield return int.Parse(match.Value);
                        continue;
                    }
                }
                var start = index == 0 ? 0 : index - 1;
                var end = (length + index) == line.Length ? line.Length : length + index + 1;

                var previousIndex = i - 1;
                var nextIndex = i + 1;

                var previousLine = i > 0 ? grid[previousIndex][start..end]:"";
                var nextLine = i + 1 < grid.Length ? grid[nextIndex][start..end]:"";

                if (previousLine.ContainsSymbol())
                {
                    TryAddGear(dict, previousIndex, match.Value, previousLine, start);
                    yield return int.Parse(match.Value);
                    continue;
                }
                if (nextLine.ContainsSymbol())
                {
                    TryAddGear(dict, nextIndex, match.Value, nextLine, start);
                    yield return int.Parse(match.Value);
                    continue;
                }
            }
        }
    }

    private static void TryAddGear(Dictionary<(int, int), List<long>> dict, int i, string value, string toCheck, int start)
    {
        var gears = Regex.Matches(toCheck, @"\*");
        foreach (Match g in gears)
        {
            AddGear(dict, i, start + g.Index, value);
        }
    }

    private static void AddGear(Dictionary<(int, int), List<long>> dict, int i, int j, string value)
    {

        if (!dict.ContainsKey((i, j)))
        {
            dict.Add((i, j), []);
        }
        dict[(i, j)].Add(int.Parse(value));
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
