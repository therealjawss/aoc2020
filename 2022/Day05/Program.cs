using ChristmasGifts;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
var testMode = false;
var d = new Day05(testMode);
if (testMode)
    await d.GetInput("test.txt");
else
    await d.GetInput();

//await d.GetInput(pattern: Environment.NewLine); 
//if (!testMode) await d.PostFirstAnswer();
Console.WriteLine($"Part 1:{d.RunFirst()}");
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//if (!testMode) await d.PostSecondAnswer(); 
public class Day05 : Christmas
{
    bool TESTMODE;
    string result = "todo";
    const int STACKS = 9;
    const int TestSTACKS = 4;

    Stack<char>[] stacks;
    Stack<char>[] testStacks;

    public Day05(bool testMode) : this()
    {
        TESTMODE = testMode;
    }
    public Day05() : base("5", "2022")
    {
    }

    private void Setup()
    {
        stacks = TESTMODE ? SetupTestCrates() : SetupCrates();
    }

    private Stack<char>[] SetupTestCrates()
    {
        return new Stack<char>[]
        {
            new Stack<char>("ZN".ToArray().AsEnumerable<char>()),
            new Stack<char>("MCD".ToArray().AsEnumerable<char>()),
            new Stack<char>("P".ToArray().AsEnumerable<char>()),
        };
    }

    private new Stack<char>[] SetupCrates()
    {
        return new Stack<char>[]
        {
            new Stack<char>("BGSC".ToArray().AsEnumerable<char>()),
            new Stack<char>("TMWHJNVG".ToArray().AsEnumerable<char>()),
            new Stack<char>("MQS".ToArray().AsEnumerable<char>()),
            new Stack<char>("BSLTWNM".ToArray().AsEnumerable<char>()),
            new Stack<char>("JZFTVGWP".ToArray().AsEnumerable<char>()),
            new Stack<char>("CTBGQHS".ToArray().AsEnumerable<char>()),
            new Stack<char>("TJPBW".ToArray().AsEnumerable<char>()),
            new Stack<char>("GDCZFTQM".ToArray().AsEnumerable<char>()),
            new Stack<char>("NSHBPF".ToArray().AsEnumerable<char>()),
        };
    }

    public override string First()
    {
        Setup();
        foreach (var x in Input.Skip(TESTMODE ? TestSTACKS : STACKS))
            Execute(x);
        result = "";
        foreach (var s in stacks)
            result += s.Peek();

        return result;
    }

    private void Execute(string x)
    {
        var pattern = @"move (\d+) from (\d+) to (\d+)";
        var parsed = new Regex(pattern).Match(x);
        if (parsed.Success)
            for (int i = 0; i < Convert.ToInt32(parsed.Groups[1].Value); i++)
            {
                stacks[Convert.ToInt32(parsed.Groups[3].Value) - 1].Push(stacks[Convert.ToInt32(parsed.Groups[2].Value) - 1].Pop());
            }
    }
    private void Execute2(string x)
    {
        var pattern = @"move (\d+) from (\d+) to (\d+)";
        var parsed = new Regex(pattern).Match(x);
        var tempStack = new Stack<char>();
        if (parsed.Success)
        {

            for (int i = 0; i < Convert.ToInt32(parsed.Groups[1].Value); i++)
            {
                tempStack.Push(stacks[Convert.ToInt32(parsed.Groups[2].Value) - 1].Pop());

            }

            do
            {
                stacks[Convert.ToInt32(parsed.Groups[3].Value) - 1].Push(tempStack.Pop());
            } while (tempStack.Count > 0);
        }
    }

    public override string Second()
    {
        Setup();
        foreach (var x in Input.Skip(TESTMODE ? TestSTACKS : STACKS))
            Execute2(x);
        result = "";
        foreach (var s in stacks)
            result += s.Peek();

        return result;
    }
}
