using ChristmasGifts;
using System.Diagnostics;

var d = new Day13();
await d.GetInput(file: "test.txt", pattern: Environment.NewLine);
//await d.GetInput(pattern: Environment.NewLine); 
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 1:{d.RunFirst()}");
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 
public class Day13 : Christmas
{
    string result = "todo";
    public Day13() : base("13", "2022") { }
    public override string First()
    {
        var pairs = RawInput.Split(Environment.NewLine+Environment.NewLine).Select(x => x.Split(Environment.NewLine)).Select(y => new Pair(new Packet(y[0]), new Packet(y[1]))).ToArray();
        var sum = 0;
        for (int i = 0; i<pairs.Length; i++)
        {
            if (pairs[i].InOrder()) sum+=i;
        }
        result = sum.ToString();

        return result;
    }
    public record Packet(string stringContent)
    {
        internal int NextInList()
        {
            throw new NotImplementedException();
        }
    }
    [DebuggerDisplay("{Left} vs {Right}")]
    public record Pair
    {
        public Pair(Packet left, Packet right)
        {
            Left = left;
            Right = right;
        }
        public Pair(string content)
        {
            content.Trim().Split();
        }
        Packet Left { get; set; }
        Packet Right { get; set; }

        public bool InOrder()
        {
            var lInside = Left.stringContent.GetInsides();
            var rInside = Right.stringContent.GetInsides();
            if (lInside.IsNotNested() && rInside.IsNotNested())
            {
                var left = lInside.Split(',', StringSplitOptions.RemoveEmptyEntries);
                var right = rInside.Split(',', StringSplitOptions.RemoveEmptyEntries);
                if (left.Length == 0 && right.Length == 0||left.Length == 0)
                    return true;
                else if (right.Length == 0) return false;
            }
           
            //var idx = 0;
            //for (; idx<left.Length; idx++)
            //{
            //    if (idx>=right.Length) return false;
            //    var leftIsNumber = int.TryParse(left[idx], out int l);
            //    var rightIsNumber = int.TryParse(right[idx], out int r);
            //    if (leftIsNumber && rightIsNumber)
            //    {
            //        if (l != r)
            //            return l<r;
            //        else
            //            continue;
            //    }
            //    else if (leftIsNumber)
            //    {
            //        return new Pair(new Packet($"[{left[idx]}]"), new Packet(right[idx])).InOrder();
            //    }
            //    else if (rightIsNumber)
            //        return new Pair(new Packet(left[idx]), new Packet($"[{right[idx]}]")).InOrder();
            //}

            return true;
        }

    }
    public override string Second()
    {
        return result;
    }
}
public static class Day13Extensions
{
    public static string GetInsides(this string outside)
    {
        try
        {
            return outside[1..FindPartnerIndex(outside)];

        }
        catch (Exception)
        {

            throw;
        }
    }
    public static bool IsNotNested(this string pattern)
    {
        return !pattern.Contains('[') && !pattern.Contains(']');
    }
    public static int FindPartnerIndex(string outside)
    {
        int idx = 0;
        int ctr = 1;
        bool found = false;
        do
        {
            idx++;

                if (outside[idx] == ']')
                ctr--;
            else if (outside[idx]=='[')
                ctr++;
            if (ctr == 0) { found = true; }
        } while (!found);

        return idx;
    }
}