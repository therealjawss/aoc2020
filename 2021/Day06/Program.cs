using ChristmasGifts;

var d = new Day6();
//await d.GetInput(file: "test.txt", pattern: ",");
await d.GetInput(pattern: ",");
//await d.PostFirstAnswer();
Console.WriteLine($"Part 1:{d.RunFirst()}");
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000);
//await d.PostSecondAnswer();


public class Day6 : Christmas
{
    string result = "todo";
    public Day6() : base("6", "2021") { }

    public override string First()
    {
        var days = 80;
        var fishList = Input.Select(x => int.Parse(x)).ToList();

        for (int i = 0; i < days; i++)
        {
            var newfishList = fishList.Select(x => x == 0 ? 6 : fishList.Contains(x) ? x - 1 : x).ToList();
            var newFish = fishList.Count(x => x == 0);
            for (int j = 0; j < newFish; j++)
            {
                newfishList.Add(8);
            }
            fishList = newfishList;
        }

        return $"{fishList.Count()}";
    }
    public override string Second()
    {
        var days = 256;

        var fishList = Input.Select(x => short.Parse(x)).GroupBy(x => x).ToDictionary(g => g.Key, g => (ulong)g.Count());
        ulong newFish = 0;
        for (int i = 1; i <= days; i++)
        {
            var newList = new Dictionary<short, ulong>();
            if (newFish > 0) newList.Add(8, newFish);

            foreach (var fish in fishList)
            {
                var f = (short)(fish.Key - 1 < 0 ? 6 : fish.Key - 1);
                newList.TryGetValue(f, out ulong value);
                newList[f] = fish.Value + value;
            }

            newFish = (ulong)(!newList.ContainsKey(0) ? 0 : newList[0]);
            fishList = newList;
        }

        var result = fishList.Aggregate(0UL, (a, c) => a + c.Value);

        return $"{result}";
    }

}
