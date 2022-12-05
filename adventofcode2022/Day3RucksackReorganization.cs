namespace AdventOfCode2022 {
    public class Day3RucksackReorganization : IAdventOfCode {
        public string Url => "https://sebastianbularca.com/temp/data/day3Input.txt";
        public DateTime Now { get; private set; }
        public List<string> Results { get; } = new();
        public void OnProcessData(string result) {
            Now = DateTime.Now;
            //part 1
            string[] rucsacs = result.Split(new [] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
            int sum = 0;
            for(int i = 0; i < rucsacs.Length; i++) {
                string rucsac = rucsacs[i];
                string comp1 = rucsac[..(rucsac.Length / 2)];
                string comp2 = rucsac[(rucsac.Length / 2)..];
                char commonElem = comp1.Intersect(comp2).ElementAt(0);
                sum += GetAsciiPriority(commonElem);
            }
            Results.Add(sum.ToString());

            //part2
            List<string> packsOfThree = new ();
            sum = 0;
            for(int i = 0; i < rucsacs.Length; i++) {
                int a = i + 3;
                while(i < a) {
                    packsOfThree.Add(rucsacs[i]);
                    i++;
                }
                i--;
                IEnumerable<char> commElem = packsOfThree[0].Intersect(packsOfThree[1]).Intersect(packsOfThree[2]);
                int prio = GetAsciiPriority(commElem.ElementAt(0));
                sum += prio;
                packsOfThree.Clear();
            }
            Results.Add(sum.ToString());

            Console.WriteLine($"Day 3 OnProcessData method execution took {(DateTime.Now - Now).TotalMilliseconds}ms");
        }

        public void PrintResults() {
            Console.WriteLine($" \nShowing Day 3 results");
            for(int i = 0; i < Results.Count; i++) {
                Console.WriteLine($"Day 3 Part {i+1} Result - Total priority is {Results[i]}");
            }
        }

        private static int GetAsciiPriority(char commonElem) {
            var asciiValue = (int)commonElem;
            if (asciiValue >= 97) {
                return asciiValue - 96;
            }

            return asciiValue - 38;
        }
    }
}
