using ChristmasGifts;

var d = new Day3(); 
//await d.GetInput(file: "test.txt", pattern:Environment.NewLine); 
await d.GetInput(); 
Console.WriteLine($"Part 1:{d.RunFirst()}"); 
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 2:{d.RunSecond()}"); 
//await Task.Delay(5000); 
await d.PostSecondAnswer(); 
public class Day3 : Christmas 
{ 
    string result = "todo";

    public Day3() : base("3", "2022") { } 
    public override string First() 
    {
        return Input.Select(x => ParseRucksack(x).CommonItem.Priority()).Sum().ToString();
    }
    public override string Second()
    {
        return GetElfGroups(Input).Select(group => group.CommonItem.Priority()).Sum().ToString();
    }

    public RuckSack ParseRucksack(string input) => new RuckSack(input[0..(input.Length / 2)]
, input[(input.Length / 2)..]);

    public ElfGroup[] GetElfGroups(string[] input) => input.Select((selector, index) => 
                input.Skip(index * 3).Take(3)).Where(a => a.Any())
            .Select(x => x.ToArray())
            .Select(x => new ElfGroup(x[0], x[1], x[2])).ToArray();
    
    public record RuckSack(string FirstCompartment, string SecondCompartment)
    {
        public char CommonItem => FirstCompartment.Where(x => SecondCompartment.Contains(x)).Select(x => x).First();
    }

    public record ElfGroup(string First, string Second, string Third)
    {
        public char CommonItem => First.Where(x => Second.Contains(x) && Third.Contains(x)).Select(x => x).First();
    }
} 

public static class CharHelpers
{
    public static int Priority(this char item) => item < 97 ? item - 38 :item - 96;
}


