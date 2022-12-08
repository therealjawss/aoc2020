using ChristmasGifts;
var d = new Day08();
//await d.GetInput(file:"test.txt"); 
await d.GetInput();
Console.WriteLine($"Part 1:{d.RunFirst()}");
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer();
public class Day08 : Christmas
{
    string result = "todo";
    public Day08() : base("8", "2022")
    {

    }
    short[][] trees;
    Dictionary<(int, int), bool> VisibleTrees = new Dictionary<(int, int), bool>();

    Dictionary<(int, int), long> Scores = new Dictionary<(int, int), long>();
    public override void Setup()
    {
        trees = Input.Where(x => x.Trim().Length>0).Select(x => x.Trim().ToCharArray().Select(tree => Int16.Parse(""+tree)).ToArray()).ToArray();
    }
    public override string First()
    {
        for (int i = 1; i<trees.Length-1; i++)
        {
            for (int j = 1; j< trees[i].Length-1; j++)
            {
                bool isVisible = trees[0..i].All(x => x[j] < trees[i][j])
                    || trees[(i+1)..(trees.Length)].All(x => x[j] < trees[i][j])
                    || trees[i][0..(j)].All(x => x<trees[i][j])
                    || trees[i][(j+1)..(trees[i].Length)].All(x => x < trees[i][j]);
                VisibleTrees.Add((i, j), isVisible);
            }
        }
        result = (VisibleTrees.Count(x => x.Value == true) + (Input.Length * 2) + (Input[0].Trim().Length-2)*2).ToString();
        return result;
    }
    public override string Second()
    {
        for (int i = 1; i<trees.Length-1; i++)
        {
            for (int j = 1; j< trees[i].Length-1; j++)
            {
                var l = GetScoreForDirection(trees[i][j], new Coord( i, j-1), Direction.Left);
                var r = GetScoreForDirection(trees[i][j], new Coord(i, j+1), Direction.Right);
                var t = GetScoreForDirection(trees[i][j], new Coord( i-1, j), Direction.Top);
                var b = GetScoreForDirection(trees[i][j], new Coord(i+1, j), Direction.Bottom);
                Scores.Add((i, j), l*r*t*b);
            }
        }
        result = Scores.Max(x => x.Value).ToString();
        return result;
    }
    enum Direction
    {
        Left, Right, Top, Bottom
    };

    private record Coord(int x, int y)
    {
        public Coord Change(Direction direction) => direction switch
        {
            Direction.Left => new Coord(x, y-1),
            Direction.Right => new Coord(x, y+1),
            Direction.Top => new Coord(x-1, y),
            Direction.Bottom => new Coord(x+1, y)
        };
    }
    private long GetScoreForDirection(short height, Coord coord, Direction direction)
    {
        if (coord.x <0 || coord.y <0 || coord.x >= trees.Length || coord.y >= trees[coord.x].Length) return 0;
        else if (trees[coord.x][coord.y]< height) return 1 + GetScoreForDirection(height, coord.Change(direction), direction);
        else if (trees[coord.x][coord.y] >= height) return 1;
        else return 0;
    }
  
}

