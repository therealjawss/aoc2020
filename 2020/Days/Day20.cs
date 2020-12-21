using System;
using System.Collections.Generic;
using System.IO;
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
			Run2();
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
			var day = new Day20();
			day.GetInput(pattern: "\n\n");
			Console.WriteLine(day.Level2(day.Input));
		}

		List<Tile> Tiles = new();
		public override string Level1(string[] input)
		{
			ReadTiles(input);
			Console.WriteLine(Tiles.Count);

			var Connected = GetConnected();
			long result = Connected.Where(x => x.Value.Count == 2).Aggregate((long)1, (val, next) => val * next.Key);

			return result.ToString();
		}

		Dictionary<Tile, List<Orientation>> actual = new();
		public override string Level2(string[] input)
		{
			ReadTiles(input);
			Map = GetConnected();
			Coord startcoord = Map.Where(x => x.Value.Count == 2).Select(t => new Coord(t.Value[0].direction, t.Value[1].direction, t.Key)).FirstOrDefault();
			do
			{
				if (startcoord != null)
				{
					var tile = Tiles.SingleOrDefault(x => x.number == startcoord.tile);
					Tiles.Remove(tile);
					Tiles.Add(tile.Turn());
					Map = GetConnected();
				}
				startcoord = Map.Where(x => x.Key == startcoord.tile).Select(t => new Coord(t.Value[0].direction, t.Value[1].direction, t.Key)).FirstOrDefault();
			} while (!(startcoord.x == 3 && startcoord.y == 2));
			
			var REFERENCESIZE = (int)Math.Sqrt(Map.Count);
			var reftiles = new Tile[REFERENCESIZE][];
			for (int i1 = 0; i1 < reftiles.Length; i1++)
			{
				reftiles[i1] = new Tile[REFERENCESIZE];
			}
			
			var start = Tiles.SingleOrDefault(x => x.number == startcoord.tile);
			var rowstart = start;
			Tile buffer = default;
			for (int i = 0; i < REFERENCESIZE; i++)
			{
				var next = rowstart;
				for (int j = 0; j < REFERENCESIZE; j++)
				{
					reftiles[i][j] = next;
					buffer = Tiles.SingleOrDefault(x => x.number == next.number);
					Tiles.Remove(buffer);
					Tiles.Add(next);
					Map = GetConnected();
					next = GetRightOf(next);
				}
				buffer = Tiles.SingleOrDefault(x => x.number == rowstart.number);
				Tiles.Remove(buffer);
				Tiles.Add(rowstart);
				Map = GetConnected();
				rowstart = GetBottomOf(rowstart);
			}

			int[][] image = new int[REFERENCESIZE * 8][];
			for (int x = 0; x < image.Length; x++)
			{
				image[x] = new int[REFERENCESIZE * 8];
			}
			
			int xctr = 0;
			int yctr = 0;
			var bounds = reftiles[xctr][yctr].grid.GetUpperBound(0) - 1;

			for (int i = 0; i < REFERENCESIZE * 8;)
			{

				yctr = 0;
				for (int j = 0; j < REFERENCESIZE * 8;)
				{
					image[i][j] =  reftiles[xctr][yctr].grid[(i % 8) + 1, (j % 8) + 1]; ;
					
					j++;
					if (j % bounds == 0)
						yctr++;
				}
				Console.WriteLine();
				i++;
				if (i % bounds == 0)
					xctr++;
			}

			image.Print();

			int[][] monster = GetMonsterGrid();
			return MonsterHunt(image, monster).ToString();
		}

		private int MonsterHunt(int[][] image, int[][] monster)
		{
			int ctr = 0;
			do
			{
				for (int i = 0; i < image.Length - monster.Length + 1; i++)
				{
					for (int j = 0; j < image[i].Length - monster[0].Length + 1; j++)
					{
						var points = image.Capture(i, j, monster);
						if (points > 0)
						{
							ctr += 1;
						}
					}
				}

				image = image.Turn();
			} while (ctr == 0);

			var result = image.Count() - ctr * monster.Count();
			return result;
		}

		private int[][] GetMonsterGrid()
		{
			var buff = File.ReadAllText("monster.in").Split("\r\n");
			int[][] result = new int[buff.Length][];
			for (int i = 0; i < buff.Length; i++)
			{
				result[i] = new int[buff[i].Length];
				for (int j = 0; j < buff[i].Length; j++)
				{
					result[i][j] = buff[i][j] switch
					{
						'#' => 1,
						_ => 0
					};
				}
			}

			return result;
		}

		Dictionary<int, List<Orientation>> Map = new();
		record Coord(int x, int y, int tile);
		public Tile GetRightOf(Tile tile)
		{
			return Map[tile.number].FirstOrDefault(x => x.direction == 2)?.Normalize();
		}
		public Tile GetBottomOf(Tile tile)
		{
			return Map[tile.number].FirstOrDefault(x => x.direction == 3)?.Normalize();
		}

		public Dictionary<int, List<Orientation>> GetConnected()
		{
			Dictionary<Tile, List<Match>> Potentials = new();
			foreach (var tile in Tiles)
			{
				var p = Tiles.Where(t => tile.grid.Match(t.grid) > 0 && tile.number != t.number).Select(x => new Match(tile.grid.Match(x.grid), x)).ToList();
				Potentials.Add(tile, p);
			}
			var s = Potentials.Where(x => x.Value.Count > 1).Select(x => x).ToList();

			Dictionary<int, List<Orientation>> Connected = new();
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
					bool flippedv = false;

					while (o == 0 && ctr < 4 && (!flipped || !flippedv))
					{
						otherGrid = otherGrid.Turn();
						o = g.FindAlignment(otherGrid);
						ctr++;
						if (ctr == 4 && !flipped)
						{
							otherGrid = otherGrid.FlipH();
							flipped = true;
							ctr = 0;
							o = g.FindAlignment(otherGrid);
						}
						if (ctr == 4 && flipped && !flippedv)
						{
							otherGrid = otherGrid.FlipV();
							flippedv = true;
							ctr = 0;
							o = g.FindAlignment(otherGrid);
						}
					}
					if (o != 0)
					{

						var connectedTile = new Orientation(o, flipped, flippedv, ctr, Other);
						if (Connected.ContainsKey(item.Key.number))
						{
							Connected[item.Key.number].Add(connectedTile);
						}
						else
						{
							Connected.Add(item.Key.number, new List<Orientation> { connectedTile });
						}
					}
				}
			}
			return Connected;
		}
		public record Orientation(int direction, bool flippedH, bool flippedV, int turns, Tile tile)
		{
			public Tile Normalize()
			{
				Tile result = tile;
				if (flippedH) result = result.FlipH();
				if (flippedV) result = result.FlipV();
				for (int i = turns; i > 0; i--)
				{
					result = result.Turn();
				}
				return result;
			}

		}
		public record Match(int number, Tile tile);
		private void ReadTiles(string[] input)
		{
			Tiles.Clear();
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

		public record Tile(int number, int[,] grid)
		{
			public Tile Turn()
			{
				return new Tile(this.number, this.grid.Turn());
			}
			public Tile FlipV()
			{
				return new Tile(this.number, this.grid.FlipV());
			}
			public Tile FlipH()
			{
				return new Tile(this.number, this.grid.FlipH());
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

		public static string[] GetStrings(this int[,] grid)
		{
			var bound = grid.GetUpperBound(0);
			string[] gridDims = new string[4];
			gridDims[0] = Enumerable.Range(0, bound + 1).Select(x => grid[0, x].ToString()).Aggregate((i, j) => i + j).ToString();
			gridDims[1] = Enumerable.Range(0, bound + 1).Select(x => grid[x, bound].ToString()).Aggregate((i, j) => i + j).ToString();
			gridDims[2] = Enumerable.Range(0, bound + 1).Select(x => grid[bound, x].ToString()).Aggregate((i, j) => i + j).ToString();
			gridDims[3] = Enumerable.Range(0, bound + 1).Select(x => grid[x, 0].ToString()).Aggregate((i, j) => i + j).ToString();

			return gridDims;
		}
		public static string[] GetAllStrings(this int[,] grid)
		{
			string[] gridStrings = new string[8];
			Array.Copy(grid.GetStrings(), gridStrings, 4);
			for (int i = 0; i < 4; i++)
			{
				gridStrings[4 + i] = new String(gridStrings[i].ToCharArray().Reverse().ToArray());
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

		public static bool Print(this int[][] Grid)
		{
			for (int i = 0; i < Grid.Length; i++)
			{
				for (int j = 0; j < Grid[i].Length; j++)
				{
					Console.Write($"{print(Grid[i][j])}");
				}
				Console.WriteLine();
			}
			return true;
		}
		public static int[][] Turn(this int[][] Grid)
		{
			var newGrid = new int[Grid.Length][];
			for (int i = 0; i < Grid.Length; i++)
			{
				newGrid[i] = new int[Grid[0].Length];
				for (int j = 0; j < Grid[0].Length; j++)
				{
					newGrid[i][j] = Grid[Grid[0].Length - 1 - j][i];
				}
			}
			return newGrid;
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

		public static int Capture(this int[][] Grid, int x, int y, int[][] Monster)
		{
			int ctr = 0;
			bool contains = true;
			for (int i = 0; i < Monster.Length; i++)
			{
				for (int j = 0; j < Monster[i].Length; j++)
				{
					if (Monster[i][j] == 1)
						contains &= Grid[i + x][j + y] == Monster[i][j];
					else if (Grid[i + x][j + y] == 1)
					{
						ctr++;
					}
				}

			}

			if (contains) return ctr;
			else return 0;
		}

		public static int Count(this int[][] Grid)
		{
			int ctr = 0;
			for (int i = 0; i < Grid.Length; i++)
			{
				for (int j = 0; j < Grid[0].Length; j++)
				{
					ctr += Grid[i][j];
				}
			}
			return ctr;
		}
	}
}