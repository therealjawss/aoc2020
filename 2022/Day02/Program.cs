using ChristmasGifts;

var d = new Day2();
await d.GetInput();
//await d.GetInput(pattern: Environment.NewLine); 
//await d.PostFirstAnswer();
Console.WriteLine($"Part 1:{d.RunFirst()}");
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
await d.PostSecondAnswer(); 
public class Day2 : Christmas
{
    string result = "todo";
    public Day2() : base("2", "2022") { }
    public override string First()
    {
        return Input.Select(x => ParseScores(x)).Sum().ToString();
    }
    public override string Second()
    {
        return Input.Select(x => ParseScores2(x)).Sum().ToString();

    }

    public static long CalculateScore(string input)
    {
        return 0;
    }
    private static long ParseScores(string input)
    {
        var scores = input.Trim().Split(" ").Select(x => translate(x)).ToArray();
        return resultOf(scores[0], scores[1]);
    }

    public static long resultOf(long opponent
        , long me)
       => (opponent, me) switch
       {
           { opponent: var o, me: var m } when o == m => m + 3,
           { opponent: var o, me: var m } when (o + 2) % 3 == m => m,
           { opponent: var o, me: var m } when (o + 1) % 3 == m => m + 6,
           { opponent: var o, me: var m } when (o + 2) == m => m,
           { opponent: var o, me: var m } when (o + 1) == m => m + 6,
           _ => throw new Exception()
       };
    private static long ParseScores2(string input)
    {
        var scores = input.Trim().Split(" ").Select(x => translate(x)).ToArray();
        var res =  strategyOf(scores[0], scores[1]);
        return res;
    }
    public static long strategyOf(long opponent
    , long me)
   => (opponent, me) switch
   {
       { opponent: var o, me: var m } when o == m => 2 * o + 1,
       { opponent: var o, me: var m } when m == 1 => o-m,
       { opponent: var o, me: var m } when m == 2 => o+3,
       { opponent: var o, me: var m } when m == 3 => o+7,
       _ => opponent - me
   };

    private static long translate(string x) =>
            x switch
            {
                string move when move == "A" || move == "X" => 1,
                string move when move == "B" || move == "Y" => 2,
                string move when move == "C" || move == "Z" => 3,
                _ => throw new Exception("unexpected")

            };


    public static string getMove(string input)
    {
        var scores = input.Split(" ").ToArray();
        return "A Z";
    }
}
