using ChristmasGifts;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
var testMode = false;
var d = new Day06();
await d.GetInput();

//await d.GetInput(pattern: Environment.NewLine); 
//if (!testMode) await d.PostFirstAnswer();
Console.WriteLine($"Part 1:{d.RunFirst()}");
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//if (!testMode) await d.PostSecondAnswer(); 
public class Day06 : Christmas
{
    string result = "todo";
    
    public Day06() : base("6", "2022")
    {
    }

   

    public override string First()
    {
        result = Get(RawInput).ToString();
        return result;
    }

    public override string Second()
    {
        result = Get(RawInput, 14).ToString();

        return result;
    }

    public static int Get(string input, int size = 4) 
    {
        for(int i=0; i< input.Length - size; i++)
        {
            if (new HashSet<char>( input[i..(i+size)]).Count() == size) 
            return i+size;
        }
        return -1;
    }
}
