using ChristmasGifts;

var d = new Day9();
await d.GetInput(file: "test.txt", pattern: "\n");
await d.GetInput(pattern: "\n");
//await d.PostFirstAnswer();
Console.WriteLine($"Part 1:{d.First()}");
Console.WriteLine($"Part 2:{d.Second()}");
//await Task.Delay(5000);
//await d.PostSecondAnswer();

/*
0 - 6 
1 - 1
2 - 5
3 - 5
4 - 4
5 - 5
6 - 
*/
public class Day9 : Christmas
{
    string result = "todo";

    public Day9() : base("9", "2021")
    {


    }
    public override string First()
    {
        var map = Input.Select(x => x.Trim().ToCharArray().Select(x => int.Parse(x+"")).ToArray()).ToArray();

        var risk = 0;
        for (int i = 0; i<map.Length; i++)
        {
            for (int j = 0; j<map[i].Count(); j++)
            {
                var point = map[i][j];
                var up = j > 0 ? map[i][j-1] : int.MaxValue;
                var down = j < map[i].Count()-1 ? map[i][j+1] : int.MaxValue;
                var left = i > 0 ? map[i-1][j] : int.MaxValue;
                var right = i < map.Count()-1 ? map[i+1][j] : int.MaxValue;
                var isLower = point < up && point < down && point < left && point < right;
                if (isLower)
                    risk += point < up && point < down && point < left && point < right ? point+1 : 0;
            }
        }
        result = risk.ToString();
        return result;
    }
    public override string Second()
    {
        return result;
    }
}
