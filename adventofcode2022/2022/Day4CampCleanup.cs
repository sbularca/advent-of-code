namespace AdventOfCode2022 {
    public class Day4CampCleanup : IAdventOfCode {
        public string Url => "https://sebastianbularca.com/temp/data/day4Input.txt";
        public DateTime Now { get; private set; }
        public List<string> Results { get; } = new();
        public bool ShouldExecute { get; set; } = false;
        public void OnProcessData(string result) {
            Now = DateTime.Now;
            List<string []> sortedPairs = new();
            string[] pairs = result.Split("\n");
            for(int i = 0; i < pairs.Length; i++) {
                string [] pairsOfPairs = pairs[i].Split(",");
                for(int j = 0; j < pairsOfPairs.Length; j++) {
                    string[] justNumbers = pairsOfPairs[j].Split("-");
                    sortedPairs.Add(justNumbers);
                }
            }

            int sum1 = 0;
            int sum2 = 0;
            for(int i = 0; i < sortedPairs.Count-1; i++) {
                var currentPair = sortedPairs[i];

                int a01 = int.Parse(currentPair[0]);
                int a02 = int.Parse(currentPair[1]);

                string[] nextPair = sortedPairs[i + 1];
                int b01 = int.Parse(nextPair[0]);
                int b02 = int.Parse(nextPair[1]);
                i++;

                //part 1
                if(a01 >= b01 && a02 <= b02 || a01 <= b01 && a02 >= b02) {
                    sum1++;
                }

                //part 2
                if(a01 < b01 && a02 < b01 || a01 > b02 && a02 > b02) {
                    continue;
                }
                sum2++;
            }
            Results.Add(sum1.ToString());
            Results.Add(sum2.ToString());
            Console.WriteLine($"Day 4 OnProcessData method execution took {(DateTime.Now - Now).TotalMilliseconds}ms");
        }

        public void PrintResults() {
            Console.WriteLine($" \nShowing Day 4 results");
            for(int i = 0; i < Results.Count; i++) {
                Console.WriteLine($"Day 4 Part {i+1} Result - Total contained pairs - {Results[i]}");
            }
        }
    }
}
