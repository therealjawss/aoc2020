using ChristmasGifts;
var d = new Day04();
//await d.GetInput(file: "test.txt", pattern: Environment.NewLine);
await d.GetInput();
Console.WriteLine($"Part 1:{d.RunFirst()}");
//await d.PostFirstAnswer();
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 11827296
public class Day04 : Christmas
{
    public Day04() : base("4", "2023") { }
    public override string First()
    {
        var score = Input.Select(line =>
        {
            var numbers = line.Split(":")[1].Split("|");
            var winning = numbers[0].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
            var mine = numbers[1].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();

            return mine.Where(x => winning.Contains(x)).Count();

        }).Where(t => t > 0).Select(t => (long)Math.Pow(2, t - 1)).Sum();

        return score.ToString();
    }

    public override string Second()
    {
        var winningCards = new int[Input.Length];
        for (var i = 0; i < Input.Length; i++)
        {
            var numbers = Input[i].Split(":")[1].Split("|");
            var winning = numbers[0].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
            var mine = numbers[1].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();

            var total = mine.Where(x => winning.Contains(x)).Count();
            for (int j = 0; j < total && (i + j + 1) < Input.Length; j++)
            {
                winningCards[i + j + 1] += winningCards[i] + 1;
            }
        }

        return (Input.Length + winningCards.Sum()).ToString();
    }
}
