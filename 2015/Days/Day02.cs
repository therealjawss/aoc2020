using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
public class Day02 {
    public static void Run() {
        var input = File.ReadAllLines("2.txt");

        for(int i=0; i<input.Length; i++){
            var pattern = @"(\d+)x(\d+)x(\d+)";
            var entry = new Regex(pattern).Match(input[i]);
            if (entry.Success)
            {
                Input.Add(new Dim(Convert.ToInt32(entry.Groups[1].Value), Convert.ToInt32(entry.Groups[2].Value), Convert.ToInt32(entry.Groups[3].Value)));
            }
        }
        long total=0;
        foreach(var item in Input){
            var areas = new List<int>();
            areas.Add(2*item.l*item.w);
            areas.Add(2*item.w*item.h);
            areas.Add(2*item.l*item.h);
            total += areas.Sum() + areas.Min()/2;
        }

        Console.WriteLine($"it's {total}");
    }
    public static List<Dim> Input = new List<Dim>();
}
public record Dim(int l, int w, int h);