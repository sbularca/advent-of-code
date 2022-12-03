namespace AdventOfCode2022 {
    public static class AdventOfCode {

        public static async Task Main(String[] args) {
            Console.WriteLine("Executing Day 1");
            Day1CalorieCounting day1 = new ();
            await GetInputData(day1.OnProcessData, day1.Url);

            Console.WriteLine("Executing Day 2");
            Day2RockPaperScissors day2 = new ();
            await GetInputData(day2.OnProcessData, day2.Url);

            Console.WriteLine("Executing Day 3");
            Day3RucksackReorganization day3 = new ();
            await GetInputData(day3.OnProcessData, day3.Url);

        }

        private static async Task GetInputData(Action<string> callback, string url) {
            Task task = Task.Run(async () => await GetInputDataFromUrlAsync(url, callback));
            await task;
        }

        private static async Task GetInputDataFromUrlAsync(string url, Action<string> callback) {
            HttpClient client = new ();
            try {
                string file = await client.GetStringAsync(url);
                callback(file);
            }
            catch(HttpRequestException e) {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
