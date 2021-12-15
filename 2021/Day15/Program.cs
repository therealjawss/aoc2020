using ChristmasGifts;
var d = new Day15();
var test = false;
if (test)
{
    await d.GetInput(file: "test.txt", pattern: "\r\n");
}
else
{
    await d.GetInput(pattern: "\n");
}

//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 1:{d.RunFirst()}");
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 
public class Day15 : Christmas
{
    public int[][] RiskMap;
    string result = "todo";
    Dictionary<(int,int), int> riskLookup = new Dictionary<(int, int), int>();
    public int GetMinimumRiskUntil(int i, int j)
    {
        if (i - 1 < 0 && j - 1 < 0)
            return 0;
        if (riskLookup.ContainsKey((i, j)))
            return riskLookup[(i, j)];
        Console.WriteLine($"looking up: {i}, {j}");
        var thisRisk = RiskMap[i][j];

        if (i - 1 < 0) 
            thisRisk += GetMinimumRiskUntil(i, j - 1);
        else if (j - 1 < 0)
            thisRisk += GetMinimumRiskUntil(i - 1, j);
        else
        {

            var opt1 = GetMinimumRiskUntil(i - 1, j);
            var opt2 = GetMinimumRiskUntil(i, j - 1);
            if (opt1 < opt2)
                thisRisk += GetMinimumRiskUntil(i - 1, j);
            else if (opt1 > opt2)
                thisRisk += GetMinimumRiskUntil(i, j - 1);
            else
                thisRisk += opt1;
        }

        riskLookup.Add((i, j), thisRisk);
        return thisRisk;
    }

    public Day15() : base("15", "2021") { }

    public Day15(int[][] riskmap) : this()
    {
        RiskMap = riskmap;
    }
    public override string First()
    {
        var risk = Input.Select(x => x)
            .Select(x => x.ToCharArray().Select(y => int.Parse(y.ToString())).ToArray())
            .ToArray();
        var totalRisk = 0;

        RiskMap = risk;
        totalRisk = GetMinimumRiskUntil(RiskMap.Length - 1, RiskMap[0].Length - 1);
        result = totalRisk.ToString();
        return result;
    }

    public int GetMinimumRisk()
    {
        return 4;
    }

    public override string Second()
    {
        return result;
    }
}
