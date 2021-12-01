namespace AOC2021.Days
{
    public class Day01
    {
        public static async void Run()
        {
            Console.WriteLine("Advent of Code 2021!\n**************");
            var input = File.ReadAllText("input.txt");
            var lines = input.Split("\r\n").Select(x => Int32.Parse(x)).ToArray();

            var ctr = 0;
            var previous = -1;
            foreach (var line in lines)
            {
                if (line > previous && previous != -1) ctr++;
                previous = line;
            }

            Console.WriteLine(ctr);
            ctr = 0;
            for (int i = 0; i < lines.Length - 3; i++)
            {
                ctr += (lines[i..(i + 3)].Sum() < lines[(i + 1)..(i + 4)].Sum()) ? 1 : 0;
            }

            Console.WriteLine(ctr);
        }
    }
}
