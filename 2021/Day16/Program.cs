using ChristmasGifts;
var d = new Day16();
await d.GetInput(file: "test.txt", pattern: Environment.NewLine);
//await d.GetInput(pattern: Environment.NewLine); 
//await d.PostFirstAnswer(); 
Console.Write($"Part 1:{d.RunFirst()}");
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 
public class Day16 : Christmas
{
    string result = "todo";

    public string ConvertHexToBin(string hex)
    {
        return String.Join(String.Empty, hex.Select(
            c => Convert.ToString(Convert.ToInt64(c.ToString(), 16), 2).PadLeft(4, '0')));
    }

    List<long> Versions = new();

    public Day16() : base("16", "2021") { }
    public override string First()
    {
        var binaries = Input.Select(x => x.Trim())
            .Select(x => ConvertHexToBin(x.ToString())).First();

        ParsePacket(binaries);
       
        result = Versions.Sum().ToString();
        return result;
    }
    public void Print(string s)
    {
        //Console.WriteLine(s); 
    }
    public (long number,int index) ParseLiteral(string toParse)
    {
        var numbers = "";
        var index = 0;
        while (toParse[index] != '0')
        {
            numbers += toParse[(index + 1)..(index + 5)];
            index += 5;
        }
        numbers += toParse[(index + 1)..(index + 5)];
        return (Convert.ToInt64(numbers,2),index + 5);
    }

    public int ParsePacket(string toParse)
    {
        Print(toParse);
        int i = 0;
        var packetVersion = Convert.ToInt64(toParse[i..(i + 3)], 2);
        Versions.Add(packetVersion);
        var typeId = Convert.ToInt64(toParse[(i + 3)..(i + 6)], 2);
        i += 6;
        if (typeId == 4)
        {
            var literalParser = ParseLiteral(toParse[i..]);
            i += literalParser.index;
        }
        else
        {
            var lengthType = Convert.ToInt64(toParse[i..(i + 1)]);
            Print(toParse[(i + 1)..(i + 2)]);
            i += 1;
            var valueFieldLength = lengthType == 0 ? 15 : 11;
            var fieldValue = Convert.ToInt64(toParse[i..(i + valueFieldLength)], 2);
  
            i += valueFieldLength;
            if (lengthType == 0)
            {
                var subpacketLength = 0;
                while (subpacketLength < fieldValue)
                {
                    var thisPacketLength = ParsePacket(toParse[(i+subpacketLength)..]);
                    subpacketLength += thisPacketLength;
                }
                i += subpacketLength;
            }
            else
            { 
                for(int x=0; x< fieldValue; x++)
                {
                    i += ParsePacket(toParse[i..]);
                }
            }
        }
        return i;
    }
    public override string Second()
    {
        return result;
    }
}
