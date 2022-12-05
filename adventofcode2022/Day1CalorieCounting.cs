namespace AdventOfCode2022 {
    public class Day1CalorieCounting : IAdventOfCode {
        public string Url => "https://sebastianbularca.com/temp/data/day1Input.txt";
        public DateTime Now { get; private set; }
        public List<string> Results { get; } = new();
        public void OnProcessData(string result) {
            Now = DateTime.Now;
            string[] elvesBackpacks = result.Split(new [] {"\n\n"}, StringSplitOptions.None);
            int output = 0;
            var allCalories = new List<int>();

            //part 1
            for(int i = 0; i < elvesBackpacks.Length; i++) {
                string[] calories = elvesBackpacks[i].Split(new [] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
                int sum = calories.Sum(int.Parse);
                output = (sum > output) ? sum : output;
                allCalories.Add(sum);
            }
            Results.Add(output.ToString());

            //part 2
            allCalories.Sort();
            output = allCalories[^1] + allCalories[^2] + allCalories[^3];
            Results.Add(output.ToString());
            Console.WriteLine($"Day 1 OnProcessData method execution took {(DateTime.Now - Now).TotalMilliseconds}ms");
        }
        public void PrintResults() {
            Console.WriteLine($" \nShowing Day 1 results");
            Console.WriteLine($"Day 1 Result Part 1 - The elf with the biggest muscles is carrying: {Results[0]} calories. Such wonder!");
            Console.WriteLine($"Day 1 Part 2 Result - The top 3 elves are carrying: {Results[1]} calories.");
        }
    }
}
