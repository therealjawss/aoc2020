using System.Text.RegularExpressions;

internal class Board
{
    public HashSet<int> Numbers = new HashSet<int>();
    List<int>[] Rows = new List<int>[5];
    List<int>[] Columns = new List<int> [5];

    public Board(string input)
    {
        Numbers = new Regex(@"\d+").Matches(input)
            .Select(x => int.Parse(x.Value))
            .ToHashSet();

        var lines = input.Split("\n");
        Rows = lines.Select(x => x.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => int.Parse(x)).ToList()).ToArray();

        Columns = Enumerable.Range(0, 5).Select(x => Rows.ToArray().Select(r => r[x]).ToList()).ToArray();
    }

    internal void Mark(int number)
    {
        Numbers.Remove(number);
        Rows.ToList().ForEach(x=>x.Remove(number));
        Columns.ToList().ForEach(x=>x.Remove(number));
    }
    public bool HasWon => Rows.Any(x => x.Count==0) || Columns.Any(x => x.Count==0);
    public override string ToString()
    {
        return $"{HasWon}";
    }
}