using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AOC2020.Days;

Christmas day = new Day6();
//day.GetInput("test.txt", "\r\n");
day.GetInput(pattern: "\n");
Console.WriteLine(day.Level1(day.Input));
day.PostL1Answer();
Task.Delay(60000);
Console.WriteLine(day.Level2(day.Input));
day.PostL2Answer();

