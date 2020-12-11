using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AOC2020.Days
{
    public class Day11 : Christmas
    {
        public override int Day => 11;
        public static void Run()
        {
            var day = new Day11();

            Console.WriteLine($"hello day {day.Day}");
            //day.GetInput(file: "test.txt", pattern: "\r\n");
            day.GetInput();
            Console.WriteLine(day.Level1(day.Input));
              day.PostL1Answer();
                Thread.Sleep(120000);
            Console.WriteLine(day.Level2(day.Input));
            day.PostL2Answer();
        }
        public int[,] Grid;
        public override string Level1(string[] input)
        {
            ParseGrid(input);
            while (Tick()) { }
            return CountOccupied(Grid).ToString();
        }

        public override string Level2(string[] input)
        {
            ParseGrid(input);

            while (Tick2()) { }
            printGrid();
            return CountOccupied(Grid).ToString();
        }

        public bool Tick()
        {
            int row = Grid.GetUpperBound(0) + 1;
            int col = Grid.GetUpperBound(1) + 1;
            int[,] nextGrid = new int[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    nextGrid[i, j] = predict(Grid, i, j);
                }
            }
            if (equivalent(Grid, nextGrid))
                return false;
            else
                Grid = nextGrid;
            return true;
        }


        private int predict(int[,] grid, int i, int j)
        {
            int result = grid[i, j];
            int occupied = CountAdjacentOccupied(grid, i, j);
            if (occupied == 0)
                return 1;
            else if (occupied >= 4)
                return 0;


            return result;
        }

        private int CountAdjacentOccupied(int[,] grid, int i, int j)
        {
            int occupied = 0;
            if (grid[i, j] == -1) return 1;
            (int, int) bounds = (grid.GetUpperBound(0), grid.GetUpperBound(1));
            for (int x = i - 1; x <= i + 1; x++)
            {
                if (x > bounds.Item1 || x < 0) continue;
                for (int y = j - 1; y <= j + 1; y++)
                {
                    if (y > bounds.Item2 || y < 0 || (x == i && y == j)) continue;
                    if (grid[x, y] == 1) occupied++;
                }
            }
            return occupied;
        }

        public bool Tick2()
        {
            int row = Grid.GetUpperBound(0) + 1;
            int col = Grid.GetUpperBound(1) + 1;
            int[,] nextGrid = new int[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    nextGrid[i, j] = predictLineOfSight(Grid, i, j);
                }
            }
            if (equivalent(Grid, nextGrid))
                return false;
            else
                Grid = nextGrid;
            return true;
        }
        private int predictLineOfSight(int[,] grid, int i, int j)
        {
            int result = grid[i, j];
            int occupied = CountLineOfSightOccupied(grid, i, j);
            if (occupied == 0)
                return 1;
            else if (occupied >= 5)
                return 0;
            return result;
        }

        private int CountLineOfSightOccupied(int[,] grid, int i, int j)
        {
            int occupied = 0;
            if (grid[i, j] == -1) return 1;

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0) continue;
                    occupied += look(grid, i + x, j + y, x, y);
                }
            }
            return occupied;
        }

        private int look(int[,] grid, int i, int j, int x, int y)
        {
            if (i < 0 || i > grid.GetUpperBound(0) || j < 0 || j > grid.GetUpperBound(1)) return 0;
            if (grid[i, j] < 0) return look(grid, i + x, j + y, x, y);
            else return grid[i, j];
        }

        private int CountOccupied(int[,] grid)
        {
            int ctr = 0;
            (int, int) bounds = (grid.GetUpperBound(0) + 1, grid.GetUpperBound(1) + 1);
            for (int i = 0; i < bounds.Item1; i++)
            {
                for (int j = 0; j < bounds.Item2; j++)
                {
                    if (grid[i, j] == 1) ctr++;
                }
            }
            return ctr;
        }

        private bool equivalent(int[,] grid, int[,] nextGrid)
        {
            (int, int) bounds = (grid.GetUpperBound(0) + 1, grid.GetUpperBound(1) + 1);
            for (int i = 0; i < bounds.Item1; i++)
            {
                for (int j = 0; j < bounds.Item2; j++)
                    if (grid[i, j] != nextGrid[i, j]) return false;
            }
            return true;
        }

        public void printGrid()
        {

            (int, int) bounds = (Grid.GetUpperBound(0) + 1, Grid.GetUpperBound(1) + 1);
            for (int i = 0; i < bounds.Item1; i++)
            {
                for (int j = 0; j < bounds.Item2; j++)
                    Console.Write(print(Grid[i, j]));
                Console.WriteLine();
            }
            Console.WriteLine();

        }

        private char print(int v)
        {
            switch (v)
            {
                case 0: return 'L';
                case -1: return '.';
                case 1:
                    return '#';
                default: return 'Z';
            }
        }
        private void ParseGrid(string[] input)
        {
            Grid = new int[input.Length, input[0].Length];
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    switch (input[i][j])
                    {
                        case 'L':
                            Grid[i, j] = 0;
                            break;
                        case '.':
                            Grid[i, j] = -1;
                            break;
                    }
                }
            }
        }
    }
}
