using ChristmasGifts;
using System.Text.RegularExpressions;

var d = new Day07();
//await d.GetInput("test.txt");
await d.GetInput();
//await d.GetInput(pattern: Environment.NewLine); 
Console.WriteLine($"Part 1:{d.RunFirst()}");
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
await d.PostSecondAnswer();
public class Day07 : Christmas
{
    string result = "todo";
    public Day07() : base("7", "2022") { }

    public void init()
    {
        Input = Input.Select(x => x.Trim()).ToArray();
        do
        {
            ParseInput(Input[linePtr]);
        } while (linePtr < Input.Length);
    }
    public override string First()
    {
        init();
        result = directories.Where(x => x.Size <=100000).Aggregate(0UL, (a, c) => a + c.Size).ToString();

        return result;
    }
    public override string Second()
    {
        var freespace = 70000000UL - Root.Size;
        var needed = 30000000UL -freespace;
        result = directories.Where(x => x.Size > needed).Min(x => x.Size).ToString();
        return result;
    }
    public record Files(string name, ulong size);
    public record Directory(string name)
    {
        public Directory Parent { get; private set; }
        public ulong Size => Directories.Aggregate(0UL, (a, c) => a + c.Size)
            + Files.Aggregate(0UL, (a, c) => a + c.size);
        public List<Directory> Directories { get; set; } = new List<Directory>();
        public List<Files> Files { get; set; } = new List<Files>();
        public void AddDirectory(Directory d)
        {
            Directories.Add(d);
            d.Parent = this;
        }

        internal void AddFiles(Files f)
        {
            Files.Add(f);
        }
    }
    public Directory Root = new Directory("/");
    private int linePtr = 0;
    private Directory dirPtr;
    private HashSet<Directory> directories = new HashSet<Directory>();
    private Directory CreateDirectory(string name)
    {
        var d = new Directory(name);
        directories.Add(d);
        return d;
    }
    private void ParseInput(string x)
    {
        var rootPattern = @"\$ cd ([\/])";
        var upPattern = @"\$ cd ..";
        var chdirPattern = @"\$ cd ([\w]+)";
        var lsPattern = @"\$ ls";
        if (new Regex(rootPattern).Match(x).Success)
        {
            var parsed = new Regex(rootPattern).Match(x);

            Root = CreateDirectory(parsed.Groups[1].Value);
            dirPtr = Root;
            linePtr++;
        }
        else if (new Regex(chdirPattern).Match(x).Success)
        {
            var parsed = new Regex(chdirPattern).Match(x);

            var nextDir = dirPtr.Directories.First(x => x.name == parsed.Groups[1].Value);
            if (nextDir == null)
            {
                nextDir =  CreateDirectory(parsed.Groups[1].Value);
                dirPtr.AddDirectory(nextDir);
            }
            dirPtr = nextDir;
            linePtr++;
        }
        else if (new Regex(lsPattern).Match(x).Success)
        {
            linePtr++;
            readContent();
        }
        else if (new Regex(upPattern).Match(x).Success)
        {
            dirPtr = dirPtr.Parent;
            linePtr++;
        }
        else
        {
            readContent();
        }

    }

    private void readContent()
    {
        var dirPattern = @"dir (\w+)";
        var filePattern = @"(\d+) ([\w]+([\.][\w]+)?)";
        if (new Regex(dirPattern).Match(Input[linePtr].Trim()).Success)
        {
            var parsed = new Regex(dirPattern).Match(Input[linePtr]);
            var d = CreateDirectory(parsed.Groups[1].Value);
            dirPtr.AddDirectory(d);
            linePtr++;
        }
        else if (new Regex(filePattern).Match(Input[linePtr]).Success)
        {
            var parsed = new Regex(filePattern).Match(Input[linePtr].Trim());
            var f = new Files(parsed.Groups[2].Value, ulong.Parse(parsed.Groups[1].Value));
            dirPtr.AddFiles(f);
            linePtr++;
        }
    }


}
