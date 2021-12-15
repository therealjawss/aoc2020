using ChristmasGifts;
var d = new Day14();
var newLine = "\r\n\r\n";
await d.GetInput(file: "test.txt", pattern: newLine);
//await d.GetInput(pattern: newLine); 
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 1:{d.RunFirst()}");
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 
public class Day14 : Christmas
{
    string result = "todo";
    public Day14() : base("14", "2021") { }
    public record Rule(string match, string toInsert);
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
    public string Process(string p, Dictionary<string, string> rules)
    {
        if (p.Length == 2)
            return p[0] + rules[p];
        var mid = p.Length / 2;
        var next = p[0..(mid + 1)];
        var other = p[(mid)..];
        return Process(next, rules) + Process(other, rules);
    }
    public override string Second()
    {
        var template = Input[0];
        var irules = Input[1].Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var rules = irules
            .Select(x => x.Split("->", StringSplitOptions.TrimEntries))
            .ToDictionary(x => x[0], y => y[1]);
        var polymer = template;
        for (int k = 0; k< polymer.Length; k++)
        {
            var subpolymer = polymer.Substring(k, 2);
            for (int i = 0; i < 40; i++)
            {
                var newString = "" + subpolymer[0];
                for (int j = 0; j < subpolymer.Length - 1; j++)
                {
                    var toAdd = rules[subpolymer.Substring(j, 2)];
                    newString += toAdd + subpolymer[j + 1];

                }
                subpolymer = newString;
            }
        }
        result = (polymer.ToCharArray().GroupBy(x => x).Max(x => x.Count()) -
            polymer.ToCharArray().GroupBy(x => x).Min(x => x.Count())).ToString();

        return result;

    }
    public string TempSecond()
    {
        var template = Input[0];
        var rulesInput = Input[1].Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Split("->", StringSplitOptions.TrimEntries));
        var rules = rulesInput
            .ToDictionary(x => x[0], y => y[1][0]);

        var count = template.GroupBy(x => x).ToDictionary(x => x, y => y.Count());

        var polymer = new NodeList(template, rules);
        for (int i = 0; i < 40; i++)
        {
            Process2(polymer);
        }
        result = (polymer.Counter.Max(x => x.Value) - polymer.Counter.Min(x => x.Value)).ToString();

        return result;
    }

    private static Dictionary<char, ulong> Process2(NodeList polymer)
    {
        if (polymer.Length > 15UL)
        {
            var ps = polymer.Split(8);
        }
        return GetCountDictionary(polymer);
    }

    private static Dictionary<char, ulong> GetCountDictionary(NodeList polymer)
    {
        polymer.TriggerInsertion();
        return polymer.Counter;
    }
}
public class NodeList
{
    public event Notify TriggerPairInsert;
    public Node First { get; private set; }
    public Node Last { get; private set; }
    public Dictionary<string, char> Rules { get; private set; }
    public ulong Length { get; internal set; }
    public Dictionary<char, ulong> Counter { get; private set; } = new Dictionary<char, ulong>();
    public NodeList(string elements, Dictionary<string, char> rules)
    {
        Rules = rules;
        Length = (ulong)elements.Length;
        HashSet<char> values = new HashSet<char>(elements.ToCharArray());
        var keys = rules.SelectMany(x => x.Key.ToCharArray()).ToHashSet();
        values = values.Union(keys).ToHashSet();
        values = values.Union(rules.Select(x => x.Value).ToHashSet()).ToHashSet();
        Counter = values.ToDictionary(x => x, y => 0UL);
        var curr = First = new Node(elements[0], this);
        Counter[elements[0]]++;
        for (int i = 1; i < elements.Length; i++)
        {
            Counter[elements[i]]++;
            var n = new Node(elements[i], this);
            curr.Next = n;
            curr = n;
        }
        Last = curr;
    }
    public NodeList(NodeList nodes, Dictionary<string, char> rules) : this(Parse(nodes), rules) { }

    public static string Parse(NodeList nodes)
    {
        string elements = "";
        var curr = nodes.First;
        do
        {
            elements += curr.Value;
            curr = curr.Next;
        } while (curr != null);

        return elements;
    }
    internal void TriggerInsertion()
    {
        TriggerPairInsert?.Invoke();
    }

    internal NodeList[] Split(ulong index)
    {
        var elements1 = "";
        var elements2 = "";
        var curr = this.First;
        for (ulong i = 0; i < this.Length && curr != null; i++)
        {
            if (i < index)
            {
                elements1 += curr.Value;
            }
            else
            {
                elements2 += curr.Value;
            }
            curr = curr.Next;
        }

        return new NodeList[] { new NodeList(elements1, Rules), new NodeList(elements2, Rules) };
    }
}
public delegate void Notify();

public class Node
{
    private NodeList nodeList;

    public Node(char v, NodeList nodeList)
    {
        this.Value = v;
        this.nodeList = nodeList;
        nodeList.TriggerPairInsert += NodeList_TriggerPairInsert;
    }

    private void NodeList_TriggerPairInsert()
    {
        if (Next != null)
        {
            var newValue = nodeList.Rules[$"{Value}{Next.Value}"];
            var newNode = new Node(newValue, nodeList);
            nodeList.Counter[newValue]++;
            newNode.Next = Next;
            Next = newNode;
            nodeList.Length++;
        }
    }

    public char Value { get; set; }
    public Node Next { get; set; }
}
