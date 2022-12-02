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
            var strategyGuide = result.Split(new [] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
            int sum = 0;
            Console.WriteLine(strategyGuide.Length);
            for(int i = 0; i < strategyGuide.Length; i++) {
                sum += rules[strategyGuide[i]] + rules[strategyGuide[i][2].ToString()] ;
            }
            Console.WriteLine($"Total score is {sum}");
        }
    }
}
