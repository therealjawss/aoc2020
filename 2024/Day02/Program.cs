using ChristmasGifts; 
var d = new Day02();

if (false) 
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
    public Day02() : base("2", "2024") { } 
    public override string First() 
    {
        var lines = Input.Select(entries =>
        {
            var items = entries.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
            return EvaluateSafety(items);
        }

        ).Count(x => x == true);
        
        return lines.ToString();
    
    }

    private static bool EvaluateSafety(List<int> items)
    {
        var direction = items[0] < items[1] ? 1 : -1;
        var isSafe = true;

        for (int i = 1; i < items.Count && isSafe; i++)
        {
            var diff = items[i] - items[i - 1];
            var currSafe = Math.Abs(diff) <= 3 && diff != 0 && diff * direction >= 0;
            isSafe = (isSafe && currSafe);
        }
        return isSafe;
    }

    public override string Second() 
    {
        var lines = Input.Select(entries =>
        {
            var items = entries.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
            var isSafe = EvaluateSafety(items);

            for (int i=0; i < items.Count && !isSafe; i++)
            {
                var dampened = items.ToList();
                dampened.RemoveAt(i);
                isSafe = EvaluateSafety(dampened);
            }
          
            return isSafe;
        }

      ).Count(x => x == true);

        return lines.ToString();
    }
} 
