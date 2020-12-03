using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AOC;

string COOKIE = File.ReadAllText("./.cookie"); //add this to gitignore next round
const int DAY = 3;

DayModule day = new DayThree() { Day = DAY, Cookie = COOKIE };
var input = day.GetInput();

Console.WriteLine(day.Level1(input));
//day.PostL1Answer();
Task.Delay(5000);
Console.WriteLine(day.Level2(input));
//day.PostL2Answer();
