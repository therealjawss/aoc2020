using System.Linq;
using System.Net;
using System.Text;

namespace AOC
{

    public abstract class DayModule
    {
        public string Day { get; set; }
        public string Cookie { get; set; }
        public string[] Input { get; set; }
        public abstract string Level1(string[] input);
        public abstract string Level2(string[] input);

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