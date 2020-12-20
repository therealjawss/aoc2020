using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace AOC2020.Days
{

	public class Day20 : Christmas
	{
		public override int Day => 20;

		public static void Run()
		{
			var day = new Day20();
			day.GetInput();
			Run1();

			//Console.WriteLine(day.Level1(day.Input));
			// day.PostL1Answer();
			//Console.WriteLine("Answer should be " + day.Level2(day.Input));
			//day.PostL2Answer();
		}
		public static void Run1()
		{
			var day = new Day20();
			day.GetInput(pattern: "\n\n");
			Console.WriteLine(day.Level1(day.Input));
		}
		public static void Run2()
		{
			var day = new Day19();
			day.GetInput();
			Console.WriteLine(day.Level2(day.Input));
		}

		public override string Level1(string[] input)
		{
			ReadTiles(Input);
			Console.WriteLine(Tiles.Count);
			Dictionary<Tile, List<Match>> Potentials = new();
			foreach (var tile in Tiles)
			{
				var p = Tiles.Where(t => tile.grid.Match(t.grid) > 0 && tile.number != t.number).Select(x => new Match(tile.grid.Match(x.grid), x)).ToList();
				Potentials.Add(tile, p);
				//	Potentials.Add(tile.number, p);
			}
			var s = Potentials.Where(x => x.Value.Count > 1).Select(x => x).ToList();
			Dictionary<Tile, List<Orientation>> Connected = new();
			foreach (var item in s)
			{
				var g = item.Key.grid;
				foreach (var otherTile in item.Value)
				{
					var Other = otherTile.tile;
					var otherGrid = Other.grid;
					var o = g.FindAlignment(otherGrid);
					int ctr = 0;
					bool flipped = false;
					while (o == 0 && ctr < 4 && !flipped)
					{
						otherGrid = otherGrid.Turn();
						ctr++;
						o = g.FindAlignment(otherGrid);
						if (ctr == 4 && !flipped)
						{
							otherGrid = otherGrid.FlipH();
							flipped = true;
							ctr = 0;
						}
					}
					var connectedTile = new Orientation(o, new Tile(Other.number, otherGrid));
					if (Connected.ContainsKey(item.Key))
					{
						Connected[item.Key].Add(connectedTile);
					}
					else
					{
						Connected.Add(item.Key, new List<Orientation> { connectedTile });
					}
				}
			}
			long result = 1;

			var order = Connected.Where(x => x.Value.Count == 2).Aggregate(result, (val, next) => val * next.Key.number);

			return default;
		}
		List<Tile> Tiles = new();
		public record Orientation(int direction, Tile tile);
		public record Match(int number, Tile tile);
		private void ReadTiles(string[] input)
		{
			foreach (var line in input)
			{
				var section = line.Split("\n");
				var tilenumber = int.Parse(section[0].Extract(@"Tile (\d+):"));
				int[,] grid = new int[10, 10];
				for (int j = 0; j < 10; j++)
				{
					for (int k = 0; k < 10; k++)
					{
						switch (section[j + 1][k])
						{
							case '#':
								grid[j, k] = 1;
								break;
							case '.':
								grid[j, k] = 0;
								break;
						}
					}
				}
				Tiles.Add(new Tile(tilenumber, grid));
			}
		}

		public override string Level2(string[] input)
		{
			return default;
		}
		public record Tile(int number, int[,] grid)
		{
			public Tile Turn()
			{
				return new Tile(this.number, this.grid.Turn());
			}

			public Tile Turn(int times)
			{
				Tile newGrid = default;
				for (int i = 0; i < times; i++)
				{
					newGrid = this.Turn();
				}
				return newGrid;
			}
			public bool Print()
			{
				Console.WriteLine($"Tile number {this.number}");
				this.grid.Print();
				return true;
			}
		}
	}
	public static class GridExtensions
	{/// <summary>
	 /// 1 top
	 /// 2 right
	 /// 3 bottom
	 /// 4 left
	 /// </summary>
	 /// <param name="Grid"></param>
	 /// <param name="OtherGrid"></param>
	 /// <returns></returns>
	
		public static int FindAlignment(this int[,] grid, int[,] other)
		{
			var g = grid.GetStrings();
			var o = other.GetStrings();
			if (g[0] == o[2]) return 1;
			else if (g[1] == o[3]) return 4;
			for (int i = 0; i < 4; i++)
			{
				if (g[i] == o[(i + 2) % 4])
					return i + 1;
			}
			return 0;
		}
	
		public static int Match(this int[,] Grid, int[,] OtherGrid)
		{
			var bound = Grid.GetUpperBound(0) + 1;
			string[] gridDims = Grid.GetStrings();
			string[] other = OtherGrid.GetAllStrings();
			if (gridDims.Any(x => other.Contains(x)))
			{
			 return 1;
			} 
			return 0;
		}
		public static string GetString(this int[,] grid, int x)
		{
			string[] gridDims = GetStrings(grid);
			return gridDims[x];
		}
		public static string[] GetStrings(this int[,] grid)
		{
			var bound = grid.GetUpperBound(0);
			string[] gridDims = new string[4];
			gridDims[0] = Enumerable.Range(0, bound + 1).Select(x => grid[0, x].ToString()).Aggregate((i, j) => i + j).ToString();
			gridDims[1] = Enumerable.Range(0, bound + 1).Select(x => grid[x, bound].ToString()).Aggregate((i, j) => j + i).ToString();
			gridDims[2] = Enumerable.Range(0, bound + 1).Select(x => grid[bound, x].ToString()).Aggregate((i, j) => j + i).ToString();
			gridDims[3] = Enumerable.Range(0, bound + 1).Select(x => grid[x, 0].ToString()).Aggregate((i, j) => j + i).ToString();

			return gridDims;
		}
 		public static string[] GetAllStrings(this int[,] grid) {
			 string[] gridStrings = new string[8];
			 Array.Copy(grid.GetStrings(), gridStrings, 4);
			 for(int i=0; i< 4; i++){
				 gridStrings[4+i] = new String(gridStrings[i].ToCharArray().Reverse().ToArray());
			 }
			 return gridStrings;
		 }
		public static void Print(this int[,] Grid)
		{
			(int, int) bounds = (Grid.GetUpperBound(0), Grid.GetUpperBound(1));
			for (int i = 0; i <= bounds.Item1; i++)
			{
				for (int j = 0; j <= bounds.Item2; j++)
					Console.Write(print(Grid[i, j]));
				Console.WriteLine();
			}
			Console.WriteLine();
		}
		public static int[,] Turn(this int[,] Grid)
		{
			var dimbound = Grid.GetUpperBound(0) + 1;
			var newGrid = new int[dimbound, dimbound];
			for (int i = 0; i < dimbound; i++)
			{
				for (int j = 0; j < dimbound; j++)
				{
					newGrid[i, j] = Grid[dimbound - 1 - j, i];
				}
			}
			return newGrid;
		}
		public static int[,] FlipH(this int[,] Grid)
		{
			var dimbound = Grid.GetUpperBound(0) + 1;
			var newGrid = new int[dimbound, dimbound];
			for (int i = 0; i < dimbound; i++)
			{
				for (int j = 0; j < dimbound; j++)
				{
					newGrid[i, j] = Grid[dimbound - i - 1, j];
				}
			}
			return newGrid;
		}
		public static int[,] FlipV(this int[,] Grid)
		{
			var dimbound = Grid.GetUpperBound(0) + 1;
			var newGrid = new int[dimbound, dimbound];
			for (int i = 0; i < dimbound; i++)
			{
				for (int j = 0; j < dimbound; j++)
				{
					newGrid[i, j] = Grid[i, dimbound - j - 1];
				}
			}
			return newGrid;
		}
		private static char print(int v)
		{
			switch (v)
			{
				case 0: return '.';
				case 1:
					return '#';
				default: return 'Z';
			}
		}
	}
}