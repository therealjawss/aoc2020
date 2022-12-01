using ChristmasGifts; 
var d = new Day01(); 
await d.GetInput(); 
//await d.GetInput(pattern: Environment.NewLine); 
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 1:{d.RunFirst()}"); 
Console.WriteLine($"Part 2:{d.RunSecond()}"); 
//await Task.Delay(5000); 
await d.PostSecondAnswer(); 
public class Day01 : Christmas 
{ 
    string result = "todo"; 
    public Day01() : base("1", "2022") { } 
    public override string First() 
    {
        var elves = RawInput.Split("\n\n");
        result = elves.Select(x => x.Trim().Split("\n")).Select(x => x.Select(calories => 
            int.Parse(calories)
            )).Select(total => total.Sum()).Max().ToString();
        
        return result; 
    } 
    public override string Second() 
    {
        var elves = RawInput.Split("\n\n");
        result = elves.Select(x => x.Trim().Split("\n")).Select(x => x.Select(calories =>
            int.Parse(calories)
            )).Select(total => total.Sum()).OrderByDescending(x => x).Take(3).Sum().ToString();

        return result; 
    }  
} 
