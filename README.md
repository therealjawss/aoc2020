# Advent of Code 2024

## [Day 3](https://adventofcode.com/2024/day/3) 

Hello regex my old acquaintance... [Here](2024/Day03/Program.cs) is my solution to day 3.

	Elapsed time:21 ms
	Part 1:182780583
	Elapsed time:2 ms
	Part 2:90772405

## [Day 2](https://adventofcode.com/2024/day/2)

Tricky one because I misread the second part. My Solution is [here](2024/Day02/Program.cs).

	Elapsed time:13 ms
	Part 1:383
	Elapsed time:4 ms
	Part 2:436

## [Day 1](https://adventofcode.com/2024/day/1)
A good and easy start. But maybe it's all downhill from here 😅 [Here](2024/Day01/Program.cs) is my solution to day 1.

	Elapsed time:20 ms
	Part 1:1320851
	Elapsed time:26 ms
	Part 2:26859182

# Advent of Code 2023

## [Day11](https://adventofcode.com/2023/day/11)
Today I learned a valuable lesson on how IEnumerable works.. I was wondering why my solution was taking so long and when I finally stepped through the code, I saw how IEnumerable evaluated multiple times. 😒 [Here](2023/Day11/Program.cs) is my solution to day 11.

	Elapsed time:353 ms
	Part 1:9274989
	Elapsed time:297 ms
	Part 2:357134560737

## [Day09](https://adventofcode.com/2023/day/9)

	Elapsed time:81 ms
	Part 1:1969958987
	Elapsed time:6 ms
	Part 2:1068

## [Day08](https://adventofcode.com/2023/day/8)
The day has arrived, it's **I love ULONG time**! [Here](2023/Day08/Program.cs) is my solution to day 8. 

Confession, i do have a Github copilot subscription. I did part 1 with my own sweat and blood but with part two, i did ask copilot to give me the LCM for the list of iterations. Is it cheating? I would say, there really is no cheating in AOC. I mostly see AOC as a way to exercise tools you want to learn how to use.

In a work environment, no one relies on me to know how to implement all the various algorithms there is. Sure it's impressive to be able to do so, but I think knowing *when* to use them is more important.

	Elapsed time:23 ms
	Part 1:16531
	Elapsed time:12 ms
	Part 2:24035773251517

## [Day07](https://adventofcode.com/2023/day/7)
The lesson to READ CAREFULLY was learned today. Wasted a full two hours because I thought we had to weight the entire deck, only to read that we are supposed to process cards in order of appearance. 😒 [Here](2023/Day07/Program.cs) is my solution to day 7.

