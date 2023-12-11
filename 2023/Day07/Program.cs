using ChristmasGifts;
using System.Diagnostics;
var d = new Day07();
if (Feature.Local)
    await d.GetInput(file: "test.txt", pattern: Environment.NewLine);
else
    await d.GetInput();
Console.WriteLine($"Part 1:{d.RunFirst()}");
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 
public static class Feature
{
    public static bool Local = false;
}
public class Day07 : Christmas
{
    string result = "todo";
    public Day07() : base("7", "2023") { }
    public override string First()
    {
        var rankedPlays = Input.Select(x =>
        {
            var line = x.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            return new Play(new Hand(line[0]), new Bet(long.Parse(line[1])));
        }).OrderBy(x => x.hand).ToArray();

        var total = 0L;
        for (int i = 0; i < rankedPlays.Count(); i++)
        {
            total += rankedPlays[i].Bet.Amount * (i + 1);
        }
        return total.ToString();
    }
    public override string Second()
    {
        var rankedPlays = Input.Select(x =>
        {
            var line = x.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            return new Play(new Hand(line[0], true), new Bet(long.Parse(line[1])));
        }).OrderBy(x => x.hand).ToArray();

        var total = 0L;
        for (int i = 0; i < rankedPlays.Count(); i++)
        {
            total += rankedPlays[i].Bet.Amount * (i + 1);
        }
        return total.ToString();
    }
    [DebuggerDisplay("{hand.Name} - {Bet.Amount}")]
    public record Play(Hand hand, Bet Bet);
    public record Bet(long Amount);
    [DebuggerDisplay("{Name} -  {CardsString} - {Hex} - {Cards.Length}")]

    public record Hand : IComparable<Hand>
    {
        public string Name { get; init; }
        private string suit = "-23456789TJQKA";
        private string suit2 = "-J23456789TQKA";
        private string hex = "123456789ABCDEF";
        public int Strength { get; init; }

        public string CardsString { get; init; }
        public string Hex { get; init; }// CardsString.OrderByDescending(x => x).Aggregate("", (x, y) => x + y);
        public Hand(string input, bool partTwo = false)
        {
            Name = input;
            var sortedString = input.GroupBy(c => c).OrderByDescending(g => g.Count());
            var jokers = input.Count(x => x == 'J');
            if (sortedString.Any(g => g.Count() == 5))
            {
                Strength = 7;
            }
            else if (sortedString.Any(g => g.Count() == 4))
            {
                if (partTwo && sortedString.Any(g => g.Key == 'J'))
                    Strength = 7;
                else
                    Strength = 6;
            }
            else if (sortedString.Any(g => g.Count() == 3))
            {
                var three = sortedString.First(g => g.Count() == 3);
                if (sortedString.Any(g => g.Count() == 2))
                {
                    if (partTwo && (sortedString.Any(g => g.Key == 'J')))
                        Strength = 7;
                    else
                        Strength = 5;
                }
                else if (partTwo && (three.Key == 'J' || jokers > 0))
                    Strength = 6;
                else
                    Strength = 4;
            }
            else if (sortedString.Where(g => g.Count() == 2).Count() == 2)
            {
                if (partTwo && sortedString.Where(g => g.Count() == 2).Any(g => g.Key == 'J'))
                    Strength = 6;
                else
                {
                    if (partTwo && sortedString.Any(g => g.Key == 'J'))
                        Strength = 5;
                    else
                        Strength = 3;
                }
            }
            else if (sortedString.Any(g => g.Count() == 2))
            {
                if (partTwo && sortedString.Any(g =>g.Key == 'J'))
                    Strength = 4;
                else
                    Strength = 2;
            }
            else
            {
                if (partTwo && jokers > 0)
                    Strength = 2;
                else
                    Strength = 1;
            }

            var refSuit = partTwo ? suit2 : suit;
            CardsString = input.OrderBy(x => x).Aggregate("", (x, y) => x + y);
            Hex = (input.Aggregate("", (x, y) => x + hex[refSuit.IndexOf(y)]));

        }

        public int CompareTo(Hand? other)
        {
            if (Strength == other.Strength)
            {
                for (int i = 0; i < Hex.Length; i++)
                {
                    if (Hex[i] != other.Hex[i])
                    {
                        return Hex[i].CompareTo(other.Hex[i]);
                    }
                }
            }
            return Strength.CompareTo(other.Strength);
        }

    }
    public record Card(int suit, int multiplier);

}
