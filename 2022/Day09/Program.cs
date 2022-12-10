using ChristmasGifts;
using System.Diagnostics;
using System.Text.RegularExpressions;

var d = new Day09();
await d.GetInput();
//await d.GetInput("test.txt");
//await d.GetInput(pattern: Environment.NewLine); 
Console.WriteLine($"Part 1:{d.RunFirst()}");
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
await d.PostSecondAnswer(); 
public class Day09 : Christmas
{
    public RopeSegment[] _ropeSegments;
    string result = "todo";
    public Day09() : base("9", "2022")
    {
    }

    public override string First()
    {
        init1();
        foreach (var item in Input)
        {
            ParseInstructions(item.Trim());
        }
        _ropeSegments[0].TailLocations.Add((0, 0));
        result = _ropeSegments[0].TailLocations.Count().ToString();
        return result;
    }

    private void init1()
    {
        _ropeSegments= new[] { new RopeSegment(new Part(0, 0,"head"), new Part(0, 0, "tail")) };
    }

    public void init2()
    {
        var parts = new[] { new Part(0, 0, "head"), new Part(0, 0, "1"), new Part(0, 0,"2"), new Part(0, 0, "3"), new Part(0, 0, "4"), new Part(0, 0,"5"), new Part(0, 0, "6"), new Part(0, 0, "7"), new Part(0, 0,"8"), new Part(0, 0, "9"), new Part(0, 0, "10") };
        var segments = new List<RopeSegment>();
        for (int i = 0; i<parts.Length-1; i++)
        {
            segments.Add(new RopeSegment(parts[i], parts[i+1]));
        }
        _ropeSegments =segments.ToArray();
    }
    private void ParseInstructions(string v)
    {
        var pattern = @"(\w) (\d+)";
        var match = new Regex(pattern).Match(v);
        if (match.Success)
        {
            var iterations = int.Parse(match.Groups[2].Value);
            Direction d = (match.Groups[1].Value) switch
            {
                "U" => Up,
                "D" => Down,
                "L" => Left,
                "R" => Right,
                _ => throw new ArgumentException()
            };
            for (int i = 0; i<iterations; i++)
            {
                _ropeSegments[0].MoveHead(d);
            }
        }
    }

    public override string Second()
    {
        init2();
        foreach (var item in Input)
        {
            ParseInstructions(item.Trim());
        }

        result = _ropeSegments[9].TailLocations.Count().ToString();
        return result;
    }

    [DebuggerDisplay("{Name}: {x}:{y}")]
    public record Part
    {
        public RopeSegment tailOwner { get; set; }
        public string Name { get; set; }
        public Part(int x, int y, string name)
        {
            this.x= x;
            this.y= y;
            Name = name;
        }
        public int x { get; set; }
        public int y { get; set; }
        public double Distance(Part part)
        {
            var xdist = Math.Abs(part.x-x);
            var ydist = Math.Abs(part.y-y);
            return xdist>ydist ? xdist : ydist;
        }

        internal void MoveTo(Part head)
        {
            var xdist = head.x - x;
            var ydist = head.y - y;
            if (xdist == 0 ^ ydist == 0)
            {
                if (xdist==0)
                {
                    var mult = ydist/Math.Abs(ydist);
                    y=y + mult;

                }
                else
                {
                    var mult = xdist/Math.Abs(xdist);

                    x=x+mult;
                }

            }
            else
            {
                var xmult = xdist/Math.Abs(xdist);
                var ymult = ydist/Math.Abs(ydist);
                if (Math.Abs(xdist) == 1)
                {
                    x=x+1*xmult;
                    y=y+1*ymult;
                }
                else
                {
                    x=x+1*xmult;
                    y=y+1*ymult;
                }
            }
            if (tailOwner!=null) tailOwner.MoveAsHeadTo(this);
        }
    }
    [DebuggerDisplay("SEGMENT:{Head} {Tail}")]
    public class RopeSegment
    {

        public HashSet<(int, int)> TailLocations = new HashSet<(int, int)>() { (0, 0) };
        public RopeSegment(Part head, Part tail)
        {
            Head=head;
            Tail=tail;
            head.tailOwner =this;
        }

        public Part Head { get; set; }
        public Part Tail { get; set; }
        internal void MoveHead(Direction d)
        {
            Head.x+=d.x;
            Head.y+=d.y;
            CatchUpTail();
        }
        internal void MoveAsHeadTo(Part d)
        {
            Head.x =d.x;
            Head.y =d.y;
            CatchUpTail();
            TailLocations.Add((Head.x, Head.y));

        }
        private void CatchUpTail()
        {
            if (Head.Distance(Tail)>1)
            {
                Tail.MoveTo(Head);

            }
        }
    }

    internal record Direction(int x, int y);
    static Direction Up = new Direction(0, 1);
    static Direction Down = new Direction(0, -1);
    static Direction Left = new Direction(-1, 0);
    static Direction Right = new Direction(1, 0);
}
