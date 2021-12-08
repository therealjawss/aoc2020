using ChristmasGifts;

var d = new Day8();
await d.GetInput(file: "test.txt", pattern: "\r\n");
//await d.GetInput(pattern: "\n");
//await d.PostFirstAnswer();
Console.WriteLine($"Part 1:{d.First()}");
Console.WriteLine($"Part 2:{d.Second()}");
//await Task.Delay(5000);
//await d.PostSecondAnswer();

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
      
        }
     
        return result;
    }
}
