using ChristmasGifts;
var d = new Day06();
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
public class Day06 : Christmas
{
    string result = "todo";
    public Day06() : base("6", "2024") { }
    List<Coord> obstructions = new List<Coord>();
    Dictionary<Coord, Direction> directions = new();
    public override string First()
    {
        var map = Input.Select(x => x.ToCharArray()).ToArray();
        var origin = map.FindOrigin('^');
        var currentPoint = origin;

        Direction currentDirection = new Up();

        while (currentPoint.x < map[0].Length && currentPoint.x >= 0 && currentPoint.y < map.Length && currentPoint.y >= 0)
        {
            var next = new Coord(currentPoint.x + currentDirection.x, currentPoint.y + currentDirection.y);
            if (next.x < 0 || next.x >= map[0].Length || next.y < 0 || next.y >= map.Length)
                break;

            map[currentPoint.y][currentPoint.x] = 'X';

            if (map[next.y][next.x] == '#')
                currentDirection = currentDirection.Turn();
            else
            {
                currentPoint = next;
            }

        }

        result = map.Select(x => x.Where(c => c == 'X').Count()).Sum().ToString();
        return result;
    }
    public override string Second()
    {
        var map = Input.Select(x => x.ToCharArray()).ToArray();

        var origin = map.FindOrigin('^');

        var loopsFound = 0;
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                map = Input.Select(x => x.ToCharArray()).ToArray();

                if (map[i][j] == '#')
                    continue;
                map[i][j] = '#';
                Direction currentDirection = new Up();
                var loopFound = false;
                Dictionary<Coord, Direction> visited = new();
                var currentPoint = origin;
                while (currentPoint.x < map[0].Length && currentPoint.x >= 0 && currentPoint.y < map.Length && currentPoint.y >= 0)
                {
                    var next = new Coord(currentPoint.x + currentDirection.x, currentPoint.y + currentDirection.y);
                    if (next.x < 0 || next.x >= map[0].Length || next.y < 0 || next.y >= map.Length)
                        break;

                    map[currentPoint.y][currentPoint.x] = 'X';
                    
                    if (visited.ContainsKey(currentPoint))
                    {
                        if (visited[currentPoint] == currentDirection)
                        {
                            loopFound = true;
                            break;
                        }
                    }
                    else
                    {
                        visited.Add(currentPoint, currentDirection);
                    }

                    if (map[next.y][next.x] == '#')
                        currentDirection = currentDirection.Turn();
                    else
                    {
                        currentPoint = next;
                    }

                }

                if (loopFound) loopsFound++;
            }
        }
        return loopsFound.ToString();
    }
}
public abstract record Direction(int x, int y)
{
    public Direction Turn()
    {
        if (this is Up)
            return new Right();
        else if (this is Right)
            return new Down();
        else if (this is Down)
            return new Left();
        else return new Up();
    }
}
public record Up() : Direction(0, -1);
public record Down() : Direction(0, 1);
public record Left() : Direction(-1, 0);
public record Right() : Direction(1, 0);

public record Coord(int x, int y);
public static class Extensions
{
    public static Coord FindOrigin(this char[][] map, char icon)
    {
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == icon)
                    return new Coord(j, i);
            }
        }
        throw new Exception("Not found");
    }
    public static void Print(this char[][] map)
    {
        Console.Clear();
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                Console.Write(map[i][j]);
            }
            Console.WriteLine();
        }
    }
}
