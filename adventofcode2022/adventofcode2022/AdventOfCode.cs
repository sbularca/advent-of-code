namespace AdventOfCode2022 {
    public interface IAdventOfCode {
        void Start();
        void OnProcessData(string result);
    }

    public static class AdventOfCode {

        public static void Main(String[] args) {
            Console.WriteLine("Executing Day 1");
            Day1CalorieCounting day1 = new ();
            day1.Start();

            Console.WriteLine("Executing Day 2");
            Day2RockPaperScissors day2 = new ();
            day2.Start();

        }

        public static void GetInputData(Action<string> callback, string url) {
            Task task = Task.Run(async () => await GetInputDataFromUrlAsync(url, callback));
            while(!task.IsCompleted) {
                Task.Yield();
            }
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
