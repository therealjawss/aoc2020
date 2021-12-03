using ChristmasGifts;

var d = new Day3();
await d.GetInput();

Console.WriteLine($"Part 1:{d.First()}");

Console.WriteLine($"Part 2:{d.Second()}");


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
