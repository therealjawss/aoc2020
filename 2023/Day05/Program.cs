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
//Console.WriteLine($"Part 1:{d.RunFirst()}");
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
        var result = findLowestLocation().ToString();
        //brute force
        //traverseMap();
        //var minLength = seeds.Select(s => s.Location).Min(x => x.Length);
        //var shortest = seeds.Where(s => s.Location.Length == minLength).OrderBy(x => x.Location).First();

        ////attempt
        //traverseRange();
        //var cand = seeds.SelectMany(s => s.Locations);
        //var minLength = cand.Min(x => x.Length);
        //var shortest = cand.Where(s => s.Length == minLength).OrderBy(x => x).First();
        return result;

    }

    private long findLowestLocation()
    {
        var cat = Category.humidity;
        var map = maps[Category.humidity];

        var infos = map.infos.OrderBy(x => x.Destination);
        var minLocation = maps.Max(x => x.Value.infos.Max(x => x.DestinationMax));
        foreach (var item in infos)
        {

            for (long i = item.Destination; i <= item.DestinationMax; i++)
            {
                cat = Category.temperature;
                var source = item.GetSource(i);

                do
                {
                    source = maps[cat--].GetSource(source);

                } while (cat != Category.soil);

                if (seeds.Any(s => s.Min <= source && s.Max >= source))
                { 
                    if (i < minLocation)
                        minLocation = i;
                }   
                
            }
        }
        return -1;
    }

    private void traverseMap()
    {
        foreach (var seed in seeds)
        {
            var point = seed.Min;
            Console.WriteLine($"Seed:{seed.Min}");
            foreach (var map in maps)
            {
                point = map.Value.GetDestination(point);
                Console.WriteLine($"{map.Value.destination}:{point}");
            }
            seed.Location = point;
        }
    }
    //private void traverseRange()
    //{
    //    foreach (var seed in seeds)
    //    {
    //        seed.Locations = Traverse(maps, Category.seed, seed).ToList();
    //    }
    //}
    //private IEnumerable<string> Traverse(Dictionary<Category, Map> maps, Category category, Seed seed)
    //{
    //    return maps[category].Traverse(seed.Min, seed.Max, maps);
    //}

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
            //brute
            GetSeeds(sections[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1..], maps[Category.seed]);
        ////attempt
        //GetSeeds(sections[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1..]);
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
    }

    private void GetSeeds(string[] strings, Map map)
    {
        for (int i = 0; i < strings.Length; i += 2)
        {
            var minVal = long.Parse(strings[i]);
            var range = long.Parse(strings[i + 1]);
            seeds.Add(new Seed(minVal, minVal + range - 1));

        }
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
    public record Map(Category source, Category destination, Info[] infos)
    {
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
                list.Add(max);
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
                var destinations = GetDestination(min, max).ToArray();
                for (int i = 0; i < destinations.Count() - 1; i++)
                {
                    var result = maps[(Category)source + 1].Traverse(destinations[i], destinations[i + 1], maps);
                    foreach (var item in result)
                    {
                        list.Add(item);
                    }
                }

            }
            return list;
        }
    }
    public record Seed(long Min, long Max)
    {
        public Seed(long min) : this(min, min) { }
        public long Location { get; set; }
        public IEnumerable<string> Next { get; set; }
        public IEnumerable<string> Locations { get; internal set; }
    }
    public class StringOperations
    {
        public static string Normalize(string input)
        {
            if (input.All(x => x == '0'))
                return "0";
            return input.TrimStart('0');
        }
        public static string Add(string addend, string augend)
        {
            var result = "";
            addend = addend.Reverse().Aggregate("", (acc, x) => acc + x);
            augend = augend.Reverse().Aggregate("", (acc, x) => acc + x);
            var index = 0;
            var carry = 0;
            var longer = addend.Length > augend.Length ? addend : augend;
            for (; index < addend.Length && index < augend.Length; index++)
            {
                var a = addend[index];
                var b = augend[index];
                var c = (a + b - '0') + carry;
                carry = 0;
                if (c - '0' > 9)
                {
                    carry = 1;
                    c -= 10;
                }
                result += (char)c;
            }
            if (index < longer.Length)
            {
                for (; index < longer.Length; index++)
                {
                    int sum;
                    if (carry > 0)
                    {
                        sum = longer[index] + char.Parse("" + carry) - '0';
                        carry = 0;

                        if (!char.IsDigit((char)sum))
                        {
                            carry = 1;
                            sum -= 10;
                        }
                        result += (char)sum;
                    }
                    else
                    {
                        result += longer[index];
                    }
                }
            }
            result = result.Reverse().Aggregate("", (acc, x) => acc + x);
            return carry > 0 ? carry + result : result;
        }

        public static string Subtract(string minuend, string subtrahend)
        {
            if (subtrahend.Length > minuend.Length)
                return "-";
            var result = "";
            minuend = minuend.Reverse().Aggregate("", (acc, x) => acc + x);
            subtrahend = subtrahend.Reverse().Aggregate("", (acc, x) => acc + x);
            var index = 0;
            var borrow = 0;
            var longer = minuend.Length > subtrahend.Length ? minuend : subtrahend;
            for (; index < minuend.Length && index < subtrahend.Length; index++)
            {
                var a = minuend[index];
                var b = subtrahend[index];
                if (borrow > 0)
                {
                    a = (char)(a - 1);
                    borrow--;
                }
                var c = (a - b + '0');
                if (!char.IsDigit((char)c))
                {
                    borrow++;
                    c += 10;
                }
                result += (char)c;
            }

            for (; index < minuend.Length; index++)
            {
                int sum;
                if (borrow > 0)
                {
                    var difference = longer[index] - char.Parse("" + borrow) + '0';
                    borrow--;

                    if (!char.IsDigit((char)difference))
                    {
                        difference += 10;
                        borrow++;
                    }
                    result += (char)difference;
                }
                else
                {
                    result += longer[index];
                }
            }
            if (borrow > 0)
                return "-";
            result = result.Reverse().Aggregate("", (acc, x) => acc + x);
            return Normalize(result);

        }
    }
}
