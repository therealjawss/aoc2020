using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Visualizations
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Dictionary<(int, int), bool> found;
		public MainWindow()
		{
			InitializeComponent();
			var input = File.ReadAllText("input.in").Split(Environment.NewLine.ToCharArray());
			var pattern = "(e|se|sw|w|nw|ne)+";

			List<List<direction>> Tiles = new List<List<direction>>();
			foreach (var line in input)
			{
				var matches = Regex.Matches(line, pattern).Cast<Match>();
				List<direction> d = new List<direction>();

				foreach (var item in matches.First().Groups[1].Captures)
				{
					d.Add(getDirection(item.ToString()));
				}
				Tiles.Add(d);

			}

			var refTile = (0, 0);
			found = new Dictionary<(int, int), bool>();
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

			dtimer = new DispatcherTimer();
			dtimer.Interval = TimeSpan.FromMilliseconds(100);
			dtimer.Tick += Timer_Tick;
			dtimer.Start();
		}
		DispatcherTimer dtimer;
		private void Timer_Tick(object sender, EventArgs e)
		{
			if (ctr < 100)
			{
				DrawCanvas(found);
				found = Tick(found);
			}
			else
			{
				window.Title = "Result is: " + found.Count(x => x.Value).ToString();
			}

		}

		Timer timer = new Timer();
		private direction getDirection(string d)
		{
			switch (d)
			{
				case "e": return direction.e;
				case "se": return direction.se;
				case "sw": return direction.sw;
				case "w": return direction.w;
				case "nw": return direction.nw;
				case "ne": return direction.ne;
			}
			return default;
		}
		private Dictionary<(int x, int y), bool> Tick(Dictionary<(int x, int y), bool> found)
		{
			var buffer = new Dictionary<(int x, int y), bool>();

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

		private void DrawCanvas(Dictionary<(int x, int y), bool> found)
		{
			foreach (var item in found)
			{

				Rectangle rect = new Rectangle { Height = 3, Width = 3 };

				rect.Fill = item.Value ? Brushes.Black : Brushes.White;
				myCanvas.Children.Add(rect);
				Canvas.SetLeft(rect, item.Key.x * (5) + 400);
				Canvas.SetTop(rect, item.Key.y * (5) + 400);
			}

			ctr++;
			window.Title = ctr + ": " + found.Count(x => x.Value).ToString();
		}

		int ctr = 0;

		private bool GetNextState(Dictionary<(int x, int y), bool> found, KeyValuePair<(int x, int y), bool> item)
		{
			var blackNeighbors = item.Key.GetNeighbors().Count(x => accessNeighbor(found, x));


			var result = item.Value ? blackNeighbors > 2 || blackNeighbors == 0 ? false : true : blackNeighbors == 2 ? true : false;

			return result;
		}

		private static bool accessNeighbor(Dictionary<(int x, int y), bool> found, (int x, int y) x)
		{
			if (!found.ContainsKey(x))
			{
				return false;
			}
			return found[x];
		}

		private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			Timer_Tick(sender, e);
		}

	
	}
	public class Tile
	{
		public coord Coordinates { get; }

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

	public static class tupleExtensions
	{
		public static (int, int) GetNeighbor(this (int x, int y) coord, direction d)
		{
			(int, int) result = default;

			switch (d)
			{
				case (direction.w):
					result = (coord.x - 2, coord.y);
					break;
				case (direction.nw):
					result = (coord.x - 1, coord.y + 2);
					break;

				case direction.ne:
					result = (coord.x + 1, coord.y + 2);
					break;
				case direction.e:
					result = (coord.x + 2, coord.y);
					break;
				case direction.se:
					result = (coord.x + 1, coord.y - 2);
					break;
				case direction.sw:
					result = (coord.x - 1, coord.y - 2);
					break;
			};
			return result;
		}
		public static IEnumerable<(int x, int y)> GetNeighbors(this (int x, int y) tile)
		{
			var directions = Enum.GetValues(typeof(direction)).Cast<direction>(); Enum.GetValues(typeof(direction)).Cast<direction>();
			foreach (var val in directions)
			{
				yield return tile.GetNeighbor(val);
			}
		}
	}
	public class coord
	{
		public int x { get; set; }
		public int y { get; set; }
		public coord(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
		public coord GetNeighbor(direction d)
		{
			coord result = default;

			switch (d)
			{
				case (direction.w):
					result = new coord(x - 2, y);
					break;
				case (direction.nw):
					result = new coord(x - 1, y + 2);
					break;

				case direction.ne:
					result = new coord(x + 1, y + 2);
					break;
				case direction.e:
					new coord(x + 2, y);
					break;
				case direction.se:
					new coord(x + 1, y - 2);
					break;
				case direction.sw:
					new coord(x - 1, y - 2);
					break;
			};
			return result;
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
