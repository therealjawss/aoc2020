using ChristmasGifts;
var d = new Day05();
if (Feature.Local)
{
    await d.GetInput(file: "test.txt", pattern: Environment.NewLine);
}
else
{
    await d.GetInput();
}
Console.WriteLine($"Part 1:{d.RunFirst()}");
//await d.PostFirstAnswer();
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 11827296

public static class Feature
{
    public static bool Local = true;
}
public class Day05 : Christmas
{
    string lnewline = "\n";
    string rnewline = "\r\n";
    public string newline => Feature.Local ? rnewline : lnewline;
    private HashSet<Seed> seeds = new();
    private Seed[] OrderedSeeds;

    private Range[] seedrange;
    private static Dictionary<Category, Map> maps = new Dictionary<Category, Map>();

    public Day05() : base("5", "2023") { }
    public override string First()
    {
        createMap();
        traverseMap();
        var shortest = seeds.OrderBy(x => x.Location).First();
        return shortest.Location.ToString();
    }

    public override string Second()
    {
        createMap(pairThatShit: true);
        traverseMap();

        var shortest = seeds.OrderBy(x => x.Location).First();
        return shortest.Location.ToString();

    }

    private void traverseMap()
    {
        foreach (var seed in seeds)
        {
            var point = long.MaxValue;
            seed.Location = long.MaxValue;
            for (long i = seed.Min; i <= seed.Max; i++)
            {
                point = i;
                foreach (var map in maps)
                {
                    point = map.Value.GetDestination(point);
                }
                if (point < seed.Location)
                {
                    seed.Location = point;
                }
            }

        }
        
    }


    private void createMap(bool pairThatShit = false)
    {
        seeds = new();
        maps = new();
        var sections = RawInput.Split($"{newline}{newline}", StringSplitOptions.RemoveEmptyEntries);

        // read maps
        for (int i = 0; i < (int)(Category.location); i++)
        {
            var source = (Category)i;
            var destination = (Category)i + 1;
            var test = sections[i + 1].Split(newline, StringSplitOptions.RemoveEmptyEntries)[1..]
                .Select(x =>
                {
                    var info = x.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => long.Parse(x)).ToArray();
                    return new Info(info[0], info[1], info[2]);
                });

            maps.Add(source, new Map(source, destination, test.ToArray()));
        }

