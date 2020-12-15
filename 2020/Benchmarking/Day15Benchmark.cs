using AOC2020.Days;
using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020.Benchmarking
{
	[MemoryDiagnoser]
	public class Day15Benchmark
	{

		//[Benchmark(Baseline = true)]
		public void Part1()
		{
			Day15.Run1();
		}

		[Benchmark(Baseline = true)]
		public void Part2()
		{
			Day15.Run2();
		}
	}
}
