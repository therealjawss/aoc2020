using ChristmasGifts;
var d = new Day10();
//await d.GetInput(file: "test.txt", pattern: Environment.NewLine);
await d.GetInput();
Console.WriteLine($"Part 1:{d.RunFirst()}");
await d.PostFirstAnswer();
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 
public class Day10 : Christmas
{
    string result = "todo";
    public Day10() : base("10", "2022") { }
    public override string First()
    {
        int cycleToWatch = 20;
        int currentCycle = 1;
 
        long totalSignalStrength = 0;
        int cycleAge = Input[0].Trim().Split(' ').Length; ;

        for (int i = 1; i < Input.Length && cycleToWatch<=220; currentCycle++)
        {

            if (currentCycle == cycleToWatch)
            {
                var signal = Input[..(i)].Select(x => x.Trim().Split(' ')).Where(x => x.Length ==2).Sum(x => int.Parse(x[1]))+1;

                totalSignalStrength+= signal * currentCycle;
                cycleToWatch+=40;
            }
            if (cycleAge == 0)
            {
                i++;
                cycleAge = Input[i].Trim().Split(' ').Length;
            }
            cycleAge--;
        }
        result = totalSignalStrength.ToString();
        return result;
    }

    public record Instruction(int increment);

    public override string Second()
    {
        return result;
    }
}