I between solving part 1 and part 2, I wanted to use the same method and preserve the behavior for part 1. I took the opportunity to make use of [ApprovalTests](https://github.com/approvals/ApprovalTests.Net#docs) to verify that I wasn't changing part 1's behavior. I set up [regression tests](2023/Tests/Day07Tests.cs) for both parts in hopes that I can refactor them later.

	Elapsed time:77 ms
	Part 1:253933213
	Elapsed time:14 ms
	Part 2:253473930

## [Day05](https://adventofcode.com/2023/day/5)
Okay, brute forced this one. [Here](2023/Day05/Program.cs) is my solution to day 5. But will try to get back and improve it later.

	Elapsed time:23 ms
	Part 1:486613012
	Elapsed time:570168 ms
	Part 2:56931769

## [Day04](https://adventofcode.com/2023/day/4)
Decided to wing it today, no TDD. [Here](2023/Day04/Program.cs) is my solution to day 4. Will probably clean this up later in the day though.

	Elapsed time:12 ms
	Part 1:21568
	Elapsed time:4 ms
	Part 2:11827296

## [Day03](https://adventofcode.com/2023/day/3)
My brain hurt a little.. lots of special cases to consider when parsing 😬 [Here](2023/Day03/Program.cs) is my solution to day 3. Spent too much time on special cases because i didn't realize that regex matches are greedy by default and that they returned indexes. 😒

	Elapsed time:17 ms
	Part 1:532331
	Elapsed time:6 ms
	Part 2:82301120

## [Day02](https://adventofcode.com/2023/day/2)
So far so good.. [Here](2023/Day02/Program.cs) is my solution to day 2. I will be trying to do TDD as much as possible this year.

	Elapsed time:17 ms
	Part 1:54249
	Elapsed time:6 ms
	Part 2:54249

## [Day01](https://adventofcode.com/2023/day/1)
Hoping I do better this year, will probably not be able to do it first thing in the morning. We shall see. 

	Elapsed time:17 ms
	Part 1:54249
	Elapsed time:6 ms
	Part 2:54249

# Advent of Code 2022

# [Day08](https://adventofcode.com/2022/day/8)
 [Today's](2022/Day08/Program.cs) solution starts using some recursion. No use fighting it, it's going to come eventually.

## [Day07](https://adventofcode.com/2022/day/7)
 [My solution](2022/Day07/Program.cs) submitted late because I went for an AW yesterday and today the brain is not working 100%.

## [Day06](https://adventofcode.com/2022/day/6)
This was fairly simple. 

## [Day05](https://adventofcode.com/2022/day/5)
Yikes, today was no tdd and there was some manual intervention involved in setting up the stacks 😁 Glad I started with regex yesterday though. [Here](2022/Day05/Program.cs) is my solution to day 5.

## [Day04](https://adventofcode.com/2022/day/4)
TDD is working great so why stop. Also tried to bite the bullet early and start getting some regex action started. [Here](2022/Day04/Program.cs) is my solution to day 4.
```console
Elapsed time:79 ms
Part 1:540
Elapsed time:33 ms
Part 2:872
```
## [Day03](https://adventofcode.com/2022/day/3)
Thought I would continue yesterdays TDD trend. It was really great to be able to break things down to smaller problems.
[My solution](2022/Day03/Program.cs) ended up with methods and functions who only have one job. 
```console
Elapsed time:7 ms
Part 1:7597
Elapsed time:6 ms
Part 2:2607
```
![one job](https://media.giphy.com/media/fXtAxz5oFQdH9Ax9E4/giphy.gif)
### Noteable Solutions
- [Anders](https://github.com/lynxz/AdventOfCode/blob/master/2022/Day03/Program.cs) and his one liners are always interesting!

## [Day02](https://adventofcode.com/2022/day/2)
[Hello bruteforce my old friend](2022/Day02/Program.cs). Tried to do a little tdd here but not sure this is the best solution. 🤷‍♀️
### Noteable Solutions
- [Anders](https://github.com/lynxz/AdventOfCode/blob/master/2022/Day02/Program.cs) and his one liners are always interesting!

## [Day01](https://adventofcode.com/2022/day/1)
Nice easy start to advent of code! Improved my batch file a bit to include the year this time. [Here](2022/Day01/Program.cs) is my solution.

# Advent of Code 2021
## [Day17](https://adventofcode.com/2021/day/17)
I tried to clever with math but gave up :D so here is brute force.

## [Day16](https://adventofcode.com/2021/day/16)
- Hex to Bin using linq, switch expressions, too much reading for a 6am activity 🤓

## [Day15](https://adventofcode.com/2021/day/15)
Cached recursion finding the lowest risk 

## [Day 11](https://adventofcode.com/2021/day/11)
Spent way too much time debugging all because i did - instead of +. Meanwhile, i made a batch file to make setup easier. 

## Day 6
Lantern fish
## Day 5

I'm sure there was a math way to solve this... Some good solutions i found through reddit:
- [jasonincanada](https://github.com/jasonincanada/aoc-2021/blob/main/AdventOfCode/AdventOfCode.CSharp/Day05.cs)

## Day 4

Well, I tried to get a headstart at using regex but eventually back pedalled and used old reliable string split.

## Day 3

Note to self there is a built in binary parser

`Convert.Int32("1010101", 2);`


## Day 2

Tried to use things I learned last year [.Aggregate](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregate?view=net-6.0). Started to package some tools as well to simplify getting input, stealing implement from Anders.

## Day 1

Was not prepared, so here it is, as is. :) As usual, great solution from [Anders](https://github.com/lynxz) https://github.com/lynxz/AdventOfCode/blob/master/2021/Day01/Program.cs Need to start doing using Linq more! Made an update to try to use [C# Ranges](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-8.0/ranges)




# Advent of Code 2020

If any christmas needs saving, this one does!
www.adventofcode.com

## Day 2

Slightly shuddering on basic regex

## Day 3

Today I learned a little about .Aggregate, thanks [Anders](https://github.com/lynxz)!

## Day 4

Eeek. More regex! learned the importance of strict ^...$ but i have to say, MAYBE i'm getting the hang of regex... my webclient reader broke though. need to fix that at some point, for now, copy paste ftw!

## Day 5

That was a nice refresher on recursion!

## Day 8

missed out updating yesterday, maybe the past few days have been too brutal. Great solutions coming from [Anders](https://github.com/lynxz/AdventOfCode/tree/master/2020) though!

## Day 10

This was really neat https://github.com/FaustVX/adventofcode/blob/master/2020/Day10/Solution.cs

## Day 11

Took a long time to find the recursive function for me, literally had to step through a number of iterations. Happy that i was able to refactor and make it work for part 1 as well!

## Day 12

Saw this very elegant solution: https://github.com/FaustVX/adventofcode/blob/master/2020/Day12/Solution.cs

## Day 13

~~Officially skipping day 13 part 2...~~
Did TDD and finally came up with the solution! Why didn't I do that first, could have avoided wasting a day!
Some solutios to read up on

- https://www.reddit.com/r/adventofcode/comments/kcb3bb/2020_day_13_part_2_can_anyone_tell_my_why_this/
- https://www.reddit.com/r/adventofcode/comments/kchxzm/2020_day_13_part_2_brute_forcing_in_1_minute_on/

## Day 14

As the problems are getting more difficult, i've decided to do part 1 first thing in the morning before work and then part 2 at the end of the day. So far it is working out.
Here are a few solutions i want to study when i have the time:

- https://github.com/DanaL/AdventOfCode/blob/master/2020/Day14.cs
- https://github.com/lynxz/AdventOfCode/blob/master/2020/Day14/Program.cs

## Day 16

- https://pastebin.com/y5vpkwHF
- https://github.com/linl33/adventofcode/blob/year2020/year2020/src/main/java/dev/linl33/adventofcode/year2020/Day16.java

## Day 18

- some inspirational day 18 code https://github.com/FaustVX/adventofcode/blob/master/2020/Day18/Solution.cs

## Day 20

- O_O
- https://github.com/FaustVX/adventofcode/blob/master/2020/Day20/Solution.cs
- https://gist.github.com/AlaskanShade/5c46b96a4b1f08cdb6568c15b6e86341
- https://github.com/erjicles/adventofcode2020/tree/main/src/AdventOfCode2020/AdventOfCode2020/Challenges/Day20

## Day 21

- https://github.com/PaulWild/advent-of-code-2020/blob/main/AdventOfCode/Days/Day21.cs
- https://github.com/hlim29/AdventOfCode2020/blob/master/Days/DayTwentyone.cs

## Day 22

- https://github.com/GoldenQubicle/AoC2020/blob/master/AoC2020/Solutions/Day22.cs

## Day 23

- https://github.com/FaustVX/adventofcode/blob/129b82029abccd85428451c2b9f1d032dce723ff/2020/Day23/Solution.cs

## Day 24

- https://github.com/PaulWild/advent-of-code-2020/blob/main/AdventOfCode/Days/Day24.cs
- https://github.com/encse/adventofcode/blob/master/2020/Day24/Solution.cs
