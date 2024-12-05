using ChristmasGifts;
var d = new Day05();
Feature.Local = false;
if (Feature.Local)
    await d.GetInput(file: "test.txt", pattern: Environment.NewLine);
else
    await d.GetInput();
Console.WriteLine($"Part 1:{d.RunFirst()}");
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
await d.PostSecondAnswer();

public class Day05 : Christmas
{
    string result = "todo";
    public Day05() : base("5", "2024") { }
    List<List<int>> incorrect = new();
    List<Order> orders = new();
    public override string First()
    {
        var parts = RawInput.Split($"{Feature.NewLine}{Feature.NewLine}");
        orders = parts[0].Split($"{Feature.NewLine}").Select(x => x.Trim().Split("|")).Select(x => new Order(int.Parse(x[0]), int.Parse(x[1]))).ToList();

        var ok = new List<List<int>>();
        var updates = parts[1].Split($"{Feature.NewLine}", StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim().Split(","))
            .Select(pages => pages.Select(x => int.Parse(x)).ToList());

        foreach (var update in updates)
        {
            var isOk = true;
            foreach (var order in orders)
            {
                if (update.Contains(order.x) && update.Contains(order.y) && update.IndexOf(order.x) > update.IndexOf(order.y))
                {
                    isOk = false;
                    incorrect.Add(update);
                    break;
                }
            }
            if (isOk)
                ok.Add(update);
        }
        var result = 0;
        foreach (var item in ok)
        {
            result += item[item.Count / 2];
        }

        return result.ToString();
    }
    public record Order(int x, int y);

    public override string Second()
    {
        var sorted = new List<int[]>();
        foreach (var update in incorrect)
        {
            sorted.Add(fixOrder(update.ToArray()));
        }

        var result = 0;
        foreach (var item in sorted)
        {
            result += item[item.Length / 2];
        }

        return result.ToString();
    }

    private int[] fixOrder(int[] update)
    {
        var relevant = orders.Where(x => update.Contains(x.x) && update.Contains(x.y)).ToList();

        for (int i = 0; i < orders.Count; i++)
        {
            var order = orders[i];
            if (!update.Contains(order.x) || !update.Contains(order.y)) continue;
            if (update.ToList().IndexOf(order.x) > update.ToList().IndexOf(order.y))
            {
                var temp = update[update.ToList().IndexOf(order.x)];
                update[update.ToList().IndexOf(order.x)] = update[update.ToList().IndexOf(order.y)];
                update[update.ToList().IndexOf(order.y)] = temp;
                i = 0;
            }
        }

        return update;
    }
}
