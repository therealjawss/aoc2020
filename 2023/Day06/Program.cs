using ChristmasGifts;
var d = new Day06();
if (Feature.Local)
{
    await d.GetInput(file: "test.txt", pattern: Environment.NewLine);
}
else
{
    await d.GetInput();
}
Console.WriteLine($"Part 1:{d.RunFirst()}");
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer();
public static class Feature
{
    public static bool Local = false;
}
public class Day06 : Christmas
{
    string result = "todo";
    string nline = "\n";
    string rline = "\r\n";
    public string newLine => Feature.Local ? rline : nline;
    public Day06() : base("6", "2023") { }
    public override string First()
    {
        var lines = RawInput.Split(newLine, StringSplitOptions.RemoveEmptyEntries);

        var Time = lines[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1..].Select(x => int.Parse(x)).ToArray();
        var Distance = lines[1].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1..].Select(x => int.Parse(x)).ToArray();

        List<int> result = new List<int>();
        for (int i = 0; i < Time.Length; i++)
        {
            var t = Time[i];
            var d = Distance[i];
            var ctr = 1;
            while ((d - ctr) * ctr < d)
                ctr++;
            var times = 0;

            for (; ctr < t; ctr++)
            {
                if ((t - ctr) * ctr > d)
                {
                    times++;
                }
            }

            result.Add(times);
        }

        return result.Aggregate((x, y) => x * y).ToString();
    }
    public override string Second()
    {
        var lines = RawInput.Split(newLine, StringSplitOptions.RemoveEmptyEntries);

        var Time = long.Parse(string.Join("", lines[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1..]));

        var Distance = long.Parse(string.Join("", lines[1].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1..]));

        result = GetLeShit(Time, Distance).ToString();

        return result;

    }

    public static long GetLeShit(long Time, long Distance)
    {
        var minDex = BinarySearchMinDex(Time, Distance);
        var mid = Time / 2;
        return Time % 2 == 0
            ? (mid - minDex) * 2 + 1
            : (mid - minDex + 1) * 2;
    }
    public static long BinarySearchMinDex(long t, long d)
    {
        long left = 1;
        long right = t;
        long minTime = t;
        long Lindex =t/2;

        while (left < right)
        {
            long mid = left + (right - left) / 2;
            long distance = (t - mid) * mid;

            if (distance > d)
            {
                if (mid < minTime)
                {
                    minTime = mid;
                }

                right = mid;
            }
            else
            {
                if (left == mid)
                    left++;
                else
                    left = mid;
            }
        }

        return Lindex;
    }
}