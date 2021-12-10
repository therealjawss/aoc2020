using ChristmasGifts;

var d = new Day10();
//await d.GetInput(file: "test.txt", pattern:Environment.NewLine);
await d.GetInput(pattern: "\n");
//await d.PostFirstAnswer();
Console.WriteLine($"Part 1:{d.First()}");
Console.WriteLine($"Part 2:{d.Second()}");
//await Task.Delay(5000);
//await d.PostSecondAnswer();


public class Day10 : Christmas
{
    string result = "todo";

    public Day10() : base("10", "2021") { }
    public override string First()
    {
        var incorrect = new List<char>();
        foreach (var input in Input)
        {
            Stack<char> output = new Stack<char>();
            foreach (var c in input.ToCharArray())
            {
                if (output.Count>0 &&(IsClosing(c)))
                {
                    if (output.Peek() != c)
                    {
                        incorrect.Add(c);
                        break;
                    }
                    else
                        output.Pop();
                }
                else
                    output.Push(GetClosing(c));
            }
        }
        result = incorrect.Aggregate(0UL, (a, c) => a + GetPoints(c)).ToString();
        return result;
    }

    private static bool IsClosing(char c) => c switch
    {
        var x when
            x == '}' ||
            x == ']'||
            x == ')'||
            x == '>' => true,
        _ => false
    };

    private char GetClosing(char c) => c switch
    {
        '{' => '}' ,
        '[' => ']' ,
        '(' => ')' ,
        '<' => '>' ,
        _ => c
    };
    public ulong GetPoints(char c) => c switch
    {
        ')' => 3,
        ']' => 57,
        '}' => 1197,
        '>' => 25137,
        _ => 0

    };
    public ulong GetSecondPoints(char c) => c switch
    {
        ')' => 1,
        ']' => 2,
        '}' => 3,
        '>' => 4,
        _ => 0

    };


    public override string Second()
    {
        var score = new List<ulong>();
        var incomplete = new List<string>();
        foreach (var input in Input)
        {
            if(IsCorrect(input))
                incomplete.Add(input);

        }

        foreach(var i in incomplete)
        {
            Stack<char> output = new Stack<char>();
            foreach(var c in i.ToCharArray())
            {
                if (!IsClosing(c))
                    output.Push(c);
                else
                    output.Pop();
            }
            var lineScore = 0UL;
            while (output.Count > 0) { 
                 var expected = GetClosing(output.Pop());  
                lineScore = lineScore * 5 + GetSecondPoints(expected);
            }
            score.Add(lineScore);
        }
        
        result = score.OrderBy(x=>x).ToArray()[score.Count/2].ToString();
        return result;

    }

    private bool IsCorrect(string input)
    {

        Stack<char> output = new Stack<char>();

        foreach (var c in input.ToCharArray())
        {
            if (output.Count>0 &&(IsClosing(c)))
            {
                if (output.Peek() != c)
                {
                    return false;
                }
                else
                    output.Pop();
            }
            else
                output.Push(GetClosing(c));
        }

        return true;
    }

}
