using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AOC2020.Days;

Christmas day = new Day4();
day.GetInput("test.txt", "\r\n\r\n");
Console.WriteLine(day.Level1(day.Input));
//day.PostL1Answer();
Task.Delay(5000);
Console.WriteLine(day.Level2(day.Input));
//day.PostL2Answer();
Console.ReadLine();
