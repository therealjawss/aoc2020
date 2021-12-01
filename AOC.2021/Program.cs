// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
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