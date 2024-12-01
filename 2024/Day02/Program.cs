using ChristmasGifts; 
var d = new Day02(); 
if (Feature.Local) 
    await d.GetInput(file: "test.txt", pattern: Environment.NewLine); 
else 
    await d.GetInput(); 
Console.WriteLine($"Part 1:{d.RunFirst()}"); 
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 2:{d.RunSecond()}"); 
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 
public class Day02 : Christmas 
{ 
    string result = "todo"; 
    public Day02() : base("02", "2024") { } 
    public override string First() 
    { 
        return result; 
    } 
    public override string Second() 
    { 
        return result; 
    }  
} 
