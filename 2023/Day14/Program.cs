using ChristmasGifts;
using System.Text.RegularExpressions;
var d = new Day14();
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
public class Day14 : Christmas
{
    string result = "todo";
    public Day14() : base("14", "2023") { }
    public override string First()
    {
        var sideways = Input.Select(x => "").ToArray();

        for (int i = 0; i < Input.Length; i++)
            for (int j = 0; j < Input[i].Length; j++)
            {
                sideways[i] += Input[j][i];
            }

        int totalWeight = 0;
        foreach (var line in sideways)
        {
            var weight = 0; 
            var matches = Regex.Matches(line, "#");
            var ptr = 0;

            if (matches.Count() > 0)
            {
                var indexes = matches.Select(x => x.Index).ToArray();
                for (int i=0; i < indexes.Length; i++)
                {
                    var w= getWeight(line[ptr..(indexes[i])].Count(i => i == 'O'), ptr);
                    weight += w;
                    ptr = indexes[i]+1;
                }
            }
            weight+= getWeight(line[ptr..].Count(i => i == 'O'), ptr);
            totalWeight+=weight;
        }
        return totalWeight.ToString();
    }

    private int getWeight(int v, int ptr)
    {
        var weight = 0;
        var load = Input.Length - ptr;
        for (;v>0;v--)
        {
            weight += load--;
        }
        return weight;
    }

    public override string Second()
    {
        return result;
    }
}
