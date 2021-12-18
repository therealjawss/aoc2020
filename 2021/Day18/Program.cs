using ChristmasGifts;
var d = new Day18();
await d.GetInput(file: "test.txt", pattern: Environment.NewLine);
//await d.GetInput(pattern: Environment.NewLine); 
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 1:{d.RunFirst()}");
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 
public class Day18 : Christmas
{
    string result = "todo";
    public Day18() : base("18", "2021") { }
    public override string First()
    {
        var sn = Input.Select(x => ParseSnailfishNumber(x));

        return result;
    }

    public SnailfishNumber ParseSnailfishNumber(string x)
    {
        SnailfishNumber result = new SnailfishNumber(0, 0);
        if (x.Length == 5) return new SnailfishNumber(int.Parse(x[1..2]), int.Parse(x[3..4]));
        if (x[1] == '[')
        {
            var x1 = 1;
            var x2 = 0;
            var y1 = 0;
            var y2 = 0;
            if (int.TryParse(x[x.Length - 2].ToString(), out var number))
            {
                x2 = x.Length - 3;
                result = new SnailfishNumber(ParseSnailfishNumber(x[x1..x2]), number);
            }
            else
            {
                y2 = x.Length - 2;
                y1 = GetYPairIndex(x, y2);
                x2 = GetXPairIndex(x, 1);
                result = new SnailfishNumber(ParseSnailfishNumber(x[x1..(x2 + 1)]), ParseSnailfishNumber(x[y1..(y2 + 1)]));
            }

        }
        else if (int.TryParse(x[1].ToString(), out var number))
        {
            result = new SnailfishNumber(number, ParseSnailfishNumber(x[3..(x.Length - 1)]));
        }
        return result;
    }

    private int GetXPairIndex(string x, int x1)
    {
        int ctr = 1;
        int i = 0;
        for (i = x1 + 1; i < x.Length && ctr != 0; i++)
        {
            ctr += x[i] switch
            {
                ']' => -1,
                '[' => 1,
                _ => 0
            };
            if (ctr == 0) return i;
        }
        return i;
    }

    private int GetYPairIndex(string x, int y2)
    {
        int ctr = 1;
        int i = 0;
        for (i = y2 - 1; i > 0 && ctr != 0; i--)
        {
            ctr += x[i] switch
            {
                ']' => 1,
                '[' => -1,
                _ => 0
            };

            if (ctr == 0) return i;
        }
        return i;
    }

    public record SnailfishNumber
    {
        private int x;
        private int y;
        private SnailfishNumber? X;
        private SnailfishNumber? Y;

        public SnailfishNumber(SnailfishNumber X, SnailfishNumber Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public SnailfishNumber(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public SnailfishNumber(SnailfishNumber X, int y)
        {
            this.X = X;
            this.y = y;
        }
        public SnailfishNumber(int x, SnailfishNumber Y)
        {
            this.x = x;
            this.Y = Y;
        }

        public override string ToString()
        {
            var xString = "";
            var yString = "";
            if (X != null) xString = $"[{X}";
            else xString = $"[{x}";
            if (Y != null) yString = $"{Y}]";
            else yString = $"{y}]";

            return $"{xString}, {yString}";
        }
    }
    public override string Second()
    {
        return result;
    }
}
