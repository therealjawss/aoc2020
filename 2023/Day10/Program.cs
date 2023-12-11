using ChristmasGifts;
var d = new Day10();
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
public class Day10 : Christmas
{
    string result = "todo";
    char[][] pipe  = new char[0][];
    char START = 'S';
    public Day10() : base("10", "2023") { }


    public override string First()
    {
        var start = FindStart(Input);
        var visited = new HashSet<Coordinates>();
        Coordinates[] exits = FindExits(Input, start).ToArray();
        var p1 = exits[0];
        var p2 = exits[1];
        visited.Add(start);
        var ctr = 0;
        do
        {
            visited.Add(p1);
            visited.Add(p2);
            ctr++;
            p1 = FindExits(Input, p1).FirstOrDefault(x => !visited.Contains(x));
            p2 = FindExits(Input, p2).FirstOrDefault(x => !visited.Contains(x));

        } while (p1 is not null && p2 is not null);
        printVisited(p1, p2, visited, ctr);

        result = ctr.ToString();
        return result;
    }

    public override string Second()
    {
        
        int count = 0;
        foreach (char[] row in pipe)
        {
            foreach (char c in row)
            {
                if (c == 'I')
                {
                    count++;
                }
            }
        }

        return count.ToString();
    }
    string CORNERS = "7JFL";
    private void printVisited(Coordinates? p1, Coordinates? p2, HashSet<Coordinates> visited, int ctr)
    {
        Console.WriteLine($"\n--    {ctr} :  {p1} {p2}   --------------------------------\n");

        pipe = Input.Select(x => new string('.', x.Length).ToCharArray()).ToArray();

        for (int row = 0; row < Input.Length; row++)
        {
            bool isOut = true;
            var corners = "";
            var pipes = "";

            for (int col = 0; col < Input[row].Length; col++)
            {

                var point = new Coordinates(row, col);
                if (point == p1 || point == p2)
                {
                    pipe[row][col] = 'X';
                    Console.Write("X");
                }
                else if (visited.Contains(point))
                {
                    var p = Input[point.Row][point.Column];
                    if (p == 'S')
                    {
                        p = START;
                    }   
                    Console.Write(p);
                    pipe[row][col] = p;
                    if (p == '|' || CORNERS.Contains(p))
                    {
                        if (p == '|')
                            pipes += p;
                        else
                            corners = corners += p;
                        isOut = (pipes.Length + CountCrosses(corners)) % 2 == 0;
                    }
                }
                else
                {
                    if (isOut)
                    {
                        pipe[row][col] = 'O';
                        Console.Write("O");
                    }
                    else
                    {
                        pipe[row][col] = 'I';
                        Console.Write("I");
                    }
                }
            }
            Console.WriteLine();
        }

        Console.WriteLine("\n--------------------------------------------\n");
    }


    public record Coordinates(int Row, int Column);

    public static Coordinates FindStart(string[] map)
    {
        for (int row = 0; row < map.Length; row++)
        {
            for (int column = 0; column < map[row].Length; column++)
            {
                if (map[row][column] == 'S')
                {
                    return new Coordinates(row, column);
                }
            }
        }
        throw new Exception("No start found");

    }
    string top = "|7FS";
    string bottom = "|JLS";
    string right = "-7JS";
    string left = "-FLS";
    public IEnumerable<Coordinates> FindExits(string[] map, Coordinates coordinates)
    {
        var pipe = map[coordinates.Row][coordinates.Column];
        var exits = new List<string>();
        if (left.Contains(pipe) && coordinates.Column + 1 < map[0].Length)
        {
            (var row, var col) = new Coordinates(coordinates.Row, coordinates.Column + 1);
            if (right.Contains(map[row][col]))
            {
                exits.Add(left);
                yield return new Coordinates(row, col);
            }
        }
        if (right.Contains(pipe) && coordinates.Column - 1 >= 0)
        {
            (var row, var col) = new Coordinates(coordinates.Row, coordinates.Column - 1);
            if (left.Contains(map[row][col]))
            {
                exits.Add(right);
                yield return new Coordinates(row, col);
            }
        }
        if (top.Contains(pipe) && coordinates.Row + 1 < map.Length)
        {
            (var row, var col) = new Coordinates(coordinates.Row + 1, coordinates.Column);
            if (bottom.Contains(map[row][col]))
            {
                exits.Add(top);
                yield return new Coordinates(row, col);
            }
        }
        if (bottom.Contains(pipe) && coordinates.Row - 1 >= 0)
        {
            (var row, var col) = new Coordinates(coordinates.Row - 1, coordinates.Column);
            if (top.Contains(map[row][col]))
            {
                exits.Add(bottom);
                yield return new Coordinates(row, col);
            }
        }
        var common = exits[0].Select(x=> exits[1].Contains(x) ? x : ' ').Where(x=>x!=' ' && x != 'S').First();
        START = common;
    }

    public int CountCrosses(string input)
    {
        Stack<char> stack = new();
        foreach (var c in input)
        {
            if (c == 'J')
            {
                if (stack.Count > 0)
                {
                    if (stack.Peek() == 'L')
                    {
                        stack.Pop();
                    }
                    else
                    {
                        stack.Push(c);
                    }
                }
            }
            else if (c == '7')
            {
                if (stack.Peek() == 'F')
                {
                    stack.Pop();
                }
                else
                {
                    stack.Push(c);
                }
            }
            else
            {
                stack.Push(c);
            }

        }
        return stack.Count() / 2;

    }
}
