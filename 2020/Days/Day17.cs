using System;

namespace AOC2020.Days
{

	public class Day17 : Christmas
	{
		public override int Day => 17;


		public static void Run()
		{
			var d = new Day17();
			d.GetInput();
			Console.WriteLine(d.Level1(d.Input));


		}
		public int[,,,] Grid;
		public override string Level1(string[] input)
		{
			ParseGrid(input);
			///printGrid();
			Console.WriteLine("begin");
			for (int i = 0; i < 6; i++)
			{
				Console.WriteLine("It's" + i);
				Tick();
			}


			return CountActive(Grid).ToString();
		}

		private int CountActive(int[,,,] grid)
		{
			int ctr = 0;
			for (int i = 0; i <= grid.GetUpperBound(0); i++)
				for (int j = 0; j <= grid.GetUpperBound(1); j++)
					for (int k = 0; k <= grid.GetUpperBound(2); k++)
						for (int l = 0; l <= grid.GetUpperBound(3); l++)
							ctr += grid[i, j, k, l];

			return ctr;
		}

		private void ParseGrid(string[] input)
		{
			Grid = new int[input.Length, input.Length, 1, 1];
			for (int i = 0; i < input.Length; i++)
			{
				for (int j = 0; j < input[i].Length; j++)
				{
					switch (input[i][j])
					{
						case '.':
							Grid[i, j, 0, 0] = 0;
							break;
						case '#':
							Grid[i, j, 0, 0] = 1;
							break;
					}
				}
			}
		}

		public void Tick()
		{
			int x = Grid.GetUpperBound(0) + 3;
			int y = Grid.GetUpperBound(1) + 3;
			int z = Grid.GetUpperBound(2) + 3;
			int w = Grid.GetUpperBound(3) + 3;
			int[,,,] nextGrid = new int[x, y, z, w];
			int[,,,] buffer = new int[x, y, z, w];
			for (int i = 0; i < x - 2; i++)
			{
				for (int j = 0; j < y - 2; j++)
				{
					for (int k = 0; k < Grid.GetUpperBound(2) + 1; k++)
					{
						for (int l = 0; l < Grid.GetUpperBound(3) + 1; l++)
						{

							buffer[i + 1, j + 1, k + 1, l + 1] = Grid[i, j, k, l];
						}
					}
				}
			}
			for (int i = 0; i < x; i++)
			{
				for (int j = 0; j < y; j++)
				{
					for (int k = 0; k < z; k++)
					{
						for (int l = 0; l < w; l++)
						{
							nextGrid[i, j, k, l] = predict(buffer, i, j, k, l);
						}
					}
				}
			}
			Grid = nextGrid;
		}


		private int predict(int[,,,] grid, int i, int j, int k, int l)
		{
			int xbound = grid.GetUpperBound(0);
			int ybound = grid.GetUpperBound(1);
			int zbound = grid.GetUpperBound(2);
			int wbound = grid.GetUpperBound(3);
			int active = 0;
			for (int x = -1; x <= 1; x++)
			{
				for (int y = -1; y <= 1; y++)
				{
					for (int z = -1; z <= 1; z++)
					{
						for (int w = -1; w <= 1; w++)
						{


							if (x == 0 && y == 0 && z == 0 && w==0) continue;
							if (i + x >= 0 && i + x <= xbound
								&& j + y >= 0 && j + y <= ybound
								&& k + z >= 0 && k + z <= zbound
								&& l + w >= 0 && l + w <= wbound)
								active += grid[i + x, j + y, k + z, l + w];
						}
					}
				}
			}
			switch (grid[i, j, k, l])
			{
				case (1):
					if (active == 2 || active == 3) return 1;
					else return 0;
				case (0):
					if (active == 3) return 1;
					else return 0;
			}
			return -1;

		}
		public void printGrid() { printGrid(Grid); }
		public void printGrid(int[,,,] Grid)
		{

			(int, int) bounds = (Grid.GetUpperBound(0) + 1, Grid.GetUpperBound(1) + 1);
			for (int w = 0; w <= Grid.GetUpperBound(3); w++)
			{

				for (int z = 0; z <= Grid.GetUpperBound(2); z++)
				{
					Console.WriteLine($"Z={z} and W={w}");
					for (int i = 0; i < bounds.Item1; i++)
					{
						for (int j = 0; j < bounds.Item2; j++)
							Console.Write(print(Grid[i, j, z, w]));
						Console.WriteLine();
					}
					Console.WriteLine();
				}
			}
		}

		private char print(int v)
		{
			switch (v)
			{
				case 0: return '.';
				case 1:
					return '#';
				default: return 'X';
			}
		}
		public override string Level2(string[] input)
		{
			return "";
		}

	}
}