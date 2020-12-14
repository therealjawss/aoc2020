using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace AOC2020.Days
{

    public class Day14 : Christmas
    {
        public override int Day => 14;

        public static void Run()
        {
            var day = new Day14();
            day.GetInput();
            Console.WriteLine(day.Level1(day.Input));
            // day.PostL1Answer();
            Console.WriteLine("Answer should be " + day.Level2(day.Input));
            Thread.Sleep(2000);
            day.PostL2Answer();
        }
        public static void Run1()
        {
            var day = new Day14();
            day.GetInput();
            Console.WriteLine(day.Level1(day.Input));
        }
        public static void Run2()
        {
            var day = new Day14();
            day.GetInput();
            Console.WriteLine(day.Level2(day.Input));
        }

        public override string Level1(string[] input)
        {
            var c = new Computer(input);
            c.Run();

            return c.Sum().ToString();
        }

        public override string Level2(string[] input)
        {
            var c = new Computer(input);
            c.Run2();

            return c.Sum().ToString();
        }
    }

    public class Computer
    {
        private string[] input;
        private List<Instruction> Instructions = new();
        public Instruction Mask { get; set; }
        public Dictionary<ulong, ulong> Memory = new();
        public Computer(string[] input)
        {
            this.input = input;

            foreach (var i in input)
            {
                var pattern = @"mask = (.*)";
                var mem = @"^mem\[(\d+)\]\s=\s(\d+)$";
                var r = new Regex(pattern).Match(i);
                if (r.Success)
                {
                    Instructions.Add(new Instruction(Ins.mask, r.Groups[1].Value, 0, 0));
                }

                r = new Regex(mem).Match(i);
                if (r.Success)
                    Instructions.Add(new Instruction(Ins.mem, "", Convert.ToUInt64(r.Groups[1].Value), Convert.ToUInt64(r.Groups[2].Value)));
            }
        }

        public void Run()
        {
            foreach (Instruction instruction in Instructions)
            {
                if (instruction.ins == Ins.mask)
                {
                    Mask = instruction;
                }
                else
                {
                    Process(instruction);
                }
            }
        }
        public void Run2()
        {
            foreach (Instruction instruction in Instructions)
            {
                if (instruction.ins == Ins.mask)
                {
                    Mask = instruction;
                }
                else
                {
                    Process2(instruction);
                }
            }
        }

        public void Process(Instruction instruction)
        {
            var val = Convert.ToUInt64(instruction.val.ToBinaryString().ApplyMask(Mask.mask), 2);
            if (Memory.ContainsKey(instruction.idx))
            {
                Memory[instruction.idx] = val;
            }
            else
            {
                Memory.Add(instruction.idx, val);
            }
        }
        public void Process2(Instruction instruction)
        {
            var binString = instruction.idx.ToBinaryString();
            var result = binString.ApplyMask2(Mask.mask);
            var values = result.GetFloatingNumbers();

            foreach (var val in values)
            {
                if (Memory.ContainsKey(val))
                {
                    Memory[val] = instruction.val;
                }
                else
                {
                    Memory.Add(val, instruction.val);
                }
            }
        }
    internal ulong Sum()
    {
        ulong sum = 0;
        foreach (var item in Memory)
        {
            sum += item.Value;
        }
        return sum;
    }
}
public record Instruction(Ins ins, string mask, ulong idx, ulong val);

public enum Ins { mask, mem }
public static class Extensions
{
    public static ulong[] ToBinary(this ulong number)
    {
        Stack<ulong> a = new();
        for (int i = 0; number > 0; i++)
        {
            a.Push(number % 2);
            number = number / 2;
        }

        return a.ToArray();
    }
    public static string ToBinaryString(this ulong number, int len = 36)
    {
        var bin = number.ToBinary();
        string a = "";
        
        for (int i = 0; i < bin.Length; i++)
        {
            a += bin[i];
        }
        a.PadLeft(len, '0');        
        return a.PadLeft(len, '0');
    }

    public static string ApplyMask(this string binary, string mask)
    {
        string a = "";
        var m = mask.ToCharArray();
        var b = binary.ToCharArray();
        for (int i = 0; i < mask.Length; i++)
        {
            if (m[i] != 'X')
            {
                a += m[i];
            }
            else
            {
                a += b[i];
            }
        }

        return a;
    }
    public static string ApplyMask2(this string memory, string bitmask)
    {
        string a = "";
        var b = bitmask.ToCharArray();
        var m = memory.ToCharArray();
        for (int i = 0; i < bitmask.Length; i++)
        {
            if (b[i] == '0')
            {
                a += m[i];
            }
            else if (b[i] == '1')
            {
                a += '1';
            }
            else
            {
                a += 'X';
            }
        }

        return a;
    }
    public static IEnumerable<int> AllIndexesOf(this string str, string c)
    {
        int minIndex = str.IndexOf(c);
        while (minIndex != -1)
        {
            yield return minIndex;
            minIndex = str.IndexOf(c, minIndex + c.Length);
        }
    }

    public static ulong[] GetFloatingNumbers(this string bitmask)
    {
        List<string> l = new List<string>();

        var floatingIndices = bitmask.AllIndexesOf("X");
        var min = bitmask.Replace('X', '0');
        l.Add((min));
        foreach (var idx in floatingIndices)
        {
            var newList = new List<string>();
            foreach (var item in l)
            {
                var sb = new StringBuilder(item);
                sb[idx] = '0';
                newList.Add(sb.ToString());
                sb[idx] = '1';
                newList.Add(sb.ToString());
            }
            l = newList;
        }

        return l.Select(x => Convert.ToUInt64(x, 2)).ToArray();
    }

}
}