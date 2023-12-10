namespace AdventOfCode2022 {
    public class Day2RockPaperScissors : IAdventOfCode {
        public string Url => "https://sebastianbularca.com/temp/data/day2Input.txt";
        public DateTime Now { get; private set; }
        public List<string> Results { get; } = new();
        public bool ShouldExecute { get; set; } = false;
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
            Results.Add(sum.ToString());

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
            Results.Add(sum.ToString());
            Console.WriteLine($"Day 2 OnProcessData method execution took {(DateTime.Now - Now).TotalMilliseconds}ms");
        }

        public void PrintResults() {
            Console.WriteLine($" \nShowing Day 2 results");
            for(int i = 0; i < Results.Count; i++) {
                Console.WriteLine($"Day 2 Part {i+1} Result - Total score is {Results[i]}");
            }
        }
    }
}
