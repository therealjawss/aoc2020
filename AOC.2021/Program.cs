Console.WriteLine("Advent of Code 2021!\n**************");
var input = await File.ReadAllTextAsync("input.txt");
var lines = input.Split("\n").Select(x=>Int32.Parse(x)).ToArray();

var ctr = 0;
var previous = -1;
foreach(var line in lines)
{
    if (line > previous && previous !=-1) ctr++;
    previous = line;
}

Console.WriteLine(ctr);
var previousWindow = -1;
ctr= 0;
for(int i = 1; i < lines.Length-1; i++)
{
    var current = lines[i - 1] + lines[i] + lines[i + 1];
    if (previousWindow != -1 && current > previousWindow)
        ctr++;
    previousWindow = current;
}

Console.WriteLine(ctr);
