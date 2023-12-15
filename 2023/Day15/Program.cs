using ChristmasGifts; 
var d = new Day15();
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
public class Day15 : Christmas 
{ 
    string result = "todo"; 
    public Day15() : base("15", "2023") { }
    public override string First()
    {
        var input = RawInput.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Where(c=>c!='\n').ToArray());

        var total = 0;
        foreach(var line in input)
        {
            var currentValue = 0;

            foreach(var c in line){
                currentValue += (int)c;
                currentValue *= 17;
                currentValue %= 256;
            };
           
            total += currentValue;
        }
        return total.ToString();    
            
    } 
    public override string Second() 
    { 
        return result; 
    }  
} 
