using ChristmasGifts;
Feature.Local = true;
var d = new Day13();
if (Feature.Local)
    await d.GetInput(file: "test.txt", pattern: Environment.NewLine);
else
    await d.GetInput();
Console.WriteLine($"Part 1:{d.RunFirst()}");
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 
public class Day13 : Christmas
{
    string result = "todo";
    public Day13() : base("13", "2023") { }
    string NL => Feature.Local ? "\r\n" : "\n";
    public override string First()
    {
        var parts = RawInput.Split(NL + NL, StringSplitOptions.RemoveEmptyEntries);
        var notes = parts.Select(p => CountFor(p)).ToList();

        result = (notes.Where(n => n.dir == 'L').Select(n => n.number).Sum() + 100 * notes.Where(n => n.dir == 'A').Select(n => n.number).Sum()) + "";
        return result;
    }
    public override string Second()
    {
        var parts = RawInput.Split(NL + NL, StringSplitOptions.RemoveEmptyEntries);
        var notes = parts.Select(p => CountFor(p, withSmudge: true)).ToList();

        result = (notes.Where(n => n.dir == 'L').Select(n => n.number).Sum() + 100 * notes.Where(n => n.dir == 'A').Select(n => n.number).Sum()) + "";
        return result;
    }

    private Note CountFor(string part, bool withSmudge = false)
    {
        var lines = part.Split(NL, StringSplitOptions.RemoveEmptyEntries);
        for (int mid = 1; ; mid++)
        {
            if (mid < lines[0].Length && lines.All(line => line[mid] == line[mid - 1]))
                if (isMirrorFromMid(isLeft: true, mid - 1, lines, withSmudge))
                    return new Note('L', mid);
            if (mid < lines.Length && lines[mid] == lines[mid - 1])
                if (isMirrorFromMid(isLeft: false, mid - 1, lines, withSmudge))
                    return new Note('A', mid);
        }
    }

    private bool isMirrorFromMid(bool isLeft, int mid, string[] lines, bool withSmudge)
    {
        int ls = 0;
        int rs = 0;
        for (int i = mid; i >= 0; i--)
        {
            if (isLeft && mid + 1 + (mid - i) < lines[0].Length)
                if (lines.Any(line => !CheckSame(mid, line, i)))
                {
                    if (!withSmudge)
                        return false;
                    else ls++;
                }
            if (!isLeft && mid + 1 + (mid - i) < lines.Length)
                if (lines[mid + 1 + (mid - i)] != lines[i])
                {
                    if (!withSmudge)
                        return false;
                    else
                        rs++;
                }
        }
        return isLeft ? ls <=1 : rs <=1;
    }

    private static bool CheckSame(int mid, string line, int i)
    {
        return line[mid + 1 + (mid - i)] == line[i];
    }

    public record Note(char dir, int number);


}
