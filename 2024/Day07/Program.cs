using ChristmasGifts;
using static Day07;
var d = new Day07();
Feature.Local = false;
if (Feature.Local)
    await d.GetInput(file: "test.txt", pattern: Environment.NewLine);
else
    await d.GetInput();
Console.WriteLine($"Part 1:{d.RunFirst()}");
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 2:{d.RunSecond()}");

d.Print();
//await Task.Delay(5000); 
//await d.PostSecondAnswer(); 
public class Day07 : Christmas
{
    string result = "todo";
    public Day07() : base("7", "2024") { }
    public void Print()
    {
        var equations = Input.Select(x =>
        {
            var parts = x.Split(":", StringSplitOptions.TrimEntries);
            return new Equation(ulong.Parse(parts[0]), new Stack<ulong>(parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(ulong.Parse).Reverse()));
        });
        foreach (var equation in equations)
        {
            equation.Print();
        }
    }
    public override string First()
    {
        var equations = Input.Select(x =>
        {
            var parts = x.Split(":", StringSplitOptions.TrimEntries);
            return new Equation(ulong.Parse(parts[0]), new Stack<ulong>(parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(ulong.Parse).Reverse()));
        });

        return result = equations.Where(x => x.IsValid()).Sum(x => (decimal)x.Result).ToString();
    }
    public override string Second()
    {
        var equations = Input.Select(x =>
        {

            var parts = x.Split(":", StringSplitOptions.TrimEntries);
            return new Equation(ulong.Parse(parts[0]), new Stack<ulong>(parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(ulong.Parse).Reverse()));
        });

        return equations.Where(x => x.IsValid(withConcat: true)).Sum(x => (decimal)x.Result).ToString();

    }

    public record class Equation(ulong Result, Stack<ulong> operands)
    {
        public bool IsValid(bool withConcat = false)
        {

            var x = operands.Pop();
            var y = operands.Pop();
            if (operands.Count == 0)
                if ((x + y == Result) || (x * y == Result))
                    return true;
                else if (withConcat)
                    return ulong.Parse($"{x}{y}") == Result;
                else
                    return false;
            else
            {

                // addin
                var addStack = new Stack<ulong>(operands.Reverse());
                addStack.Push(x + y);
                var addEquation = new Equation(Result, addStack);
                // multin
                var multStack = new Stack<ulong>(operands.Reverse());
                multStack.Push(x * y);
                var multEquation = new Equation(Result, multStack);
                // concatin
                var concatStack = new Stack<ulong>(operands.Reverse());
                concatStack.Push(ulong.Parse($"{x}{y}"));
                var concatEquation = new Equation(Result, concatStack);
                return (addEquation.IsValid(withConcat) || multEquation.IsValid(withConcat) || (withConcat && concatEquation.IsValid(withConcat)));

            }
        }
        public override string ToString()
        {
            return $"{Result}: {string.Join(" ", operands.Reverse())}";
        }
    }
}

public static class Extensions
{
    public static void Print(this Equation equation, bool withConcat = false)
    {
        ConsoleColor color = equation.IsValid(withConcat) ? ConsoleColor.Green : ConsoleColor.Red;

        Console.ForegroundColor = color;
        Console.WriteLine(equation);
        Console.ResetColor();
    }
}
