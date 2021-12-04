using ChristmasGifts;
using System.Text.RegularExpressions;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var d = new Day4();
//await d.GetInput(pattern: "\r\n\r\n", file: "test.txt");
await d.GetInput(pattern: "\n\n");
Console.WriteLine($"Part 1:{d.First()}");
//await d.PostFirstAnswer();

Console.WriteLine($"Part 2:{d.Second()}");
await d.PostSecondAnswer();


public class Day4 : Christmas
{
    public Day4() : base("4", "2021")
    {

    }

    public override string First()
    {
        var BingoNumbers = Input[0].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
        var game = new Game(Input[1..]);

        foreach (var number in BingoNumbers)
        {
            var winner = game.Call(number);
            if (winner!=null)
                return $"{winner.Numbers.Aggregate(0, (a, b) => a + b) * number}";
        }
        return $"Todo";
    }


    public override string Second()
    {
        var BingoNumbers = Input[0].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
        var game = new Game(Input[1..]);
        foreach (var number in BingoNumbers)
        {
            var winner = game.Call(number);
            if (winner!=null && game.Winners.Count() == game.Boards.Count())
                return $"{winner.Numbers.Aggregate(0, (a, b) => a + b) * number}";

        }
        return $"Todo";
    }

    private class Game
    {
        public readonly Board[] Boards;
        public List<Board> Winners => Boards.Where(x => x.HasWon).ToList();
        public Game(string[] vs)
        {
            Boards = GetBoards(vs);
        }
        private static Board[] GetBoards(string[] vs)
        {
            List<Board> boards = new List<Board>();
            foreach (var v in vs)
            {
                boards.Add(new Board(v.Trim()));
            }

            return boards.ToArray();
        }

        internal Board? Call(int number)
        {
            Board? thisWinner = null;
            foreach (var b in Boards)
            {
                var newWinner = !b.HasWon;
                b.Mark(number);
                if (newWinner && b.HasWon)
                    thisWinner = b;
            }
            return thisWinner;
        }
    }
    internal class Board
    {
        public HashSet<int> Numbers = new HashSet<int>();
        List<int>[] Rows = new List<int>[5];
        List<int>[] Columns = new List<int>[5];

        public Board(string input)
        {
            Numbers = new Regex(@"\d+").Matches(input)
                .Select(x => int.Parse(x.Value))
                .ToHashSet();

            var lines = input.Split("\n");
            Rows = lines.Select(x => x.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x)).ToList()).ToArray();

            Columns = Enumerable.Range(0, 5).Select(x => Rows.ToArray().Select(r => r[x]).ToList()).ToArray();
        }

        internal void Mark(int number)
        {
            Numbers.Remove(number);
            Rows.ToList().ForEach(x => x.Remove(number));
            Columns.ToList().ForEach(x => x.Remove(number));
        }

        public bool HasWon => Rows.Any(x => x.Count==0) || Columns.Any(x => x.Count==0);
        public override string ToString()
        {
            return $"{HasWon}";
        }
    }
}