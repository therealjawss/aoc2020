using ChristmasGifts;

var d = new Day9();
//await d.GetInput(file: "test.txt", pattern: "\n");
await d.GetInput(pattern: "\n");
//await d.PostFirstAnswer();
Console.WriteLine($"Part 1:{d.First()}");
Console.WriteLine($"Part 2:{d.Second()}");
//await Task.Delay(5000);
await d.PostSecondAnswer();

/*
0 - 6 
1 - 1
2 - 5
3 - 5
4 - 4
5 - 5
6 - 
*/
public class Day9 : Christmas
{
    string result = "todo";

    public Day9() : base("9", "2021")
    {


    }
    public override string First()
    {
        var map = Input.Select(x => x.Trim().ToCharArray().Select(x => int.Parse(x + "")).ToArray()).ToArray();

        var risk = 0;
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Count(); j++)
            {
                var point = map[i][j];
                var up = j > 0 ? map[i][j - 1] : int.MaxValue;
                var down = j < map[i].Count() - 1 ? map[i][j + 1] : int.MaxValue;
                var left = i > 0 ? map[i - 1][j] : int.MaxValue;
                var right = i < map.Count() - 1 ? map[i + 1][j] : int.MaxValue;
                var isLower = point < up && point < down && point < left && point < right;
                if (isLower)
                    risk += point < up && point < down && point < left && point < right ? point + 1 : 0;
            }
        }
        result = risk.ToString();
        return result;
    }

    private static bool isLowpoint(int[][] map, int i, int j)
    {
        var point = map[i][j];
        var up = j > 0 ? map[i][j - 1] : int.MaxValue;
        var down = j < map[i].Count() - 1 ? map[i][j + 1] : int.MaxValue;
        var left = i > 0 ? map[i - 1][j] : int.MaxValue;
        var right = i < map.Count() - 1 ? map[i + 1][j] : int.MaxValue;
        return point < up && point < down && point < left && point < right;
    }

    public override string Second()
    {
        var map = Input.Select(x => x.Trim().ToCharArray().Select(x => int.Parse(x + "")).ToArray()).ToArray();

        result = GetAllBasins(map).ToString();

        return result;
    }
    static List<HashSet<(int, int)>> Basins = new List<HashSet<(int, int)>>();
    public static int GetAllBasins(int[][] map)
    {
        for (int i = 0; i < map.Length; i++)
            for (int j = 0; j < map[i].Count(); j++)
            {
                if (Basins.Any(x => x.Contains((i, j))))
                    continue;
                else
                {
                    var basin = new HashSet<(int, int)>();
                    GetBasin(map, i, j, basin);
                    if (basin.Count > 0)
                        Basins.Add(basin);
                }
            }

        return Basins.OrderByDescending(x => x.Count).Take(3).Aggregate(1, (c, basin) => c * basin.Count);
    }

    private static void GetBasin(int[][] map, int i, int j, HashSet<(int, int)> basin)
    {
        if (i < 0 || i > map.Length - 1 || j < 0 || j > map[i].Length - 1)
            return;
        else if (map[i][j] == 9)
            return;
        else
        {
            basin.Add((i, j));
            if (!basin.Contains((i + 1, j)))
                GetBasin(map, i + 1, j, basin);
            if (!basin.Contains((i, j + 1)))
                GetBasin(map, i, j + 1, basin);
            if (!basin.Contains((i - 1, j)))
                GetBasin(map, i - 1, j, basin);
            if (!basin.Contains((i, j - 1)))
                GetBasin(map, i, j - 1, basin);

        }
    }

    public static int GetSize(int[][] map, int i, int j)
    {
        if (i < 0 || i > map.Length - 1 || j < 0 || j > map.Length - 1)
            return 0;
        else if (map[i][j] == 9)
            return 0;
        else
            return 1 + GetSize(map, i + 1, j) + GetSize(map, i, j + 1);
    }
}
