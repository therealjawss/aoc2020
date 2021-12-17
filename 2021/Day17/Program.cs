using ChristmasGifts;

var d = new Day17();
await d.GetInput(file: "test.txt", pattern: Environment.NewLine);
//await d.GetInput(pattern: Environment.NewLine); 
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 1:{d.RunFirst()}");
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 

public record Boundaries(int start, int end)
{
    public bool Contains(int x)
    {
        if (start < 0 && end < 0 && x < 0)
            return Math.Abs(x) >= Math.Abs(start) && Math.Abs(x) <= Math.Abs(end);
        return x >= start && x <= end;

    }
}

public class Day17 : Christmas
{
    string result = "todo";
    public Day17() : base("17", "2021") { }

    //Boundaries X = new Boundaries(20, 30);
    //Boundaries Y = new Boundaries(-5, -10);

    Boundaries X = new Boundaries(195, 238);
    Boundaries Y = new Boundaries(-67, -93);
    public override string First()
    {
        var startX = X.end;
        var startY = Math.Abs(Y.end);
        var maxHeight = 0;
        var ctr = 0;

        for (int i = startX; i > 0; i--)
        {
            for (int j = startY; j >= Y.end; j--)
            {
                var height = 0;
                var current = new Position(0, 0);
                var next = (v: new Velocity(0, 0), p: new Position(0, 0));
                var Velocity = new Velocity(i, j);

                while (next.p.x <= X.end && next.p.y >= Y.end)
                {

                    height = current.y > height ? current.y : height;
                    next = Step(Velocity, current);
                    if (Y.Contains(current.y) && X.Contains(current.x))
                    {
                        ctr++;
                        if (height > maxHeight) maxHeight = height;
                        break;
                    }
                    current = next.p;
                    Velocity = next.v;

                }
            }
        }
        result = maxHeight.ToString();
        return result;
    }

    public record Velocity(int X, int Y);
    public record Position(int x, int y);
    private (Velocity v, Position p) Step(Velocity v, Position p)
    {
        var newP = new Position(p.x + v.X, p.y + v.Y);
        var newV = new Velocity(v.X + (v.X > 0 ? -1 : (v.X < 0 ? 1 : 0)), v.Y - 1);
        return (newV, newP);
    }

    public override string Second()
    {
        var startX = X.end;
        var startY = Math.Abs(Y.end);
        var ctr = 0;

        for (int i = startX; i > 0; i--)
        {
            for (int j = startY; j >= Y.end; j--)
            {
                var height = 0;
                var current = new Position(0, 0);
                var next = (v: new Velocity(0, 0), p: new Position(0, 0));
                var Velocity = new Velocity(i, j);

                while (next.p.x <= X.end && next.p.y >= Y.end)
                {

                    height = current.y > height ? current.y : height;
                    next = Step(Velocity, current);
                    if (Y.Contains(current.y) && X.Contains(current.x))
                    {
                        ctr++;
                        break;
                    }
                    current = next.p;
                    Velocity = next.v;

                }
            }
        }
        result = ctr.ToString();
        return result;
    }
}
