namespace ChristmasGifts
{
    public abstract class Christmas
    {
        protected string Year { get; }
        protected string Day { get; }

        private string inputfile = "input.txt";
        private readonly HttpClient _httpClient;
        protected string[] Input;
        public Christmas(string day, string year)
        {
            Day = day;
            Year = year;
            var cookie = $"session={File.ReadAllText("cookie")}";
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://adventofcode.com")
            };
            _httpClient.DefaultRequestHeaders.Add("Cookie", cookie);
        }

        public virtual async Task<string[]> GetInput(string? file = null, string pattern = "\n", Func<string, bool> predicate = null)
        {
            if (predicate == null)
                predicate = x => !string.IsNullOrWhiteSpace(x);

            string buffer = await ReadBuffer(file);
             Input = buffer.Split(pattern).Where(predicate).ToArray();
          
            return Input;
        }
        private async Task<string> ReadBuffer(string? file = null)
        {
            inputfile = file ?? $"{Day}.txt";

            if (File.Exists(inputfile))
                return await File.ReadAllTextAsync(inputfile);
            
            var request = new HttpRequestMessage(HttpMethod.Get, $"{Year}/day/{Day}/input");
            using (var response = await _httpClient.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    await File.WriteAllTextAsync(inputfile, data);
                    return data;
                }
                else
                {
                    throw new Exception("whoops");
                }

            }
        }
        public abstract string First();
        public async Task PostFirstAnswer()
        {
            await PostAnswer("1", First());
        }
        public async Task PostSecondAnswer()
        {
            await PostAnswer("2", Second());
        }
        public abstract string Second();
        async Task PostAnswer(string level, string answer)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{Year}/day/{Day}/answer");
            request.Content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string> ("level", level.ToString()),
                new KeyValuePair<string, string>("answer", answer)
            });


            using (var response = await _httpClient.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (content.Contains("That's the right answer!"))
                    {
                        System.Console.WriteLine("Have a Coffee");
                    }
                    else
                    {
                        var start = content.IndexOf("<main>") + 6;
                        var stop = content.IndexOf("</main>");
                        System.Console.WriteLine(content.Substring(start, stop - start));
                    }
                }
            }
         
        }
    }
}