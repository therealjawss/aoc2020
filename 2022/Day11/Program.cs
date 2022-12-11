using ChristmasGifts;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

var d = new Day11();
//await d.GetInput(file: "test.txt", pattern: Environment.NewLine);
await d.GetInput();
//Console.WriteLine($"Part 1:{d.RunFirst()}");
//await d.PostFirstAnswer(); 
Console.WriteLine($"Part 2:{d.RunSecond()}");
//await Task.Delay(5000); 
await d.PostSecondAnswer();
public class Day11 : Christmas
{
    string result = "todo";
    Monkey[] monkeys;
    public Day11() : base("11", "2022") { }
    public override string First()
    {
        Init();
        for (int i = 0; i<20; i++)
        {
            for (int j = 0; j<monkeys.Count(); j++)
            {
                monkeys[j].ProcessItems();
            }
        }
        result = monkeys.OrderByDescending(x => x.InspectionCount).Take(2).Select(x => x.InspectionCount).Aggregate(1, (total, next) => total*next).ToString();
        return result;
    }

    private void Init()
    {
        var pattern = @"Monkey (\d+):\s+Starting items: (\d+(?:, \d+){0,10})\s+Operation: new = (\w+) ([-+*]) (\w+)\s+Test: divisible by (\d+)\s+If true: throw to monkey (\d+)\s+If false: throw to monkey (\d+)";

        var matches = new Regex(pattern).Matches(RawInput);
        if (matches.Count>0)
        {
            monkeys = matches.Select(match =>
             new Monkey(this, int.Parse(match.Groups[1].Value), match.Groups[2].Value, new Operation(match.Groups[3].Value, match.Groups[4].Value[0], match.Groups[5].Value), ulong.Parse(match.Groups[6].Value),
                    int.Parse(match.Groups[7].Value), int.Parse(match.Groups[8].Value)
            )).ToArray();
        }
    }

    public override string Second()
    {
        Init();
        for (int i = 0; i<10000; i++)
        {
            for (int j = 0; j<monkeys.Count(); j++)
            {
                monkeys[j].ProcessItems(calmDown:false);
            }
        }
        var topTwo = monkeys.OrderByDescending(x => x.InspectionCount).Take(2).ToArray();
        result = topTwo.Select(x => x.InspectionCount).Aggregate(1, (total, next) => total*next).ToString();
        decimal total = (decimal)topTwo[0].InspectionCount*(decimal)topTwo[1].InspectionCount; 
        result =total.ToString();
        return result;
    }
    private  void ThrowItemToMonkey(ulong worryLevel, int nextMonkey)
    {
      
        monkeys.First(x => x.index==nextMonkey).Items.Enqueue(worryLevel);
    }
    [DebuggerDisplay("{DebugDisplay}")]
    public record Monkey(Day11 day11, int index, string startItems,
        Operation operation, ulong divisibleBy, int truthMonkey, int falseMonkey)
    {
        public Queue<ulong> Items { get; set; } = new Queue<ulong>(startItems.Split(',').Select(x => ulong.Parse(x.Trim())));
        public int InspectionCount = 0;
        public void ProcessItems(bool calmDown=true)
        {
            while(Items.Count > 0)
            {
                var worryLevel = Items.Dequeue();
                worryLevel =  operation.apply(worryLevel);
                InspectionCount++; 
                worryLevel = calmDown ? applyCalmDown(worryLevel) : worryLevel;
                var divisor = day11.monkeys.Aggregate(1UL, (total, next) => total*next.divisibleBy);
               
                worryLevel%= divisor;
                var nextMonkey = worryLevel % divisibleBy == 0 ? truthMonkey : falseMonkey;
                day11.ThrowItemToMonkey(worryLevel, nextMonkey);
            }
        }

        private ulong applyCalmDown(ulong worryLevel)
        {
            return worryLevel/3;
        }
        public string DebugDisplay => $"{InspectionCount} with {Items.Aggregate("", (result, next) => $"{result}, {next}")}";
    }

    public record Operation(string firstOperand, char op, string secondOperand)
    {
        internal ulong apply(ulong worryLevel)
        {
            var f = firstOperand == "old" ? worryLevel : ulong.Parse(firstOperand);
            var s = secondOperand == "old"? worryLevel : ulong.Parse(secondOperand);

            return op switch
            {
                '*' => f*s,
                '+' => f+s,
                '-' => f-s,
                _ => throw new NotImplementedException()
            };
        }
    }
}
