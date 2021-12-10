dotnet new console -o Day%1
copy cookie Day%1
copy batter Day%1.csproj
@echo off
echo using ChristmasGifts; > f
echo var d = new Day%1(); >> f
echo //await d.GetInput(file: "test.txt", pattern:Environment.NewLine); >> f
echo await d.GetInput(pattern: "\n"); >> f
echo //await d.PostFirstAnswer(); >> f
echo Console.WriteLine($"Part 1:{d.First()}"); >> f
echo Console.WriteLine($"Part 2:{d.Second()}"); >> f
echo //await Task.Delay(5000); >> f
echo //await d.PostSecondAnswer(); >> f
echo public class Day%1 : Christmas >> f
echo { >> f
echo     string result = "todo"; >> f
echo     public Day%1() : base("10", "2021") { } >> f
echo     public override string First() >> f
echo     { >> f
echo         return result; >> f
echo     } >> f
echo     public override string Second() >> f
echo     { >> f
echo         return result; >> f
echo     }  >> f
echo } >> f