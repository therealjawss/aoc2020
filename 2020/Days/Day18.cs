using System;
using System.Collections.Generic;
using System.Threading;

namespace AOC2020.Days
{

	public class Day18 : Christmas
	{
		public override int Day => 18;

		public static void Run()
		{
			var d = new Day18();
			d.GetInput();
			Console.WriteLine(d.Level1(d.Input));
			Console.WriteLine(d.Level2(d.Input));
		}

		public override string Level1(string[] input)
		{
			long total = 0;
			int i = 0;
			foreach (var line in input)
			{
				i = 0;
				total += Compute(line.Replace(" ", ""), ref i);
			}
			return total.ToString();
		}
		public long GetOperand(string input, ref int i)
		{
			long num1 = 0;

			if (input[i] == '(')
			{
				i++;
				num1 = Compute(input, ref i);
			}
			else
				num1 = long.Parse(input[i].ToString());
			return num1;
		}

		public long Compute(string input, ref int i, int layer = 0)
		{
			long num1 = 0;
			long num2 = 0;

			num1 = GetOperand(input, ref i);

			while (i + 1 < input.Length)
			{
				i++;
				var op = input[i];
				i++;
				num2 = GetOperand(input, ref i);
				num1 = calculate(num1, op, num2);

				if (i + 1 < input.Length) if (input[i + 1] == ')')
					{
						i++;
						return num1;
					}
			}
			return num1;
		}

		private long calculate(long num1, char op, long num2)
		{
			return op switch
			{
				'*' => num1 * num2,
				'+' => num1 + num2,
				'-' => num1 - num2
			};
		}

		public override string Level2(string[] input)
		{
			long total = 0;
			foreach (var line in input)
			{
				total += (long)Evaluate(line);
			}

			return total.ToString();
		}

		internal long Evaluate(string expr)
		{
			var s = expr.Replace(" ", "");
			var stack = new Stack<string>();
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == '(')
				{
					var inner = GetInner(s, i);
					var sub = Evaluate(inner);
					stack.Push(sub.ToString());
					i += inner.Length+1;
				}
				else
				if (s[i].isOperator())
				{
					if (s[i] == '+')
					{
						var buf = stack.Pop();
						var num1 = long.Parse(buf);
						long num = GetNextOperand(s, i, out int ctr);
						var val = calculate(num1, s[i], num);
						i += ctr;
						stack.Push(val.ToString());
					}
					else if (s[i] == '*')
					{
						stack.Push(s[i].ToString());
						int ctr;
						long num = GetNextOperand(s, i, out ctr);
						stack.Push(num.ToString());
						i += ctr;
					}
				}
				else
				{
					stack.Push(s[i].ToString());
				}
			}

			while (stack.Count > 1)
			{
				var num1 = long.Parse(stack.Pop());
				var op = stack.Pop();
				var num2 = long.Parse(stack.Pop());
				stack.Push(calculate(num1, op[0], num2).ToString());
			}
			return long.Parse(stack.Pop());
		}

		private long GetNextOperand(string s, int i, out int offset)
		{
			var next = s[i+1];
			offset = 1;
			long num = 0;
			if (next == '(')
			{
				var inner = GetInner(s, i+1);
				num = Evaluate(inner);
				offset = inner.Length + 2;
			}
			else
			{
				num = long.Parse(next.ToString());
			}
			return num;
		}

		public string GetInner(string s, int i)
		{
			int start = i;
			int pair = 0;
			do
			{
				if (s[i] == '(')
				{
					pair++;
				}
				else if (s[i] == ')')
				{
					pair--;
				}
				i++;

			} while (pair > 0);
			return s.Substring(start + 1, i - start - 2);
		}


	}
	public static class CharExtensions
	{
		public static bool isOperator(this char c)
		{
			return c == '*' || c == '+' || c == '-';
		}
	}
}