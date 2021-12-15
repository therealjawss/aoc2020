using ChristmasGifts;
using System.Diagnostics;

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
    Dictionary<(int, int), int> riskLookup = new Dictionary<(int, int), int>();
    public int GetMinimumRiskUntil(int i, int j)
    {
        if (i - 1 < 0 && j - 1 < 0)
            return 0;
        if (riskLookup.ContainsKey((i, j)))
            return riskLookup[(i, j)];
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
    Dictionary<string, int> megaRiskLookup = new Dictionary<string, int>();

    public int GetMinimumRiskForMegaUntil(int i, int j)
    {
        var keyTemplate = $"{i}:{j}";
        if (i - 1 < 0 && j - 1 < 0)
            return 0;
        if (megaRiskLookup.ContainsKey(keyTemplate))
            return megaRiskLookup[keyTemplate];
        var thisRisk = MegaRiskMap[i][j];

        if (i - 1 < 0)
            thisRisk += GetMinimumRiskForMegaUntil(i, j - 1);
        else if (j - 1 < 0)
            thisRisk += GetMinimumRiskForMegaUntil(i - 1, j);
        else
        {

            var opt1 = GetMinimumRiskForMegaUntil(i - 1, j);
            var opt2 = GetMinimumRiskForMegaUntil(i, j - 1);
            if (opt1 < opt2)
                thisRisk += GetMinimumRiskForMegaUntil(i - 1, j);
            else if (opt1 > opt2)
                thisRisk += GetMinimumRiskForMegaUntil(i, j - 1);
            else
                thisRisk += opt2;
        }

        if (!megaRiskLookup.ContainsKey(keyTemplate))
            megaRiskLookup.Add(keyTemplate, thisRisk);

        return thisRisk;
    }

    public int[][] GenerateFullMap()
    {
        MegaRiskMap = new int[5 * RiskMap.Length][];

        for (int i = 0; i < MegaRiskMap.Length; i++)
        {
            MegaRiskMap[i] = new int[RiskMap.Length * 5];
        }
        for (int i = 0; i < RiskMap.Length; i++)
        {
            MegaRiskMap[i] = new int[RiskMap.Length * 5];

            for (int j = 0; j < RiskMap[i].Length; j++)
            {
                var jLen = RiskMap[i].Length;
                for (int k = 0; k < 5; k++)
                {
                    var value = k > 0 ? MegaRiskMap[i][(j + jLen * (k - 1))]+1 : RiskMap[i][j] + k;
                    value = value > 9 ? 1 : value;
                    MegaRiskMap[i + jLen * k][j] = value;
                    MegaRiskMap[i][j + jLen * k] = value;
                }
            }
        }

        for (int x = RiskMap.Length; x < MegaRiskMap.Length; x++)
        {
            for (int y = RiskMap[0].Length; y < MegaRiskMap.Length; y++)
            {
                var ylen = RiskMap[0].Length;
                var value = MegaRiskMap[x - ylen][y] + 1;
                MegaRiskMap[x][y] = value > 9 ? 1 : value;
            }

        }
        //Print(MegaRiskMap); 
        return MegaRiskMap;
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

    int[][] MegaRiskMap;
    public override string Second()
    {
        var risk = Input.Select(x => x)
                   .Select(x => x.ToCharArray().Select(y => int.Parse(y.ToString())).ToArray())
                   .ToArray();
        var totalRisk = 0;

        RiskMap = risk;
        MegaRiskMap = GenerateFullMap();
        totalRisk = GetMinimumRiskForMegaUntil(MegaRiskMap.Length - 1, MegaRiskMap[0].Length - 1);
        result = totalRisk.ToString();
        return result;
    }

    public void Print(int[][] map)
    {
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                Trace.Write(map[i][j]);
            }
            Trace.WriteLine("");
        }

        Trace.WriteLine("");
    }

    public void PrintUntil(int[][] map, int x, int y)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Console.Write(map[i][j]);
            }
            Console.WriteLine("");
        }
        Console.WriteLine();
    }
}
