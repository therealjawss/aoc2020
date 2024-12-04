using ChristmasGifts;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Transactions;
var d = new Day04();
if (false)
    await d.GetInput(file: "test.txt", pattern: Environment.NewLine);
else
    await d.GetInput();
Console.WriteLine($"Part 1:{d.RunFirst()}");
//await d.PostFirstAnswer();
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 
public class Day04 : Christmas
{
    string result = "todo";
    public Day04() : base("4", "2024") { }
    public override string First()
    {
        var words = Input.ToList();
        var width = Input[0].Length;

        for (int i = 0; i < Input.Length; i++)
        {
            words.Add(Input.Select(x => x[i]).Select(x => x.ToString()).Aggregate((x, y) => x + y));
        }
        // diagonal up
        for (int i = 3; i < Input.Length; i++)
        {
            var currWord = "";
            for (int j = 0, k = i; j < width && k >= 0; j++, k--)
            {
                currWord += words[k][j];
            }
            words.Add(currWord);
            if (i == Input.Length - 1)
            {
                var l = 1;
                for (; l < width; l++)
                {
                    currWord = "";
                    for (int j = l, k = i; k >= 0 && j < width; j++, k--)
                    {
                        currWord += words[k][j];
                    }
                    words.Add(currWord);
                }
            }
        }

        //diagonal down
        for (int i = Input.Length - 4; i >= 0; i--)
        {
            var currWord = "";
            for (int j = 0, k = i; j < width && k < Input.Length; j++, k++)
            {
                currWord += words[k][j];
            }
            words.Add(currWord);
            if (i == 0)
            {
                var l = 1;
                for (; l < width; l++)
                {
                    currWord = "";
                    for (int j = l, k = i; k < Input.Length && j < width; j++, k++)
                    {
                        currWord += words[k][j];
                    }
                    words.Add(currWord);
                }
            }
        }
        var pattern = @"SAMX";

        var matchCount = words.Sum(word => Regex.Matches(word, "SAMX").Count) + words.Sum(word => Regex.Matches(word, "XMAS").Count);
        result = matchCount.ToString();


        return result;
    }
    public override string Second()
    {
        char[][] words = Input.Select(x => x.ToCharArray()).ToArray();

        var count = 0;
        for (int i = 0; i < words.Length - 2; i++)
        {
            for (int j = 0; j < words[0].Length-2; j++)
            {
                count += XMAS(words, i, j);
            }
        }

        return count.ToString();
    }

    private int XMAS(char[][] words, int i, int j)
    {
        var count = 0;
        var TL = words[i][j];
        var TR = words[i][j + 2];
        var MM = words[i + 1][j + 1];
        var LL = words[i + 2][j];
        var LR = words[i + 2][j + 2];
        if (MM != 'A') return count;


        if (TL == LL  &&  TR == LR )
        {
            if (TL == 'M' && TR == 'S')
            {
                count++;
            }
            if (TL == 'S' && TR == 'M')
            {
                count++;
            }
        }
        if (TL == TR && LL == LR)
        {
            if (TL == 'M' && LL == 'S')
            {
                count++;
            }
            if (TL == 'S' && LL == 'M')
            {
                count++;
            }
        }

    
        return count;
    }
}
