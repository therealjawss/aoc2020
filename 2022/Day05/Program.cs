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

    public override void Setup()
    {
        var index = Input.TakeWhile(x => !x.Contains("1")).Count();
        var numberOfStacks = Convert.ToInt32(Input[index].Split(" ", StringSplitOptions.RemoveEmptyEntries).Last());
        var list = new List<char>[numberOfStacks];
        stacks = new Stack<char>[numberOfStacks];
        for (int i = 0; i < numberOfStacks; i++)
        {
            list[i] = new List<char>();
        }
        for (int i = 0; i < index; i++)
            for (int j = 1; j < Input[i].Length; j += 4)
            {
                var character = Input[i][j];
                if (character >= 'A' && character <= 'Z')
                    list[j / 4].Add(Input[i][j]);
            }
        for (int i = 0; i < numberOfStacks; i++)
        {
            list[i].Reverse();
            stacks[i] = new Stack<char>(list[i]);
        }
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
