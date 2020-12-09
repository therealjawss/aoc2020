using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC2020.Days
{

	public class Christmas
	{
		public virtual int Day { get; }
		public string Cookie { get; set; }
		public string[] Input { get; set; }
		public Christmas()
		{
			Cookie = "session=" + File.ReadAllText("./.cookie");
		}
		public virtual string Level1(string[] input)
		{
			return "todo";
		}
		public virtual string Level2(string[] input)
		{
			return "todo";
		}

		public void PostL1Answer()
		{
			PostAnswer("1", Level1(Input));
		}
		public void PostL2Answer()
		{
			PostAnswer("2", Level2(Input));
		}
		void PostAnswer(string level, string answer)
		{
			string answerURL = $"https://adventofcode.com/2020/day/{Day}/answer";

			using (WebClient client = new WebClient())
			{
				client.Headers.Add(HttpRequestHeader.Cookie, Cookie);
				var postData = $"level={level}&answer={answer}";
				client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
				var result = client.UploadString(answerURL, postData);
			}
		}
		public virtual string[] GetInput(string file = null, string pattern = "\n", Func<string, bool> predicate = null)
		{
			if (predicate == null)
			{
				predicate = x => !string.IsNullOrWhiteSpace(x);
			}

			string buffer = ReadBuffer(file);
			if (pattern == null)
			{
				Input = buffer.Split(pattern).Where(predicate).ToArray();
			}
			else
			{

				Input = buffer.Split(pattern).Where(predicate).ToArray();
			}
			return Input;
		}

		protected string ReadBuffer(string file)
		{
			string url = $"https://adventofcode.com/2020/day/{Day}/input";
			string result = "";
			if (file == null)
			{
				if (!File.Exists($"{Day}.txt"))
				{

					using (WebClient client = new WebClient())
					{
						client.Headers.Add(HttpRequestHeader.Cookie, Cookie);

						result = Encoding.UTF8.GetString(client.DownloadData(url));
						File.WriteAllText($"{Day}.txt", result);
					}
				}
				else
				{
					result = File.ReadAllText($"{Day}.txt");
				}
			}
			else
			{
				result = File.ReadAllText(file);
			}
			return result;
		}

	}
}