using ChristmasGifts;
using System.Text.RegularExpressions;
var d = new Day02();
//await d.GetInput(file: "test.txt", pattern: Environment.NewLine);
await d.GetInput();
Console.WriteLine($"Part 1:{d.RunFirst()}");
//await d.PostFirstAnswer();
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
await d.PostSecondAnswer(); 
public class Day02 : Christmas
{
    string result = "todo";
    public Day02() : base("2", "2023") { }
    public override string First()
    {
        result = Input.Select(x => new Game(x)).Where(x => x.IsPossibleWithLoad(new Load(12, 13, 14))).Sum(g => g.Number).ToString();
        
        return result;
    }
    public override string Second()
    {
        result = Input.Select(i => new Game(i).GetPower()).Sum().ToString();

        return result;
    }
}
public class Game
{
    public Game(string input)
    {
        var split = input.Split(':');
        Number = int.Parse(split[0].Trim().Replace("Game ", ""));
        Turns = split[1].Split(';').Select(x => new Turn(x)).ToList();
    }

    public int Number { get; internal set; } 
    public List<Turn> Turns { get; internal set; }

    public Load GetMinimumLoad()
    {
        int maxRed = Turns.Max(turn => turn.Red);
        int maxGreen = Turns.Max(turn => turn.Green);
        int maxBlue = Turns.Max(turn => turn.Blue);

        return new Load(maxRed, maxGreen, maxBlue);
    }

    public int GetPower()
    {
        return GetMinimumLoad().Power;
    }

    public bool IsPossibleWithLoad(Load load)
    {
        return Turns.All(turn => turn.Green <= load.green && turn.Red <= load.red && turn.Blue <= load.blue);
    }
}
public record Load(int red, int green, int blue)
{
    public int Power => red * green * blue;
}

public class Turn
{
    public int Blue { get; internal set; }
    public int Red { get; internal set; }
    public int Green { get; internal set; }

    public Turn(string input)
    {
        Blue = GetMatch("blue", input);
        Red = GetMatch("red", input);
        Green = GetMatch("green", input);
    }

    private static int GetMatch(string color, string input)
    {
        var regex = new Regex(@"(\d+) " + color);
        var match = regex.Match(input);
        if (match.Success)
        {
            return int.Parse(match.Groups[1].Value);
        }
        else
        {
            return 0;
        }
    }
}
