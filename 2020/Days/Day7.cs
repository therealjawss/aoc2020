using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2020.Days
{
	public class Day7 : Christmas
	{
		public override int Day => 7;

		public static void Run()
		{
			Console.WriteLine("hello");
			var day = new Day7();
			day.GetInput(file: "test.txt", pattern: "\n");
			//	day.GetInput();
			Console.WriteLine(day.Level1(day.Input));
			//day.PostL1Answer();
			Task.Delay(60000);
			Console.WriteLine(day.Level2(day.Input));
			//day.PostL2Answer();
		}

		public static Bag ParseRule(string rule)
		{
			Bag r;
			var pattern = @"(?<outer>[\w\s]+)(?: bags contain )((\d+) ([\w+\s]+)(?: bag[s]?,\s))*((\d+) ([\w+\s]+)(?: bag[s]?).)";
			var m = new Regex(pattern).Match(rule);
			var gold = new Regex(@"(?:.*) contain (?:.*)shiny gold bag").Match(rule).Success;
			if (m.Success)
			{

				r = new Bag(m.Groups["outer"].Value, new List<ContainedBag>(), gold);
				for (var i = 1; i + 6 < m.Groups.Count && m.Groups[i].Value.Length > 0; i += 3)
				{
					r.containedBags.Add(new ContainedBag(Convert.ToInt32(m.Groups[i + 1].Value), new Bag(m.Groups[i + 2].Value, new List<ContainedBag>(), false)));
				}
				int len = m.Groups.Count;
				r.containedBags.Add(new ContainedBag(Convert.ToInt32(m.Groups[len - 3].Value), new Bag(m.Groups[len - 2].Value, new List<ContainedBag>(), false)));
			}
			else
			{
				pattern = @"(?<outer>[\w\s]+)(?: bags contain no other bags.)";
				m = new Regex(pattern).Match(rule);
				r = new Bag(m.Groups["outer"].Value, new List<ContainedBag>(), false);

			}

			return r;
		}

		public override string[] GetInput(string file = null, string pattern = null, Func<string, bool> predicate = null)
		{
			return base.GetInput(file, pattern);
		}
		public override string Level1(string[] input)
		{
			parseRules(input);


			int ctr = 0;
			foreach (var bag in Bags)
			{
				for (int i = 0; i < bag.containedBags.Count; i++)
				{
					bag.containedBags[i].bag = Bags.FirstOrDefault(x => x.description == bag.containedBags[i].bag.description);
				}

			}
			
			var hasGold = Bags.Where(x => x.hasGold).ToList();
			var containers = new List<Bag>();
			foreach(var bag in hasGold)
			{
				containers = containers.Concat(ListContainers(bag)).ToList();

			}
			ctr = containers.Distinct().Count();
			return ctr.ToString();

		}
		private List<Bag> ListContainers(Bag bag)
		{
			var result = new List<Bag>()
			{bag };
			var containers = Bags.Where(x => x.Contains(bag)).ToList();
			if (containers.Count == 0)
				return result;
			foreach( var container in containers)
			{
				result = result.Concat(ListContainers(container)).ToList();
			}
			return result;
		}

		private bool ContainsGold(Bag bag)
		{
			bool containsGold = bag.hasGold;
			if (containsGold)
				return true;
			foreach (var containedBag in bag.containedBags)
			{
				containsGold = containsGold || ContainsGold(containedBag.bag);
				if (containsGold) return true;
			}

			return false;
		}

		public List<Bag> Bags { get; set; } = new List<Bag>();

		private void parseRules(string[] input)
		{
			foreach (var entry in input)
			{
				Bags.Add(ParseRule(entry));
			}
		}

		public override string Level2(string[] input)
		{
			return base.Level2(input);
		}

	}

	public class Bag
	{
		public string description { get; set; }
		public List<ContainedBag> containedBags { get; set; }
		public bool hasGold { get; set; }
		public Bag(string description, List<ContainedBag> containedBags, bool hasGold)
		{
			this.description = description;
			this.containedBags = containedBags;
			this.hasGold = hasGold;

		}
		public bool Contains(Bag bag)
		{
			return containedBags.Where(x => x.bag == bag).Count() > 0;
		}

	}
	public class ContainedBag
	{
		public int number { get; set; }
		public Bag bag { get; set; }
		public ContainedBag(int number, Bag bag)
		{
			this.number = number;
			this.bag = bag;
		}
	}
}
