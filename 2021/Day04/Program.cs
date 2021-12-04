using ChristmasGifts;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var d = new Day4();
//await d.GetInput(pattern: "\r\n\r\n", file: "test.txt");
await d.GetInput(pattern: "\n\n");
Console.WriteLine($"Part 1:{d.First()}");
//await d.PostFirstAnswer();

Console.WriteLine($"Part 2:{d.Second()}");
//await d.PostSecondAnswer();


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
            var win = game.Call(number);
            if (win.win)
                return $"{win.winner.Numbers.Aggregate(0, (a, b) => a + b) * number}";
        }
        return $"Todo";
    }



    public override string Second()
    {
        return $"Todo";
    }

    private class Game
    {
        private readonly Board[] Boards;

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

        internal (bool win, Board? winner) Call(int number)
        {
            foreach (var b in Boards)
            {
                b.Mark(number);
                if (b.HasWon) return (true, b);
            }
            return (false, null);
        }
    }
}