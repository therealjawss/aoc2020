using ChristmasGifts;
using System.Data;
using static Day12;

var d = new Day12();
await d.GetInput(file: "test.txt", pattern: Environment.NewLine);
//await d.GetInput();
Console.WriteLine($"Part 1:{d.RunFirst()}");
//await d.PostFirstAnswer();
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 
public class Day12 : Christmas
{
    string result = "todo";
    public Day12() : base("12", "2022") { }
    public static int xBound;
    public static int yBound;
    public override string First()
    {
        var map = Input.Select(x => x.Trim().ToCharArray()).ToArray();
        yBound =Input.Length;
        xBound = Input[0].Trim().Length;

        int yIndex, xIndex;
        var sLocation = FindLocation('S');
        var seenZone = new HashSet<Coord>() { sLocation };
        var distance = TravelToTop(map, sLocation, seenZone);


        result = distance.Length.ToString();
        return result;
    }

    Dictionary<(Coord,Coord, int), string> memory = new();
    HashSet<(Coord, Coord)> fails = new();
    private string TravelToTop(char[][] map, Coord start, HashSet<Coord> seenZone)
    {
        var startCar = map.Get(start) == 'S' ? 'a' : map.Get(start);
        if (startCar == 'z'&& (map.Get(start.Left()) == 'E' || map.Get(start.Right()) == 'E' || map.Get(start.Up())== 'E'|| map.Get(start.Down()) == 'E')) return startCar.Value.ToString();
        var nextMoves = GetPossibleNextMoves(start, map);
        var possible = new List<string>();
        string? tentative = null;
        foreach (var move in nextMoves)
        {
            if (fails.Contains((start, move)))
                continue;
            else if (memory.ContainsKey((start, move, seenZone.GetHashCode())))
            {
                tentative=memory[(start, move, seenZone.GetHashCode())];
            }
            else
            {
                if (!seenZone.Contains(move))
                {
                    seenZone.Add(move);
                    tentative = TravelToTop(map, move, seenZone);
                    seenZone.Remove(move);
                }
            }
            if (tentative == null)
            {
                fails.Add((start, move));
                continue;
            }
            else
            {
                possible.Add(tentative);
                memory[(start, move, seenZone.GetHashCode())] = tentative;

            }
        }
        if (possible.Count > 0)
        {
            var shortest = possible.OrderBy(x => x.Length).First();
            return startCar + shortest;
        }
        seenZone.Remove(start);
        return null;
    }

    private IEnumerable<Coord> GetPossibleNextMoves(Coord start, char[][] map)
    {
        try
        {
            var startChar = map.Get(start) == 'S' ? 'a' : map.Get(start)!.Value;
            var moves = new List<Coord>() { start.Up(), start.Down(), start.Left(), start.Right() };
            var possible = moves.Where(x => map.Get(x)!=null)
                .Select(x => x);

            return possible.Where(x =>
            {
                var c = map.Get(x);
                return c<=startChar +1;
            }).ToList();
        }
        catch (Exception)
        {

            throw;
        }
       
    }

    public override string Second()
    {
        return result;
    }
    public record Coord(int x, int y);
    private Coord FindLocation(char target)
    {
        var yIndex = -1;
        Enumerable.Range(0, Input.Length).Select(x => yIndex = Input[x].Contains(target) ? x : yIndex).ToList();
        var xIndex = Input.First(x => x.Contains(target)).IndexOf(target);
        return new Coord(xIndex, yIndex);
    }

}
public static class CharArrayExtensions
{
    public static char? Get(this char[][] map, Coord coord) => coord.x <0 || coord.x >= Day12.xBound || coord.y<0 || coord.y >= Day12.yBound ? null : map[coord.y][coord.x];
    public static Coord Up(this Coord coord) => new Coord(coord.x, coord.y-1);
    public static Coord Down(this Coord coord) => new Coord(coord.x, coord.y+1);
    public static Coord Left(this Coord coord) => new Coord(coord.x-1, coord.y);
    public static Coord Right(this Coord coord) => new Coord(coord.x+1, coord.y);
}


//var startCar = start == new Coord(0, 0) ? 'a' : map.Get(start);

//if (start.x < 0 || start.y < 0 || start.x >= Day12.xBound || start.y >= yBound || seenZone.Count == Day12.xBound * Day12.yBound) { return 0; }
//if (startCar == 'z' && (map.Get(start.Left()) == 'E' || map.Get(start.Right()) == 'E' || map.Get(start.Up())== 'E'|| map.Get(start.Down()) == 'E'))
//    return 1;
//else
//{
//    seenZone.Add(start);
//    var nextMoves = GetPossibleNextMoves(start, map, seenZone);

//    var min = int.MaxValue;
//    Coord? minCoord = null;
//    foreach (var nextMove in nextMoves)
//    {
//        var possMin = TravelToTop(map, nextMove, seenZone);
//        if (possMin < min)
//        {
//            min = possMin;
//            minCoord = nextMove;
//        }
//    }
//    seenZone.Remove(start);
//    return minCoord == null ? 0 : 1 + min;
//            //var up = !seenZone.Contains(start.Up()) && map.Get(start.Up()) <=startCar+1? measureMinimum(map, start.Up(), seenZone): 0;
//            //var down = !seenZone.Contains(start.Down()) && map.Get(start.Down()) <=startCar+1 ? measureMinimum(map, start.Down(), seenZone): 0;
//            //var left = !seenZone.Contains(start.Left())&& map.Get(start.Left()) <=startCar+1 ? measureMinimum(map, start.Left(), seenZone):0;
//            //var right = !seenZone.Contains(start.Right())&& map.Get(start.Right()) <=startCar+1 ? measureMinimum(map, start.Right(), seenZone):0;
