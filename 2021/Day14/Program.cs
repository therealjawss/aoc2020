using ChristmasGifts;
var d = new Day14();
var newLine = "\n\n";
//await d.GetInput(file: "test.txt", pattern: newLine);
await d.GetInput(pattern: newLine); 
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 1:{d.RunFirst()}");
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 
public class Day14 : Christmas
{
    string result = "todo";
    public Day14() : base("14", "2021") { }
    public override string First()
    {
        var template = Input[0];
        var irules = Input[1].Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var rules = irules
            .Select(x => x.Split("->", StringSplitOptions.TrimEntries))
            .ToDictionary(x => x[0], y => y[1]);
        var polymer = template;
        for (int i = 0; i < 10; i++)
        {
            var newString = "" + polymer[0];
            for (int j = 0; j < polymer.Length - 1; j++)
            {
                var toAdd = rules[polymer.Substring(j, 2)];
                newString += toAdd + polymer[j + 1];

            }
            polymer = newString;
        }
        result = (polymer.ToCharArray().GroupBy(x => x).Max(x => x.Count()) -
            polymer.ToCharArray().GroupBy(x => x).Min(x => x.Count())).ToString();

        return result;
    }

    public override string Second()
    {
        var template = Input[0];
        var irules = Input[1].Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var rules = irules
            .Select(x => x.Split("->", StringSplitOptions.TrimEntries))
            .ToDictionary(x => x[0], y => y[1][0]);
        var polymerCount = rules.ToDictionary(x => x.Key, y => 0UL);
        var letterCount = polymerCount.Keys.SelectMany(x => x.ToCharArray()).Distinct().ToDictionary(x => x, y => 0UL);

        for (int i = 0; i < template.Length - 1; i++)
        {
            polymerCount[template.Substring(i, 2)] = 1;
        }
        foreach(var c in template)
        {
            letterCount[c]++;
        }

        for (int i = 0; i < 40; i++)
        {
            var newPolymerCount = rules.ToDictionary(x => x.Key, y => 0UL);
            foreach (var polymer in polymerCount.Where(x => x.Value > 0))
            {
                var count = polymer.Value;
                var toAdd = rules[polymer.Key];
                letterCount[toAdd] += count;
                newPolymerCount[$"{polymer.Key[0]}{toAdd}"] += count;
                newPolymerCount[$"{toAdd}{polymer.Key[1]}"] += count;
            }
            polymerCount = newPolymerCount;
        }

        result = (letterCount.Max(x => x.Value) - letterCount.Min(x => x.Value)).ToString();
        return result;

    }

}