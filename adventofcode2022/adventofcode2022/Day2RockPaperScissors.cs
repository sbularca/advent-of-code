namespace AdventOfCode2022 {
    public class Day2RockPaperScissors : IAdventOfCode {
        public string Url => "https://sebastianbularca.com/temp/data/day2Input.txt";
        public DateTime Now { get; private set; }

        private readonly Dictionary<string, int> standardRules = new (){
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

        public void OnProcessData(string result) {
            Now = DateTime.Now;
            string[] strategyGuide = result.Split(new [] {"\n"}, StringSplitOptions.RemoveEmptyEntries);

            // part 1
            int sum = strategyGuide.Sum(t => standardRules[t] + standardRules[t[2].ToString()]);
            Console.WriteLine($"Day 2 Part 1 Result - Total score is {sum}");

            //part 2
            sum = 0;
            for(int i = 0; i < strategyGuide.Length; i++) {
                string currentMatch = strategyGuide[i];
                switch(currentMatch[^1]) {
                    case 'X':
                        foreach(var kvp in standardRules) {
                            if(kvp.Key[0] == currentMatch[0] && kvp.Value == 0) {
                                sum += standardRules[kvp.Key[^1].ToString()];
                            }
                        }
                        break;
                    case 'Y':
                        foreach(var kvp in standardRules) {
                            if(kvp.Key[0] == currentMatch[0] && kvp.Value == 3) {
                                sum += standardRules[kvp.Key[^1].ToString()] + 3;
                            }
                        }
                        break;
                    case 'Z':
                        foreach(var kvp in standardRules) {
                            if(kvp.Key[0] == currentMatch[0] && kvp.Value == 6) {
                                sum += standardRules[kvp.Key[^1].ToString()] + 6;
                            }
                        }
                        break;
                }
            }
            Console.WriteLine($"Day 2 Part 2 Result - Total score is {sum}");
            Console.WriteLine($"Method execution took {(DateTime.Now - Now).TotalMilliseconds}ms");
        }
    }
}
