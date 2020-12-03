using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace AOC2020.Days
{

    public class Christmas
    {
        public virtual int Day { get; }
        public string Cookie { get; set; }
        public string[] Input { get; set; }
		public Christmas()
		{
            Cookie = File.ReadAllText("./.cookie");
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
        public string[] GetInput()
        {
            string url = $"https://adventofcode.com/2020/day/{Day}/input";
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Cookie, Cookie);

                var result = client.DownloadData(url);
                Input = Encoding.UTF8.GetString(result).Split("\n").Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                return Input;
            }
        }
    }
}