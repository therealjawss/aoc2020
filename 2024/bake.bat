dotnet new console -o Day%1
copy cookie Day%1
copy batter Day%1.csproj
move Day%1.csproj Day%1\Day%1.csproj
echo > test.txt
move test.txt Day%1
@echo off
echo using ChristmasGifts; > f
echo var d = new Day%1(); >> f

echo if (Feature.Local) >> f
echo     await d.GetInput(file: "test.txt", pattern: Environment.NewLine); >> f
echo else >> f
echo     await d.GetInput(); >> f
echo Console.WriteLine($"Part 1:{d.RunFirst()}"); >> f
echo //await d.PostFirstAnswer(); >> f
echo Console.WriteLine($"Part 2:{d.RunSecond()}"); >> f
echo //await Task.Delay(5000); >> f
echo //await d.PostSecondAnswer(); >> f
echo public class Day%1 : Christmas >> f
echo { >> f
echo     string result = "todo"; >> f
echo     public Day%1() : base("%1", "%2") { } >> f
echo     public override string First() >> f
echo     { >> f
echo         return result; >> f
echo     } >> f
echo     public override string Second() >> f
echo     { >> f
echo         return result; >> f
echo     }  >> f
echo } >> f

move f Day%1\Program.cs
cd Day%1