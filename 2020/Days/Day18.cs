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
			//Console.Writeline(d.Level2(d.Input));
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

		private void ParseInput(string[] input)
		{
			throw new NotImplementedException();
		}

		public override string Level2(string[] input)
		{
			var total = 0;
			foreach (var line in input)
			{
				total = Compute2(line);
			}

			return total.ToString();
		}

		private int Compute2(string line)
		{
			var stack = new Stack<int>();
			return 0;

		}
		internal int Evaluate(string expr)
		{
			var s = expr.Replace(" ", "");
			var stack = new Stack<string>();
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] == '(')
				{
					int ctr = 0;
					var sub = EvaluateSub(s, i, ref ctr);
					stack.Push(sub.ToString());
					i += ctr;
				}
				else
				if (s[i].isOperator())
				{
					if (s[i] == '+')
					{
						var num1 = long.Parse(stack.Pop());
						long num = 0;
						var next = s[i + 1];
						int ctr = 1;
						if (next == '(')
						{
							num = EvaluateSub(s, i+1, ref ctr );
						}
						else
						{
							num = long.Parse(next.ToString());
						}
						var val = calculate(num1, s[i], num);
						i += ctr;
						stack.Push(val.ToString());
					}
					else if (s[i] == '*')
					{
						stack.Push(s[i].ToString());
						var next = s[i + 1];
						int ctr =1;
						long num = 0;
						if (next == '(')
						{
							num = EvaluateSub(s, i + 1, ref ctr);
							ctr++;
						}
						else
						{
							num = long.Parse(next.ToString());
						}
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
			return int.Parse(stack.Pop());
		}

		private long EvaluateSub(string s, int i, ref int ctr)
		{
			long num;

			int pair = 0;
			ctr = 0;
			do
			{
				if (s[i + ctr] == '(')
				{
					pair++;
				}
				else if (s[i + ctr] == ')')
				{
					pair--;
				}
				ctr++;
			} while (pair != 0);

			num = Evaluate(s.Substring(i + 1, ctr-2));
			return num;
		}

		record Expression(int operand, char op);
	}
	public static class CharExtensions
	{
		public static bool isOperator(this char c)
		{
			return c == '*' || c == '+' || c == '-';
		}
	}
}