        if (pairThatShit)
            GetSeeds(sections[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1..], maps[Category.seed]);
        else
            seeds = sections[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1..].OrderBy(x => x.Length).Select(x => new Seed(long.Parse(x))).ToHashSet();

    }
    public record Range(long min, long max)
    {
        public static Range Create(long min, long range) => new Range(min, min + range - 1);
        public Range Intersection(Range range)
        {
            if (this.min < range.min && this.max < range.min ||
                 this.min > range.max && this.max > range.min)
                return new Range(-1, -1);
            var min = this.min < range.min ? range.min : this.min;
            var max = this.max > range.max ? range.max : this.max;

            return new Range(min, max);
        }

        internal Range[] RangeUnion(Range valueRange)
        {
            var r = new long[] { this.min, this.max, valueRange.min, valueRange.max }.OrderBy(x => x).ToArray();    
            var result = new List<Range>();
            for(int i=1; i<r.Length; i++)
            {
                result.Add(new Range(min: r[i - 1], max: r[i]));
            }

            return result.ToArray();
        }
    }

    private void GetSeeds(string[] strings, Map map)
    {
        var list = new List<Range>();
        for (int i = 0; i < strings.Length; i += 2)
        {
            var minVal = long.Parse(strings[i]);
            var range = long.Parse(strings[i + 1]);
            seeds.Add(new Seed(minVal, minVal + range - 1));
            list.Add(new Range(minVal, minVal + range - 1));

        }
        seedrange = list.ToArray();
        OrderedSeeds = seeds.OrderBy(x => x.Min).ToArray();

    }
    public enum Category { seed, soil, fertilizer, water, light, temperature, humidity, location };

    [System.Diagnostics.DebuggerDisplay("{Destination} {Min} {Max}")]
    public record Info(long Destination, long SourceMin, long InfoRange)
    {
        private long _max = SourceMin + InfoRange - 1;
        public long SourceMax => _max;
        public long DestinationMax => Destination + InfoRange - 1;
        public long GetDestination(long source)
        {
            var diff = source - SourceMin;
            if (diff < 0 || (InfoRange - diff) < 0)
                return -1;
            return Destination + diff;
        }

        public long GetSource(long destination)
        {
            var diff = destination - Destination;
            if (diff < 0 || (InfoRange - diff) < 0)
                return -1;
            return SourceMin + diff;
        }
        public Range ValueRange = Range.Create(SourceMin, InfoRange);
    }

    [System.Diagnostics.DebuggerDisplay("{Source} {Destination} {Infos.Length}")]
    public record Map(Category source, Category? destination, Info[] infos)
    {
        Info[] Sorted = infos.OrderBy(x => x.Destination).ToArray();
        public long GetDestination(long source)
        {
            foreach (var info in infos)
            {
                var destination = info.GetDestination(source);
                if (destination > 0)
                    return destination;
            }
            return source;
        }

        public long GetSource(long destination)
        {
            foreach (var info in infos)
            {
                var source = info.GetSource(destination);
                if (source > 0)
                    return source;
            }
            return destination;
        }

        public IEnumerable<long> GetDestination(long min, long max)
        {
            var result = new List<long>();
            var query = infos.Where(i => min - i.SourceMax >= 0 || max - i.SourceMin >= 0);
            var points = query.Select(i => i.SourceMin).Union(query.Select(i => i.SourceMax)).Union([min, max]).Distinct().ToList();
            var tmp = points
                .Where(p => p - min >= 0 && max - p >= 0).ToList();

            foreach (var info in query)
            {
                foreach (var point in tmp)
                {
                    var destination = info.GetDestination(point);
                    if (destination >= 0)
                        result.Add(destination);
                    else
                        result.Add(min);
                }
            }
            return result.OrderBy(x => x);
        }

        public IEnumerable<long> Traverse(long min, long max, Dictionary<Category, Map> maps)
        {
            var list = new HashSet<long>();
            if ((Category)(source + 1) == Category.location)
            {
                list.Add(min);
            }
            else if (infos.All(i => i.SourceMax - min >= 0) && infos.All(i => max - i.SourceMin >= 0))
            {

                var result = maps[(Category)source + 1].Traverse(min, max, maps);
                foreach (var item in result)
                {
                    list.Add(item);
                }
            }
            else
            {
                var destinations = GetDestination(min, max).Distinct().ToArray();
                for (int i = 0; i < destinations.Count() - 1; i++)
                {
                    if (maps.ContainsKey((Category)source + 1))
                    {
                        var result = maps[(Category)source + 1].Traverse(destinations[i], destinations[i + 1], maps);
                        foreach (var item in result)
                        {
                            list.Add(item);
                        }
                    }

                }

            }
            return list;
        }

        internal IEnumerable<string> Traverse(long min, Dictionary<Category, Map> maps)
        {
            foreach (var info in Sorted)
            {
                var destination = info.GetDestination(min);
                if (destination > 0)
                {
                    var result = maps[(Category)source + 1].Traverse(destination, maps);
                    foreach (var item in result)
                    {
                        yield return item;
                    }
                }
            }
        }
    }
    public record Seed(long Min, long Max)
    {
        public Seed(long min) : this(min, min) { }
        public long Location { get; set; }
        public IEnumerable<string> Next { get; set; }
        public IEnumerable<long> Locations { get; internal set; }

        public void Travel(Dictionary<Category, Map> map)
        {
            Locations = map[0].Traverse(Min, Max, map);
        }

        internal void VisitLowest(Map[] maps)
        {
            throw new NotImplementedException();
        }
    }
}
