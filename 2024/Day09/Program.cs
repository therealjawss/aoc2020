using ChristmasGifts;
var d = new Day09();
Feature.Local = false;
if (Feature.Local)
    await d.GetInput(file: "test.txt", pattern: Environment.NewLine);
else
    await d.GetInput();
Console.WriteLine($"Part 1:{d.RunFirst()}");
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 
public class Day09 : Christmas
{
    string result = "todo";
    public Day09() : base("9", "2024") { }
    public override string First()
    {
        var diskmap = Info.Parse(Input[0]).ToList();
        var compressed = diskmap.Compress();
        var checksum = compressed.Checksum();
        return checksum.ToString();
    }
    public override string Second()
    {
        var diskMap = Info.Parse(Input[0]).ToList();
        var compressed = diskMap.ChunkCompress();

        var checksum = compressed.Checksum();
        return checksum.ToString();
        // 9826752182521
    }
}
public class Info(int ID, int fileBlocks, int freeSpace)
{
    public int ID { get; } = ID;
    public int FileBlocks { get; set; } = fileBlocks;
    public int FreeSpace { get; set; } = freeSpace;

    public static IEnumerable<Info> Parse(string input)
    {

        var result = Enumerable.Range(0, input.Length / 2)
                                .Select(i => new Info(i, int.Parse(input[2 * i].ToString()), int.Parse(input[2 * i + 1].ToString())))
                                .ToList();
        if (input.Length % 2 != 0)
        {
            result.Add(new Info(result.Count, int.Parse(input[^1].ToString()), 0));
        }

        return result;
    }
}


public static class Extensions
{
    public static ulong Checksum(this List<int> input)
    {
        ulong res = 0;
        for (int i = 0; i < (int)input.Count; i++)
        {
            res += ulong.Parse($"{input[i]}") * (ulong)i;


        }
        return res;
    }
    public static List<int> Compress(this List<Info> list)
    {
        var result = new List<int>();
        var spaces = list.Select(x => x.FreeSpace).Sum();
        var fillers = list.ToList();

        var filler = list.Last();
        do
        {
            var item = list.First();

            var id = item.ID;
            for (int i = 0; i < item.FileBlocks;)
            {
                result.Add(id);
                item.FileBlocks--;
            }
            for (int i = 0; i < item.FreeSpace && filler != null && filler.FileBlocks != 0;)
            {
                result.Add(filler.ID);
                filler.FileBlocks--;
                if (filler.FileBlocks == 0)
                {
                    list.Remove(filler);
                    filler = list.LastOrDefault();
                }
                item.FreeSpace--;
            }
            if (item.FreeSpace == 0 && item.FileBlocks == 0)
            {
                list.Remove(item);
            }
            else if (list.All(x => x.FileBlocks == 0))
            {
                break;
            }

        } while (list.Count != 0);

        return result;

    }

    public static List<int> ChunkCompress(this List<Info> list)
    {
        var result = new List<int>();
        var spaces = list.Select(x => x.FreeSpace).Sum();
        var fillers = list.ToList();
        int fillerIndex = list.Count - 1;

        while (fillerIndex > 0)
        {
            var filler = list[fillerIndex];
            var idx = list[0..fillerIndex].FindIndex(x => x.FreeSpace >= filler.FileBlocks);
            if (idx != -1 && idx < fillerIndex)
            {
                list[fillerIndex - 1].FreeSpace += filler.FileBlocks + filler.FreeSpace;
                list.Remove(filler);
                var item = list[idx];
                item.FreeSpace -= filler.FileBlocks;
                filler.FreeSpace = item.FreeSpace;
                item.FreeSpace = 0;
                list.Insert(idx + 1, filler);
            }
            else
            {
                fillerIndex--;
            }
        }
        foreach (var item in list)
        {
            var id = item.ID;
            for (int i = 0; i < item.FileBlocks; i++)
            {
                result.Add(id);
            }
            for (int i = 0; i < item.FreeSpace; i++)
            {
                result.Add(0);
            }
        }

        return result;
    }
    public static void Print(this List<Info> list)
    {
        foreach (var item in list)
        {
            var id = item.ID;
            for (int i = 0; i < item.FileBlocks; i++)
            {
                Console.Write(id);
            }
            for (int i = 0; i < item.FreeSpace; i++)
            {
                Console.Write(".");
            }
        };
        Console.WriteLine();
    }
}
