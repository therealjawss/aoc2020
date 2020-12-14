using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
			Console.WriteLine("Answer should be " + day.Level2(day.Input));
		}

		public override string Level1(string[] input)
		{
			var c = new Computer(input);
			c.Run();
			
			return c.Sum().ToString();
		}

		public override string Level2(string[] input)
		{
			var result = "";

			return "";
		}

	}

	public class Computer
	{
		private string[] input;
		private List<Instruction> Instructions = new();
		public Computer(string[] input)
		{
			this.input = input;

			foreach (var i in input)
			{
				var pattern = @"mask = (.*)";
				var mem = @"mem\[(\d+)\]\s=\s(\d+)";
				var r = new Regex(pattern).Match(i);
				if (r.Success)
				{
					Instructions.Add(new Instruction(Ins.mask, r.Groups[1].Value, 0, 0));

				}

				r = new Regex(mem).Match(i);
				if (r.Success)
					Instructions.Add(new Instruction(Ins.mem, "", Convert.ToInt32(r.Groups[1].Value), Convert.ToInt64(r.Groups[2].Value)));
			}
		}

		public Instruction Mask { get; set; }
		public Dictionary<int, long> Memory = new();
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

		public void Process(Instruction instruction)
		{
			try
			{

			var val = Convert.ToInt64(instruction.val.ToBinaryString().ApplyMask(Mask.mask), 2);
			if (Memory.ContainsKey(instruction.idx))
			{
				Memory[instruction.idx] = val;
			}
			else
			{

				Memory.Add(instruction.idx, val);
			}
			}catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		internal long Sum()
		{
			long sum = 0;
			foreach (var item in Memory) {
				sum += item.Value;
			}
			return sum;
		}
	}
	public record Instruction(Ins ins, string mask, int idx, long val);

	public enum Ins { mask, mem }
	public static class IntExtensions
	{
		public static long[] ToBinary(this long number)
		{
			Stack<long> a = new();
			for (int i = 0; number > 0; i++)
			{
				a.Push(number % 2);
				number = number / 2;
			}

			return a.ToArray();
		}
		public static string ToBinaryString(this long number, int len = 36)
		{
			var bin = number.ToBinary();
			int padding = len - bin.Length;
			string a = "";
			for (int i = 0; i < padding; i++)
			{
				a += 0;
			}
			for (int i = 0; i < bin.Length; i++)
			{
				a += bin[i];
			}
			return a;
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
	}
}