using ChristmasGifts;
var d = new Day11();
//await d.GetInput(file: "test.txt", pattern: Environment.NewLine);
await d.GetInput(pattern: "\n");
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 1:{d.RunFirst()}");
Console.WriteLine($"Part 2:{d.RunSecond()}");
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
        var octopus = Input.Select(x => x.ToCharArray().Select(y => int.Parse(y.ToString())).ToArray()).ToArray();
        for (int i = 0; i < totalSteps; i++)
        {
            var step = octopus.Step();
            octopus = step.octopus;
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
        while (flashes != 100)
        {
            totalSteps++;
            var step = octupuses.Step();
            octupuses = step.octopus;
            flashes = step.flashes;
        }
        result = totalSteps.ToString();
        return result;
    }
}
public static class Day11Extensions
{
    public static void Print(this int[][] octopus)
    {
        for (int i = 0; i < octopus.Length; i++)
        {
            for (int j = 0; j < octopus[i].Length; j++)
            {
                Console.Write(string.Format("{0,-3}", octopus[i][j]));
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public static (int[][] octopus, int flashes) Step(this int[][] octupus)
    {
        for (int i = 0; i < octupus.Length; i++)
            for (int j = 0; j < octupus[i].Length; j++)
            {
                octupus[j][i] += 1;
            }
        return octupus.CascadeFlash();
    }
    public static (int[][] octupus, int flashes) CascadeFlash(this int[][] octopus)
    {
        HashSet<(int, int)> flashed = new();

        for (int i = 0; i < octopus.Length; i++)
            for (int j = 0; j < octopus[i].Length; j++)
                TryFlash(octopus, flashed, i, j);

        for (int i = 0; i < octopus.Length; i++)
            for (int j = 0; j < octopus[i].Length; j++)
                octopus[i][j] = octopus[i][j] > 9 ? 0 : octopus[i][j];
        return (octopus, flashed.Count);
    }
    private static void TryFlash(int[][] octopus, HashSet<(int, int)> flashed, int i, int j)
    {
        if (flashed.Contains((i, j)) || i < 0 || i >= octopus.Length || j < 0 || j >= octopus[i].Length)
            return;
        if (octopus[i][j] > 9)
            Flash(octopus, flashed, i, j);
        return;
    }
    private static void Flash(int[][] octopus, HashSet<(int, int)> flashed, int i, int j)
    {
        flashed.Add((i, j));
        octopus[i][j] += 1;
        Flashed(octopus, flashed, i, j + 1);
        Flashed(octopus, flashed, i, j - 1);
        Flashed(octopus, flashed, i - 1, j);
        Flashed(octopus, flashed, i - 1, j - 1);
        Flashed(octopus, flashed, i - 1, j + 1);
        Flashed(octopus, flashed, i + 1, j);
        Flashed(octopus, flashed, i + 1, j - 1);
        Flashed(octopus, flashed, i + 1, j + 1);
    }
    private static void Flashed(int[][] octopus, HashSet<(int, int)> flashed, int i, int j)
    {

        if (i < 0 || i >= octopus.Length || j < 0 || j >= octopus[i].Length)
            return;
        octopus[i][j] += 1;
        TryFlash(octopus, flashed, i, j);
    }
}