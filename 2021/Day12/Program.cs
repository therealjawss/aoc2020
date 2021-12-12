using ChristmasGifts;

var d = new Day12();
//await d.GetInput(file: "test.txt", pattern:Environment.NewLine); 
await d.GetInput(pattern: "\n");
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 1:{d.RunFirst()}");
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 
public class Day12 : Christmas
{
    string result = "todo";
    public Day12() : base("12", "2021") { }
    public override string First()
    {
        var map = new Dictionary<string, List<string>>();
        var paths = Input.Select(x => x.Split("-")).Select(y => (y[0], y[1]));
        paths.ToList().ForEach(path =>
        {
            if (map.ContainsKey(path.Item1)) map[path.Item1].Add(path.Item2);
            else map[path.Item1] = new List<string> { path.Item2 };

            if (map.ContainsKey(path.Item2)) map[path.Item2].Add(path.Item1);
            else map[path.Item2] = new List<string> { path.Item1 };

        });
        var uniquePaths = new HashSet<string>();

        var next = map["start"];
        foreach (var cave in next)
        {
            var passedSmallCaves = new HashSet<string>();
            var possibilities = Visit(cave, "start", map, passedSmallCaves, "start");
            foreach (var possib in possibilities)
            {
                uniquePaths.Add(possib);
            }
        }
        result = uniquePaths.Count.ToString();
        return result;
    }


    private List<string> Visit(string cave, string from, Dictionary<string, List<string>> map, HashSet<string> passedSmallCaves, string soFar)
    {
        var possibilities = new List<string>();
        if (cave == "end")
        {
            possibilities.Add($"{soFar},end");
            return possibilities;
        }

        var big = cave.ToUpper() == cave;
        if (!big)
        {
            if (!passedSmallCaves.Contains(cave))
                passedSmallCaves.Add(cave);
            else return possibilities;
        }
        var next = map[cave].Where(x => x!="start").Select(x => x);
        foreach (var n in next)
        {
            var passed = passedSmallCaves.ToHashSet();
            var paths = Visit(n, from, map, passed, $"{soFar},{cave}");
            possibilities.AddRange(paths);
        }
        return possibilities;
    }


    public override string Second()
    {
        var map = new Dictionary<string, List<string>>();
        var paths = Input.Select(x => x.Split("-")).Select(y => (y[0], y[1]));
        paths.ToList().ForEach(path =>
        {
            if (map.ContainsKey(path.Item1)) map[path.Item1].Add(path.Item2);
            else map[path.Item1] = new List<string> { path.Item2 };

            if (map.ContainsKey(path.Item2)) map[path.Item2].Add(path.Item1);
            else map[path.Item2] = new List<string> { path.Item1 };

        });
        var uniquePaths = new HashSet<string>();

        var next = map["start"];
        foreach (var cave in next)
        {
            var passedSmallCaves = new Dictionary<string,int>();
            var possibilities = Visit2(cave, "start", map, passedSmallCaves, "start");
            foreach (var possib in possibilities)
            {
                uniquePaths.Add(possib);
            }
        }
        result = uniquePaths.Count.ToString();
        return result;
    }

    private List<string> Visit2(string cave, string from, Dictionary<string, List<string>> map, Dictionary<string,int> passedSmallCaves, string soFar)
    {
        var possibilities = new List<string>();
        if (cave == "end")
        {
            possibilities.Add($"{soFar},end");
            return possibilities;
        }

        var big = cave.ToUpper() == cave;
        if (!big)
        {
            if (!passedSmallCaves.ContainsKey(cave))
                passedSmallCaves.Add(cave, 1);
            else if(passedSmallCaves[cave] < 2 && !passedSmallCaves.Any(x=>x.Value>1)) 
                passedSmallCaves[cave]++;
            else return possibilities;
        }
        var next = map[cave].Where(x => x!="start").Select(x => x);
        foreach (var n in next)
        {
            var passed = passedSmallCaves.ToDictionary(x=>x.Key, y=>y.Value);
            var paths = Visit2(n, from, map, passed, $"{soFar},{cave}");
            possibilities.AddRange(paths);
        }
        return possibilities;
    }
}
