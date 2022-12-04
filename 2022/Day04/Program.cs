using ChristmasGifts;
using System.Formats.Asn1;
using System.Text.RegularExpressions;

var d = new Day4(); 
await d.GetInput(); 
//await d.GetInput(pattern: Environment.NewLine); 
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 1:{d.RunFirst()}"); 
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
await d.PostSecondAnswer(); 
public class Day4 : Christmas
{
    string result = "todo";
    public Day4() : base("4", "2022") { }
    public override string First()
    {
        return Input.Select(x=>ParseInput(x)).Where(x=> x?.IsOverlapping ?? false).Count().ToString();
    }
    public override string Second()
    {
        return Input.Select(x => ParseInput(x)).Where(x => x?.IsPartiallyOverlapping ?? false).Count().ToString();
    }
    public record Area(int start, int end)
    {
        public bool FullyOverlapsWith(Area area) => FullyContains(area) || area.FullyContains(this);
        public bool PartiallyOverlapsWith(Area area) => PartiallyContains(area) || area.PartiallyContains(this);

        public bool FullyContains(Area area) => !(start > area.start) && !(end < area.end);

        public bool PartiallyContains(Area area) => !(area.start
            < start) && !(area.start > end) || !(area.end < start) && !(area.end > end);
    }

    public record Pair(Area Elf1, Area Elf2)
    {
        public bool IsOverlapping => Elf1.FullyOverlapsWith(Elf2);
        public bool IsPartiallyOverlapping => Elf1.PartiallyOverlapsWith(Elf2);
    }
    public Pair? ParseInput(string input)
    {
        var pattern = @"(\d+)-(\d+),(\d+)-(\d+)";
        var parsed = new Regex(pattern).Match(input);

        return parsed.Success ? new Pair(
            new Area(
                Convert.ToInt32(parsed.Groups[1].Value),
                Convert.ToInt32(parsed.Groups[2].Value)),
            new Area(
                Convert.ToInt32(parsed.Groups[3].Value),
                Convert.ToInt32(parsed.Groups[4].Value)))
            : null;



    }
} 
