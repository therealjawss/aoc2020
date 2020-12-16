using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2020.Days
{

	public class Day16 : Christmas
	{
		public override int Day => 16;

		public static void Run()
		{
			Run2();
		}
		public static void Run1()
		{

			var d = new Day16();
			d.GetInput(pattern: "");
			Console.WriteLine(d.Level1(d.Input));
		}
		public static void Run2()
		{
			var d = new Day16();
			d.GetInput(pattern: "");
			Console.WriteLine(d.Level2(d.Input));
			d.PostL2Answer();
		}
		public override string Level1(string[] input)
		{
			parse(input[0]);
			long sum = 0;
			foreach (var ticket in OtherTickets)
			{
				for (int j = 0; j < ticket.Count; j++)
				{
					int field = ticket[j];
					bool valid = false;
					foreach (var f in Fields)
					{
						valid |= ((field >= f.ax && field <= f.ay) || (field >= f.bx && field <= f.by));

					}
					if (!valid)
					{
						sum += field;
					}
				}
			}
			return sum.ToString();
		}

		internal List<int> ValidForIndices(List<int> ticket, Field rules)
		{
			return ticket.Where(x => isValid(x, rules)).ToList();
		}

		internal List<Field> ValidForFields(int ticket, List<Field> rules)
		{
			return rules.Where(x => isValid(ticket, x)).ToList();
		}

		List<Field> Fields = new();
		int[] Ticket;
		List<List<int>> OtherTickets = new();

		public List<Field> ParseRules(string input)
		{
			var pattern = @"([\w\s]+): (\d+)-(\d+) or (\d+)-(\d+)\n";
			var fields = Regex.Matches(input, pattern).Cast<Match>();
			var Fields = fields.Select(x => new Field(x.Groups[1].Value, int.Parse(x.Groups[2].Value), int.Parse(x.Groups[3].Value), int.Parse(x.Groups[4].Value), int.Parse(x.Groups[5].Value))).ToList();
			return Fields;
		}
		public List<List<int>> ParseOtherTickets(string input)
		{
			var pattern = @"nearby tickets:[\s]+((\d+,?\n?\r?)+)";
			var otherTickets = Regex.Matches(new Regex(pattern).Match(input).Groups[1].Value, @"((\d+)(?:\,)?)+(\n)?").Cast<Match>();
			
			return otherTickets.Select(x => x.Groups[2].Captures.Select(y => int.Parse(y.Value)).ToList()).ToList();
		}

		private void parse(string input)
		{
			Fields = ParseRules(input);
			OtherTickets = ParseOtherTickets(input);

			var pattern = @"your ticket:[\s]+((\d+,?)+)";
			var yourTicket = Regex.Matches(new Regex(pattern).Match(input).Groups[1].Value, @"(\d+)").Cast<Match>();
			Ticket = yourTicket.Select(x => int.Parse(x.Groups[1].Value)).ToArray();
		}
		List<int[]> ValidTickets = new();
		public override string Level2(string[] input)
		{
			parse(input[0]);
			long sum = 0;
			foreach (var ticket in OtherTickets)
			{
				bool valid = ticket.All(t => ValidForFields(t, Fields).Count > 0);

				if (valid)
				{
					ValidTickets.Add(ticket.ToArray());
				}
			}
			ValidTickets.Add(Ticket);
			Dictionary<int, List<Field>> map = new();

			for (int i = 0; i < 20; i++)
			{
				var t = ValidTickets.Select(x => x[i]);
				var fields = Fields.Where(f => t.All(x => isValid(x, f))).ToList();
				map.Add(i, fields);
			}
			Dictionary<int, string> realMap = new();

			while (map.Any(x => x.Value.Count > 1) || map.Count > 0)
			{
				var singles = map.Where(x => x.Value.Count == 1).ToList();
				foreach (var item in singles)
				{
					var val = item.Value.First();
					if (!realMap.ContainsKey(item.Key))
					{
						realMap.Add(item.Key, val.field);
					}
				}
				singles.All(s => map.All(x => map.Remove(s.Key)));
				singles.All(s => map.All(x => x.Value.Remove(s.Value.First())));
			}


			var indices = realMap.Where(x => x.Value.StartsWith("departure")).Select(x => x.Key).ToList();
			ulong result = 1;
			foreach(var idx in indices)
			{
				result *= (ulong)Ticket[idx];
			}

			return result.ToString();
		}
		public  bool isValid(int value, Field f)
		{
			return ((value >= f.ax && value <= f.ay) || (value >= f.bx && value <= f.by));
		}
	}

	public record Field(string field, int ax, int ay, int bx, int by);
}