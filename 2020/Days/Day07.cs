using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2020.Days
{
	public class Day07 : Christmas
	{
		public override int Day => 7;

		public static void Run()
		{
			Console.WriteLine("hello");
			var day = new Day07();
			//day.GetInput(file: "test.txt", pattern: "\n");
				day.GetInput();
			Console.WriteLine(day.Level1(day.Input));
		//	day.PostL1Answer();
			Task.Delay(60000);
			Console.WriteLine(day.Level2(day.Input));
			//day.PostL2Answer();
		}
		public override string[] GetInput(string file = null, string pattern = null, Func<string, bool> predicate = null)
		{
			return base.GetInput(file, pattern);
		}
		private void parseRules(string[] input)
		{
			foreach (var entry in input)
			{
				ParseRule(entry, Bags);
			}
		}

		public static Bag ParseRule(string rule, List<Bag> bags)
		{
			Bag r;
			var outer = @"(?<outer>[\w\s]+)(?: bags contain )";
			var middleBagsPattern = "(\\d+) ([\\w+\\s]+)(?: bag[s]?[.,])";
			var m = new Regex(outer).Match(rule);
			var gold = new Regex(@"(?:.*) contain (?:.*)shiny gold bag").Match(rule).Success;
			string bagName;
			if (m.Success)
			{
				bagName = m.Groups["outer"].Value;
				r = bags.FirstOrDefault(x => x.description.Equals(bagName));
				if (r == null)
				{
					r = new Bag(bagName, new List<ContainedBag>(), gold);
					bags.Add(r);
				}
				r.hasGold = gold;
				var mb = new Regex(middleBagsPattern).Matches(rule);

				for (int i = 0; i < mb.Count; i++)
				{
					bagName = mb[i].Groups[2].Value;
					var num = Convert.ToInt32(mb[i].Groups[1].Value);
					var containedBag = bags.FirstOrDefault(bags => bags.description.Equals(bagName));
					if (containedBag == null)
					{
						containedBag = new Bag(bagName, new List<ContainedBag>(), false);
						bags.Add(containedBag);
					}
					r.containedBags.Add(new ContainedBag(num, containedBag));
				}
			}
			else
			{
				outer = @"(?<outer>[\w\s]+)(?: bags contain no other bags.)";
				m = new Regex(outer).Match(rule);
				bagName = m.Groups["outer"].Value;
				r = bags.FirstOrDefault(x => x.description.Equals(bagName));
				if (r == null)
				{
					r = new Bag(m.Groups["outer"].Value, new List<ContainedBag>(), false);
					bags.Add(r);
				}
			}

			return r;
		}

		public override string Level1(string[] input)
		{
			parseRules(input);

			int ctr = 0;
			var hasGold = Bags.Where(x => x.hasGold).ToList();
			var containers = new List<Bag>();
			foreach (var bag in hasGold)
			{
				containers = containers.Concat(ListContainers(bag)).ToList();

			}
			ctr = containers.Distinct().Count();

			var anotherCtr = 0;
			foreach (var bag in Bags)
			{
				if (ContainsGold(bag))
					anotherCtr++;
			}

			return ctr.ToString();
		}

		public override string Level2(string[] input)
		{
			var gBag = Bags.FirstOrDefault(x => x.description.Equals("shiny gold"));
			long ctr = 0;
			foreach (var contained in gBag.containedBags)
			{
				ctr += contained.number +contained.number * CountBagsIn(contained);
			}
			return ctr.ToString();
		}

		private long CountBagsIn(ContainedBag contained)
		{
			if (contained.bag.containedBags.Count == 0)
				return 0;
			long ctr = 0;
			foreach (var bag in contained.bag.containedBags)
			{
				ctr +=  bag.number + bag.number * CountBagsIn(bag);
			}
			return ctr;
		}

		private List<Bag> ListContainers(Bag bag)
		{
			var result = new List<Bag>()
			{bag };
			var containers = Bags.Where(x => x.Contains(bag)).ToList();
			if (containers.Count == 0)
				return result;
			foreach (var container in containers)
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
		public override string ToString()
		{
			return $"{description} contains {containedBags.Count} and has gold = {hasGold}";
		}
	}
	public record ContainedBag(int number, Bag bag);

}
