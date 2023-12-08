using ChristmasGifts;
using System.Text.RegularExpressions;


var d = new Day08();
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
public class Day08 : Christmas
{
    string result = "todo";
    Instructions instructions;
    Dictionary<string, Node> dictionary = new();

    public Day08() : base("8", "2023") { }
    public override string First()
    {
        ParseInput();

        int ctr = 0;
        for (var node = dictionary["AAA"]; !node.Value.Equals("ZZZ"); ctr++)
        {
            var next = instructions.Next();
            var dir = next == 0 ? "left" : "right";
            node = next == 0 ? dictionary[node.Left] : dictionary[node.Right];
        }

        return ctr.ToString();
    }

    public override string Second()
    {
        ParseInput(withFillings: true);
        var nodes = dictionary.Where(x => x.Value.Value.EndsWith("A")).Select(x=>x.Value).ToArray();
        List<ulong> lcm = new();
        for(int i=0; i< nodes.Length; i++)
        {
            int ctr = 0;
            instructions.index = 0;

            for (var node = nodes[i]; !node.Value.EndsWith("Z"); ctr++)
            {
                var next = instructions.Next();
                var dir = next == 0 ? "left" : "right";
                 node = next == 0 ? dictionary[node.Left] : dictionary[node.Right];
            }

            lcm.Add((ulong)ctr);
        }

        return ComputeLCM(lcm).ToString();
    }


    ulong ComputeLCM(List<ulong> lcmList)
    {
        ulong result = lcmList[0];
        for (int i = 1; i < lcmList.Count; i++)
        {
            result = ComputeLCM(result, lcmList[i]);
        }
        return result;
    }

    ulong ComputeLCM(ulong a, ulong b)
    {
        return (a * b) / ComputeGCD(a, b);
    }

    ulong ComputeGCD(ulong a, ulong b)
    {
        while (b != 0)
        {
            ulong temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    private void ParseInput(bool withFillings = false)
    {
        dictionary = new();
        var parts = RawInput.Split(Feature.NewLine + Feature.NewLine, StringSplitOptions.RemoveEmptyEntries);
        instructions = new Instructions(parts[0]);
        var nodes = parts[1].Split(Feature.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(x => Node.Create(x));

        foreach (var node in nodes) { dictionary.Add(node.Value, node); }

        if (withFillings)
        {
            foreach (var item in dictionary)
            {
                fillings.Add(item.Key + "0", item.Value.Left);
                fillings.Add(item.Key + "1", item.Value.Right);
            }
        }
    }
    Dictionary<string, string> fillings = new();


    public record Node(string Value, string Left, string Right)
    {
        public static Node Create(string input)
        {
            string pattern = @"\b[A-Z0-9]{3}\b";
            MatchCollection matches = Regex.Matches(input, pattern);
            return new Node(matches[0].Value, matches[1].Value, matches[2].Value);

        }
    }
    public record Instructions(string instructions)
    {
        public int index = 0;
        public int Next()
        {
            var instruction = instructions[index] switch
            {
                'L' => 0,
                'R' => 1,
                _ => throw new Exception("Invalid input")
            };
            index++;
            if (index == instructions.Length)
                index = 0;
            return instruction;
        }
    }



}
