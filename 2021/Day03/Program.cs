using ChristmasGifts;

var d = new Day3();
await d.GetInput();

Console.WriteLine($"Part 1:{d.First()}");
//await d.PostFirstAnswer();

Console.WriteLine($"Part 2:{d.Second()}");
//await d.PostSecondAnswer();


public class Day3 : Christmas
{
    public Day3() : base("3", "2021")
    {
    }

    public override string First()
    {
        var gamma = "";
        var epsilon = "";
        for (int i = 0; i < Input[0].Length; i++)
        {
            var toParse = Input.Select(x => x[i]);
            if (toParse.Count(x => x == '0') > toParse.Count(x => x == '1')) {
                gamma+="0";
                epsilon+="1";
            } else
            {
                gamma+="1";
                epsilon+="0";
            }
        }
        return (ToLong(gamma) * ToLong(epsilon)).ToString();
    }

    public override string Second()
    {
        var og = Input.Select(x => x).ToArray();
        for (int i = 0; i < og[0].Length|| og.Count()!=1; i++)
        {
            var toParse = og.Select(x => x[i]);
            if (toParse.Count(x => x == '0') > toParse.Count(x => x == '1'))
                og = og.Where(x => x[i] == '0').ToArray();
            else
                og = og.Where(x => x[i] == '1').ToArray();
        }

        var co2scrubber = Input.Select(x => x).ToArray();
        for (int i = 0; i < co2scrubber[0].Length && co2scrubber.Count()>1; i++)
        {
            var toParse = co2scrubber.Select(x => x[i]);
            if (toParse.Count(x => x == '0') > toParse.Count(x => x == '1'))
                co2scrubber = co2scrubber.Where(x => x[i] == '1').ToArray();
            else
                co2scrubber = co2scrubber.Where(x => x[i] == '0').ToArray();
        }
        return $"{ToLong(og.First()) * ToLong(co2scrubber.First())}";
    }

    public long ToLong(string binaryStr)
    {
        long result = 0;

        for (int i = 0; i < binaryStr.Length; i++)
        {
            if (binaryStr[i] == '1')
                result |= (1L << (binaryStr.Length - i -1));
        }
        return result;
    }

}
