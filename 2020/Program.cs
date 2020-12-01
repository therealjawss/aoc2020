using System;
using System.Net;
using System.Text;
using AOC;
const string COOKIE = "session=53616c7465645f5fe310bef593138f44614822f63afb9fd7c347e813346379d8863b2126ecc59f5cb2f9e2515171db95";
const string DAY = "2";

DayModule day = new DayOne() { Day = DAY, Cookie = COOKIE };
var input = day.GetInput();

//Console.WriteLine(day.Level1(input));
//Console.WriteLine(day.Level2(input));
//day.PostL1Answer();
//day.PostL2Answer();
