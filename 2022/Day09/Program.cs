using ChristmasGifts;
using System.Text.RegularExpressions;

var d = new Day09(); 
await d.GetInput(); 
//await d.GetInput("test.txt"); 
//await d.GetInput(pattern: Environment.NewLine); 
Console.WriteLine($"Part 1:{d.RunFirst()}"); 
await d.PostFirstAnswer(); 
Console.WriteLine($"Part 2:{d.RunSecond()}"); 
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 
public class Day09 : Christmas 
{
    public Rope _rope;
    string result = "todo"; 
    public Day09() : base("9", "2022") {
        _rope= new Rope(new Part(0, 0), new Part(0, 0));
    } 
    
    public override string First() 
    { 
        foreach(var item in Input)
        {
            ParseInstructions(item.Trim());
        }
        _rope.TailLocations.Add(new Part(0, 0));
        result = _rope.TailLocations.Count().ToString();
        return result; 
    }

    private void ParseInstructions(string v)
    {
        var pattern = @"(\w) (\d+)";
        var match = new Regex(pattern).Match(v);
        if ( match.Success )
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
            for(int i=0; i<iterations; i++)
            {
                _rope.MoveHead(d);
            }
        }
    }

    public override string Second() 
    { 
        return result; 
    }  

    public record Part(int x, int y)
    {
        public double Distance(Part part)
        {
            var xdist = Math.Abs(part.x-x);
            var ydist = Math.Abs(part.y-y);
            return xdist>ydist ? xdist : ydist;
        }

        internal Part MoveTo(Part head)
        {
            var xdist = head.x - x;
            var ydist = head.y - y;
            if (xdist == 0 ^ ydist == 0)
            {
                if (xdist==0)
                {
                    var mult = ydist/Math.Abs(ydist);
                    return new Part(x, y + mult * (Math.Abs(ydist)-1));

                } else { 
                    var mult = xdist/Math.Abs(xdist);

                  return new Part(x+mult*(Math.Abs(xdist)-1), y);
                } 
                    
            }
            else
            {
                var xmult = xdist/Math.Abs(xdist);
                var ymult = ydist/Math.Abs(ydist);
                if (Math.Abs(xdist) == 1)
                {
                    return new Part(x+Math.Abs(xdist)*xmult, y+Math.Abs(xdist)*ymult);
                }
                else
                {
                    return new Part(x+Math.Abs(ydist)*xmult, y+Math.Abs(ydist)*ymult);
                }
            }
        }
    }
    public class Rope   {

        public HashSet<Part> TailLocations = new HashSet<Part>();
        public Rope(Part head, Part tail)
        {
            Head=head;
            Tail=tail;
        }

        public Part Head { get; set; }
        public Part Tail { get; set; }
        internal void MoveHead(Direction d)
        {
            Head = new Part(Head.x +d.x, Head.y+d.y);
            CatchUpTail();
        }

        private void CatchUpTail()
        {
            if (Head.Distance(Tail)>1)
            {
                Tail = Tail.MoveTo(Head);
                TailLocations.Add(Tail);
            }
        }
    }

    internal record Direction(int x, int y);
    static Direction Up = new Direction(0, 1);
    static Direction Down = new Direction(0,-1);
    static Direction Left = new Direction(-1, 0);
    static Direction Right = new Direction(1, 0);
} 
