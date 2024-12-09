using ChristmasGifts; 
var d = new Day08(); 
if (Feature.Local) 
    await d.GetInput(file: "test.txt", pattern: Environment.NewLine); 
else 
    await d.GetInput(); 
Console.WriteLine($"Part 1:{d.RunFirst()}"); 
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 
public class Day08 : Christmas
{
    string result = "todo";
    public Day08() : base("08", "2024") { }
    public override string First()
    {
        //Dictionary<char, Coord[]> lookup = new();
        //var map = Input.Select(x=>x.ToCharArray()).ToArray();
        //for(int i =0; i < map.Length; i++)
        //    for(int j= 0; j < map[i].Length; j++)
        //    {
        //        lookup.TryAdd(map[i][j],
        //            new Coord(i, j));
        //    }
        return result;
    }
    public record Coord(int x, int y);
    public record Map(char character, Coord[] coords);

    public override string Second()
    {
        return result;
    }

}