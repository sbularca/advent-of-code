namespace AdventOfCode2022 {
    public class Day2RockPaperScissors : IAdventOfCode {
        private const string url = "https://sebastianbularca.com/temp/data/day2Input.txt";

        private Dictionary<string, int> rules = new (){
            {"A X", 3},
            {"B Y", 3},
            {"C Z", 3},

            {"A Y", 6},
            {"B Z", 6},
            {"C X", 6},

            {"A Z", 0},
            {"B X", 0},
            {"C Y", 0},

            {"X", 1},
            {"Y", 2},
            {"Z", 3}

        };

        public void Start() {
            AdventOfCode.GetInputData(OnProcessData, url);
        }
        public void OnProcessData(string result) {
            string[] strategyGuide = result.Split(new [] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
            int sum = strategyGuide.Sum(t => rules[t] + rules[t[2].ToString()]);
            Console.WriteLine($"Day 2 Result - Total score is {sum}");
        }
    }
}
