using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2020.Days
{

	public class Day21 : Christmas
	{
		public override int Day => 21;


		public static void Run()
		{
			var d = new Day21();
			d.GetInput();
			Console.WriteLine(d.Level1(d.Input));
			Console.WriteLine(d.Level2(d.Input));
		}
		public override string Level1(string[] input)
		{
			ParseIngredients(input);
			List<string> safe = ExtractSafeList();
			var ans = safe.Aggregate(0, (val, next) => FoodList.Where(x => x.listedingredients.Contains(next)).Count() + val);
			return ans.ToString();
		}

		private List<string> ExtractSafeList()
		{
			var ingredientsList = FoodList.SelectMany(x => x.listedingredients).Distinct().ToList();
			var allergenList = FoodList.SelectMany(x => x.listedallergens).Distinct().ToList();
			foreach (var allergen in allergenList)
			{
				var possibilities = FoodList.Where(f => f.listedallergens.Contains(allergen)).Select(i => i.listedingredients);
				var result = ingredientsList.Where(i => possibilities.All(p => p.Contains(i))).ToList();
				var surelist = allergyPossibilites.Where(x => x.Value.Count == 1).Select(x => x.Value[0]).ToList();
				result = result.Where(r => !surelist.Contains(r)).ToList();
				allergyPossibilites[allergen] = result;
			}
			var safe = ingredientsList.Where(i => allergyPossibilites.Values.Where(a => a.Contains(i)).Count() == 0).ToList();
			return safe;
		}

		public override string Level2(string[] input)
		{
			ParseIngredients(input);
			List<string> safe = ExtractSafeList();
			do {
				var surelist = allergyPossibilites.Where(x => x.Value.Count == 1).Select(x => x.Value[0]).ToList();
				foreach(var item in surelist)
				{
					var tentative = allergyPossibilites.Where(x => x.Value.Count > 1);
					foreach(var i in tentative)
					{
						if(i.Value.Contains(item)) 
							i.Value.Remove(item);
					}
				}

			} while (allergyPossibilites.Any(x => x.Value.Count > 1));
			var answer =allergyPossibilites.OrderBy(x => x.Key).Select(x => x.Value[0]).Aggregate("", (val,next) => val+","+next)[1..];
			return answer;
		}

		List<posibilities> byAllergy = new();
		List<food> FoodList = new();
		Dictionary<string, List<string>> allergyPossibilites= new();
		private void ParseIngredients(string[] input)
		{
			allergyPossibilites.Clear();
			FoodList.Clear();
			foreach (var line in input)
			{
				var ingredients = line.Split("(contains")[0].Trim().Split(" ").ToList();
				var allergens = line.Split("contains")[1][..^1].Split(",").AsEnumerable();
				FoodList.Add(new food(ingredients, allergens.ToList()));
				foreach (var a in allergens)
				{
					byAllergy.Add(new posibilities(a, ingredients));
					if (allergyPossibilites.ContainsKey(a))
					{
						var list = allergyPossibilites[a].Concat(ingredients).Distinct().ToList();
						allergyPossibilites[a] = list;
					}
					else
					{
						allergyPossibilites.Add(a, ingredients);
					}
				}
			}
		}
		record food(List<string> listedingredients, List<string> listedallergens);
		record posibilities(string name, List<string> items);
	
	}
}