using ChristmasGifts;
var d = new Day11();
//await d.GetInput(file: "test.txt", pattern: Environment.NewLine);
await d.GetInput(pattern: "\n"); 
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 1:{d.First()}");
Console.WriteLine($"Part 2:{d.Second()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 
public class Day11 : Christmas
{
    string result = "todo";
    public Day11() : base("11", "2021") { }
    public override string First()
    {
        var totalSteps = 100;
        var flashes = 0;
        var octupuses = Input.Select(x => x.ToCharArray().Select(y => int.Parse(y.ToString())).ToArray()).ToArray();
        for (int i = 0; i < totalSteps; i++)
        {
            var step = octupuses.Step();
            octupuses = step.octupus;
            flashes += step.flashes;
        }
        result = flashes.ToString();
        return result;
    }
    public override string Second()
    {
        var totalSteps = 0;
        var flashes = 0;
        var octupuses = Input.Select(x => x.ToCharArray().Select(y => int.Parse(y.ToString())).ToArray()).ToArray();
        while (flashes != 100) {
            totalSteps++;
            var step = octupuses.Step();
            octupuses = step.octupus;
            flashes =  step.flashes;
        }
        result = totalSteps.ToString();
        return result;
    }
}
public static class Day11Extensions
{
    public static void Print(this int[][] octupus)
    {
        for (int i = 0; i < octupus.Length; i++)
        {
            for (int j = 0; j < octupus[i].Length; j++)
            {
                Console.Write(string.Format("{0,-3}", octupus[i][j]));
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public static (int[][] octupus, int flashes) Step(this int[][] octupus)
    {
        for (int i = 0; i < octupus.Length; i++)
            for (int j = 0; j < octupus[i].Length; j++)
            {
                octupus[j][i] += 1;
            }
        octupus.Print();
        return octupus.CascadeFlash();
    }
    public static (int[][] octupus, int flashes) CascadeFlash(this int[][] octupus)
    {
        HashSet<(int, int)> flashed = new();

        for (int i = 0; i < octupus.Length; i++)
            for (int j = 0; j < octupus[i].Length; j++)
                TryFlash(octupus, flashed, i, j);

        for (int i = 0; i < octupus.Length; i++)
            for (int j = 0; j < octupus[i].Length; j++)
                octupus[i][j] = octupus[i][j] > 9 ? 0 : octupus[i][j];

        return (octupus, flashed.Count);
    }
    private static void TryFlash(int[][] octupus, HashSet<(int, int)> flashed, int i, int j)
    {
        if (flashed.Contains((i, j)) ||i < 0 || i>= octupus.Length || j<0 || j>=octupus[i].Length)
            return;
        if (octupus[i][j]>9)
            Flash(octupus, flashed, i, j);
        return;
    }
    private static void Flash(int[][] octupus, HashSet<(int, int)> flashed, int i, int j)
    {
        flashed.Add((i, j));
        octupus[i][j]+=1;
        Flashed(octupus, flashed, i, j+1);
        Flashed(octupus, flashed, i, j-1);
        Flashed(octupus, flashed, i-1, j);
        Flashed(octupus, flashed, i-1, j-1);
        Flashed(octupus, flashed, i-1, j+1);
        Flashed(octupus, flashed, i+1, j);
        Flashed(octupus, flashed, i+1, j-1);
        Flashed(octupus, flashed, i+1, j+1);
    }
    private static void Flashed(int[][] octupus, HashSet<(int, int)> flashed, int i, int j)
    {

        if (i < 0 || i>= octupus.Length || j<0 || j>=octupus[i].Length)
            return;
        octupus[i][j]+=1;
        TryFlash(octupus, flashed, i, j);
    }
}