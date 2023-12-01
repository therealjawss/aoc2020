// See https://aka.ms/new-console-template for more information
using ChristmasGifts;

Console.WriteLine("Hello, World!");
var d = new Day01();
await d.GetInput();

Console.WriteLine($"Part 1:{d.RunFirst()}");
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await d.PostFirstAnswer();
await d.PostSecondAnswer();



public class Day01 : Christmas
{
    public Day01() : base("1", "2023")
    {
    }

    public override string First()
    {
        var result = Input.Select(x => x.GetNumbersFromString()).Sum();
        return result.ToString();
    }

    public override string Second()
    {
        var result = Input.Select(x => x.GetNumbersFromString()).Sum();
        return result.ToString();
    }
}

public static class TextExtensions
{
    static string[] Digits = { "zero","one","two", "three","four","five","six","seven","eight","nine" };
    public static int GetNumbersFromString(this string text)
    {

        return int.Parse($"{text.FirstNumber()}{text.LastNumber()}");
    }
    public static int FirstNumber(this string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            if (int.TryParse($"{text[i]}", out int result))
            {
                return result;
            }

            else if (Digits.Any(d => text[i..].StartsWith(d, StringComparison.CurrentCultureIgnoreCase)))
            {
                var index = Digits.Select((str, idx) => new { String = str, Index = idx })
                    .FirstOrDefault(match => text[i..].ToLower().StartsWith(match.String))?.Index ?? -1;
                return index;
            }

        }
        return -1;
    }
    public static int LastNumber(this string text)
    {

        for (var i = text.Length - 1; i >= 0; i--)
        {
            if (int.TryParse($"{text[i]}", out int result))
            {
                return result;
            }
            else if (Digits.Any(d => text[i..].StartsWith(d, StringComparison.CurrentCultureIgnoreCase)))
            {
                var index = Digits.Select((str, idx) => new { String = str, Index = idx })
                    .FirstOrDefault(match => text[i..].ToLower().StartsWith(match.String))?.Index ?? -1;
                return index;
            }

        }
        return -1;
    }
}