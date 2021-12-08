using ChristmasGifts;

var d = new Day8();
//await d.GetInput(file: "test.txt", pattern: "\r\n");
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
public class Day8 : Christmas
{
    string result = "todo";
    Dictionary<string, int> decoded = new Dictionary<string, int>();
    public Day8() : base("8", "2021")
    {
        decoded.Add("cf", 1);
        decoded.Add("bcdf", 4);
        decoded.Add("acf", 7);
        decoded.Add("abcdefg", 8);
        decoded.Add("acdeg", 2);
        decoded.Add("acdfg", 3);
        decoded.Add("abdfg", 5);
        decoded.Add("abcefg", 0);
        decoded.Add("abdefg", 6);
        decoded.Add("abcdfg", 9);

    }
    public override string First()
    {
        var sum = 0;
        for (int i = 0; i < Input.Length; i++)
        {
            sum += Input[i].Split("|")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Count(x => x.Length == 2 || x.Length == 4 || x.Length == 3 || x.Length == 7);
        }
        result = sum.ToString();
        return result;
    }
    public override string Second()
    {
        int output = 0;
        for (int i = 0; i < Input.Length; i++)
        {
            var d = new Decoder(Input[i]);

            var o =d.GetOutput();
            output+=o;
        }
        result=output.ToString();
        return result;
    }
}

public class Decoder
{
    private string v;
    public List<string> OutputValues { get; set; }
    public List<string> SignalPatterns { get; set; }
    public List<(string key,int value)> Original = new List<(string, int)> {  ("cf",1),
        ("bcdf",4),
        ("acf",7),
        ("abcdefg",8),
        ("acdeg",2),
        ("acdfg",3),
        ("abdfg",5),
        ("abcefg",0),
        ("abdefg",6),
        ("abcdfg",9)
         };

    public Dictionary<string, List<int>> PossibleValues = new Dictionary<string, List<int>>();
    public Dictionary<string, int> SureValue => PossibleValues.Where(x => x.Value.Count == 1).Select(x => x).ToDictionary(x => x.Key, y => y.Value.First());
    public Decoder(string v)
    {
        OutputValues= v.Split("|", StringSplitOptions.TrimEntries)[1].Split(" ")
            .Select(x => new string(x.ToCharArray()
                .OrderBy(x => x).ToArray())).ToList();
        SignalPatterns= v.Split("|", StringSplitOptions.TrimEntries)[0].Split(" ")
            .Select(x => new string(x.ToCharArray()
                .OrderBy(x => x).ToArray())).ToList();
        var single = Original.GroupBy(x => x.key.Length)
            .Where(x => x.Count() == 1).Select(x => x.First());
    
        var g = Original.GroupBy(x => x.key.Length);
        var originalKeys = Original.Select(x=>x.key).ToList();
        foreach(var s in SignalPatterns)
        {
            var p = Original.Where(x => x.key.Length == s.Length).Select(x=>x.value).ToList();
            PossibleValues[s] = p;
        }
        var one = SureValue.First(x => x.Value==1);
        var four = SureValue.First(x => x.Value==4);
        var seven = SureValue.First(x => x.Value==7);
        var eight = SureValue.First(x => x.Value==8);

        var fives = SignalPatterns.Where(x => x.Length == 5).ToList();
        var sixes = SignalPatterns.Where(x => x.Length == 6).ToList();

        var threeKey = fives.First(x => one.Key.ToHashSet().IsProperSubsetOf(x.ToHashSet<char>()));
        Confirm(threeKey, 3);
   
        var nineKey = sixes.First(x => four.Key.ToHashSet().IsProperSubsetOf(x.ToHashSet<char>()));
        Confirm(nineKey, 9);

        var fiveKey = PossibleValues.Where(x => x.Value.Count>1).Select(x => x.Key)
            .First(x => x.ToHashSet().IsProperSubsetOf(nineKey.ToHashSet<char>()));
        Confirm(fiveKey, 5);
 
        var zeroKey = PossibleValues.Where(x=>x.Value.Count>1).Select(x=>x.Key)
            .First(x => seven.Key.ToHashSet().IsProperSubsetOf(x.ToHashSet<char>()));
        Confirm(zeroKey, 0);

        /*
        decoded.Add("cf", 1);
        decoded.Add("bcdf", 4);
        decoded.Add("acf", 7);
        decoded.Add("abcdefg", 8);
        decoded.Add("acdeg", 2);
        decoded.Add("acdfg", 3);
        decoded.Add("abdfg", 5);
        decoded.Add("abcefg", 0);
        decoded.Add("abdefg", 6);
        decoded.Add("abcdfg", 9);*/
    }

    private void Confirm(string key, int v)
    {
        foreach(var p in PossibleValues)
        {
            if (p.Key == key)
                PossibleValues[key] = new List<int>() { v };
            else 
                p.Value.Remove(v);
        }
    }

    internal int GetOutput()
    {
        string output = "";
        foreach(var o in OutputValues)
        {
            output+=SureValue[o];
        }

        return int.Parse(output);
    }
}