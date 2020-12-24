using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2020.Days
{
	public class Day24 : Christmas
	{
		public override int Day => 24;
		public static void Run()
		{
			var d = new Day24();
			d.GetInput();
			Console.WriteLine(d.Level1(d.Input));
			Console.WriteLine(d.Level2(d.Input));
		}
		public override string Level1(string[] input)
		{
			var pattern = "(e|se|sw|w|nw|ne)+";
			var Tiles = input.Select(lines => Regex.Matches(lines, pattern).Cast<Match>()
				.Select(x => x.Groups[1].Captures.Select(y => y.Value)).ToList()[0]
				.Select(d => d switch
					{
						"e" => direction.e,
						"se" => direction.se,
						"sw" => direction.sw,
						"w" => direction.w,
						"nw" => direction.nw,
						"ne" => direction.ne
					})).ToList();

			var refTile = new Tile(new coord(0, 0));
			Dictionary<coord, Tile> found = new();
			found.Add(refTile.Coordinates, refTile);
			foreach (var tile in Tiles)
			{
				var current = refTile;
				var directions = tile.ToList();
				var location = refTile.Coordinates;
				foreach (var d in directions)
				{
					current = new Tile(current.GetNeighbor(d));
				}
				if (!found.ContainsKey(current.Coordinates))
				{
					found.Add(current.Coordinates, current);
				}
				found[current.Coordinates].black = !found[current.Coordinates].black;

			}

			return found.Where(x => x.Value.black == true).Count().ToString();
		}
		public override string Level2(string[] input)
		{
			var pattern = "(e|se|sw|w|nw|ne)+";
			var Tiles = input.Select(lines => Regex.Matches(lines, pattern).Cast<Match>()
				.Select(x => x.Groups[1].Captures.Select(y => y.Value)).ToList()[0]
				.Select(d => d switch
				{
					"e" => direction.e,
					"se" => direction.se,
					"sw" => direction.sw,
					"w" => direction.w,
					"nw" => direction.nw,
					"ne" => direction.ne
				})).ToList();

			var refTile = new coord(0, 0);
			Dictionary<coord, bool> found = new();
			found.Add(refTile, false);
			foreach (var tile in Tiles)
			{
				var current = refTile;
				var directions = tile.ToList();
				foreach (var d in directions)
				{

					current = current.GetNeighbor(d);

					if (!found.ContainsKey(current))
					{
						found.Add(current, false);
					}

				}

				
				found[current] = !found[current];
				if (found[current])
				{
					var neighborsneighbors = current.GetNeighbors();
					foreach (var nn in neighborsneighbors)
					{
						if (!found.ContainsKey(nn))
						{
							found.Add(nn, false);
						}
					}
				}

			}

			for (int i = 0; i < 100; i++)
			{
				found = Tick(found);
			}

			return found.Where(x => x.Value == true).Count().ToString();
		}

		Dictionary<coord, IEnumerable<KeyValuePair<coord,bool>>> Neighbors = new();
		private Dictionary<coord, bool> Tick(Dictionary<coord, bool> found)
		{
			var buffer = new Dictionary<coord, bool>();

			foreach (var item in found)
			{
				var next = GetNextState(found, item);
				if (!buffer.ContainsKey(item.Key)) buffer.Add(item.Key, false);
				buffer[item.Key] = next;
				if (next)
				{
					var n = item.Key.GetNeighbors();

					foreach (var d in n)
					{
						if (!buffer.ContainsKey(d))
						{
							buffer.Add(d, false);
						}
					}
				}
			}

			return buffer;
		}

		Dictionary<coord, List<KeyValuePair<coord, bool>>> nlookup = new();
		private bool GetNextState(Dictionary<coord, bool> found, KeyValuePair<coord, bool> item)
		{
			var neighbors = found.Where(x => item.Key.GetNeighbors().ToList().Contains(x.Key)).ToList();
			var blackNeighbors = neighbors.Count(x => x.Value);


			return item.Value ? blackNeighbors switch
			{
				> 2 or 0 => false,
				_ => true
			} : blackNeighbors == 2 ? true : false;
		}

		public class Tile
		{
			public coord Coordinates { get; }

			public coord GetNeighbor(direction d)
			{
				return d switch
				{
					(direction.w) => new coord(Coordinates.x - 2, Coordinates.y),
					(direction.nw) => new coord(Coordinates.x - 1, Coordinates.y + 2),
					(direction.ne) => new coord(Coordinates.x + 1, Coordinates.y + 2),
					(direction.e) => new coord(Coordinates.x + 2, Coordinates.y),
					(direction.se) => new coord(Coordinates.x + 1, Coordinates.y - 2),
					(direction.sw) => new coord(Coordinates.x - 1, Coordinates.y - 2)
				};
			}

			public IEnumerable<coord> GetNeighbors()
			{
				var directions = Enum.GetValues(typeof(direction)).Cast<direction>(); Enum.GetValues(typeof(direction)).Cast<direction>();
				foreach (var val in directions)
				{
					yield return GetNeighbor(val);
				}
			}
			public Tile(coord c)
			{
				this.Coordinates = c;
			}
			public override string ToString()
			{
				return $"{Coordinates.x} : {Coordinates.y} - Black: {black}";
			}
			public bool black { get; set; } = false;

		}
		public record coord(int x, int y)
		{
			public coord GetNeighbor(direction d)
			{
				return d switch
				{
					(direction.w) => new coord(x - 2, y),
					(direction.nw) => new coord(x - 1, y + 2),
					(direction.ne) => new coord(x + 1, y + 2),
					(direction.e) => new coord(x + 2, y),
					(direction.se) => new coord(x + 1, y - 2),
					(direction.sw) => new coord(x - 1, y - 2)
				};
			}
			public IEnumerable<coord> GetNeighbors()
			{
				var directions = Enum.GetValues(typeof(direction)).Cast<direction>(); Enum.GetValues(typeof(direction)).Cast<direction>();
				foreach (var val in directions)
				{
					yield return GetNeighbor(val);
				}
			}
		}
		public enum direction
		{
			e = 0,
			se = 1,
			sw = 2,
			w = 3,
			nw = 4,
			ne = 5
		}
	}
}
