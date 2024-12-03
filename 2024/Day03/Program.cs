using ChristmasGifts;
using System.Text.RegularExpressions;
var d = new Day03(); 
if (Feature.Local) 
    await d.GetInput(file: "test.txt", pattern: Environment.NewLine); 
else 
    await d.GetInput(); 
Console.WriteLine($"Part 1:{d.RunFirst()}"); 
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 2:{d.RunSecond()}"); 
await Task.Delay(5000); 
await d.PostSecondAnswer(); 
public class Day03 : Christmas 
{ 
    string result = "todo"; 
    public Day03() : base("3", "2024") { } 
    public override string First() 
    { 
        var pattern = @"mul\((\d+),(\d+)\)";
        var matches = Regex.Matches(RawInput, pattern);

        var result = matches.Where(x=>x.Success).Select(x=> int.Parse(x.Groups[1].Value) * int.Parse(x.Groups[2].Value)).Sum().ToString();
        return result; 
    } 
    public override string Second() 
    {
        var pattern = @"(mul)\((\d+),(\d+)\)|(do|don't)\(\)";

        var include = true;
        var matches = Regex.Matches(RawInput, pattern);
        int res = 0;
        foreach(Match match in matches)
        {
            var op = match.Value;
            if (op == "do()")
            {
                include = true;
            }
            else if (op == "don't()")
            {
                include = false;
            }
            else if (include)
            {
                res += int.Parse(match.Groups[2].Value) * int.Parse(match.Groups[3].Value);
            }
        }
        return res.ToString();
    }  
} 
