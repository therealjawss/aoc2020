using System;
using System.Net;
using System.Text;
using AOC;
const string COOKIE = "session=yourcookie";
const string DAY = "2";

DayModule day = new DayTwo() { Day = DAY, Cookie = COOKIE };
var input = day.GetInput();

//Console.WriteLine(day.Level1(input));
//day.PostL1Answer();
//Console.WriteLine(day.Level2(input));
day.PostL2Answer();
var result = DayTwo.IsValid("1-3 a: abcde");
Console.WriteLine(result);
