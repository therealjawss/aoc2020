using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AOC2020.Days;

Christmas day = new Day4();
var input = day.GetInput();

//Console.WriteLine(day.Level1(input));
//day.PostL1Answer();
Task.Delay(5000);
Console.WriteLine(day.Level2(input));
//day.PostL2Answer();
