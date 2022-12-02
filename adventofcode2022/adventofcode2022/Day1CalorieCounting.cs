namespace AdventOfCode2022 {
    public class Day1CalorieCounting : IAdventOfCode {
        private const string url = "https://sebastianbularca.com/temp/data/day1Input.txt";

        public void Start() {
            AdventOfCode.GetInputData(OnProcessData, url);
        }
        public void OnProcessData(string result) {
            string[] elvesBackpacks = result.Split(new [] {"\n\n"}, StringSplitOptions.None);
            int output = 0;
            for(int i = 0; i < elvesBackpacks.Length; i++) {
                string[] calories = elvesBackpacks[i].Split(new [] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
                int sum = calories.Sum(int.Parse);
                output = (sum > output) ? sum : output;
            }
            Console.WriteLine($"Day 1 Result - The elf with the biggest muscles is carrying: {output} calories. Such wonder!");
        }
    }
}
