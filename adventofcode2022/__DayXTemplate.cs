namespace AdventOfCode2022 {
    public class DayXTemplate : IAdventOfCode {
        public string Url => "https://sebastianbularca.com/temp/data/dayXInput.txt";
        public DateTime Now { get; private set; }
        public List<string> Results { get; } = new();
        public bool ShouldExecute { get; set; } = false;

        public void OnProcessData(string result) {
            Now = DateTime.Now;

            //string[] lines = result.Split(new [] {"\n"}, StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine($"Day X OnProcessData method execution took {(DateTime.Now - Now).TotalMilliseconds}ms");
        }

        public void PrintResults() {
            Console.WriteLine($" \nShowing Day X results");
            for(int i = 0; i < Results.Count; i++) {
                Console.WriteLine($"Day X Part {i+1} Result - Result is {Results[i]}");
            }
        }
    }
}
