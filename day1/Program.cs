using System;
using System.Net;
using System.Text;

string url = "https://adventofcode.com/2020/day/1/input";

var input = GetInput(url);

for (int i = 0; i < input.Length - 1; i++)
{
    for (int j = 1; j < input.Length - 1; j++)
    {
        for (int k = 1; k < input.Length - 1; k++)
        {
            if (i == j || j == k || i == k) continue;

            var first = Int32.Parse(input[i]);
            var second = Int32.Parse(input[j]);
            var third = Int32.Parse(input[k]);
            if (first + second + third == 2020)
            {
                Console.WriteLine(first * second * third);
                i = j = k = input.Length;
                break;
            }
        }
    }
}

string[] GetInput(string url)
{

    using (WebClient client = new WebClient())
    {
        client.Headers.Add(HttpRequestHeader.Cookie, "session=53616c7465645f5fcf5e84e34859f03dca614ba7568e38b51c7ce9bd54f24f2114bfae06059cb74e3d5bc9ed165b910e");

        var result = client.DownloadData(url);
        var input = Encoding.UTF8.GetString(result).Split("\n");

        return input;
    }
}